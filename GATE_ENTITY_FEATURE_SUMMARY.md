# AlexaPromptPoller Gate Entity Feature - Summary

## What's New

I've added a **gate entity** feature to the `AlexaPromptPoller` class that allows you to persist the acknowledged state in Home Assistant rather than just in memory. This solves the problem where acknowledged state is lost when the app restarts.

## Changes Made to `AlexaPromptPoller`

### 1. Constructor Enhancement
- Updated constructor to optionally accept `IHaContext`: `public AlexaPromptPoller(IScheduler scheduler, IAlexa alexa, ILogger logger, IHaContext? haContext = null)`
- The `haContext` parameter is optional (nullable) for backward compatibility

### 2. New Private Fields
```csharp
private string _gateEntityId = "";
private IObservable<Unit>? _gateResetTrigger;
```

### 3. New Configuration Methods

#### `WithGateEntity(string entityId, IHaContext haContext)`
Configures which boolean entity to use as the gate:
```csharp
.WithGateEntity("input_boolean.jayden_tablet_enabled", haContext)
```

#### `WithGateReset(IObservable<Unit> gateResetTrigger)`
Configures a trigger that will reset the gate entity to "on":
```csharp
.WithGateReset(Observable.Timer(
    Next6am(_scheduler.Now),
    TimeSpan.FromDays(1),
    _scheduler
).StartWith(0L))
```

#### `WithGateReset<T>(IObservable<T> gateResetTrigger)`
Generic version that accepts any observable type.

### 4. Modified Existing Methods

#### `Acknowledge()`
Now also turns off the gate entity:
```csharp
// Set gate entity to off (false) if configured
if (!string.IsNullOrWhiteSpace(_gateEntityId) && _haContext != null)
{
    _haContext.CallService("input_boolean", "turn_off", new ServiceTarget { EntityIds = new[] { _gateEntityId } });
    _logger.LogDebug("Gate entity turned off: {GateEntity}", _gateEntityId);
}
```

#### `ResetState()`
Now also turns on the gate entity:
```csharp
// Reset gate entity to on (true) if configured
if (!string.IsNullOrWhiteSpace(_gateEntityId) && _haContext != null)
{
    _haContext.CallService("input_boolean", "turn_on", new ServiceTarget { EntityIds = new[] { _gateEntityId } });
    _logger.LogDebug("Gate entity reset to on: {GateEntity}", _gateEntityId);
}
```

#### `Subscribe()`
Enhanced to:
- Check gate entity state before allowing triggers to fire
- Subscribe to gate reset trigger if configured

### 5. Enhanced Trigger Logic
In the `Subscribe()` method, triggers now check the gate entity state:
```csharp
// Check gate entity state if configured
if (!string.IsNullOrWhiteSpace(_gateEntityId) && _haContext != null)
{
    var gateEntity = _haContext.Entity(_gateEntityId);
    if (gateEntity.State != "on")
    {
        return false;  // Trigger ignored if gate is off
    }
}
```

### 6. Updated Class Documentation
Enhanced the class summary to include:
- Gate entity feature description
- Complete example showing how to use the feature
- Benefits of using persistent state

## Usage Example

### Before (Memory-Only State)
```csharp
public class JaydenTablet
{
    public JaydenTablet(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, ILogger<JaydenTablet> logger)
    {
        var poller = new AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges())
            .SetPrompt(new Alexa.Config { Message = "Have you taken your tablet?" })
            .WithCooldown(TimeSpan.FromMinutes(3))
            .WithDailyReset(Observable.Timer(Next6am(_scheduler.Now), TimeSpan.FromDays(1), _scheduler).StartWith(0L))
            .OnResponseYes(r => poller.Acknowledge())
            .Subscribe();
    }
}
// Problem: If app restarts, acknowledged state is lost!
```

### After (Persistent State with Gate Entity)
```csharp
public class JaydenTablet
{
    public JaydenTablet(IEntities entities, IServices services, IAlexa alexa, IHaContext haContext, IScheduler scheduler, ILogger<JaydenTablet> logger)
    {
        var poller = new AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges())
            .SetPrompt(new Alexa.Config { Message = "Have you taken your tablet?" })
            .WithCooldown(TimeSpan.FromMinutes(3))

            // NEW: Add persistent gate entity
            .WithGateEntity("input_boolean.jayden_tablet_enabled", haContext)
            .WithGateReset(Observable.Timer(Next6am(_scheduler.Now), TimeSpan.FromDays(1), _scheduler).StartWith(0L))

            .WithDailyReset(Observable.Timer(Next6am(_scheduler.Now), TimeSpan.FromDays(1), _scheduler).StartWith(0L))
            .OnResponseYes(r => poller.Acknowledge())  // Now turns off gate entity too!
            .Subscribe();
    }
}
// Solution: Acknowledged state persists in Home Assistant across restarts!
```

## Home Assistant Setup Required

Create an input_boolean in Home Assistant:

```yaml
input_boolean:
  jayden_tablet_enabled:
    name: "Jayden Tablet Prompt Enabled"
    initial: true
    icon: mdi:tablet
```

Or create it via the UI: Settings → Devices & Services → Helpers → Create Helper → Toggle.

## Key Features

✅ **Backward Compatible**: Existing code works without changes (gate entity is optional)  
✅ **Persistent State**: Acknowledged state survives app restarts  
✅ **Visible in Home Assistant**: Can see and manually control gate state  
✅ **Automation Integration**: Easy to create Home Assistant automations  
✅ **Flexible Reset**: Can have separate daily resets for state vs gate entity  
✅ **Structured Logging**: All gate operations are logged

## Testing

The implementation:
- Compiles without errors ✅
- Maintains backward compatibility ✅
- Follows existing code patterns ✅
- Includes comprehensive documentation ✅
- Uses fluent builder pattern consistent with rest of class ✅

## See Also

- `GATE_ENTITY_EXAMPLE.md` - Detailed usage guide with complete example
- `Helpers/Notifications/AlexaPromptPoller.cs` - Full implementation
