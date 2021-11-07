using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NetDaemon.Common.Reactive;

namespace LightsManager
{
    public class Manager
    {
        private readonly INetDaemonRxApp _app;
        private          DateTime        _expiry;
        private          IDisposable?    _timer;
        public bool HasActiveTimer => _timer != null;

        public Manager(INetDaemonRxApp app, LightsManagerConfig config)
        {
            _app = app;
            SetConfigurator(config);
            RegisterEventHandlers();
            InitState(config);
        }

        public Configurator Configurator { get; private set; }
        public ManagerState State { get; set; }

        private bool PresenceIsActive => HasActiveTimer || Configurator.ActivePresenceSensors.Any() && State != ManagerState.Override;

        public event EventHandler<EntityOverrideEventArgs> EntityOverride;
        public event EventHandler<ManagerStateEventArgs> ManagerStateChanged;
        public event EventHandler<ManagerTimerResetEventArgs> ManagerTimerReset;
        public event EventHandler<ManagerTimerSetEventArgs> ManagerTimerSet;

        private bool IncorrectRoomEvent(object sender)
        {
            return sender.ToString() != Configurator.RoomName;
        }

        private void InitState(LightsManagerConfig config)
        {
            if (_app.EntityState(config.EnabledSwitchEntityId) == "off")
            {
                ManagerStateChanged.Invoke(config.Name, new ManagerStateEventArgs("Initialize", ManagerState.Disabled));
                return;
            }

            if (Configurator.AllControlEntities.Select(e => e.EntityId).Any(entityId => _app.EntityState(entityId) == "on"))
                ManagerStateChanged.Invoke(config.Name, new ManagerStateEventArgs("Initialize", ManagerState.Active));
        }

        private void OnEnabledChanged(object sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            _app.LogInformation("{RoomName} | {CorrelationId} - Enabled Toggled: Enabled set to {Enabled}", sender, args.CorrelationId, Configurator.IsEnabled);
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
            _app.LogInformation("{RoomName} | {CorrelationId} - Entity Override: {Entity} override to {State}", sender, args.CorrelationId, args.EntityId, args.NewState);

            if (args.NewState == "on" || Configurator.AllControlEntities.Any(e => e.State == "on"))
                ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Override));
            else
                ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Idle));
        }

        private void OnHouseModeChanged(object sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            _app.LogInformation("{RoomName} | {CorrelationId} - House Mode Changed: {State}", sender, args.CorrelationId, args.EntityStates.New.State);
            if (!PresenceIsActive)
            {
                Configurator.Configure(_app); // Configure so entities are ready for next time
                _app.LogInformation("{RoomName} | {CorrelationId} - Configurator Configured", sender, args.CorrelationId);
                return;
            }

            if (State == ManagerState.Disabled)
            {
                _app.LogInformation("{RoomName} | {CorrelationId} - Manager Disabled", sender, args.CorrelationId);
                return;
            }

            TurnOffControlEntities(sender, args);
            Configurator.Configure(_app);
            TurnOnControlEntities(sender, args);
        }

        private void OnManagerStateChanged(object sender, ManagerStateEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            var oldState = State;
            State = args.State;
            _app.LogInformation("{RoomName} | {CorrelationId} - Manager State Changed: {State}", sender, args.CorrelationId, args.State);

            if (oldState == State) return;
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
                    ManagerTimerSet.Invoke(sender, new ManagerTimerSetEventArgs(args.CorrelationId, TimeSpan.FromSeconds(900)));
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
            _app.LogInformation("{RoomName} | {CorrelationId} - Timer Reset", sender, args.CorrelationId);
        }

        private void OnManagerTimerSet(object sender, ManagerTimerSetEventArgs args)
        {
            _timer  = _app.RunIn(args.TimeoutSeconds, () => ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Idle)));
            _expiry = DateTime.Now + args.TimeoutSeconds;
            _app.LogInformation("{RoomName} | {CorrelationId} - Timer Set: Timeout of {Timeout} expiring at {Expiry}", sender, args.CorrelationId, args.TimeoutSeconds, _expiry);
        }


        private void OnManualEntityStateChange(object sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            var newState = $"{args.EntityStates.New.State}";
            _app.LogInformation("{RoomName} | {CorrelationId} - Manual State Change: {Entity} changed to {State}", sender, args.CorrelationId, args.EntityId, newState);
            if (State == ManagerState.Disabled)
            {
                _app.LogInformation("{RoomName} | {CorrelationId} - Manager Disabled", sender, args.CorrelationId);
                return;
            }

            EntityOverride.Invoke(sender, new EntityOverrideEventArgs(args.CorrelationId, newState) { EntityId = args.EntityId });
        }

        private void OnPresenceStarted(object sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            _app.LogInformation("{RoomName} | {CorrelationId} - Presence Started", sender, args.CorrelationId);
            if (State == ManagerState.Disabled)
            {
                _app.LogInformation("{RoomName} | {CorrelationId} - Manager Disabled", sender, args.CorrelationId);
                return;
            }

            ManagerTimerReset.Invoke(sender, new ManagerTimerResetEventArgs(args.CorrelationId));
            if (Configurator.LuxAboveLimit) return;
            ManagerStateChanged.Invoke(sender, new ManagerStateEventArgs(args.CorrelationId, ManagerState.Active));
        }

        private void OnPresenceStopped(object sender, HassEventArgs args)
        {
            if (IncorrectRoomEvent(sender)) return;
            _app.LogInformation("{RoomName} | {CorrelationId} - Presence Stopped", sender, args.CorrelationId);
            if (State == ManagerState.Disabled)
            {
                _app.LogInformation("{RoomName} | {CorrelationId} - Manager Disabled", sender, args.CorrelationId);
                return;
            }

            if (PresenceIsActive)
            {
                _app.LogInformation("{RoomName} | {CorrelationId} - No timer set: Active sensors {Sensors}", sender, args.CorrelationId, Configurator.ActivePresenceSensors.ToEntityIdsString());
                return;
            }

            ManagerTimerSet.Invoke(sender, new ManagerTimerSetEventArgs(args.CorrelationId, Configurator.TimeoutSeconds));
        }

        private void RegisterEventHandlers()
        {
            ManagerStateChanged += OnManagerStateChanged;
            ManagerTimerReset   += OnManagerTimerReset;
            ManagerTimerSet     += OnManagerTimerSet;
            EntityOverride      += OnEntityOverride;

            Subscriptions.PresenceStarted         += OnPresenceStarted;
            Subscriptions.PresenceStopped         += OnPresenceStopped;
            Subscriptions.HouseModeChanged        += OnHouseModeChanged;
            Subscriptions.ManagerEnabledChanged   += OnEnabledChanged;
            Subscriptions.ManualEntityStateChange += OnManualEntityStateChange;
            Subscriptions.Setup(_app, Configurator);
            _app.LogInformation("Event Handlers Registered");
        }

        private void SetConfigurator(LightsManagerConfig config)
        {
            Configurator = new Configurator(config);
            Configurator.Configure(_app);
            _app.LogInformation("Configurator Configured");
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

            _app.SetState(Configurator.Enabled.EntityId, Configurator.Enabled.State, attrs);
            _app.LogInformation("{RoomName} | {CorrelationId} - HA State Set", sender, args.CorrelationId);
        }

        private void TurnOffControlEntities(object sender, HassEventArgs args)
        {
            var controlEntities = Configurator.AllControlEntities;
            controlEntities.ForEach(e => e.TurnOff());
            _app.LogInformation("{RoomName} | {CorrelationId} - Control Entities Turned Off: {Entities}", sender, args.CorrelationId, controlEntities.ToEntityIdsString());
        }

        private void TurnOnControlEntities(object sender, HassEventArgs args)
        {
            var controlEntities = Configurator.Lights.Union(Configurator.Switches).ToList();
            controlEntities.ForEach(e => e.TurnOn());
            _app.LogInformation("{RoomName} | {CorrelationId} - Control Entities Turned On: {Entities}", sender, args.CorrelationId, controlEntities.ToEntityIdsString());
        }
    }
}