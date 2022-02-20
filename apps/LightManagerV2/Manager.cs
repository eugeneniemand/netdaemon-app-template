using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using NetDaemon.HassModel.Entities;

namespace LightManagerV2;

public class Manager
{
    private readonly List<string>           _onStates = new() { "on", "playing" };
    private          ILogger<LightsManager> _logger;
    private          string                 _ndUserId;
    private          IRandomManager         _randomManager;
    private          IScheduler             _scheduler;
    public bool Debug { get; set; }


    public bool IsAnyControlEntityOn => AllControlEntities.Any(e => e.IsOn());
    public bool IsNightMode => NightTimeEntity != null && NightTimeEntityStates.Contains(NightTimeEntity.State);
    public bool IsOccupied => PresenceEntities.Union(KeepAliveEntities).Any(entity => entity.IsOn() || _onStates.Contains(entity.State!));
    public bool IsTooBright => LuxEntity != null && ( LuxLimitEntity != null ? LuxEntity.State >= LuxLimitEntity.State : LuxEntity.State >= LuxLimit );
    public IEnumerable<BinarySensorEntity> KeepAliveEntities { get; set; } = new List<BinarySensorEntity>();
    public IEnumerable<BinarySensorEntity> PresenceEntities { get; set; } = new List<BinarySensorEntity>();
    public IEnumerable<LightEntity> ControlEntities { get; set; } = new List<LightEntity>();
    public IEnumerable<LightEntity> NightControlEntities { get; set; } = new List<LightEntity>();
    public IEnumerable<string> NightTimeEntityStates { get; set; } = new List<string>();
    public IEnumerable<string> RandomStates { get; set; } = new List<string>();
    public InputSelectEntity? NightTimeEntity { get; set; }
    public int NightTimeout { get; set; }
    public int Timeout { get; set; }
    public int? LuxLimit { get; set; }
    public List<LightEntity> AllControlEntities => ControlEntities.Union(NightControlEntities).ToList();
    public NumericSensorEntity? LuxEntity { get; set; }
    public NumericSensorEntity? LuxLimitEntity { get; set; }
    public string Name { get; set; } = null!;
    public SwitchEntity? CircadianSwitchEntity { get; set; }
    public TimeSpan DynamicTimeout => TimeSpan.FromSeconds(IsNightMode ? NightTimeout == 0 ? 90 : NightTimeout : Timeout);

    public void Init(ILogger<LightsManager> logger, string ndUserId, IRandomManager randomManager, IScheduler scheduler)
    {
        _logger        = logger;
        _ndUserId      = ndUserId;
        _randomManager = randomManager;
        _scheduler     = scheduler;
        _logger.LogInformation("Setup {room}", Name);

        SubscribePresenceOnEvent();
        SubscribePresenceOffEvent();
        SubscribeOverrideEvent();
        SubscribeHouseModeEvent();

        if (RandomStates.Any())
            _randomManager.Init(ControlEntities, RandomStates);
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

            if (IsNightMode)
                ControlEntities.Except(NightControlEntities).ToList()
                               .ForEach(f => f.TurnOff());
            if (!IsNightMode)
                NightControlEntities.Except(ControlEntities).ToList()
                                    .ForEach(f => f.TurnOff());

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
                        .Throttle(_ => Observable.Timer(DynamicTimeout, _scheduler))
                        .Subscribe(e =>
                        {
                            _logger.LogInformation("{room} No Motion", Name);
                            if (IsOccupied)
                            {
                                _logger.LogInformation("{room} Cant turn off - Occupied", Name);
                                return;
                            }

                            _logger.LogInformation("{room} Turn Off", Name);
                            AllControlEntities
                                .Where(w => w.IsOn()).ToList()
                                .ForEach(e => e.TurnOff());
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
                        });
    }


    private void TurnOnEntities()
    {
        List<LightEntity> lightEntities;
        if (IsNightMode)
        {
            _logger.LogInformation("{room} Turn On Night Control Entities", Name);
            lightEntities = NightControlEntities.ToList();
        }
        else
        {
            _logger.LogInformation("{room} Turn On Control Entities", Name);
            lightEntities = ControlEntities.ToList();
        }

        foreach (var e in lightEntities.Where(e => e.IsOff()))
        {
            e.TurnOn();
            if (CircadianSwitchEntity == null) return;
            _logger.LogInformation("{room} Turn on circadian switch", Name);
            CircadianSwitchEntity.TurnOn();
        }
    }
}