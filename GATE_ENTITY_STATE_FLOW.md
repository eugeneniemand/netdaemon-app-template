# AlexaPromptPoller Gate Entity - State Diagram

## State Machine Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                        Gate Entity State Flow                        │
└─────────────────────────────────────────────────────────────────────┘

INITIAL STATE: input_boolean.jayden_tablet_enabled = ON
                         ↓
          ┌─────────────────────────────┐
          │   Listening for Triggers    │
          │   (Gate = ON, not ack'd)    │
          └──────────┬──────────────────┘
                     │
                     │ Trigger fires (motion, door, etc.)
                     │
                     ↓
          ┌─────────────────────────────┐
          │    Check Gate Entity         │
          │  Is entity state == "on"?    │
          └──────────┬──────────────────┘
                     │
        ┌────────────┴────────────┐
        │ YES                     │ NO
        ↓                         ↓
    ┌─────────────┐      ┌──────────────┐
    │ Check Cooldown    │ Ignore Trigger│
    │ & Ack Status       │ (Silently drop)
    └────┬───────┘      └──────────────┘
         │
    ┌────┴─────────────────┐
    │ Ready?                │ Not ready?
    │ (Cooldown ok,         │ (In cooldown or
    │  not acked)           │  already acked)
    ↓                       ↓
┌────────────┐      ┌──────────────┐
│ SEND PROMPT │      │ Ignore Trigger
│ to Alexa   │      │ (Silently drop)
└────────────┘      └──────────────┘
    │
    ↓ Alexa waits for response...
    │
    ├─────────────────────────────────────┬─────────────┐
    │ User says...                        │             │
    ↓                                     ↓             ↓
┌──────────┐                      ┌──────────┐    ┌──────────┐
│   "YES"  │                      │   "NO"   │    │  (Silent)│
│ (Ack'd)  │                      │ (Not OK) │    │ (Timeout)│
└────┬─────┘                      └──────────┘    └──────────┘
     │
     ↓ Response received
     │
     ├─ Call Acknowledge()
     │  (or response handler calls it)
     │
     ↓
┌─────────────────────────────────────┐
│ Turn OFF gate entity                │
│ input_boolean.turn_off              │
│ (entity_id: jayden_tablet_enabled)  │
└─────────────────────────────────────┘
     │
     ↓
  ┌──────────────────────────────┐
  │  Gate Entity = OFF            │
  │  ✗ No more prompts allowed    │
  │  (even if triggers fire)      │
  └──────────────────────────────┘
     │
     │ Time passes...
     │ (Wait for reset)
     │
     ↓ Daily Reset Time (6 AM) fires
     │
     ├─ Call ResetState()
     │  (via WithDailyReset trigger)
     │
     ├─ Call Gate Reset
     │  (via WithGateReset trigger)
     │
     ↓
┌─────────────────────────────────────┐
│ Turn ON gate entity                 │
│ input_boolean.turn_on               │
│ (entity_id: jayden_tablet_enabled)  │
└─────────────────────────────────────┘
     │
     ↓
  ┌──────────────────────────────┐
  │  Gate Entity = ON             │
  │  ✓ Prompts allowed again      │
  │  Back to Listening state      │
  └──────────────────────────────┘
     │
     └─────────────────────────────→ (Loop back to start)
```

## Decision Tree

```
TRIGGER FIRES
    │
    ├─→ Gate Entity exists?
    │   │
    │   ├─ NO  → Continue
    │   └─ YES → Entity state == "on"?
    │           │
    │           ├─ NO  → REJECT (Ignore trigger)
    │           └─ YES → Continue
    │
    ├─→ Acknowledged flag set?
    │   │
    │   ├─ YES → REJECT (Ignore trigger)
    │   └─ NO  → Continue
    │
    ├─→ Cooldown elapsed?
    │   │
    │   ├─ NO  → REJECT (Ignore trigger)
    │   └─ YES → Continue
    │
    └─→ ✓ SEND PROMPT
        │
        └─→ Wait for response...
            │
            ├─→ "YES" → Acknowledge() → Gate OFF
            ├─→ "NO"  → No ack action (user's choice)
            └─→ Timeout → No ack action (no response)
```

## Code Flow

```
new AlexaPromptPoller(scheduler, alexa, logger)
    │
    ├─ .AddTrigger(...)                      ← Add one or more triggers
    │
    ├─ .SetPrompt(...)                       ← Configure prompt
    │
    ├─ .WithCooldown(...)                    ← Set cooldown between prompts
    │
    ├─ .WithGateEntity("entity_id", haCtx)   ← ⭐ NEW: Enable gate feature
    │   └─ Checks entity state before allowing prompts
    │   └─ Turns entity OFF on Acknowledge()
    │   └─ Turns entity ON on ResetState()
    │
    ├─ .WithDailyReset(observable)           ← Reset state daily
    │
    ├─ .WithGateReset(observable)            ← ⭐ NEW: Reset gate daily
    │   └─ Calls ResetState() which turns gate ON
    │
    ├─ .OnResponseYes(handler)                ← Handle user acceptance
    │   └─ handler calls poller.Acknowledge()
    │   └─ Acknowledge() turns gate OFF
    │
    └─ .Subscribe()                           ← Start listening
       │
       └─ When trigger fires:
          ├─ Check gate entity state ✓ NEW
          ├─ Check acknowledged flag
          ├─ Check cooldown
          └─ Send prompt if all checks pass
```

## Entity State Examples

### Scenario 1: Fresh Start (6 AM)
```
input_boolean.jayden_tablet_enabled = ON
_lastPromptTime = DateTimeOffset.MinValue
_isAcknowledged = false
_isWaitingForResponse = false

Result: Ready to send prompts
```

### Scenario 2: After Acknowledgment (12 PM)
```
input_boolean.jayden_tablet_enabled = OFF  ← Set by Acknowledge()
_lastPromptTime = 2024-01-01 12:30:00
_isAcknowledged = true
_isWaitingForResponse = false

Result: Prompts blocked (gate is OFF)
         Any triggers are silently ignored
```

### Scenario 3: After Manual Reset (2 PM - user toggles entity in HA)
```
input_boolean.jayden_tablet_enabled = ON   ← Manually turned ON
_lastPromptTime = DateTimeOffset.MinValue  ← Reset by ResetState()
_isAcknowledged = false                    ← Reset by ResetState()
_isWaitingForResponse = false

Result: Ready to send prompts again
         Next trigger will fire (if cooldown allows)
```

### Scenario 4: Daily Reset (6 AM next day)
```
Daily reset trigger fires:
  ├─ ResetState() is called
  │  ├─ _lastPromptTime = DateTimeOffset.MinValue
  │  ├─ _isAcknowledged = false
  │  └─ turn_on(gate entity)
  │
  └─ WithGateReset trigger fires
     └─ turn_on(gate entity)  ← Extra confirmation

input_boolean.jayden_tablet_enabled = ON
_lastPromptTime = DateTimeOffset.MinValue
_isAcknowledged = false
_isWaitingForResponse = false

Result: Ready for new day of prompts
```

## Home Assistant Integration Points

### 1. Entity Check
```csharp
var gateEntity = _haContext.Entity(_gateEntityId);
if (gateEntity.State != "on")
    return false;
```

### 2. Turn Off on Acknowledge
```csharp
_haContext.CallService("input_boolean", "turn_off", 
    new ServiceTarget { EntityIds = new[] { _gateEntityId } });
```

### 3. Turn On on Reset
```csharp
_haContext.CallService("input_boolean", "turn_on", 
    new ServiceTarget { EntityIds = new[] { _gateEntityId } });
```

### 4. Home Assistant Automation (Optional)
```yaml
automation:
  - alias: Reset Jayden Tablet Prompt
    trigger:
      platform: time
      at: "06:00:00"
    action:
      service: input_boolean.turn_on
      target:
        entity_id: input_boolean.jayden_tablet_enabled
```

### 5. Home Assistant Script (Optional)
```yaml
script:
  reset_jayden_tablet_prompt:
    sequence:
      - service: input_boolean.turn_on
        target:
          entity_id: input_boolean.jayden_tablet_enabled
```
