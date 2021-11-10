using System;
using System.Collections.Generic;
using System.Linq;
using LightsManager;
using NetDaemon.Common;

public partial class LightsManagerTests
{
    private void GivenTheControlEntityIs(string entityId, string state)
    {
        _config.ControlEntityIds = new List<string>(_config.ControlEntityIds.ToList()) { entityId };
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void GivenTheControlEntityIs(string state)
    {
        GivenTheControlEntityIs(LightMyLight, state);
    }

    private void GivenTheKeepAliveEntityIs(string entityId, string state)
    {
        _config.KeepAliveEntityIds = new List<string> { entityId };
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void GivenTheKeepAliveEntityIs(string state)
    {
        GivenTheKeepAliveEntityIs(SensorKeepAlive, state);
    }

    private void GivenTheLuxEntityIs(string entityId, int state)
    {
        _config.LuxEntityId = entityId;
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void GivenTheLuxEntityIs(int state)
    {
        GivenTheLuxEntityIs(SensorLux, state);
    }

    private void GivenTheLuxLimitIs(string entityId, int state)
    {
        _config.LuxLimitEntityId = entityId;
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void GivenTheLuxLimitIs(int state)
    {
        GivenTheLuxLimitIs(InputLuxLimit, state);
    }


    private void GivenTheManagerEnabledIs(string state)
    {
        MockState.Add(new EntityState { EntityId = _config.EnabledSwitchEntityId, State = state });
    }

    private void GivenTheManagerIsInitialised()
    {
        _manager                     =  new Manager(Object, _config);
        _managerFiredEvents          =  new List<(object, HassEventArgs)>();
        _manager.ManagerTimerReset   += (s, e) => _managerFiredEvents.Add(( s, e ));
        _manager.ManagerTimerSet     += (s, e) => _managerFiredEvents.Add(( s, e ));
        _manager.ManagerStateChanged += (s, e) => _managerFiredEvents.Add(( s, e ));
    }

    private void GivenTheNightControlEntityIs(string entityId, string state)
    {
        _config.NightControlEntityIds = new List<string>(_config.NightControlEntityIds.ToList()) { entityId };
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void GivenTheNightControlEntityIs(string state)
    {
        GivenTheNightControlEntityIs(LightMyNightLight, state);
    }

    private void GivenTheNightTimeEntityIs(string entityId, string state)
    {
        _config.NightTimeEntityId = entityId;
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void GivenTheNightTimeEntityIs(string state)
    {
        GivenTheNightTimeEntityIs(BinarySensorHouseMode, state);
    }

    private void GivenTheNightTimeEntityStatesAre(string entityId)
    {
        _config.NightTimeEntityStates = new List<string>(_config.NightTimeEntityStates.ToList()) { entityId };
    }

    private void GivenTheNightTimeoutIsSeconds(int timeout)
    {
        _config.NightTimeout = timeout;
    }

    private void GivenThePresenceEntityIs(string state)
    {
        GivenThePresenceEntityIs(state, BinarySensorMyMotionSensor);
    }

    private void GivenThePresenceEntityIs(string state, string entityId)
    {
        _config.PresenceEntityIds = new List<string>(_config.PresenceEntityIds.ToList()) { entityId };
        MockState.Add(new EntityState { EntityId = entityId, State = state });
    }

    private void GivenTheRoom()
    {
        _config                       = new LightsManagerConfig();
        _config.Name                  = "TestRoom";
        _config.PresenceEntityIds     = new List<string>();
        _config.ControlEntityIds      = new List<string>();
        _config.NightTimeEntityStates = new List<string>();
        _config.NdUserId              = "NetDaemon";
        MockState.Clear();
    }

    private void GivenTheTimeoutIsSeconds(int timeout)
    {
        _config.Timeout = timeout;
    }

    private void GivenTheGuardTimeoutIsSeconds(int timeout)
    {
        _config.GuardTimeout = timeout;
    }
}