using System;
using System.Collections.Generic;
using System.Linq;
using Ha;
using NetDaemon.Common.Reactive;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;

namespace LightManager;

public class Configurator
{
    private LightsManagerConfig _config;
    private IHaContext          _app;

    public Configurator(LightsManagerConfig config)
    {
        _config  = config;
        RoomName = _config.Name;
    }

    public List<SwitchEntity> Lights { get; set; }
    public List<SwitchEntity> Switches { get; set; }
    public List<SwitchEntity> AllControlEntities { get; set; }
    public List<BinarySensorEntity> PresenceSensors { get; set; }
    public BinarySensorEntity NightTime { get; set; }

    public SwitchEntity Enabled { get; set; }

    private double Lux => _config.LuxEntityId == null || _app.EntityState(_config.LuxEntityId) == LightsManager.UNKNOWN ? 0 : double.Parse(_app.EntityState(_config.LuxEntityId));
    private int? LuxLimit => _config.LuxEntityId == null ? _config.LuxLimit : int.Parse(_app.EntityState(_config.LuxLimitEntityId));
    private bool IsNightTime => _config.NightTimeEntityId != null && _config.NightTimeEntityStates.Contains(_app.EntityState(_config.NightTimeEntityId));
    public bool IsEnabled => _app.EntityState(_config.EnabledSwitchEntityId) == "on";
    public TimeSpan OverrideTimeoutSeconds => TimeSpan.FromSeconds(_config.OverrideTimeout);

    public TimeSpan TimeoutSeconds => TimeSpan.FromSeconds(IsNightTime ? _config.NightTimeout == 0 ? 90 : _config.NightTimeout : _config.Timeout);

    public bool LuxAboveLimit => Lux >= LuxLimit;
    public string RoomName { get; set; }
    public List<BinarySensorEntity> ActivePresenceSensors => PresenceSensors.Where(e => string.Equals(e.State, "on", StringComparison.OrdinalIgnoreCase)).ToList();
    public string NdUserId => _config.NdUserId;

    public void Configure(IHaContext app)
    {
        _app = app;

        Lights = new List<SwitchEntity>();
        foreach (var entityId in FilterControlEntities("light.")) Lights.Add(new SwitchEntity(app, entityId));

        Switches = new List<SwitchEntity>();
        foreach (var entityId in FilterControlEntities("switch.")) Switches.Add(new SwitchEntity(app, entityId));

        AllControlEntities = new List<SwitchEntity>();
        foreach (var entityId in _config.ControlEntityIds.Union(_config.NightControlEntityIds)) AllControlEntities.Add(new SwitchEntity(app, entityId));

        PresenceSensors = new List<BinarySensorEntity>();
        foreach (var entityId in _config.PresenceEntityIds.Union(_config.KeepAliveEntityIds)) PresenceSensors.Add(new BinarySensorEntity(app, entityId));
        Enabled   = new SwitchEntity(app, _config.EnabledSwitchEntityId);
        NightTime = new BinarySensorEntity(app, _config.NightTimeEntityId);
    }

    private IEnumerable<string> FilterControlEntities(string filter)
    {
        return !IsNightTime || !_config.NightControlEntityIds.Any()
            ? _config.ControlEntityIds.Where(entityId => entityId.Contains(filter, StringComparison.OrdinalIgnoreCase))
            : _config.NightControlEntityIds.Where(entityId => entityId.Contains(filter, StringComparison.OrdinalIgnoreCase));
    }
}