using System;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private void SetRoomState(RoomState roomState)
        {
            LogDebug("Attempt to set Room State To: {roomState}", roomState.ToString());
            var _attributes = SetAttributes(roomState);
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

            void SetTimer(TimeSpan ts, Action action)
            {
                DisposeTimer();
                Timer = _app.RunIn(ts, action);
            }

            void DisposeTimer()
            {
                Timer?.Dispose();
                Timer = null;
            }

            void SetRoomStateOverride()
            {
                if (Timer != null && RoomIs(RoomState.Active)) return;

                SetTimer(_overrideTimeout, () => TimeoutEvent.Invoke(this, new HassEventArgs { EntityId = _eventEntity }));
                _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Override.ToString().ToLower(), _attributes);
            }

            void SetRoomStateDisabled()
            {
                DisposeTimer();
                _brightnessTimer?.Dispose();
                _brightnessTimer = null;
                _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Disabled.ToString().ToLower(), null);
            }

            void SetRoomStateActive()
            {
                SetTimer(Timeout, () => TimeoutEvent.Invoke(this, new HassEventArgs { EntityId = _eventEntity }));
                _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Active.ToString().ToLower(), _attributes);
                TurnOnControlEntities();
            }

            void SetRoomStateIdle()
            {
                TurnOffControlEntities();
                _brightnessTimer?.Dispose();
                _brightnessTimer = null;
                DisposeTimer();
                _app.Delay(TimeSpan.FromSeconds(1));
                _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.Idle.ToString().ToLower(), _attributes);
                EnableCircadian();
            }

            object SetAttributes(RoomState state)
            {
                return new
                {
                    EventEntity = _eventEntity,
                    ActiveEntities,
                    PresenceEntityIds = _presenceEntityIds,
                    KeepAliveEntities,
                    ControlEntityIds      = _controlEntityIds,
                    NightControlEntityIds = _nightControlEntityIds,
                    Expiry = state == RoomState.Override
                        ? DateTime.Now.AddSeconds(_overrideTimeout.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss")
                        : Expiry
                };
            }
        }
    }
}