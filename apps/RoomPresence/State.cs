using System;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private void SetRoomState(RoomState roomState)
        {
            LogInfoJson(hassEventArgs, data: roomState.ToString());
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
                case RoomState.RandomWait:
                    SetRoomStateRandom();
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

            void SetRoomStateRandom()
            {
                var waitDuration = _controller.GetRandomDuration();
                SetRandomOnTimer(waitDuration);
                _attributes = SetAttributes(RoomState.RandomWait, waitDuration);
                _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.RandomWait.ToString().ToLower(), _attributes);
            }

            void SetRandomOnTimer(int duration)
            {
                SetTimer(TimeSpan.FromMinutes(duration), () =>
                {
                    if (!LuxBelowThreshold()) return;
                    var onDuration = _controller.GetRandomDuration();

                    TurnOnControlEntities();
                    TurnOffRandomAndReset(onDuration);

                    _attributes = SetAttributes(RoomState.RandomActive, onDuration);
                    _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.RandomActive.ToString().ToLower(), _attributes);
                });
            }

            void TurnOffRandomAndReset(int duration)
            {
                SetTimer(TimeSpan.FromMinutes(duration), () =>
                {
                    var offDuration = _controller.GetRandomDuration();
                    SetRoomStateIdle();
                    SetRandomOnTimer(offDuration);

                    _attributes = SetAttributes(RoomState.RandomWait, offDuration);
                    _app.SetState(_roomConfig.RoomPresenceEntityId, RoomState.RandomWait.ToString().ToLower(), _attributes);
                });
            }

            void SetRoomStateOverride()
            {
                if (Timer != null && ( RoomIs(RoomState.Active) || RoomIs(RoomState.RandomActive) )) return;

                SetTimer(_overrideTimeout, () => TimeoutEvent.Invoke(this, new HassEventArgs(_roomConfig.Name, nameof(SetRoomStateOverride)) { EntityId = _eventEntity }));
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
                SetTimer(Timeout, () => TimeoutEvent.Invoke(this, new HassEventArgs(_roomConfig.Name, nameof(SetRoomStateActive)) { EntityId = _eventEntity }));
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

            object SetAttributes(RoomState state, int? randomDuration = null)
            {
                string expiryAtt = "";
                if (randomDuration == null)
                    expiryAtt =
                        state == RoomState.Override
                            ? DateTime.Now.AddSeconds(_overrideTimeout.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss")
                            : Expiry;
                else
                    expiryAtt = DateTime.Now.AddMinutes((int) randomDuration).ToString("yyyy-MM-dd HH:mm:ss");


                var attrs = new
                {
                    EventEntity = _eventEntity,
                    ActiveEntities,
                    PresenceEntityIds = _presenceEntityIds,
                    KeepAliveEntities,
                    ControlEntityIds      = _controlEntityIds,
                    NightControlEntityIds = _nightControlEntityIds,
                    _roomConfig.RandomEntityId,
                    _roomConfig.RandomStates,
                    RandomDuration = randomDuration ?? 0,
                    Expiry         = expiryAtt
                };
                LogVerboseJson(hassEventArgs, data: attrs);
                return attrs;
            }
        }
    }
}