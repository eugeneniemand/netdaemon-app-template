using System;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private void SetRoomState(RoomState roomState)
        {
            LogDebug("Attempt to set Room State To: {roomState}", roomState.ToString());
            switch (roomState)
            {
                case RoomState.Idle:
                    SetRoomStateIdle();
                    break;
                case RoomState.Active:
                    SetRoomStateActive();
                    break;
                case RoomState.Disabled:
                    SetRoomStateDisabled();
                    break;
                case RoomState.Override:
                    SetRoomStateOverride();
                    break;
            }
        }

        private void SetRoomStateOverride()
        {
            if (string.Equals(EntityState(_roomConfig.RoomPresenceEntityId), RoomState.Active.ToString(),
                StringComparison.CurrentCultureIgnoreCase)) return;
            Timer?.Dispose();
            Timer = null;
            Timer = _app.RunIn(_overrideTimeout, HandleTimer);
            _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Override.ToString().ToLower(), new
            {
                ActiveEntities,
                PresenceEntityIds = _presenceEntityIds,
                KeepAliveEntities,
                ControlEntityIds = _controlEntityIds,
                NightControlEntityIds = _nightControlEntityIds,
                Expiry = DateTime.Now.AddSeconds(_overrideTimeout.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

        private void SetRoomStateDisabled()
        {
            Timer?.Dispose();
            Timer = null;
            _brightnessTimer?.Dispose();
            _brightnessTimer = null;
            _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Disabled.ToString().ToLower(), null);
        }

        private void SetRoomStateActive()
        {
            if (RoomIs(RoomState.Override)) return;
            Timer?.Dispose();
            Timer = null;
            Timer = _app.RunIn(Timeout, HandleTimer);
            _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Active.ToString().ToLower(), new
            {
                ActiveEntities,
                PresenceEntityIds = _presenceEntityIds,
                KeepAliveEntities,
                ControlEntityIds      = _controlEntityIds,
                NightControlEntityIds = _nightControlEntityIds,
                Expiry
            });
            TurnOnControlEntities();
        }

        private void SetRoomStateIdle()
        {
            TurnOffControlEntities();
            _brightnessTimer?.Dispose();
            _brightnessTimer = null;
            Timer?.Dispose();
            Timer = null;
            _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Idle.ToString().ToLower(), new
            {
                PresenceEntityIds = _presenceEntityIds,
                KeepAliveEntities,
                ControlEntityIds      = _controlEntityIds,
                NightControlEntityIds = _nightControlEntityIds
            });
            _app.Delay(TimeSpan.FromSeconds(1));
            EnableCircadian();
        }
    }
}