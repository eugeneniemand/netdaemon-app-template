using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private void SetRoomState(RoomState roomState)
        {
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

            LogDebug("Set Room State To: {roomState}", roomState.ToString());
        }

        private void SetRoomStateOverride()
        {
            if (EntityState(_roomConfig.RoomPresenceEntityId).ToLower() == RoomState.Active.ToString().ToLower()) return;
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
            BrightnessTimer?.Dispose();
            BrightnessTimer = null;
            _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Disabled.ToString().ToLower(), null);
        }

        private void SetRoomStateActive()
        {
            if (EntityState(_roomConfig.RoomPresenceEntityId).ToLower() == RoomState.Override.ToString().ToLower()) return;
            Timer?.Dispose();
            Timer = null;
            Timer = _app.RunIn(_timeout, HandleTimer);
            _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Active.ToString().ToLower(), new
            {
                ActiveEntities,
                PresenceEntityIds = _presenceEntityIds,
                KeepAliveEntities,
                ControlEntityIds = _controlEntityIds,
                NightControlEntityIds = _nightControlEntityIds,
                Expiry
            });
            TurnOnControlEntities();
        }

        private void SetRoomStateIdle()
        {
            TurnOffControlEntities();
            BrightnessTimer?.Dispose();
            BrightnessTimer = null;
            Timer?.Dispose();
            Timer = null;
            _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Idle.ToString().ToLower(), new
            {
                PresenceEntityIds = _presenceEntityIds,
                KeepAliveEntities,
                ControlEntityIds = _controlEntityIds,
                NightControlEntityIds = _nightControlEntityIds
            });
            _app.Delay(TimeSpan.FromSeconds(1));
            EnableCircadian();
        }
    }
}
