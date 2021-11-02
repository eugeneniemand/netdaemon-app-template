using System;
using System.Linq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;

namespace LightsManager
{
    public class Manager
    {
        public event EventHandler<ManagerStateEventArgs> ManagerStateIsIdle;
        public event EventHandler<ManagerStateEventArgs> ManagerStateIsActive;
        public event EventHandler<ManagerStateEventArgs> ManagerStateIsDisabled;
        public event EventHandler<ManagerResetTimerEventArgs> ManagerResetTimer;
        public event EventHandler<ManagerSetTimerEventArgs> ManagerSetTimer;
        public event EventHandler<EntityOverrideEventArgs> EntityOverride;

        private readonly INetDaemonRxApp _app;
        private          IDisposable?    _timer;
        private          DateTime        _expiry;


        public ManagerState State { get; set; }
        public Configurator Configurator { get; private set; }

        private bool PresenceIsActive => _timer != null || Configurator.ActivePresenceSensors.Any();

        public Manager(INetDaemonRxApp app, LightsManagerConfig config)
        {
            _app = app;
            SetConfigurator(config);
            SetupEnabledSwitchEntity();
            RegisterEventHandlers();
        }

        private void SetConfigurator(LightsManagerConfig config)
        {
            Configurator = new Configurator(config);
            Configurator.Configure(_app);
        }

        private void RegisterEventHandlers()
        {
            ManagerStateIsIdle += OnManagerStateIsIdle;
            ManagerStateIsIdle += OnManagerStateChanged;

            ManagerStateIsActive += OnManagerStateIsActive;
            ManagerStateIsActive += OnManagerStateChanged;

            ManagerStateIsDisabled += OnManagerStateChanged;

            ManagerResetTimer += OnManagerResetTimer;
            ManagerSetTimer   += OnManagerSetTimer;

            EntityOverride += OnEntityEntityOverride;

            Subscriptions.PresenceStartedHandler       += OnPresenceStarted;
            Subscriptions.PresenceStoppedHandler       += OnPresenceStopped;
            Subscriptions.HouseModeChangedHandler      += OnHouseModeChanged;
            Subscriptions.ManagerEnabledChangedHandler += OnEnabledChanged;
            Subscriptions.ManualEntityStateChange      += OnManualEntityStateChange;
            Subscriptions.Setup(_app, Configurator);
        }

        private void SetupEnabledSwitchEntity()
        {
            var switchEntity = _app.States.FirstOrDefault(e => e.EntityId == Configurator.Enabled.EntityId);

            if (switchEntity != null) return;

            _app.SetState(Configurator.Enabled.EntityId, "on", null);
        }

        private void OnManualEntityStateChange(object? sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            EntityOverride.Invoke(sender, new EntityOverrideEventArgs(Configurator.RoomName, $"{args?.State.New?.State}"));
        }

        private void OnEntityEntityOverride(object? sender, EntityOverrideEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            switch (args.NewState)
            {
                case null:
                    return;
                case "on":
                    ManagerSetTimer.Invoke(sender, new ManagerSetTimerEventArgs(Configurator.RoomName, args.CorrelationId, TimeSpan.FromSeconds(900)));
                    ManagerStateIsActive.Invoke(sender, new ManagerStateEventArgs(Configurator.RoomName, ManagerState.Override));
                    break;
                case "off":
                    ManagerStateIsIdle.Invoke(sender, new ManagerStateEventArgs(Configurator.RoomName, ManagerState.Idle));
                    break;
            }
        }


        private void OnEnabledChanged(object? sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            switch (Configurator.IsEnabled)
            {
                case true:
                    ManagerStateIsActive.Invoke(sender, new ManagerStateEventArgs(Configurator.RoomName, ManagerState.Idle));
                    break;
                case false:
                    ManagerResetTimer.Invoke(sender, new ManagerResetTimerEventArgs(sender.ToString(), args.CorrelationId));
                    ManagerStateIsDisabled.Invoke(sender, new ManagerStateEventArgs(Configurator.RoomName, ManagerState.Disabled));
                    break;
            }
        }

        private void OnManagerResetTimer(object sender, ManagerResetTimerEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            _timer?.Dispose();
            _timer  = null;
            _expiry = DateTime.MinValue;
        }

        private void OnManagerStateIsIdle(object sender, ManagerStateEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            ManagerResetTimer.Invoke(sender, new ManagerResetTimerEventArgs(sender.ToString(), args.CorrelationId));
            TurnOffControlEntities();
        }

        private void OnManagerStateIsActive(object sender, ManagerStateEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            ManagerResetTimer.Invoke(sender, new ManagerResetTimerEventArgs(sender.ToString(), args.CorrelationId));
            TurnOnControlEntities();
        }

        private void TurnOffControlEntities()
        {
            Configurator.Lights.ForEach(e => e.TurnOff());
            Configurator.Switches.ForEach(e => e.TurnOff());
        }

        private void TurnOnControlEntities()
        {
            Configurator.Lights.ForEach(e => e.TurnOn());
            Configurator.Switches.ForEach(e => e.TurnOn());
        }

        public void OnPresenceStarted(object sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            if (State == ManagerState.Disabled) _app.LogInformation("{CorrelationId} - Manager Disabled: {RoomName}", args.CorrelationId, args.RoomName);
            if (State != ManagerState.Idle || Configurator.LuxAboveLimit) return;
            ManagerStateIsActive.Invoke(sender, new ManagerStateEventArgs(Configurator.RoomName, ManagerState.Active));
        }

        private bool IncorrectRoomEvent(object? sender)
        {
            return sender?.ToString() != Configurator.RoomName;
        }

        public void OnPresenceStopped(object sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            ManagerSetTimer.Invoke(sender, new ManagerSetTimerEventArgs(sender.ToString(), args.CorrelationId, Configurator.TimeoutSeconds));
        }

        public void OnManagerSetTimer(object sender, ManagerSetTimerEventArgs args)
        {
            if (PresenceIsActive)
            {
                _app.LogInformation(
                    "{CorrelationId} - Presence Active: {RoomName} no timer set. Active sensors {Sensors}",
                    args.CorrelationId, args.RoomName,
                    string.Join(", ", Configurator.ActivePresenceSensors.Select(p => p.EntityId)));
                return;
            }

            _timer  = _app.RunIn(args.TimeoutSeconds, () => ManagerStateIsIdle.Invoke(sender, new ManagerStateEventArgs(Configurator.RoomName, ManagerState.Idle)));
            _expiry = DateTime.Now + args.TimeoutSeconds;
            _app.LogInformation("{CorrelationId} - Timer set: {RoomName} for {Timeout} at {Expiry}", args.CorrelationId, args.RoomName, args.TimeoutSeconds, _expiry);
        }

        public void OnHouseModeChanged(object sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            if (!PresenceIsActive)
            {
                Configurator.Configure(_app); // Configure so entities are ready for next time
                return;
            }

            TurnOffControlEntities();
            Configurator.Configure(_app);
            TurnOnControlEntities();
        }

        public void OnManagerStateChanged(object sender, ManagerStateEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            State = args.State;

            var stateString = args.State.ToString();
            var attrs = new
            {
                State                 = stateString,
                EventEntity           = args.EntityId,
                ActivePresenceSensors = Configurator.ActivePresenceSensors.Select(e => e.EntityId),
                PresenceEntityIds     = Configurator.PresenceSensors.Select(e => e.EntityId),
                ControlEntityIds      = Configurator.Lights.Union(Configurator.Switches).Select(e => e.EntityId),
                Expiry                = _expiry != DateTime.MinValue ? _expiry.ToString("T") : ""
            };

            _app.SetState(Configurator.Enabled.EntityId, Configurator.Enabled.State, attrs);
            _app.LogInformation("{CorrelationId} - Event Handled: {EventType} for {RoomName} to State {state}", args.CorrelationId, args.EventType, args.RoomName, args.State);
        }
    }

    public enum ManagerState
    {
        Idle,
        Active,
        Disabled,
        Override
    }
}