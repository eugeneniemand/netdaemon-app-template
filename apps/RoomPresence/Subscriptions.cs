using NetDaemon.Common.Reactive;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private void SetupSubscriptions()
        {
            LogTrace("SetupSubscriptions()");
            SetupPresenceSubscription();
            SetupKeepAliveSubscription();
            SetupManualTurnOffSubscription();
            SetupManualTurnOnSubscription();
            SetupBrightnessColourSubscription();
            SetupEnabledSwitchSubscription();
            SetupNightModeSubscription();
        }

        private void SetupNightModeSubscription()
        {
            if (_app.States.Any(s => s.EntityId == _roomConfig.NightTimeEntityId))
                _app.Entity(_roomConfig.NightTimeEntityId!)
                   .StateChanges
                   .Subscribe(s =>
                   {
                       LogDebug("Night Mode Switched: {state}", s.New.State);
                       NightModeChanged();
                   });
        }

        private void SetupEnabledSwitchSubscription()
        {
            _app.Entity(_roomConfig.EnabledSwitchEntityId)
                .StateChanges
                .Subscribe(s =>
                {
                    LogDebug("Enabled Switch event changed to: {state}", s.New.State);
                    if (s.New.State == "on") SetRoomState(RoomState.Idle);
                    if (s.New.State == "off") SetRoomState(RoomState.Disabled);
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
                        SetRoomState(RoomState.Override);
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
                           && (e.Old?.Attribute?.brightness != e.New?.Attribute?.brightness || e.Old?.Attribute?.color_temp != e.New?.Attribute?.color_temp))

                    .Subscribe(s =>
                    {
                        LogDebug("Brightness/Colour Manually Changed: {entityId}", s.New.EntityId);
                        DisableCircadian();
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
                        LogDebug("Entitiy Manually Turned Off: {entityId}", s.New.EntityId);
                        SetRoomState(RoomState.Idle);
                    });
        }

        private void SetupKeepAliveSubscription()
        {
            foreach (var entityId in _keepAliveEntityIds)
                _app.Entity(entityId)
                    .StateChanges
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on" || e.Old?.State == "on" && e.New?.State == "on")
                    .Subscribe(s =>
                    {
                        LogDebug("Keep Alive event: {entityId}", s.New.EntityId);
                        HandleEvent();
                    });
        }

        private void SetupPresenceSubscription()
        {
            foreach (var entityId in _presenceEntityIds)
                _app.Entity(entityId)
                    .StateAllChanges
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on" ||
                                e.Old?.State == "on" && e.New?.State == "on" && (e.New?.LastUpdated! - e.Old?.LastUpdated!).Value.TotalSeconds < 80)
                    .Subscribe(s =>
                    {
                        LogDebug("Presence event: {entityId}", s.New.EntityId);
                        HandleEvent();
                    });
        }

        private void StartGuardDog()
        {
            _app.RunEvery(TimeSpan.FromMinutes(1), () =>
            {
                LogTrace("Guard Dog - Looking for Turned On Keep Alive Enitities()");
                if (_keepAliveEntityIds.Any(entityId => EntityState(entityId) == "on"))
                    SetRoomState(RoomState.Active);

            });

            _app.RunEvery(TimeSpan.FromMinutes(1), () =>
            {
                LogTrace("Guard Dog - Looking for Turned On Control Enitities()");
                if (Presence || !RoomIs(RoomState.Idle)) return;
                foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds)
                                                          .Where(entityId => EntityState(entityId) == "on"))
                {
                    LogDebug("Guard Dog found: {entityId}", entityId);
                    Timer?.Dispose();
                    Timer = _app.RunIn(_timeout, HandleTimer);
                    SetRoomState(RoomState.Override);
                }
            });
        }

    }
}
