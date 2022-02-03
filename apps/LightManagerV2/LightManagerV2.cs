using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using daemonapp.apps.LightManagerV2;
using Ha;
using Microsoft.Extensions.Logging;
using NetDaemon.Common;
using NetDaemon.HassModel.Common;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Entities;


namespace LightManagerV2;

[Focus]
[NetDaemonApp]
public class LightsManager : IInitializable
{
    private readonly IHaContext             _ha;
    private readonly ILogger<LightsManager> _logger;
    private readonly INetDaemonScheduler    _scheduler;
    private readonly Entities               _entities;

    public string NdUserId { get; set; }
    public int GuardTimeout { get; set; } = 900;
    public int MinDuration { get; set; }
    public int MaxDuration { get; set; }

    public LightsManager(IHaContext ha, ILogger<LightsManager> logger, INetDaemonScheduler scheduler)
    {
        _ha        = ha;
        _logger    = logger;
        _scheduler = scheduler;
        _entities  = new Entities(_ha);
    }


    public void Initialize()
    {
        var configs = Rooms.Any(r => r.Debug) ? Rooms.Where(r => r.Debug).ToList() : Rooms.ToList();
        foreach (var config in configs)
        {
            var manager = new Manager(_ha, _logger, config);
        }
    }

    public IEnumerable<LightsManagerConfig>? Rooms { get; set; }
}

public class Manager
{
    private readonly IHaContext               _ha;
    private readonly ILogger<LightsManager>   _logger;
    private readonly LightsManagerConfig      _config;
    private          List<BinarySensorEntity> _sensors;
    private          List<BinarySensorEntity> _keepAlive;
    private          List<LightEntity>        _lights;
    private          InputSelectEntity?       _houseMode;
    private          List<LightEntity>        _nightLights;
    private          SwitchEntity?            _circadianSwitch;
    private          SensorEntity?            _luxLimit;
    private          SensorEntity?            _lux;
    private          string                   _room;

    public Manager(IHaContext ha, ILogger<LightsManager> logger, LightsManagerConfig config)
    {
        _ha     = ha;
        _logger = logger;
        _config = config;
        Init();
    }

    public void Init()
    {
        _logger.LogInformation("Setup {room}", _config.Name);
        _room            = _config.Name;
        _sensors         = _config.PresenceEntityIds.Select(entityId => new BinarySensorEntity(_ha, entityId)).ToList();
        _keepAlive       = _config.KeepAliveEntityIds.Select(entityId => new BinarySensorEntity(_ha, entityId)).ToList();
        _lights          = _config.ControlEntityIds.Select(entityId => new LightEntity(_ha, entityId)).ToList();
        _houseMode       = _config.NightTimeEntityId != null ? new InputSelectEntity(_ha, _config.NightTimeEntityId) : null;
        _nightLights     = _config.NightControlEntityIds.Select(entityId => new LightEntity(_ha, entityId)).ToList();
        _circadianSwitch = _config.CircadianSwitchEntityId != null ? new SwitchEntity(_ha, _config.CircadianSwitchEntityId) : null;
        _luxLimit        = _config.LuxLimitEntityId != null ? new SensorEntity(_ha, _config.LuxLimitEntityId) : null;
        _lux             = _config.LuxEntityId != null ? new SensorEntity(_ha, _config.LuxEntityId) : null;

        _sensors.StateChanges()
                .Where(e => e.New.IsOn())
                .Subscribe(e =>
                {
                    _logger.LogInformation("{room} Motion", _room);
                    if (_lux != null && _luxLimit != null && _lux.AsNumeric().State >= _luxLimit.AsNumeric().State)
                    {
                        _logger.LogInformation("{room} Too Bright", _room);
                        return;
                    }

                    TurnOnEntities();

                    _circadianSwitch?.TurnOn();
                });

        _sensors.Union(_keepAlive)
                .StateChanges()
                .Where(e => e.New.IsOff())
                .Throttle(TimeSpan.FromMinutes(5))
                .Subscribe(e =>
                {
                    _logger.LogInformation("{room} No Motion", _room);
                    if (_sensors.Union(_keepAlive).Any(entity => entity.IsOn()))
                    {
                        _logger.LogInformation("{room} Cant turn off active sensors", _room);
                        return;
                    }

                    _logger.LogInformation("{room} Turn Off", _room);
                    _lights.Union(_nightLights).ToList().ForEach(e => e.TurnOff());
                });

        _lights
            .StateAllChanges()
            .Where(e =>
                e.Old.IsOn() && e.New.IsOn()
                             && ( e.Old.Attributes.Brightness != e.New.Attributes.Brightness
                                  || e.Old.Attributes.ColorTemp != e.New.Attributes.ColorTemp ))
            .Subscribe(e =>
            {
                if (e.IsNdUserOrNull()) return;
                _logger.LogInformation("{room} Override by user", _room);
                _circadianSwitch?.TurnOff();
            });

        _houseMode?.StateChanges().Subscribe(e =>
        {
            _logger.LogInformation("{room} House Mode Changed", _room);
            if (!_lights.Union(_nightLights).Any(e => e.IsOn())) return;
            _lights.Except(_nightLights).ToList().ForEach(e => e.TurnOff());
            TurnOnEntities();
        });
    }

    private void TurnOnEntities()
    {
        _logger.LogInformation("{room} Turn On", _room);
        if (_houseMode != null && _config.NightTimeEntityStates.Contains(_houseMode.State))
            _nightLights.ForEach(e => e.TurnOn());
        else
            _lights.ForEach(e => e.TurnOn());
    }
}