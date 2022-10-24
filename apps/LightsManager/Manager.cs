using NetDaemon.Extensions.MqttEntityManager;

namespace LightManagerV2;

public class Manager
{
    private readonly List<string>           _onStates;
    private          string                 _enabledSwitch;
    private          IMqttEntityManager     _entityManager;
    private          IHaContext             _haContext;
    private          ILogger<LightsManager> _logger;
    private          string                 _ndUserId;
    private          IRandomManager         _randomManager;
    private          IScheduler             _scheduler;
    private          IDisposable            overrideSchedule;

    public Manager()
    {
        ControlEntities       = new List<LightEntity>();
        KeepAliveEntities     = new List<BinarySensorEntity>();
        PresenceEntities      = new List<BinarySensorEntity>();
        NightControlEntities  = new List<LightEntity>();
        NightTimeEntityStates = new List<string>();
        RandomStates          = new List<string>();
        _onStates             = new List<string> { "on", "playing" };
        Name                  = null!;
    }

    public bool Debug { get; set; }


    public bool IsAnyControlEntityOn => AllControlEntities.Any(e => e.IsOn());
    public bool IsNightMode => NightTimeEntity != null && NightTimeEntityStates.Contains(NightTimeEntity.State);
    public bool IsOccupied => PresenceEntities.Union(KeepAliveEntities).Any(entity => entity.IsOn() || _onStates.Contains(entity.State!));
    public bool IsTooBright => LuxEntity != null && ( LuxLimitEntity != null ? LuxEntity.State >= LuxLimitEntity.State : LuxEntity.State >= LuxLimit );
    public Entity? ConditionEntity { get; set; }
    public InputSelectEntity? NightTimeEntity { get; set; }
    public int NightTimeout { get; set; }
    public int Timeout { get; set; }
    public int? LuxLimit { get; set; }
    public List<BinarySensorEntity> KeepAliveEntities { get; set; }
    public List<BinarySensorEntity> PresenceEntities { get; set; }
    public List<LightEntity> AllControlEntities => ControlEntities!.Union(NightControlEntities).ToList();
    public List<LightEntity> ControlEntities { get; set; }
    public List<LightEntity> NightControlEntities { get; set; }
    public List<string> NightTimeEntityStates { get; set; }
    public List<string> RandomStates { get; set; }
    public NumericSensorEntity? LuxEntity { get; set; }
    public NumericSensorEntity? LuxLimitEntity { get; set; }
    public string Name { get; set; }
    public string? ConditionEntityState { get; set; }
    public SwitchEntity ManagerEnabled { get; set; }
    public SwitchEntity? CircadianSwitchEntity { get; set; }
    public TimeSpan DynamicTimeout => TimeSpan.FromSeconds(OverrideActive ? 10 : TimeoutParsed);
    private bool ConditionEntityStateNotMet => ConditionEntity != null && ConditionEntityState != null && ConditionEntity.State != ConditionEntityState;
    private bool OverrideActive { get; set; }
    private int NightTimeoutParsed => NightTimeout == 0 ? 90 : NightTimeout;
    private int TimeoutParsed => IsNightMode ? NightTimeoutParsed : Timeout;

    public async Task Init(ILogger<LightsManager> logger, string ndUserId, IRandomManager randomManager, IScheduler scheduler, IHaContext haContext, IMqttEntityManager entityManager)
    {
        _logger        = logger;
        _ndUserId      = ndUserId;
        _randomManager = randomManager;
        _scheduler     = scheduler;
        _haContext     = haContext;
        _entityManager = entityManager;
        _enabledSwitch = $"switch.light_manager_{Name.ToLower()}";
        _logger.LogInformation("Setup {room}", Name);
        await SetupEnabledSwitch();
        SubscribePresenceOnEvent();
        SubscribePresenceOffEvent();
        SubscribeOverrideEvent();
        SubscribeManualTurnOnOverrideEvent();
        SubscribeManualTurnOffOverrideEvent();
        SubscribeHouseModeEvent();

        if (RandomStates.Any())
            _randomManager.Init(ControlEntities, RandomStates);
    }

    private bool IsNdUserOrHa(StateChange stateChange) => stateChange?.New?.Context?.UserId == null || stateChange?.New?.Context?.UserId == _ndUserId;

    private bool LightAttributesOverride(StateChange<LightEntity, EntityState<LightAttributes>> e) =>
        !IsNdUserOrHa(e) &&
        e.Old.IsOn() && e.New.IsOn()
        && ( !Equals(e.Old?.Attributes?.Brightness, e.New?.Attributes?.Brightness)
             || !Equals(e.Old?.Attributes?.ColorTemp, e.New?.Attributes?.ColorTemp) );

    private bool LightTurnedOffManually(StateChange<LightEntity, EntityState<LightAttributes>> e) =>
        !IsNdUserOrHa(e) &&
        e.Old.IsOn() && e.New.IsOff();

    private bool LightTurnedOnManually(StateChange<LightEntity, EntityState<LightAttributes>> e) =>
        !IsNdUserOrHa(e) &&
        e.Old.IsOff() && e.New.IsOn();

    private async Task SetupEnabledSwitch()
    {
        _logger.LogDebug("{room} Setup Enabled Switch", Name);
        if (_haContext.Entity(_enabledSwitch).State == null || string.Equals(_haContext.Entity(_enabledSwitch).State, "unavailable", StringComparison.InvariantCultureIgnoreCase))
        {
            _entityManager.CreateAsync(_enabledSwitch, new EntityCreationOptions(Name: $"Light Manager {Name}", DeviceClass: "switch", Persist: true)).GetAwaiter().GetResult();
        }
        else
        {
            ManagerEnabled = new SwitchEntity(_haContext, _enabledSwitch);
            ManagerEnabled.TurnOn();
        }

        if (_enabledSwitch != "switch.light_manager_testroom")
            ( await _entityManager.PrepareCommandSubscriptionAsync(_enabledSwitch) ).Subscribe(async s =>
                await _entityManager.SetStateAsync(_enabledSwitch, s)
            );
    }

    private void SubscribeHouseModeEvent()
    {
        _logger.LogDebug("{room} Subscribed to House Mode Changed Events", Name);
        NightTimeEntity?.StateChanges().Subscribe(async e =>
        {
            _logger.LogInformation("{room} House Mode Changed", Name);
            if (!IsAnyControlEntityOn)
            {
                await UpdateAttributes();
                return;
            }

            if (IsNightMode)
                ControlEntities.Except(NightControlEntities).ToList()
                               .ForEach(f => f.TurnOff());
            if (!IsNightMode)
                NightControlEntities.Except(ControlEntities).ToList()
                                    .ForEach(f => f.TurnOff());

            TurnOnEntities();
            await UpdateAttributes();
        });
    }

    private void SubscribeManualTurnOffOverrideEvent()
    {
        _logger.LogDebug("{room} Subscribed to Manual Turn Off Override Events", Name);
        ControlEntities
            .StateAllChanges()
            .Where(LightTurnedOffManually)
            .Subscribe(async e =>
            {
                _logger.LogInformation("{room} Manual Turn Off Override by user", Name);
                _logger.LogInformation("{room} Resetting override as control entities are all off", Name);
                OverrideActive = false;
                await UpdateAttributes();
            });
    }

    private void SubscribeManualTurnOnOverrideEvent()
    {
        _logger.LogDebug("{room} Subscribed to Manual Turn On Override Events", Name);
        ControlEntities
            .StateAllChanges()
            .Where(LightTurnedOnManually)
            .Subscribe(async e =>
            {
                _logger.LogInformation("{room} Manual Turn On Override by user", Name);
                OverrideActive = true;
                overrideSchedule?.Dispose();
                overrideSchedule = _scheduler.Schedule(DynamicTimeout, TurnOffEntities);
                await UpdateAttributes();
            });
    }

    private void SubscribeOverrideEvent()
    {
        _logger.LogDebug("{room} Subscribed to Attribute Override Events", Name);
        ControlEntities
            .StateAllChanges()
            .Where(LightAttributesOverride)
            .Subscribe(async e =>
            {
                _logger.LogInformation("{room} Attribute Override by user", Name);
                OverrideActive = true;
                if (CircadianSwitchEntity == null)
                {
                    await UpdateAttributes();
                    return;
                }

                _logger.LogInformation("{room} Turn off circadian switch", Name);
                CircadianSwitchEntity.TurnOff();
                await UpdateAttributes();
            });
    }

    private void SubscribePresenceOffEvent()
    {
        _logger.LogDebug("{room} Subscribed to Presence Off Events", Name);
        PresenceEntities!.Union(KeepAliveEntities)
                         .StateChanges()
                         .Where(e => e.New.IsOff())
                         .Throttle(_ => Observable.Timer(DynamicTimeout, _scheduler))
                         .Subscribe(async e =>
                         {
                             _logger.LogInformation("{room} No Motion", Name);
                             TurnOffEntities();
                             await UpdateAttributes();
                         });
    }

    private void SubscribePresenceOnEvent()
    {
        _logger.LogDebug("{room} Subscribed to Presence On Events", Name);
        PresenceEntities!.StateAllChanges()
                         .Where(e => e.New.IsOn())
                         .Subscribe(async e =>
                         {
                             _logger.LogInformation("{room} Motion", Name);
                             TurnOnEntities();
                             await UpdateAttributes();
                         });
    }

    private void TurnOffEntities()
    {
        if (ManagerEnabled.IsOff())
        {
            _logger.LogInformation("{room} Manager Disabled", Name);
            return;
        }

        if (IsOccupied)
        {
            _logger.LogInformation("{room} Cant turn off - Occupied", Name);
            return;
        }


        _logger.LogInformation("{room} Reset override", Name);
        OverrideActive = false;

        _logger.LogInformation("{room} Turn Off", Name);
        AllControlEntities
            .Where(w => w.IsOn()).ToList()
            .ForEach(e => e.TurnOff());
    }


    private void TurnOnEntities()
    {
        List<LightEntity> lightEntities;

        if (ManagerEnabled.IsOff())
        {
            _logger.LogInformation("{room} Manager Disabled", Name);
            return;
        }

        if (ConditionEntityStateNotMet)
        {
            _logger.LogInformation("{room} Condition not met {conditionEntity}!={state}", Name, ConditionEntity, ConditionEntityState);
            return;
        }

        if (IsTooBright)
        {
            _logger.LogInformation("{room} Too Bright", Name);
            return;
        }

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

    private async Task UpdateAttributes()
    {
        var attributes = new
        {
            OverrideActive,
            TurningOff = _scheduler.Now + DynamicTimeout,
            TimeoutParsed,
            IsOccupied,
            IsTooBright,
            ConditionEntityStateMet = ConditionEntity?.EntityId == null ? "N/A" : ConditionEntityStateNotMet.ToString(),
            ConditionEntity         = ConditionEntity?.EntityId ?? "N/A",
            ConditionEntityState    = ConditionEntityState ?? "N/A"
        };
        await _entityManager.SetAttributesAsync(_enabledSwitch, attributes);
    }
}