using System;
using System.Collections.Generic;
using System.Linq;
using LightsManager;
using Moq;
using NetDaemon.Common;
using Xunit;

public partial class LightsManagerTests
{
    private const string              _andAfterSecondsTemplate         = "and after <b>{0}</b> seconds";
    private const string              _theControlEntityIsTemplate      = "and the <b>control</b> entity <b>{0}</b> is <b>{1}</b>";
    private const string              _theKeepAliveEntityIsTemplate    = "and the <b>keep alive</b> entity <b>{0}</b> is <b>{1}</b>";
    private const string              _theNightControlEntityIsTemplate = "and the <b>night control</b> entity <b>{0}</b> is <b>{1}</b>";
    private const string              _thePresenceEntityIsTemplate     = "and the <b>presence</b> entity <b>{0}</b> is <b>{1}</b>";
    private const string              _thenEntityTurnsTemplate         = "then <b>{0}</b> turns <b>{1}</b>";
    private const string              _whenEntityTurnsTemplate         = "when <b>{0}</b> turns <b>{1}</b>";
    private const string              _thenEntityTurnedOffTemplate     = "then <b>{0}</b> has <b>{1}</b> turned <b>off</b>";
    private const string              _thenEntityTurnedOnTemplate      = "then <b>{0}</b> has <b>{1}</b> turned <b>on</b>";
    private const string              _stateChangedTemplate            = "when <b>{0}</b> changes from <b>{1}</b> to <b>{2}</b>";
    private       LightsManagerConfig _config;
    private       Manager             _manager;

    private void AfterSeconds(int seconds)
    {
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(seconds).Ticks);
    }

    private void EntityTurns(string entityId, string newState)
    {
        switch (newState)
        {
            case "on":
                TriggerStateChange(entityId, "off", "on");
                break;
            case "off":
                TriggerStateChange(entityId, "on", "off");
                break;
            default:
                throw new ArgumentException($"EntityTurns is not configured for supplied value: '{newState}'", nameof(newState));
        }
    }

    private void OverrideEntity(string entityId, string newState)
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

    private void TheKeepAliveEntityIs(string entityId, string state)
    {
        _config.KeepAliveEntityIds = new List<string> { entityId };
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void GivenTheRoom(string roomName)
    {
        _config                       = new LightsManagerConfig();
        _config.Name                  = roomName;
        _config.PresenceEntityIds     = new List<string>();
        _config.ControlEntityIds      = new List<string>();
        _config.NightTimeEntityStates = new List<string>();
        _config.NdUserId              = "NetDaemon";
        MockState.Clear();
    }

    private void TheNightTimeEntityIs(string entityId, string state)
    {
        _config.NightTimeEntityId = entityId;
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void TheNightTimeEntityStatesAre(string entityId)
    {
        _config.NightTimeEntityStates = new List<string>(_config.NightTimeEntityStates.ToList()) { entityId };
    }

    private void TheControlEntitiesAre(params string[] entityIds)
    {
        Assert.Equal(_config.ControlEntityIds.ToList().OrderBy(i => i), entityIds.ToList().OrderBy(i => i));
    }

    private void TheNightControlEntitiesAre(params string[] entityIds)
    {
        Assert.Equal(_config.NightControlEntityIds.ToList().OrderBy(i => i), entityIds.ToList().OrderBy(i => i));
    }

    private void TheNightControlEntityIs(string entityId, string state)
    {
        _config.NightControlEntityIds = new List<string>(_config.NightControlEntityIds.ToList()) { entityId };
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void TheLuxLimitEntityIs(string entityId, int state)
    {
        _config.LuxLimitEntityId = entityId;
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void TheLuxEntityIs(string entityId, int state)
    {
        _config.LuxEntityId = entityId;
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void TheControlEntityIs(string entityId, string state)
    {
        _config.ControlEntityIds = new List<string>(_config.ControlEntityIds.ToList()) { entityId };
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }


    private void TheEntityTurnedOffTimes(string entityId, Times times)
    {
        VerifyEntityTurnOff(entityId, times: times);
    }

    private void TheEntityTurnedOnTimes(string entityId, Times times)
    {
        VerifyEntityTurnOn(entityId, times: times);
    }

    private void TheManagerIsInitialised()
    {
        _manager = new Manager(Object, _config);
    }

    private void ThePresenceEntityIs(string entityId, string state)
    {
        _config.PresenceEntityIds = new List<string>(_config.PresenceEntityIds.ToList()) { entityId };
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void TheTimeoutIsSeconds(int timeout)
    {
        _config.Timeout = timeout;
    }

    private void TheNightTimeoutIsSeconds(int timeout)
    {
        _config.NightTimeout = timeout;
    }

    private void TimeoutExpired()
    {
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(_config.Timeout).Ticks);
    }

    private void TheManagerEnabledIs(string state)
    {
        MockState.Add(new EntityState { EntityId = _config.EnabledSwitchEntityId, State = state });
    }

    private void TheManagerStateIs(ManagerState state)
    {
        Assert.Equal(state, _manager.State);
    }
}