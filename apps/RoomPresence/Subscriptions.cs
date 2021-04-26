using System;
using System.Linq;
using System.Reactive.Linq;

namespace Presence
{
    public class HassEventArgs : EventArgs
    {
        public string EntityId { get; set; }
    }

    public partial class RoomPresenceImplementation
    {
        public event EventHandler<HassEventArgs> TimeoutEvent;
        public event EventHandler<HassEventArgs> PresenceEvent;
        public event EventHandler<HassEventArgs> GuardDogEvent;
        public event EventHandler<HassEventArgs> KeepAliveEvent;
        public event EventHandler<HassEventArgs> ManualTurnOnEvent;
        public event EventHandler<HassEventArgs> ManualTurnOffEvent;
        public event EventHandler<HassEventArgs> NightModeChangedEvent;

        private void SetupSubscriptions()
        {
            PresenceEvent         += OnPresence;
            ManualTurnOnEvent     += OnManualTurnOn;
            ManualTurnOffEvent    += OnManualTurnOff;
            KeepAliveEvent        += OnKeepAlive;
            GuardDogEvent         += OnGuardDog;
            TimeoutEvent          += OnTimeoutEvent;
            NightModeChangedEvent += OnNightModeChanged;

            LogCurrentState("Before SetupSubscriptions");
            SetupPresenceSubscription();
            SetupKeepAliveSubscription();
            SetupManualTurnOffSubscription();
            SetupManualTurnOnSubscription();
            SetupBrightnessColourSubscription();
            SetupEnabledSwitchSubscription();
            SetupNightModeSubscription();
            LogCurrentState("After SetupSubscriptions");
        }


        private void SetupNightModeSubscription()
        {
            if (_app.States.Any(s => s.EntityId == _roomConfig.NightTimeEntityId))
                _app.Entity(_roomConfig.NightTimeEntityId!)
                    .StateChanges
                    .Subscribe(s =>
                    {
                        LogDebug("Night Mode Switched: {state}", s.New.State);
                        LogCurrentState("Before handling Night Mode Switched event");
                        NightModeChangedEvent.Invoke(this, new HassEventArgs() { EntityId = _roomConfig.NightTimeEntityId! });
                        LogCurrentState("After handling Night Mode Switched event");
                    });
        }

        private void SetupEnabledSwitchSubscription()
        {
            _app.Entity(_roomConfig.EnabledSwitchEntityId)
                .StateChanges
                .Subscribe(s =>
                {
                    LogDebug("Enabled Switch event changed to: {state}", s.New.State);
                    LogCurrentState("Before handling Enabled Switch event");
                    if (s.New.State == "on") SetRoomState(RoomState.Idle);
                    if (s.New.State == "off") SetRoomState(RoomState.Disabled);
                    LogCurrentState("After handling Enabled Switch event");
                });
        }

        private void SetupManualTurnOnSubscription()
        {
            foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds))
                _app.Entity(entityId)
                    .StateChanges
                    .Where(e => e.New?.Context?.UserId == null || e.New?.Context?.UserId != NdUserId)
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on")
                    .Subscribe(s =>
                    {
                        LogDebug("Entitiy Manually Turned On: {entityId}", s.New.EntityId);
                        LogCurrentState("Before handling Manually Turned On event");
                        ManualTurnOnEvent.Invoke(this, new HassEventArgs { EntityId = s.New.EntityId });
                        LogCurrentState("After handling Manually Turned On event");
                    });
        }

        private void SetupBrightnessColourSubscription()
        {
            foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds))
                _app.Entity(entityId)
                    .StateAllChanges
                    .Where(e => e.New?.Context?.UserId == null || e.New?.Context?.UserId != NdUserId)
                    .Where(e => e.Old?.State == "on" && e.New?.State == "on"
                                                     && e.New?.Context?.UserId != null
                                                     && ( e.Old?.Attribute?.brightness !=
                                                          e.New?.Attribute?.brightness ||
                                                          e.Old?.Attribute?.color_temp !=
                                                          e.New?.Attribute?.color_temp ))
                    .Subscribe(s =>
                    {
                        LogDebug("Brightness/Colour Manually Changed: {entityId}", s.New.EntityId);
                        LogCurrentState("Before handling Manually Changed event");
                        DisableCircadian();
                        LogCurrentState("After handling Manually Changed event");
                    });
        }

        private void SetupManualTurnOffSubscription()
        {
            foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds))
                _app.Entity(entityId)
                    .StateChanges
                    .Where(e => e.New?.Context?.UserId == null || e.New?.Context?.UserId != NdUserId)
                    .Where(e => e.Old?.State == "on" && e.New?.State == "off")
                    .Subscribe(s =>
                    {
                        LogDebug("Entity Manually Turned Off: {entityId}", s.New.EntityId);
                        LogCurrentState("Before handling Manually Turned Off event");
                        ManualTurnOffEvent.Invoke(this, new HassEventArgs { EntityId = s.New.EntityId });
                        LogCurrentState("After handling Manually Turned Off event");
                    });
        }

        private void SetupKeepAliveSubscription()
        {
            foreach (var entityId in _keepAliveEntityIds)
                _app.Entity(entityId)
                    .StateAllChanges
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on" ||
                                e.Old?.State == "on" && e.New?.State == "on")
                    .Subscribe(s =>
                    {
                        LogDebug("Keep Alive event: {entityId}", s.New.EntityId);
                        LogCurrentState("Before handling Keep Alive event");
                        KeepAliveEvent.Invoke(this, new HassEventArgs { EntityId = s.New.EntityId });
                        LogCurrentState("After handling Keep Alive event");
                    });
        }

        private void SetupPresenceSubscription()
        {
            foreach (var entityId in _presenceEntityIds)
                _app.Entity(entityId)
                    .StateAllChanges
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on" ||
                                e.Old?.State == "on" && e.New?.State == "on" &&
                                ( e.New?.LastUpdated! - e.Old?.LastUpdated! ).Value.TotalSeconds < 80)
                    .Subscribe(s =>
                    {
                        LogDebug("Presence event: {entityId}", s.New.EntityId);
                        LogCurrentState("Before handling Presence event");
                        PresenceEvent.Invoke(this, new HassEventArgs { EntityId = s.New.EntityId });
                        LogCurrentState("After handling Presence event");
                    });
        }

        private void StartGuardDog()
        {
            _app.RunEvery(TimeSpan.FromSeconds(_presenceConfig.GuardTimeout), () =>
            {
                LogTrace("Guard Dog - Looking for Turned On Control Entities");
                var overrideEntities = string.Join(", ", _controlEntityIds
                                                         .Union(_nightControlEntityIds)
                                                         .Where(entityId => EntityState(entityId) == "on"));
                if (string.IsNullOrWhiteSpace(overrideEntities)) return;
                LogDebug("Guard Dog found: {entityId}", overrideEntities);
                GuardDogEvent.Invoke(this, new HassEventArgs { EntityId = overrideEntities });
            });
        }
    }
}