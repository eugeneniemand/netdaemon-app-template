# Alexa Actionable Notifications (Prompts)

A two-way voice interaction system that lets NetDaemon apps **ask a question through an Amazon
Echo device and receive a spoken answer back** (Yes / No / a selection / a number / a duration).
Internally these are called **Prompts**.

Unlike a normal TTS announcement — which is fire-and-forget — a Prompt:

1. Speaks a question on one or more Echo devices.
2. Opens an Alexa skill session that **listens for the user's answer**.
3. Routes the answer back into NetDaemon as a strongly-typed `PromptResponse` observable.
4. Lets the app react per response type (e.g. turn a light on when the user says *Yes*).

> This README is intended both for humans and for AI agents working on the codebase. It documents
> the end-to-end data flow, the moving parts, and the extension points.

---

## High-level flow

```
┌──────────────┐  Prompt(config)   ┌──────────────────────────────┐
│  NetDaemon   │ ────────────────▶ │  Alexa helper (Alexa.cs)     │
│     App      │                   │  buffers + routes "prompt"   │
└──────────────┘                   └───────────────┬──────────────┘
	   ▲                                           │ ProcessPrompts
	   │                                           ▼
	   │                     ┌───────────────────────────────────────────┐
	   │                     │ NotificationProcessor.ProcessAsync        │
	   │                     │  AlexaActionableNotificationDeliveryStrategy
	   │                     └───────────────┬───────────────────────────┘
	   │                                     │ Deliver()
	   │             ┌───────────────────────┴────────────────────────────┐
	   │             │ 1. MQTT publish → topic "alexa_actionable_notification"
	   │             │    payload { "text": "...", "event": "<eventId>" }   │
	   │             │ 2. media_player.play_media                           │
	   │             │    media_content_type = "skill"                      │
	   │             │    media_content_id   = "amzn1.ask.skill.9fd6a51d..."│
	   │             └───────────────────────┬────────────────────────────┘
	   │                                     ▼
	   │        ┌─────────────────────────────────────────────────────────┐
	   │        │ Home Assistant                                           │
	   │        │  • MQTT sensor  sensor.alexa_actionable_notification_prompt
	   │        │    exposes { text, event } as attributes                 │
	   │        │  • Echo device launches the Alexa skill                  │
	   │        └───────────────────────┬─────────────────────────────────┘
	   │                                ▼
	   │        ┌─────────────────────────────────────────────────────────┐
	   │        │ Alexa Skill  (AWS Lambda — lambda_funtion.py)            │
	   │        │  • GET /api/states/sensor.alexa_actionable_notification_prompt
	   │        │    → reads { event, text }                               │
	   │        │  • Speaks `text`, listens for the user's answer          │
	   │        │  • POST /api/events/alexa_actionable_notification        │
	   │        │    { event_id, event_response, event_response_type,      │
	   │        │      event_person_id }                                   │
	   │        └───────────────────────┬─────────────────────────────────┘
	   │                                ▼
	   │        ┌─────────────────────────────────────────────────────────┐
	   │        │ HA fires event "alexa_actionable_notification"           │
	   │        └───────────────────────┬─────────────────────────────────┘
	   │                                ▼
	   │        ┌─────────────────────────────────────────────────────────┐
	   └────────│ PromptResponseHandler → PromptResponse → observable      │
				│ ProcessPrompts reverts volume; app handlers run          │
				└─────────────────────────────────────────────────────────┘
```

---

## The two sides

The system spans **two codebases that must stay in sync**:

| Side | Location | Responsibility |
| ---- | -------- | -------------- |
| NetDaemon (C#) | `Helpers/Notifications/` | Send prompts, listen for response events, dispatch to app handlers |
| Alexa Skill (Python) | `Helpers/Notifications/lambda_funtion.py` | Runs in AWS Lambda, reads the question from HA, speaks it, captures the answer, posts it back to HA |

The glue between them is **Home Assistant**, acting as the message bus:

- Outbound (NetDaemon → Skill): an **MQTT sensor** `sensor.alexa_actionable_notification_prompt`
  whose attributes carry `text` and `event`.
- Inbound (Skill → NetDaemon): the HA event **`alexa_actionable_notification`**.

---

## NetDaemon components

All files live in `Helpers/Notifications/`.

| File | Role |
| ---- | ---- |
| `Alexa.cs` | Facade (`IAlexa`). Public API (`Prompt`, `Announce`, `TextToSpeech`). Buffers messages and routes `"prompt"` to `ProcessPrompts`. Exposes `PromptResponses` observable. |
| `NotificationProcessor.cs` | Shared pipeline (`ProcessAsync`) + delivery strategies. `INotificationDeliveryStrategy.Deliver` is the extension point. |
| `AlexaActionableNotificationDeliveryStrategy` | Prompt delivery: MQTT publish + `play_media` skill launch. |
| `AlexaMediaDeliveryStrategy` | Non-prompt delivery via `notify.alexa_media` (announcements/TTS). |
| `PromptResponseHandler.cs` | Subscribes to the HA `alexa_actionable_notification` event, maps it to a `PromptResponse`, resolves the person name, and pushes onto the `Subject<PromptResponse>`. |
| `PromptResponseEvent.cs` | DTOs: `PromptResponseEvent` (raw HA event), `PromptResponse` (app-facing), `PromptResponseTraceEvent`. |
| `PromptResponseType.cs` | `PromptResponseType` enum + tolerant JSON converter (normalizes `Response_Yes` → `ResponseYes`). |
| `AlexaPromptPoller.cs` | High-level fluent helper: triggers → prompt → cooldown → per-response-type handlers, with optional persistent gate entity. |
| `VolumeManager.cs` | Stores/sets/restores per-device volume around a prompt. |
| `MessageFormatter.cs` | Concatenates buffered messages, applies voice/whisper SSML, word-count timing. |
| `VoiceProvider.cs` | Supplies a random Polly voice. |
| `AlexaConfig.cs` / `AlexaConfig.yaml` | `People` (person-id → name) and per-`Devices` volume config. |

### Public API — sending a prompt

```csharp
// Simple overload
alexa.Prompt("media_player.kitchen", "Should I turn off the lights?", eventId: "lights_off");

// Full config overload
alexa.Prompt(new Alexa.Config
{
	Entities = { "media_player.kitchen", "media_player.office" },
	Message  = "Should I turn off the lights?",
	EventId  = "lights_off",   // correlates the response back to this prompt
	VolumeLevel = 0.6,         // optional override
});
```

`Alexa.Config` derives from `AlexaNotificationConfig`. Key fields:

| Field | Meaning |
| ----- | ------- |
| `Message` | The question text spoken by Alexa. |
| `Entity` / `Entities` | Target media player(s). `Entity` is auto-merged into `Entities`. |
| `EventId` | **Correlation id.** Echoed in the MQTT payload and returned in the response event. |
| `NotifyType` | Set internally to `"prompt"` by `Prompt(...)`. |
| `Whisper`, `VolumeLevel`, `VolumeResetDelay`, `UseDefaultVoice` | Optional per-prompt overrides (default to device config). |

### Message buffering

`Alexa.cs` buffers inbound messages for `AlexaProcessingConfig.MessageBufferDelay` and splits the
stream by type:

- `tts` / `announce` → `ProcessNotifications` (uses `AlexaMediaDeliveryStrategy`).
- `prompt` → `ProcessPrompts` (uses `AlexaActionableNotificationDeliveryStrategy`).

### Delivery (`INotificationDeliveryStrategy.Deliver`)

`AlexaActionableNotificationDeliveryStrategy.Deliver` performs the two outbound actions:

```csharp
_services.Mqtt.Publish("alexa_actionable_notification",
	$"{{\"text\": \"{formattedMessage}\", \"event\": \"{eventId}\"}}");

_services.MediaPlayer.PlayMedia(ServiceTarget.FromEntity(entity),
	new MediaPlayerPlayMediaParameters
	{
		Media = new { media_content_type = "skill",
					  media_content_id   = "amzn1.ask.skill.9fd6a51d-54f1-43ec-9a74-cd3fbecd1664" }
	});
```

1. The MQTT publish updates the HA sensor so the skill can read the question.
2. The `play_media` with `media_content_type = "skill"` launches the skill on the target Echo,
   which starts an interactive session.

### Volume handling for prompts

`ProcessPrompts` differs from normal notifications: because a prompt waits for the *user* to
answer (unknown duration), volume is **not** reverted on a timer. Instead it:

1. Stores current volumes for each entity.
2. Delivers the prompt via `NotificationProcessor.ProcessAsync`.
3. Subscribes to `_promptResponses` filtered by `EventId`, takes the first match, and only then
   calls `VolumeManager.RestoreVolumesAsync`.

### Receiving the answer (`PromptResponseHandler`)

`SetupEventSubscription` (called once from the `Alexa` constructor) subscribes to:

- `alexa_actionable_notification` → mapped to `PromptResponse` and pushed to the shared
  `Subject<PromptResponse>` (deduped by `{ EventId, ResponseType }`).
- `alexa_actionable_notification_trace` → debug logging only.

The raw HA event is deserialized into `PromptResponseEvent`:

| HA field | C# property |
| -------- | ----------- |
| `event_id` | `EventId` |
| `event_response` | `Response` |
| `event_response_type` | `ResponseType` (`PromptResponseType`) |
| `event_person_id` | `ResponsePersonId` (resolved to `ResponsePersonName` via `People` config) |

Apps consume answers via `IAlexa.PromptResponses` (filter by `EventId`), or more conveniently via
`AlexaPromptPoller`.

### `PromptResponseType`

```
ResponseYes · ResponseNo · ResponseNone · ResponseSelect · ResponseNumeric · ResponseDuration · ResponseUnknown
```

The JSON converter is underscore/case-insensitive, so skill values like `Response_Yes` or
`responseyes` all map correctly, falling back to `ResponseUnknown`.

### `AlexaPromptPoller` — the recommended app-level API

A fluent state machine (`Idle → Listening → WaitingForResponse (cooldown) → Idle`) that wires
triggers to prompts and responses:

```csharp
var poller = new AlexaPromptPoller(scheduler, alexa, logger, haContext)
	.AddTrigger(entities.BinarySensor.Motion.StateChanges(), s => s.New.IsOn())
	.SetPrompt(new Alexa.Config { Entity = "media_player.kitchen",
								  Message = "Leaving? Should I lock up?",
								  EventId = "lockup" })
	.WithCooldown(TimeSpan.FromMinutes(3))
	.WithGateEntity("input_boolean.lockup_prompt_enabled", haContext) // persists across restarts
	.WithDailyReset(Observable.Timer(Next6am(), TimeSpan.FromDays(1), scheduler))
	.OnResponseYes(r => { lockService.Lock(); poller.Acknowledge(); })
	.OnResponseNo(r => { /* ... */ })
	.Subscribe();
```

Features: multiple merged triggers, per-response-type handlers, cooldown to prevent re-prompting,
daily/event resets, and an optional **gate entity** (a HA `input_boolean`) so an acknowledged
state survives NetDaemon restarts.

---

## Alexa Skill side (`lambda_funtion.py`)

Runs as an AWS Lambda backing the custom Alexa skill `amzn1.ask.skill.9fd6a51d-...`.

Configuration constants at the top of the file:

- `HOME_ASSISTANT_URL` — your HA frontend URL.
- `VERIFY_SSL` — set `False` for self-signed certs.
- `TOKEN` — long-lived token (blank = use account-linking token from the Alexa request).
- `PROMPT_ENTITY = "sensor.alexa_actionable_notification_prompt"` — the MQTT sensor to read.

Flow inside the skill:

1. **Read the question** — `HomeAssistant.get_ha_state()` does
   `GET /api/states/sensor.alexa_actionable_notification_prompt` and reads the `attributes`
   (`event`, `text`).
2. **Speak & listen** — the skill speaks `text` and collects the user's response via intents
   (`ResponseYes`, `ResponseNo`, `ResponseSelect`, `ResponseNumeric`, `ResponseDuration`, …).
3. **Post the answer back** — `post_ha_event()` does
   `POST /api/events/alexa_actionable_notification` with body:

   ```json
   {
	 "event_id": "<the event from the sensor>",
	 "event_response": "<the user's answer>",
	 "event_response_type": "ResponseYes",
	 "event_person_id": "<amzn1.ask.person...>"
   }
   ```

   The `event_person_id` is taken from the Alexa request context (voice-recognized person),
   enabling per-person handling via the `People` map in `AlexaConfig.yaml`.

There is also a `post_ha_trace_event` → `alexa_actionable_notification_trace` event used purely for
debugging/telemetry.

---

## Home Assistant configuration (bridge)

Two pieces must exist in HA:

1. **MQTT sensor** listening on the `alexa_actionable_notification` topic, exposing the JSON
   payload fields (`text`, `event`) as attributes:

   ```yaml
   mqtt:
	 sensor:
	   - name: "Alexa Actionable Notification Prompt"
		 state_topic: "alexa_actionable_notification"
		 value_template: "{{ value_json.event }}"
		 json_attributes_topic: "alexa_actionable_notification"
		 json_attributes_template: "{{ {'event': value_json.event, 'text': value_json.text} | tojson }}"
   ```

   (Entity id: `sensor.alexa_actionable_notification_prompt`.)

2. The **custom Alexa skill** linked to this HA instance (account linking or a long-lived token in
   the Lambda), so the skill can call the HA REST API.

---

## Correlation contract (the critical invariant)

The `EventId` is the thread that ties the whole round-trip together:

```
App sets Config.EventId
   → MQTT payload { "event": EventId }
   → sensor attribute "event"
   → skill reads "event", posts event_id = EventId
   → HA event event_id = EventId
   → PromptResponse.EventId = EventId
   → ProcessPrompts / app handlers filter on EventId
```

If a prompt never gets a response (volume never reverts, handler never fires), check that the
`EventId` is unique and consistent end-to-end.

---

## Extending the system

- **New response type**: add to the `PromptResponseType` enum, add the matching intent in the
  skill, and handle it in `AlexaPromptPoller`/app code.
- **New delivery mechanism**: implement `INotificationDeliveryStrategy` and select it in
  `Alexa.cs`.
- **New person**: add the `amzn1.ask.person...` id → name entry under `People` in
  `apps/AlexaConfig.yaml`.
- **New skill id**: update `media_content_id` in `AlexaActionableNotificationDeliveryStrategy` and
  redeploy the skill.

---

## Troubleshooting

| Symptom | Likely cause |
| ------- | ------------ |
| Alexa speaks nothing | `play_media` skill launch failed, or the Echo isn't the target entity. |
| Alexa speaks the *previous* question | MQTT sensor didn't update before the skill read it; check MQTT retain/ordering. |
| Response never reaches the app | `EventId` mismatch, HA event name wrong, or skill failed to POST (check Lambda logs / token). |
| Volume stays low after a prompt | No response event arrived for that `EventId`, so `RestoreVolumesAsync` never ran. |
| Wrong/`UNKNOWN` person | `event_person_id` missing, or not present in `People` config. |
