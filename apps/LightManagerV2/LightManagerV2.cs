#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Ha;
using Microsoft.Extensions.Logging;
using NetDaemon.Common;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;

#endregion

namespace LightManagerV2;

[Focus]
[NetDaemonApp]
public class LightsManager : IInitializable
{
    private readonly ILogger<LightsManager> _logger;

    public LightsManager(IHaContext ha, ILogger<LightsManager> logger, INetDaemonScheduler scheduler)
    {
        _logger = logger;
    }

    public IEnumerable<Manager>? Rooms { get; set; } = new List<Manager>();

    public int GuardTimeout { get; set; } = 900;
    public int MaxDuration { get; set; }
    public int MinDuration { get; set; }
    public string NdUserId { get; set; }


    public void Initialize()
    {
        ( Rooms.Any(r => r.Debug) ? Rooms.Where(r => r.Debug).ToList() : Rooms.ToList() ).ForEach(r => r.Init(_logger, NdUserId));
    }
}

public class Manager
{
    private          ILogger<LightsManager> _logger;
    private          string                 _ndUserId;
    private readonly List<string>           _onStates = new() { "on", "playing" };
    public bool Debug { get; set; }
    public IEnumerable<BinarySensorEntity> KeepAliveEntities { get; set; } = new List<BinarySensorEntity>();
    public IEnumerable<BinarySensorEntity> PresenceEntities { get; set; } = new List<BinarySensorEntity>();
    public IEnumerable<LightEntity> ControlEntities { get; set; } = new List<LightEntity>();
    public IEnumerable<LightEntity> NightControlEntities { get; set; } = new List<LightEntity>();
    public IEnumerable<string> NightTimeEntityStates { get; set; } = new List<string>();
    public InputSelectEntity? NightTimeEntity { get; set; }
    public int NightTimeout { get; set; }
    public int Timeout { get; set; }
    public int? LuxLimit { get; set; }
    public NumericSensorEntity? LuxEntity { get; set; }
    public NumericSensorEntity? LuxLimitEntity { get; set; }
    public string Name { get; set; } = null!;
    public SwitchEntity? CircadianSwitchEntity { get; set; }

    private bool IsAnyControlEntityOn => AllControlEntities.Any(e => e.IsOn());
    private bool IsNightMode => NightTimeEntity != null && NightTimeEntityStates.Contains(NightTimeEntity.State);
    private bool IsOccupied => PresenceEntities.Union(KeepAliveEntities).Any(entity => entity.IsOn() || _onStates.Contains(entity.State!));
    private bool IsTooBright => LuxEntity != null && ( LuxLimitEntity != null ? LuxEntity.State >= LuxLimitEntity.State : LuxEntity.State >= LuxLimit );
    private List<LightEntity> AllControlEntities => ControlEntities.Union(NightControlEntities).ToList();
    private TimeSpan DynamicTimeout => TimeSpan.FromSeconds(IsNightMode ? NightTimeout == 0 ? 90 : NightTimeout : Timeout);

    public void Init(ILogger<LightsManager> logger, string ndUserId)
    {
        _logger   = logger;
        _ndUserId = ndUserId;
        _logger.LogInformation("Setup {room}", Name);

        SubscribePresenceOnEvent();
        SubscribePresenceOffEvent();
        SubscribeOverrideEvent();
        SubscribeHouseModeEvent();
    }

    private bool IsNdUserOrHa(StateChange stateChange) => stateChange?.New?.Context?.UserId == null || stateChange?.New?.Context?.UserId == _ndUserId;

    private bool IsOverride(StateChange<LightEntity, EntityState<LightAttributes>> e) =>
        !IsNdUserOrHa(e) &&
        e.Old.IsOn() && e.New.IsOn()
        && ( !Equals(e.Old?.Attributes?.Brightness, e.New?.Attributes?.Brightness)
             || !Equals(e.Old?.Attributes?.ColorTemp, e.New?.Attributes?.ColorTemp) );

    private void SubscribeHouseModeEvent()
    {
        _logger.LogInformation("{room} Subscribed to House Mode Changed Events", Name);
        NightTimeEntity?.StateChanges().Subscribe(e =>
        {
            _logger.LogInformation("{room} House Mode Changed", Name);
            if (!IsAnyControlEntityOn) return;
            ControlEntities.Except(NightControlEntities).ToList().ForEach(e => e.TurnOff());
            TurnOnEntities();
        });
    }

    private void SubscribeOverrideEvent()
    {
        _logger.LogInformation("{room} Subscribed to Override Events", Name);
        ControlEntities
            .StateAllChanges()
            .Where(IsOverride)
            .Subscribe(e =>
            {
                _logger.LogInformation("{room} Override by user", Name);

                if (CircadianSwitchEntity == null) return;
                _logger.LogInformation("{room} Turn off circadian switch", Name);
                CircadianSwitchEntity.TurnOff();
            });
    }

    private void SubscribePresenceOffEvent()
    {
        _logger.LogInformation("{room} Subscribed to Presence Off Events", Name);
        PresenceEntities.Union(KeepAliveEntities)
                        .StateChanges()
                        .Where(e => e.New.IsOff())
                        .Throttle(_ => Observable.Timer(DynamicTimeout))
                        .Subscribe(e =>
                        {
                            _logger.LogInformation("{room} No Motion", Name);
                            if (IsOccupied)
                            {
                                _logger.LogInformation("{room} Cant turn off - Occupied", Name);
                                return;
                            }

                            _logger.LogInformation("{room} Turn Off", Name);
                            AllControlEntities.ForEach(e => e.TurnOff());
                        });
    }

    private void SubscribePresenceOnEvent()
    {
        _logger.LogInformation("{room} Subscribed to Presence On Events", Name);
        PresenceEntities.StateChanges()
                        .Where(e => e.New.IsOn())
                        .Subscribe(e =>
                        {
                            _logger.LogInformation("{room} Motion", Name);
                            if (IsTooBright)
                            {
                                _logger.LogInformation("{room} Too Bright", Name);
                                return;
                            }

                            TurnOnEntities();

                            if (CircadianSwitchEntity == null) return;
                            _logger.LogInformation("{room} Turn on circadian switch", Name);
                            CircadianSwitchEntity.TurnOn();
                        });
    }

    private void TurnOnEntities()
    {
        if (IsNightMode)
        {
            _logger.LogInformation("{room} Turn On Night Control Entities", Name);
            NightControlEntities.ToList().ForEach(e => e.TurnOn());
        }
        else
        {
            _logger.LogInformation("{room} Turn On Control Entities", Name);
            ControlEntities.ToList().ForEach(e => e.TurnOn());
        }
    }
}