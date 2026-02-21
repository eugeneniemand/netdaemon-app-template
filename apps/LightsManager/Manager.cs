using System.Reactive.Disposables;
using NetDaemon.Extensions.MqttEntityManager;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace LightManagerV2;

public class Manager
{
    private readonly List<string> _onStates = ["on", "playing"];
    private string _enabledSwitch;
    private IMqttEntityManager _entityManager;
    private int _guardTimeout;
    private IHaContext _haContext;
    private ILogger<LightsManager> _logger;
    private string _ndUserId;
    private bool _overrideActive;
    private IScheduler _scheduler;
    private IServices _services;
    private IDisposable _overrideSchedule = Disposable.Empty;

    public bool Debug { get; }
    public bool IsAnyControlEntityOn => AllControlEntities.Any(e => e.IsOn());
    private bool AllControlEntitiesAreOff => AllControlEntities.All(e => e.IsOff());
    private bool IsNightMode => NightTimeEntity != null && NightTimeEntityStates.Contains(NightTimeEntity.State!);
    private bool IsOccupied => PresenceEntities.Union(KeepAliveEntities).Any(entity => entity.IsOn() || _onStates.Contains(entity.State!));
    private bool IsTooBright => LuxEntity != null && (LuxLimitEntity != null ? (LuxEntity.State ?? 0d) >= LuxLimitEntity.State : (LuxEntity.State ?? 0d) >= LuxLimit);
    public bool Watchdog { get; set; } = true;
    public Entity? ConditionEntity { get; set; }
    public InputSelectEntity? NightTimeEntity { get; init; }
    public int NightTimeout { get; init; }
    public int OverrideTimeout { get; init; } 
    public int Timeout { get; init; }
    public int? LuxLimit { get; set; }
    public int? DefaultBrightness { get; set; }
    public List<BinarySensorEntity> KeepAliveEntities { get; init; } = [];
    public List<BinarySensorEntity> PresenceEntities { get; init; } = [];
    private IEnumerable<LightEntity> AllControlEntities => ControlEntities.Union(NightControlEntities).Union(MonitorEntities).ToList();
    public List<LightEntity> ControlEntities { get; init; } = [];
    private List<LightEntity> MonitorEntities { get; } = [];
    public List<LightEntity> NightControlEntities { get; init; } = [];
    public List<string> NightTimeEntityStates { get; init; } = [];
    public List<string> RandomStates { get; } = [];
    public NumericSensorEntity? LuxEntity { get; set; }
    public NumericSensorEntity? LuxLimitEntity { get; set; }
    public string Name { get; init; } = null!;
    public string RoomState { get; set; }
    public string? ConditionEntityState { get; set; }
    public SwitchEntity ManagerEnabled { get; set; }
    public SwitchEntity? CircadianSwitchEntity { get; set; }
    private TimeSpan DynamicTimeout => TimeSpan.FromSeconds(_overrideActive ? OverrideTimeoutParsed : TimeoutParsed);
    private bool ConditionEntityStateNotMet => ConditionEntity != null && ConditionEntityState != null && ConditionEntity.State != ConditionEntityState;
    private int NightTimeoutParsed => NightTimeout == 0 ? 90 : NightTimeout;
    private int OverrideTimeoutParsed => OverrideTimeout == 0 ? 1800 : OverrideTimeout;
    private int TimeoutParsed => IsNightMode ? NightTimeoutParsed : Timeout;
    private int DynamicBrightness => IsNightMode ? 2 : DefaultBrightness ?? 100;


    private List<Task> Tasks { get; } = [];

    public async Task Init(ILogger<LightsManager> logger, string ndUserId, IScheduler scheduler, IHaContext haContext, IServices services, IMqttEntityManager entityManager, int guardTimeout)
    {
        _logger = logger;
        _ndUserId = ndUserId;
        _scheduler = scheduler;
        _haContext = haContext;
        _entityManager = entityManager;
        _enabledSwitch = $"switch.light_manager_{Name.ToLower()}";
        _guardTimeout = guardTimeout;
        _services = services;
        _logger.LogInformation("Setup {room}", Name);
        await SetupEnabledSwitch();
        SubscribePresenceOnEvent();
        SubscribePresenceOffEvent();
        SubscribeOverrideEvent();
        //SubscribeManualTurnOnOverrideEvent();
        SubscribeManualTurnOffOverrideEvent();
        SubscribeHouseModeEvent();
        SubscribeTurnOnEvent();
        SubscribeGuard();
    }

    private bool IsNdUserOrHa(StateChange stateChange) => stateChange.New?.Context?.UserId == null || stateChange.New?.Context?.UserId == _ndUserId;

    private bool LightAttributesOverride(StateChange<LightEntity, EntityState<LightAttributes>> e) =>
        !IsNdUserOrHa(e) &&
        e.Old.IsOn() && e.New.IsOn()
        && (!Equals(e.Old?.Attributes?.Brightness, e.New?.Attributes?.Brightness)
             || !Equals(e.Old?.Attributes?.ColorTempKelvin, e.New?.Attributes?.ColorTempKelvin));

    private bool LightTurnedOffManually(StateChange<LightEntity, EntityState<LightAttributes>> e) =>
        !IsNdUserOrHa(e) &&
        e.Old.IsOn() && e.New.IsOff();

    private bool LightTurnedOnManually(StateChange<LightEntity, EntityState<LightAttributes>> e) =>
        !IsNdUserOrHa(e) &&
        e.Old.IsOff() && e.New.IsOn();

    //private bool LightTurnedOnNd(StateChange<LightEntity, EntityState<LightAttributes>> e) =>
    //    IsNdUserOrHa(e) &&
    //    e.Old.IsOff() && e.New.IsOn();

    private void ResetOverride()
    {
        _logger.LogDebug("{room} Reset Override", Name);
        _overrideSchedule.Dispose();
        _overrideActive = true;
        _overrideSchedule = _scheduler.Schedule(DynamicTimeout, _ =>
        {
            _logger.LogDebug("{room} Override Timeout", Name);
            _overrideActive = false;
            TurnOffEntities($"Override ({Name})");
            WaitAllTasks();
        });
        UpdateAttributes(true);
    }

    private async Task SetupEnabledSwitch()
    {
        _logger.LogDebug("{room} Setup Enabled Switch", Name);
        if (_haContext.Entity(_enabledSwitch).State == null || string.Equals(_haContext.Entity(_enabledSwitch).State, "unavailable", StringComparison.InvariantCultureIgnoreCase))
            await _entityManager.CreateAsync(_enabledSwitch, new EntityCreationOptions(Name: $"Light Manager {Name}", DeviceClass: "switch", Persist: true));

        ManagerEnabled = new SwitchEntity(_haContext, _enabledSwitch);
        ManagerEnabled.TurnOn();

        if (_enabledSwitch != "switch.light_manager_testroom")
            (await _entityManager.PrepareCommandSubscriptionAsync(_enabledSwitch)).SubscribeAsync(async s =>
                {
                    _logger.LogDebug("{room} Changing Enabled Switch", Name);
                    await _entityManager.SetStateAsync(_enabledSwitch, s);
                    _logger.LogDebug("{room} Enabled Switch Set to {state}", Name, s);
                }
            );
    }

    private void SubscribeGuard()
    {
        if (!Watchdog)
        {
            _logger.LogDebug("{room} Watchdog Disabled", Name);

            return;
        }

        _logger.LogDebug("{room} Subscribed to Watchdog Schedule", Name);

        var totalMinutes = (int)TimeSpan.FromSeconds(_guardTimeout).TotalMinutes;

        _scheduler.ScheduleCron($"*/{totalMinutes} * * * *", () =>
        {
            if (RoomState == "on" || _overrideActive || AllControlEntities.All(e => e.IsOff()) || _haContext.Entity("switch.wiser_away_mode").IsOn()) return;
            _logger.LogDebug("{room} Watchdog turning off entities", Name);
            TurnOffEntities($"Watchdog ({Name})");
            WaitAllTasks();
        });
    }

    private void SubscribeHouseModeEvent()
    {
        _logger.LogDebug("{room} Subscribed to House Mode Changed Events", Name);
        NightTimeEntity?.StateChanges().Subscribe(_ =>
        {
            _logger.LogDebug("{room} House Mode Changed", Name);

            if (AllControlEntitiesAreOff)
            {
                UpdateAttributes();
                WaitAllTasks();
                return;
            }

            _logger.LogDebug("{room} Control Entities On: {entities}", Name, AllControlEntities.Select(e => e.EntityId));
            var controlEntities = GetControlEntities();
            foreach (var entity in AllControlEntities.Except(controlEntities).Where(e => e.IsOn()))
            {
                entity.TurnOff();
            }

            _scheduler.Sleep(TimeSpan.FromMilliseconds(250)).GetAwaiter().GetResult();

            //TurnOffEntities("House Mode Change", true);
            foreach (var entity in controlEntities)
            {
                entity.TurnOn(new LightTurnOnParameters() { BrightnessPct = DynamicBrightness, ColorTempKelvin = IsNightMode ? entity.Attributes?.MinColorTempKelvin : entity.Attributes?.MaxColorTempKelvin ?? 5000 });
            }

            //TurnOnEntities("House Mode Change", true);
            UpdateAttributes();

            WaitAllTasks();
        });
    }

    private void SubscribeManualTurnOffOverrideEvent()
    {
        _logger.LogDebug("{room} Subscribed to Manual Turn Off Override Events", Name);
        AllControlEntities
            .StateAllChanges()
            .Where(LightTurnedOffManually)
            .Subscribe(_ =>
            {
                _logger.LogDebug("{room} Manual Turn Off Override by user", Name);
                if (AllControlEntities.Any(e => e.IsOn()))
                {
                    _logger.LogDebug("{room} Override active as some control entities are on", Name);
                    return;
                }

                _logger.LogDebug("{room} Override reset as all control entities are off", Name);
                _overrideActive = false;
                _overrideSchedule.Dispose();
                UpdateAttributes();

                WaitAllTasks();
            });
    }

    //private void SubscribeManualTurnOnOverrideEvent()
    //{
    //    _logger.LogDebug("{room} Subscribed to Manual Turn On Override Events", Name);
    //    AllControlEntities
    //        .StateAllChanges()
    //        .Where(LightTurnedOnManually)
    //        .Subscribe(e =>
    //        {
    //            _logger.LogDebug("{room} Manual Turn On Override for {light} by user", Name, e.New?.EntityId);
    //            LogInLogbook(e.New?.EntityId ?? "UNKNOWN", "Override Triggered");
    //            ResetOverride();                
    //            TurnOnLightWithColorAndBrightness(e.Entity);
    //            UpdateAttributes(true);

    //            WaitAllTasks();
    //        });
    //}

    private void SubscribeTurnOnEvent()
    {
        // _logger.LogDebug("{room} Subscribed to Manual Turn On Override Events", Name);
        // AllControlEntities
        //     .StateAllChanges()
        //     .Where(LightTurnedOnNd)
        //     .Subscribe(e =>
        //     {
        //         _logger.LogDebug("{room} Nd Turn On for {light}", Name, e.New?.EntityId);
        //         LogInLogbook(e.New?.EntityId ?? "UNKNOWN", "Override Triggered");
        //         
        //         e.Entity.TurnOn( new LightTurnOnParameters() { BrightnessPct = IsNightMode ? 1 : 100, ColorTemp = IsNightMode ? 550 : 100});
        //         
        //         UpdateAttributes(true);
        //         WaitAllTasks();
        //     });
    }

    private void SubscribeOverrideEvent()
    {
        _logger.LogDebug("{room} Subscribed to Attribute Override Events", Name);
        AllControlEntities
            .StateAllChanges()
            .Where(e => LightTurnedOnManually(e) || LightAttributesOverride(e))
            .Buffer(TimeSpan.FromMilliseconds(100), _scheduler) // Buffer events in a 100ms window
            .Where(buffer => buffer.Any()) // Ignore empty buffers
            .Select(buffer =>
            {
                // If there's more than one event, take the last one (it has brightness/color)
                // If there's only one event, take it (it might be the only "on" event)
                Console.WriteLine($"Buffer size: {buffer.Count}");
                var chosenEvent = buffer.Last();
                Console.WriteLine($"Chosen event: {chosenEvent.New?.State}, Brightness: {chosenEvent.New?.Attributes?.Brightness}");
                return buffer.Last();
            })
            .Subscribe(e =>
            {
                _logger.LogDebug("OLD:" + JsonSerializer.Serialize(e.Old));
                _logger.LogDebug("NEW:" + JsonSerializer.Serialize(e.New));
                _logger.LogDebug("{room} Attribute Override by user", Name);
                LogInLogbook(e.New?.EntityId ?? "UNKNOWN", "Override Triggered");

                if (LightAttributesOverride(e))
                    LogInLogbook(e.New?.EntityId ?? "UNKNOWN", "Override attributes supplied");
                else
                    TurnOnLightWithColorAndBrightness(e.Entity);

                ResetOverride();
            });
    }

    private void SubscribePresenceOffEvent()
    {
        _logger.LogDebug("{room} Subscribed to Presence Off Events", Name);
        PresenceEntities.Union(KeepAliveEntities)
                        .StateChanges()
                        .Throttle(_ => Observable.Timer(DynamicTimeout, _scheduler))
                        .Where(e => e.New.IsOff())
                        .Subscribe(e =>
                        {
                            _logger.LogDebug("{room} No Motion Timeout '{entity}'", Name, e.New?.EntityId);
                            if (_overrideActive)
                            {
                                _logger.LogDebug("{room} Not turning off - Override active ", Name);
                                return;
                            }

                            TurnOffEntities(e.New?.EntityId);
                            UpdateAttributes(true);

                            WaitAllTasks();
                        });

        PresenceEntities.Union(KeepAliveEntities)
                        .StateChanges()
                        .Where(e => e.New.IsOff())
                        .Subscribe(e =>
                        {
                            _logger.LogDebug("{room} No Motion '{entity}'", Name, e.New?.EntityId);
                            UpdateAttributes(true);

                            WaitAllTasks();
                        });
    }

    private void SubscribePresenceOnEvent()
    {
        _logger.LogDebug("{room} Subscribed to Presence On Events", Name);
        PresenceEntities.StateAllChanges()
                        .Where(e => e.New.IsOn())
                        .Subscribe(e =>
                        {
                            _logger.LogDebug("{room} Motion '{entity}'", Name, e.New?.EntityId);

                            if (_overrideActive)
                            {
                                _logger.LogDebug("{room} Not turning on - resetting Override timeout", Name);
                                ResetOverride();
                                WaitAllTasks();
                                return;
                            }

                            TurnOnEntities(e.New?.EntityId);
                            UpdateAttributes();

                            WaitAllTasks();
                        });
    }

    private void TurnOffEntities(string? trigger, bool ignoreConditions = false)
    {
        if (ManagerEnabled.IsOff())
        {
            _logger.LogInformation("{room} Manager Disabled", Name);
            return;
        }

        if (!ignoreConditions && IsOccupied)
        {
            _logger.LogDebug("{room} Cant turn off - Occupied", Name);
            return;
        }

        //if (!ignoreConditions && ConditionEntityStateNotMet)
        //{
        //    _logger.LogDebug("{room} Cant turn off - Condition not met {conditionEntity}!={state}", Name, ConditionEntity?.EntityId, ConditionEntityState);
        //    return;
        //}

        var triggerMsg = $"Turned off by {trigger ?? "UNKNOWN"}";
        _logger.LogDebug("{room} Turn Off by {trigger}", Name, triggerMsg);

        foreach (var e in AllControlEntities.ToList())
        {
            _logger.LogDebug("{room} Turning Off {light} ", Name, e.EntityId);
            e.TurnOff();
            LogInLogbook(e, triggerMsg);
        }

        if (CircadianSwitchEntity != null && CircadianSwitchEntity.IsOff())
        {
            _logger.LogDebug("{room} Turn On Circadian Switch", Name);
            CircadianSwitchEntity.TurnOn();
        }

        RoomState = "off";
    }

    private void LogInLogbook(Entity entity, string triggerMsg)
    {
        Tasks.Add(Task.Run(() =>
            _services.Logbook.Log(
                entityId: entity.EntityId,
                message: triggerMsg,
                name: entity.EntityId,
                domain: "light")
        ));
    }

    private void LogInLogbook(string entityId, string triggerMsg)
    {
        Tasks.Add(Task.Run(() =>
            _services.Logbook.Log(
                entityId: entityId,
                message: triggerMsg,
                name: entityId,
                domain: "light")
        ));
    }


    private void TurnOnEntities(string? trigger, bool ignoreConditions = false)
    {
        if (ManagerEnabled.IsOff())
        {
            _logger.LogInformation("{room} Manager Disabled", Name);
            return;
        }

        if (!ignoreConditions && ConditionEntityStateNotMet)
        {
            _logger.LogInformation("{room} Condition not met {conditionEntity}!={state}", Name, ConditionEntity?.EntityId, ConditionEntityState);
            return;
        }

        if (!ignoreConditions && IsTooBright)
        {
            _logger.LogDebug("{room} Too Bright", Name);
            return;
        }

        var controlEntities = GetControlEntities();

        if (CircadianSwitchEntity != null && CircadianSwitchEntity.IsOff())
        {
            _logger.LogDebug("{room} Turn On Circadian Switch", Name);
            CircadianSwitchEntity.TurnOn();
        }

        foreach (var e in controlEntities.Where(l => l.IsOff()))
        {
            _logger.LogDebug("{room} Turning On {light}", Name, e.EntityId);
            TurnOnLightWithColorAndBrightness(e);

            LogInLogbook(e, $"Turned on by {trigger ?? "UNKNOWN"}");
        }

        RoomState = "on";
    }

    private void TurnOnLightWithColorAndBrightness(LightEntity? e)
    {
        if (e.Attributes != null && e.Attributes.SupportedColorModes != null)
        {
            if (e.Attributes.SupportedColorModes.Contains("color_temp"))
                e.TurnOn(new LightTurnOnParameters()
                {
                    BrightnessPct = DynamicBrightness,
                    ColorTempKelvin = IsNightMode ? e.Attributes.MinColorTempKelvin : e.Attributes.MaxColorTempKelvin,
                    Transition = 0
                });
            else if (e.Attributes.SupportedColorModes.Contains("brightness"))
                e.TurnOn(new LightTurnOnParameters()
                {
                    BrightnessPct = DynamicBrightness
                });
            else if (e.Attributes.SupportedColorModes.Contains("onoff"))

            {
                e.TurnOn();                
            }
            else
                _logger.LogWarning("{room} Unsupported Attributes when tunring on {light}: {colorModes}", Name, e.EntityId, e.Attributes.SupportedColorModes);
        }
        else
            e.TurnOn();
    }


    private List<LightEntity> GetControlEntities()
    {
        List<LightEntity> lightEntities;
        if (IsNightMode)
        {
            _logger.LogDebug("{room} Turn On Night Control Entities", Name);
            lightEntities = NightControlEntities.ToList();
        }
        else
        {
            _logger.LogDebug("{room} Turn On Control Entities", Name);
            lightEntities = ControlEntities.ToList();
        }

        return lightEntities;
    }

    private void UpdateAttributes(bool showTurningOff = false)
    {
        //_logger.LogDebug("{room} Updating Attributes", Name);

        var attributes = showTurningOff
            ? new
            {
                OverrideActive = _overrideActive,
                TurningOff = (_scheduler.Now + DynamicTimeout).ToString(),
                DynamicTimeout,
                IsOccupied,
                IsTooBright,
                ConditionEntityStateMet = ConditionEntity?.EntityId == null ? "N/A" : ConditionEntityStateNotMet.ToString(),
                ConditionEntity = ConditionEntity?.EntityId ?? "N/A",
                ConditionEntityState = ConditionEntityState ?? "N/A",
                LastUpdated = DateTime.Now.ToString("G")
            }
            : new
            {
                OverrideActive = _overrideActive,
                TurningOff = "Unknown",
                DynamicTimeout,
                IsOccupied,
                IsTooBright,
                ConditionEntityStateMet = ConditionEntity?.EntityId == null ? "N/A" : ConditionEntityStateNotMet.ToString(),
                ConditionEntity = ConditionEntity?.EntityId ?? "N/A",
                ConditionEntityState = ConditionEntityState ?? "N/A",
                LastUpdated = DateTime.Now.ToString("G")
            };
        Tasks.Add(_entityManager.SetAttributesAsync(_enabledSwitch, attributes));
        _logger.LogTrace("{room} Attributes updated to {attr}", Name, attributes);
    }

    private void WaitAllTasks()
    {
        Task.WaitAll(Tasks.ToArray());
    }
}