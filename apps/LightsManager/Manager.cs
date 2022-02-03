using System;
using System.Linq;
using Ha;
using Microsoft.Extensions.Logging;
using NetDaemon.Common.Reactive;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Common;

namespace LightManager;

public class Manager
{
    private readonly IHaContext             _app;
    private readonly ILogger<LightsManager> _logger;
    private readonly INetDaemonScheduler    _scheduler;
    private          DateTime               _expiry;
    private          IDisposable?           _timer;
    private readonly Services               _services;

    public Manager(IHaContext app, ILogger<LightsManager> logger, INetDaemonScheduler scheduler, LightsManagerConfig config)
    {
        _app       = app;
        _logger    = logger;
        _scheduler = scheduler;
        _services  = new Services(app);
        SetConfigurator(config);
        RegisterEventHandlers();
        InitState(config);
    }

    public Configurator Configurator { get; private set; }
    public bool HasActiveTimer => _timer != null;
    public ManagerState State { get; set; }

    private bool PresenceIsActive => HasActiveTimer || Configurator.ActivePresenceSensors.Any() && State != ManagerState.Override;

    public event EventHandler<EntityOverrideEventArgs> EntityOverride;
    public event EventHandler<ManagerGuardDogArgs> ManagerGuardDogPatrolling;
    public event EventHandler<ManagerStateEventArgs> ManagerStateChanged;
    public event EventHandler<ManagerTimerResetEventArgs> ManagerTimerReset;
    public event EventHandler<ManagerTimerSetEventArgs> ManagerTimerSet;

    private bool IncorrectRoomEvent(object sender)
    {
        return sender.ToString() != Configurator.RoomName;
    }

    private void InitState(LightsManagerConfig config)
    {
        if (_app.GetState(config.EnabledSwitchEntityId).State == "off")
        {
            ManagerStateChanged.Invoke(config.Name, new ManagerStateEventArgs("Initialize", ManagerState.Disabled));
            return;
        }

        _scheduler.RunEvery(TimeSpan.FromSeconds(config.GuardTimeout), () => { ManagerGuardDogPatrolling.Invoke(config.Name, new ManagerGuardDogArgs(Guid.NewGuid().ToString())); });
    }

    private void OnEnabledChanged(object sender, HassEventArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        _logger.LogInformation("{RoomName} | {CorrelationId} - Enabled Toggled: Enabled set to {Enabled}", sender, args.CorrelationId, Configurator.IsEnabled);
        switch (Configurator.IsEnabled)
        {
            case true:
                ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Idle));
                break;
            case false:
                ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Disabled));
                break;
        }
    }

    private void OnEntityOverride(object sender, EntityOverrideEventArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        _logger.LogInformation("{RoomName} | {CorrelationId} - Entity Override: {Entity} override to {State}", sender, args.CorrelationId, args.EntityId, args.NewState);

        if (args.NewState == "on" || Configurator.AllControlEntities.Any(e => e.State == "on"))
            ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Override));
        else
            ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Idle));
    }

    private void OnHouseModeChanged(object sender, HassEventArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        _logger.LogInformation("{RoomName} | {CorrelationId} - House Mode Changed: {State}", sender, args.CorrelationId, (string)args.EntityStates.New.State); //("{RoomName} | {CorrelationId} - House Mode Changed: {State}", sender, args.CorrelationId, args.EntityStates.New.State);
        if (!PresenceIsActive)
        {
            Configurator.Configure(_app); // Configure so entities are ready for next time
            _logger.LogInformation("{RoomName} | {CorrelationId} - Configurator Configured", sender, args.CorrelationId);
            return;
        }

        if (State == ManagerState.Disabled)
        {
            _logger.LogInformation("{RoomName} | {CorrelationId} - Manager Disabled", sender, args.CorrelationId);
            return;
        }

        TurnOffControlEntities(sender, args);
        Configurator.Configure(_app);
        TurnOnControlEntities(sender, args);
    }

    private void OnManagerGuardDogPatrols(object sender, ManagerGuardDogArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        if (Configurator.AllControlEntities.Any(e => e.State == "on") && State == ManagerState.Idle)
        {
            ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Override));
            ManagerTimerSet.Invoke(sender, new ManagerTimerSetEventArgs(args.CorrelationId, Configurator.TimeoutSeconds));
        }

        _logger.LogInformation("{RoomName} | {CorrelationId} - Guard Dog Patrolling", sender, args.CorrelationId);
    }

    private void OnManagerStateChanged(object sender, ManagerStateEventArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        var oldState = State;
        State = args.State;
        _logger.LogInformation("{RoomName} | {CorrelationId} - Manager State Changed: {State}", sender, args.CorrelationId, args.State);

        //if (oldState == State) return;
        switch (args.State)
        {
            case ManagerState.Idle:
                ManagerTimerReset.Invoke(sender, new ManagerTimerResetEventArgs(args.CorrelationId));
                TurnOffControlEntities(sender, args);
                break;
            case ManagerState.Active:
                ManagerTimerReset.Invoke(sender, new ManagerTimerResetEventArgs(args.CorrelationId));
                TurnOnControlEntities(sender, args);
                break;
            case ManagerState.Disabled:
                ManagerTimerReset.Invoke(sender, new ManagerTimerResetEventArgs(args.CorrelationId));
                break;
            case ManagerState.Override:
                ManagerTimerReset.Invoke(sender, new ManagerTimerResetEventArgs(args.CorrelationId));
                break;
        }

        SetManagerHaState(sender, args);
    }

    private void OnManagerTimerReset(object sender, ManagerTimerResetEventArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        _timer?.Dispose();
        _timer  = null;
        _expiry = DateTime.MinValue;
        _logger.LogInformation("{RoomName} | {CorrelationId} - Timer Reset", sender, args.CorrelationId);
    }

    private void OnManagerTimerSet(object sender, ManagerTimerSetEventArgs args)
    {
        _timer  = _scheduler.RunIn(args.TimeoutSeconds, () => ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Idle)));
        _expiry = DateTime.Now + args.TimeoutSeconds;
        _logger.LogInformation("{RoomName} | {CorrelationId} - Timer Set: Timeout of {Timeout} expiring at {Expiry}", sender, args.CorrelationId, args.TimeoutSeconds, _expiry);
    }


    private void OnManualEntityStateChange(object sender, HassEventArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        var newState = $"{args.EntityStates.New.State}";
        _logger.LogInformation("{RoomName} | {CorrelationId} - Manual State Change: {Entity} changed to {State}", sender, args.CorrelationId, args.EntityId, newState);
        if (State == ManagerState.Disabled)
        {
            _logger.LogInformation("{RoomName} | {CorrelationId} - Manager Disabled", sender, args.CorrelationId);
            return;
        }

        EntityOverride.Invoke(sender, new EntityOverrideEventArgs(args.CorrelationId, newState) { EntityId = args.EntityId });
    }

    private void OnPresenceStarted(object sender, HassEventArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        _logger.LogInformation("{RoomName} | {CorrelationId} - Presence Started", sender, args.CorrelationId);
        if (Configurator.LuxAboveLimit)
        {
            _logger.LogInformation("{RoomName} | {CorrelationId} - Lux Above Limit", sender, args.CorrelationId);
            return;
        }

        switch (State)
        {
            case ManagerState.Disabled:
                _logger.LogInformation("{RoomName} | {CorrelationId} - Manager Disabled", sender, args.CorrelationId);
                return;
            case ManagerState.Override:
                ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Override));
                break;
            case ManagerState.Idle:
            case ManagerState.Active:
                ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Active));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnPresenceStopped(object sender, HassEventArgs args)
    {
        if (IncorrectRoomEvent(sender)) return;
        _logger.LogInformation("{RoomName} | {CorrelationId} - Presence Stopped", sender, args.CorrelationId);
        if (State == ManagerState.Disabled)
        {
            _logger.LogInformation("{RoomName} | {CorrelationId} - Manager Disabled", sender, args.CorrelationId);
            return;
        }

        if (PresenceIsActive)
        {
            ManagerTimerReset.Invoke(sender, new ManagerTimerResetEventArgs(args.CorrelationId));
            _logger.LogInformation("{RoomName} | {CorrelationId} - No timer set: Active sensors {Sensors}", sender, args.CorrelationId, Configurator.ActivePresenceSensors.ToEntityIdsString());
            return;
        }

        ManagerTimerSet.Invoke(sender, new ManagerTimerSetEventArgs(args.CorrelationId, Configurator.TimeoutSeconds));
    }

    private void RegisterEventHandlers()
    {
        ManagerGuardDogPatrolling += OnManagerGuardDogPatrols;
        ManagerStateChanged       += OnManagerStateChanged;
        ManagerTimerReset         += OnManagerTimerReset;
        ManagerTimerSet           += OnManagerTimerSet;
        EntityOverride            += OnEntityOverride;

        Subscriptions.PresenceStarted         += OnPresenceStarted;
        Subscriptions.PresenceStopped         += OnPresenceStopped;
        Subscriptions.HouseModeChanged        += OnHouseModeChanged;
        Subscriptions.ManagerEnabledChanged   += OnEnabledChanged;
        Subscriptions.ManualEntityStateChange += OnManualEntityStateChange;
        Subscriptions.Setup(_app, _logger, Configurator);
        _logger.LogInformation("Event Handlers Registered");
    }

    private void SetConfigurator(LightsManagerConfig config)
    {
        Configurator = new Configurator(config);
        Configurator.Configure(_app);
        _logger.LogInformation("Configurator Configured");
    }

    private void SetManagerHaState(object sender, ManagerStateEventArgs args)
    {
        var stateString = args.State.ToString();
        var attrs = new
        {
            State                 = stateString,
            EventEntity           = args.EntityId,
            ActivePresenceSensors = Configurator.ActivePresenceSensors.ToEntityIds(),
            PresenceEntityIds     = Configurator.PresenceSensors.ToEntityIds(),
            ControlEntityIds      = Configurator.AllControlEntities.ToEntityIds(),
            Expiry                = _expiry != DateTime.MinValue ? _expiry.ToString("T") : ""
        };

        _services.Netdaemon.EntityUpdate(Configurator.Enabled.EntityId, Configurator.Enabled.State, attributes: attrs);
        _logger.LogInformation("{RoomName} | {CorrelationId} - HA State Set", sender, args.CorrelationId);
    }

    private void TurnOffControlEntities(object sender, HassEventArgs args)
    {
        var controlEntities = Configurator.AllControlEntities;
        controlEntities.ForEach(e => e.TurnOff());
        _logger.LogInformation("{RoomName} | {CorrelationId} - Control Entities Turned Off: {Entities}", sender, args.CorrelationId, controlEntities.ToEntityIdsString());
    }

    private void TurnOnControlEntities(object sender, HassEventArgs args)
    {
        var controlEntities = Configurator.Lights.Union(Configurator.Switches).ToList();
        controlEntities.ForEach(e => e.TurnOn());
        _logger.LogInformation("{RoomName} | {CorrelationId} - Control Entities Turned On: {Entities}", sender, args.CorrelationId, controlEntities.ToEntityIdsString());
    }
}