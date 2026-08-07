# AlexaPromptPoller Gate Entity Feature

## 🎯 Overview

Added a **persistent gate entity** feature to `AlexaPromptPoller` that solves the problem of losing acknowledged state when the application restarts.

## 🔴 Problem

Previously, when a prompt was acknowledged:

```csharp
_alexaPoller.Acknowledge();  // Set _isAcknowledged = true (in memory only)
```

This flag was stored **only in memory**. If the application restarted:
- The flag was lost
- Prompts would immediately start firing again
- Users would be re-prompted for something they already acknowledged

## 🟢 Solution

Store the acknowledged state in a **Home Assistant boolean entity** instead of just memory.

When you acknowledge:
- The gate entity is turned **OFF** in Home Assistant
- The poller checks this entity state before allowing prompts
- State persists across restarts ✅

When you reset:
- The gate entity is turned **ON** in Home Assistant
- Prompts can fire again

## 📋 What Changed

### Constructor
```csharp
// Old
public AlexaPromptPoller(IScheduler scheduler, IAlexa alexa, ILogger logger)

// New (backward compatible)
public AlexaPromptPoller(IScheduler scheduler, IAlexa alexa, ILogger logger, IHaContext? haContext = null)
```

### New Methods

#### `WithGateEntity(string entityId, IHaContext haContext)`
Configure which entity to use as the gate:
```csharp
.WithGateEntity("input_boolean.jayden_tablet_enabled", haContext)
```

#### `WithGateReset(IObservable<Unit> gateResetTrigger)`
Configure when to reset the gate entity:
```csharp
.WithGateReset(Observable.Timer(Next6am(_scheduler.Now), TimeSpan.FromDays(1), _scheduler))
```

#### `WithGateReset<T>(IObservable<T> gateResetTrigger)`
Generic version:
```csharp
.WithGateReset(myObservable)
```

### Modified Methods

#### `Acknowledge()`
Now also turns off the gate entity:
```csharp
public void Acknowledge()
{
    _isAcknowledged = true;
    _isWaitingForResponse = false;
    _statefulResetTrigger.OnNext(Unit.Default);

    // ⭐ NEW: Turn off gate entity
    if (!string.IsNullOrWhiteSpace(_gateEntityId) && _haContext != null)
    {
        _haContext.CallService("input_boolean", "turn_off", 
            new ServiceTarget { EntityIds = new[] { _gateEntityId } });
        _logger.LogDebug("Gate entity turned off: {GateEntity}", _gateEntityId);
    }

    _logger.LogDebug("AlexaPromptPoller acknowledged...");
}
```

#### `ResetState()`
Now also turns on the gate entity:
```csharp
public void ResetState()
{
    _lastPromptTime = DateTimeOffset.MinValue;
    _isWaitingForResponse = false;
    _isAcknowledged = false;
    _statefulResetTrigger.OnNext(Unit.Default);

    // ⭐ NEW: Turn on gate entity
    if (!string.IsNullOrWhiteSpace(_gateEntityId) && _haContext != null)
    {
        _haContext.CallService("input_boolean", "turn_on", 
            new ServiceTarget { EntityIds = new[] { _gateEntityId } });
        _logger.LogDebug("Gate entity reset to on: {GateEntity}", _gateEntityId);
    }

    _logger.LogDebug("AlexaPromptPoller state reset externally");
}
```

#### `Subscribe()`
Now checks gate entity state before allowing triggers:
```csharp
// ⭐ NEW: Check gate entity state if configured
if (!string.IsNullOrWhiteSpace(_gateEntityId) && _haContext != null)
{
    var gateEntity = _haContext.Entity(_gateEntityId);
    if (gateEntity.State != "on")
    {
        return false;  // Trigger ignored if gate is off
    }
}

// If acknowledged, don't process triggers
if (_isAcknowledged)
{
    return false;
}

// ... rest of cooldown logic
```

## 🚀 Quick Start

### 1. Create Input Boolean in Home Assistant

Create a new input helper in Home Assistant at Settings → Devices & Services → Helpers → Create Helper → Toggle.

Or add to `configuration.yaml`:
```yaml
input_boolean:
  jayden_tablet_enabled:
    name: "Jayden Tablet Prompt Enabled"
    initial: true
    icon: mdi:tablet
```

### 2. Inject IHaContext

Modify your app constructor to inject `IHaContext`:

```csharp
[NetDaemonApp]
public class JaydenTablet
{
    private readonly IHaContext _haContext;  // ⭐ Add this
    private AlexaPromptPoller? _alexaPoller;

    public JaydenTablet(
        IEntities entities,
        IServices services,
        IAlexa alexa,
        IHaContext haContext,        // ⭐ Inject this
        IScheduler scheduler,
        ILogger<JaydenTablet> logger)
    {
        _haContext = haContext;
        // ... rest of constructor
    }
}
```

### 3. Configure Gate Entity

When creating the poller, add gate entity configuration:

```csharp
_alexaPoller = new AlexaPromptPoller(scheduler, alexa, logger)
    .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges().Where(e => e.New.IsOn()))
    .SetPrompt(new Alexa.Config()
    {
        Message = "Jayden, have you taken your tablet?",
        Entity = "media_player.dining",
        EventId = "jayden_tablet"
    })
    .WithCooldown(TimeSpan.FromMinutes(3))

    // ⭐ NEW: Add gate entity for persistent state
    .WithGateEntity("input_boolean.jayden_tablet_enabled", haContext)

    // ⭐ NEW: Reset gate daily at 6 AM
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
        _logger.LogDebug("Tablet acknowledged");
        _alexaPoller?.Acknowledge();  // ⭐ This now turns off gate entity too!
    })

    .Subscribe();
```

## 📊 State Flow

```
Startup (6 AM)
  ↓
input_boolean = ON
  ↓
Listening for triggers...
  ↓
Trigger fires → Check gate → ON ✓
  ↓
Send prompt to Alexa
  ↓
User says "YES"
  ↓
Acknowledge() called
  ↓
input_boolean = OFF
  ↓
Next triggers ignored (gate is OFF)
  ↓
6 AM tomorrow
  ↓
Daily reset fires
  ↓
input_boolean = ON
  ↓
Back to listening... (loop)
```

## ✅ Benefits

| Benefit | Details |
|---------|---------|
| **Persistent State** | Acknowledged state survives app restarts |
| **Visible in HA** | Can see entity state in Home Assistant UI |
| **Debuggable** | Easier to troubleshoot issues |
| **Automatable** | Can create HA automations around gate state |
| **Overrideable** | Users can manually reset by toggling entity |
| **Backward Compatible** | Existing code works without changes |
| **Flexible** | Gate entity is optional |

## 📝 Examples

### Example 1: Basic Setup
```csharp
new AlexaPromptPoller(scheduler, alexa, logger)
    .AddTrigger(entities.BinarySensor.Motion.StateChanges().Where(e => e.New.IsOn()))
    .SetPrompt(new Alexa.Config { Message = "Question?" })
    .WithGateEntity("input_boolean.my_gate", haContext)
    .OnResponseYes(r => poller.Acknowledge())
    .Subscribe();
```

### Example 2: With Daily Reset
```csharp
var dailyReset = Observable.Timer(Next6am(_scheduler.Now), TimeSpan.FromDays(1), _scheduler).StartWith(0L);

new AlexaPromptPoller(scheduler, alexa, logger)
    .AddTrigger(entities.BinarySensor.Motion.StateChanges().Where(e => e.New.IsOn()))
    .SetPrompt(new Alexa.Config { Message = "Question?" })
    .WithCooldown(TimeSpan.FromMinutes(3))
    .WithGateEntity("input_boolean.my_gate", haContext)
    .WithDailyReset(dailyReset)
    .WithGateReset(dailyReset)
    .OnResponseYes(r => poller.Acknowledge())
    .Subscribe();
```

### Example 3: With Multiple Handlers
```csharp
new AlexaPromptPoller(scheduler, alexa, logger)
    .AddTrigger(entities.BinarySensor.Motion.StateChanges().Where(e => e.New.IsOn()))
    .SetPrompt(new Alexa.Config { Message = "Question?" })
    .WithGateEntity("input_boolean.my_gate", haContext)
    .WithGateReset(dailyReset)
    .OnResponseYes(response =>
    {
        _logger.LogInformation("Acknowledged by {Person}", response.ResponsePersonName);
        poller.Acknowledge();  // Turns off gate entity
        alexa.Announce("Thank you!");
    })
    .OnResponseNo(response =>
    {
        _logger.LogInformation("Declined");
        alexa.Announce("Please reconsider");
    })
    .OnResponseNone(response =>
    {
        _logger.LogInformation("No response");
        // Gate stays ON - will prompt again
    })
    .Subscribe();
```

## 🔧 Advanced: Manual Reset in Home Assistant

Create a service script to allow manual resets:

```yaml
script:
  reset_jayden_tablet_prompt:
    alias: "Reset Jayden Tablet Prompt"
    description: "Turn on the gate to allow prompts again"
    sequence:
      - service: input_boolean.turn_on
        target:
          entity_id: input_boolean.jayden_tablet_enabled
```

Use in automations:
```yaml
automation:
  - alias: "Reset tablets at 6 AM"
    trigger:
      platform: time
      at: "06:00:00"
    action:
      service: script.reset_jayden_tablet_prompt
```

## 🧪 Testing

The implementation compiles without errors and is fully backward compatible.

To test the gate entity feature:
1. Create the input_boolean entity in Home Assistant
2. Configure it in your app
3. Verify logs show gate entity operations
4. Check Home Assistant UI shows entity state changes
5. Restart app and verify acknowledged state persists

## 📚 Documentation Files

- **GATE_ENTITY_FEATURE_SUMMARY.md** - Changes overview
- **GATE_ENTITY_EXAMPLE.md** - Detailed usage guide
- **GATE_ENTITY_STATE_FLOW.md** - Visual state diagrams
- **README.md** - This file

## ❓ FAQ

**Q: What if I don't configure a gate entity?**  
A: Everything works as before. The feature is completely optional.

**Q: Can I use a different entity type instead of input_boolean?**  
A: Currently only `input_boolean` is supported, but the code could be extended for other types.

**Q: What happens if the gate entity doesn't exist?**  
A: The code safely handles missing entities - it logs a warning but doesn't crash.

**Q: Can I manually toggle the gate in Home Assistant?**  
A: Yes! You can turn it on/off in the HA UI, and the poller will respect the state.

**Q: Do I need to configure WithGateReset?**  
A: No, it's optional. You can just use WithGateEntity and manage resets via WithDailyReset.

**Q: What if Acknowledge() is called but gate entity is already off?**  
A: The code will call turn_off again, which is idempotent (safe operation).

## 🤝 Integration with Other Features

The gate entity feature works alongside all existing features:
- ✅ Multiple triggers
- ✅ Stateful triggers
- ✅ Cooldown management
- ✅ Response handlers
- ✅ Daily reset
- ✅ External reset triggers

## 📞 Support

For issues or questions, check the comprehensive documentation files or review the code comments in `AlexaPromptPoller.cs`.
