# AlexaPromptPoller Gate Entity Feature

## Overview

The `AlexaPromptPoller` now supports a **gate entity** feature that allows you to persist the "acknowledged" state in Home Assistant rather than just in memory. This means that if the app restarts, the acknowledged state is preserved.

## Problem Solved

Previously, when `Acknowledge()` was called, it only set an in-memory flag `_isAcknowledged`. If the app restarted, this flag would be lost, and the prompts would start firing again immediately.

With the gate entity feature, the acknowledged state is stored in a Home Assistant boolean entity (e.g., `input_boolean.jayden_tablet_enabled`), so it persists across restarts.

## How It Works

1. **Gate Entity**: You configure a boolean input entity in Home Assistant (e.g., `input_boolean.jayden_tablet_enabled`)
2. **Gate Check**: Before allowing a prompt to fire, the poller checks if the gate entity is "on"
3. **On Acknowledge**: When `Acknowledge()` is called, the gate entity is turned "off"
4. **On Reset**: When `ResetState()` or gate reset trigger fires, the gate entity is turned "on"

## Setup

### 1. Create the Input Boolean in Home Assistant

Add this to your Home Assistant `configuration.yaml` or create via UI:

```yaml
input_boolean:
  jayden_tablet_enabled:
    name: "Jayden Tablet Prompt Enabled"
    initial: true
    icon: mdi:tablet
```

### 2. Update Your App Constructor

You need to inject `IHaContext` to use the gate entity feature. Here's an example:

```csharp
[NetDaemonApp]
public class JaydenTablet
{
    private readonly IEntities _entities;
    private readonly IHaContext _haContext;  // Add this
    private readonly IScheduler _scheduler;
    private readonly ILogger<JaydenTablet> _logger;
    private AlexaPromptPoller? _alexaPoller;

    public JaydenTablet(
        IEntities entities, 
        IServices services, 
        IAlexa alexa, 
        IHaContext haContext,      // Inject this
        IScheduler scheduler, 
        ILogger<JaydenTablet> logger)
    {
        _entities = entities;
        _haContext = haContext;
        _scheduler = scheduler;
        _logger = logger;

        // Create the poller with gate entity
        _alexaPoller = new AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges().Where(e => e.New.IsOn()))
            .SetPrompt(new Alexa.Config()
            {
                Message = "Jayden, have you taken your tablet?",
                Entity = "media_player.dining",
                EventId = "jayden_tablet"
            })
            .WithCooldown(TimeSpan.FromMinutes(3))

            // NEW: Add gate entity for persistent state
            .WithGateEntity("input_boolean.jayden_tablet_enabled", haContext)

            // NEW: Reset gate at 6 AM daily (along with other state)
            .WithGateReset(Observable.Timer(
                Next6am(_scheduler.Now),
                TimeSpan.FromDays(1),
                _scheduler
            ).StartWith(0L))

            .WithDailyReset(Observable.Timer(
                Next6am(_scheduler.Now),
                TimeSpan.FromDays(1),
                _scheduler
            ).StartWith(0L))

            .OnResponseYes(response =>
            {
                var person = response.ResponsePersonName ?? "Unknown";
                _logger.LogDebug("Tablet acknowledged by {Person}", person);

                // This will now turn OFF the input_boolean
                _alexaPoller?.Acknowledge();

                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = $"Thank you {person}",
                    Entity = "media_player.dining",
                    EventId = "jayden_tablet"
                });
            })
            .OnResponseNotYes(response =>
            {
                _logger.LogDebug("Not acknowledged: {ResponseType}", response.ResponseType);
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = "Please ensure you take it",
                    Entity = "media_player.dining",
                    EventId = "jayden_tablet"
                });
            });

        _alexaPoller.Subscribe();
    }

    private DateTimeOffset Next6am(DateTimeOffset now)
    {
        var todaySix = new DateTimeOffset(now.Year, now.Month, now.Day, 6, 0, 0, now.Offset);
        return now < todaySix ? todaySix : todaySix.AddDays(1);
    }
}
```

## Gate Entity Methods

### `WithGateEntity(string entityId, IHaContext haContext)`

Configures the gate entity ID and Home Assistant context.

- **entityId**: The entity ID of an `input_boolean` entity (e.g., `"input_boolean.jayden_tablet_enabled"`)
- **haContext**: The `IHaContext` instance to communicate with Home Assistant

```csharp
.WithGateEntity("input_boolean.jayden_tablet_enabled", haContext)
```

### `WithGateReset(IObservable<Unit> gateResetTrigger)`

Sets an observable that will reset the gate entity to "on" (true).

```csharp
.WithGateReset(Observable.Timer(
    Next6am(_scheduler.Now),
    TimeSpan.FromDays(1),
    _scheduler
).StartWith(0L))
```

### `WithGateReset<T>(IObservable<T> gateResetTrigger)`

Generic version that accepts any observable type and converts it to Unit.

```csharp
.WithGateReset(myObservable)
```

## State Flow

1. **Initial State**: Gate entity is "on" (true) in Home Assistant
2. **Prompt Sent**: Trigger fires → Prompt is sent to Alexa (only if gate is "on")
3. **Response**: User responds "Yes"
4. **Acknowledged**: `Acknowledge()` is called → Gate entity turned "off" (false)
5. **No More Prompts**: Subsequent triggers are ignored while gate is "off"
6. **Reset**: Daily reset trigger fires or external reset → Gate entity turned "on" (true) again
7. **Cycle Repeats**: Back to step 2

## Automation in Home Assistant

You can create automations in Home Assistant to manually reset the gate if needed:

```yaml
automation:
  - alias: "Reset Jayden Tablet Prompt"
    trigger:
      platform: time
      at: "06:00:00"
    action:
      service: input_boolean.turn_on
      target:
        entity_id: input_boolean.jayden_tablet_enabled
```

Or you can create a service call script:

```yaml
script:
  reset_jayden_tablet_prompt:
    sequence:
      - service: input_boolean.turn_on
        target:
          entity_id: input_boolean.jayden_tablet_enabled
```

## Benefits

✅ **Persistent State**: Acknowledged state survives app restarts  
✅ **Visible in Home Assistant**: You can see and manually control the gate state  
✅ **Automation Integration**: Easily create automations around the gate state  
✅ **Debugging**: Easier to troubleshoot issues by checking entity state  
✅ **Manual Override**: Users can manually reset the prompt by turning on the gate entity
