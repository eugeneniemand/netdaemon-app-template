using System;
using NetDaemon.Common;

public partial class LightsManagerTests
{
    private void WhenAfterSeconds(int seconds)
    {
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(seconds).Ticks);
    }

    private void WhenEntityTurns(string entityId, string state)
    {
        switch (state)
        {
            case "on":
                TriggerStateChange(entityId, "off", "on");
                break;
            case "off":
                TriggerStateChange(entityId, "on", "off");
                break;
            default:
                throw new ArgumentException($"EntityTurns is not configured for supplied value: '{state}'", nameof(state));
        }
    }

    private void WhenHouseModeChangesTo(string state)
    {
        switch (state)
        {
            case "day":
                TriggerStateChange(BinarySensorHouseMode, "night", "day");
                break;
            case "night":
                TriggerStateChange(BinarySensorHouseMode, "day", "night");
                break;
            default:
                throw new ArgumentException($"WhenHouseModeChangesTo is not configured for supplied value: '{state}'", nameof(state));
        }
    }

    private void WhenKeepAliveEntityTurns(string state)
    {
        WhenEntityTurns(SensorKeepAlive, state);
    }

    private void WhenManagerIsTurned(string state)
    {
        switch (state)
        {
            case "on":
                TriggerStateChange(_config.EnabledSwitchEntityId, "off", "on");
                break;
            case "off":
                TriggerStateChange(_config.EnabledSwitchEntityId, "on", "off");
                break;
            default:
                throw new ArgumentException($"EntityTurns is not configured for supplied value: '{state}'", nameof(state));
        }
    }

    private void WhenControlEntityIsManuallyTurned(string newState)
    {
        WhenControlEntityIsManuallyTurned(LightMyLight, newState);
    }

    private void WhenTheGuardDogPatrols()
    {
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(_config.GuardTimeout).Ticks);
    }


    private void WhenControlEntityIsManuallyTurned(string entityId, string newState)
    {
        EntityState oldEntityState;
        EntityState newEntityState;
        switch (newState)
        {
            case "on":
                oldEntityState = new EntityState
                {
                    EntityId  = entityId,
                    State     = "off",
                    Attribute = null,
                    Context   = new Context { UserId = "" }
                };
                newEntityState = new EntityState
                {
                    EntityId  = entityId,
                    State     = "on",
                    Attribute = null,
                    Context   = new Context { UserId = "Eugene" }
                };
                break;
            case "off":
                oldEntityState = new EntityState
                {
                    EntityId  = entityId,
                    State     = "on",
                    Attribute = null,
                    Context   = new Context { UserId = "" }
                };
                newEntityState = new EntityState
                {
                    EntityId  = entityId,
                    State     = "off",
                    Attribute = null,
                    Context   = new Context { UserId = "Eugene" }
                };

                break;
            default:
                throw new ArgumentException($"EntityTurnsAsNetDaemon is not configured for supplied value: '{newState}'", nameof(newState));
        }

        TriggerStateChange(oldEntityState, newEntityState);
    }

    private void WhenPresenceEntityTurns(string state)
    {
        WhenEntityTurns(BinarySensorMyMotionSensor, state);
    }

    private void WhenLuxChangesTo(int state)
    {
        TriggerStateChange(SensorLux, null, state);
    }

    private void WhenTimeoutExpired()
    {
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(_config.Timeout + 5).Ticks);
    }
}