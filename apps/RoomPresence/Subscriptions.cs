using System;
using System.Linq;
using System.Reactive.Linq;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private void SetupSubscriptions()
        {
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
                        NightModeChanged();
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
                        SetRoomState(RoomState.Override);
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
                        SetRoomState(RoomState.Idle);
                        LogCurrentState("After handling Manually Turned Off event");
                    });
        }

        private void SetupKeepAliveSubscription()
        {
            foreach (var entityId in _keepAliveEntityIds)
                _app.Entity(entityId)
                    .StateChanges
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on" ||
                                e.Old?.State == "on" && e.New?.State == "on")
                    .Subscribe(s =>
                    {
                        LogDebug("Keep Alive event: {entityId}", s.New.EntityId);
                        LogCurrentState("Before handling Keep Alive event");
                        HandleEvent();
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
                        HandleEvent();
                        LogCurrentState("After handling Presence event");
                    });
        }

        private void StartGuardDog()
        {
            _app.RunEvery(TimeSpan.FromMinutes(5), () =>
            {
                LogTrace("Guard Dog - Looking for Turned On Keep Alive Entities()");
                if (_keepAliveEntityIds.Any(entityId => EntityState(entityId) == "on") && RoomIs(RoomState.Idle))
                    HandleEvent();
            });

            _app.RunEvery(TimeSpan.FromMinutes(1), () =>
            {
                LogTrace("Guard Dog - Looking for Turned On Control Entities()");
                if (Timer != null && ( Presence || RoomIs(RoomState.Override) )) return;
                foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds)
                                                          .Where(entityId => EntityState(entityId) == "on"))
                {
                    LogDebug("Guard Dog found: {entityId}", entityId);
                    SetRoomState(RoomState.Override);
                    Timer?.Dispose();
                    Timer = _app.RunIn(TimeSpan.FromSeconds(_roomConfig.OverrideTimeout), HandleTimer);
                }
            });
        }
    }
}