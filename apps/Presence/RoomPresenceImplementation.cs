using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;

namespace Presence
{
    /// <summary>
    ///     The NetDaemonApp implements System.Reactive API
    ///     currently the default one
    /// </summary>
    public class RoomPresenceImplementation
    {
        private readonly INetDaemonRxApp _app;
        private readonly string[] _controlEntityIds;
        private readonly string[] _keepAliveEntityIds;
        private readonly string[] _nightControlEntityIds;
        private readonly TimeSpan _nightTimeout;
        private readonly TimeSpan _normalTimeout;
        private readonly string[] _presenceEntityIds;
        private readonly RoomConfig _roomConfig;
        private readonly string _tracePrefix;

        private TimeSpan _timeout => IsNightTime ? _nightTimeout : _normalTimeout;

        private string ActiveEntities => string.Join(", ", _presenceEntityIds.Union(_keepAliveEntityIds).Where(entityId => _app.State(entityId)?.State == "on"));
        private bool NoPresence => string.IsNullOrEmpty(ActiveEntities);

        private string Expiry => DateTime.Now.AddSeconds(_timeout.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss");

        private bool IsNightTime
        {
            get
            {
                if (_roomConfig.NightTimeEntityId == null) return false;
                string value = _app.State(_roomConfig.NightTimeEntityId)?.State?.ToString() ?? "";
                return _roomConfig.NightTimeEntityStates.Contains(value);
            }
        }

        private string KeepAliveEntities => string.Join(", ", _keepAliveEntityIds);

        private int Lux
        {
            get
            {
                if (_roomConfig.LuxEntityId == null) return 0;
                string luxState = _app.State(_roomConfig.LuxEntityId)?.State?.ToString() ?? "";
                return int.TryParse(luxState, out var luxInt) ? luxInt : 0;
            }
        }


        private IDisposable? Timer { get; set; }

        public RoomPresenceImplementation(INetDaemonRxApp app, RoomConfig roomConfig)
        {
            _app = app;

            try
            {
                _roomConfig = roomConfig;
                _tracePrefix = $"({_roomConfig.Name}) - ";
                _normalTimeout = TimeSpan.FromSeconds(roomConfig.Timeout != 0 ? roomConfig.Timeout : 300);
                _nightTimeout = TimeSpan.FromSeconds(roomConfig.NightTimeout != 0 ? roomConfig.NightTimeout : 60);
                _presenceEntityIds = roomConfig.PresenceEntityIds.ToArray();
                _controlEntityIds = roomConfig.ControlEntityIds.ToArray();
                _nightControlEntityIds = roomConfig.NightControlEntityIds?.ToArray() ?? Array.Empty<string>();
                _keepAliveEntityIds = roomConfig.KeepAliveEntityIds.ToArray();
            }
            catch (Exception e)
            {
                _app.LogError(e, "Error in Constructor");
                throw;
            }
        }

        public void HandleEvent()
        {
            LogTrace("HandleEvent()");

            if (IsDisabled() || !LuxBelowThreshold()) return;

            TurnOnControlEntities();
            ResetTimer();
        }


        public void Initialize()
        {
            try
            {
                LogTrace("Initialize Started");
                VerifyConfig(_roomConfig);
                LogConfig(_roomConfig);
                IsDisabled();
                SetupSubscriptions();
                StartGuardDog();
                LogTrace("Initialize Complete");
            }
            catch (Exception e)
            {
                _app.LogError(e, "Error in Initialize");
                throw;
            }
        }

        private IEnumerable<string> GetControlEntities()
        {
            LogTrace("GetControlEntities()");
            IEnumerable<string> entities = _controlEntityIds;

            LogTrace("If room state is Override then get control and night control entities to enable guard dog");
            if (RoomIs(RoomState.Override))
            {
                entities = _controlEntityIds.Union(_nightControlEntityIds);
            }

            LogTrace("Do Night Control entities exist");
            if (!_nightControlEntityIds.Any())
            {
                LogTrace("Night Control entities doesnt exists");
                entities = _controlEntityIds;
            }
            else if (IsNightTime)
            {
                LogTrace("Return Night Control Entities");
                entities = _nightControlEntityIds;
            }

            LogDebug("Control Entities: {entities}", entities);
            return entities;
        }

        private bool RoomIs(RoomState roomState)
        {
            LogTrace("Is RoomState: {s}?", roomState.ToString().ToLower());

            string roomEntityState = "";
            if (_app.States.Any(e => e.EntityId == _roomConfig.RoomPresenceEntityId)) 
                roomEntityState = _app.State(_roomConfig.RoomPresenceEntityId)?.State?.ToString() ?? "";

            LogTrace("RoomState is: {roomState}", roomEntityState);
            return
                roomEntityState == roomState.ToString().ToLower();
        }

        private void HandleTimer()
        {
            LogTrace("Is there Presence?");
            if (NoPresence)
            {
                LogTrace("Presence: {p}", false);
                TurnOffControlEntities();
            }
            else
            {
                LogTrace("Presence: {p}", true);
                ResetTimer();
            }
        }

        private bool IsDisabled()
        {
            LogTrace("IsDisabled()");

            if (RoomIs(RoomState.Active)) return false;

            LogTrace("Does {switch} exist ", _roomConfig.EnabledSwitchEntityId);
            var switchEntity = _app.States.FirstOrDefault(e => e.EntityId == _roomConfig.EnabledSwitchEntityId);

            if (switchEntity == null)
            {
                LogTrace("{switch} does not exist, creating and turning it on", _roomConfig.EnabledSwitchEntityId);
                switchEntity = _app.SetState(_roomConfig.EnabledSwitchEntityId, "on", null);
            }

            LogTrace("Is {switch} on?", _roomConfig.EnabledSwitchEntityId);
            if (switchEntity?.State == "on")
            {
                SetRoomState(RoomState.Idle);
                return false;
            }
            SetRoomState(RoomState.Disabled);
            return true;
        }

        private void LogConfig(RoomConfig roomConfig)
        {
            LogTrace("LogConfig");
            LogTrace($"{nameof(roomConfig)} name: {roomConfig.Name}");
            _app.LogInformation($"Config for roomConfig: {roomConfig.Name}"
                                + $"\n  Timeout: {_normalTimeout}"
                                + $"\n  NightTimeout: {_nightTimeout}"
                                + $"\n  LuxLimit: {LuxLimit()}"
                                + $"\n  LuxEntityId: {roomConfig.LuxEntityId}"
                                + $"\n  LuxLimitEntityId: {roomConfig.LuxLimitEntityId}"
                                + $"\n  NightTimeEntityId: {roomConfig.NightTimeEntityId}"
                                + $"\n  PresenceEntityIds:\n   - {string.Join("\n   - ", _presenceEntityIds)}"
                                + $"\n  KeepAliveEntityIds:\n   - {string.Join("\n   - ", _keepAliveEntityIds)}"
                                + $"\n  ControlEntityIds:\n   - {string.Join("\n   - ", _controlEntityIds)}"
                                + $"\n  NightControlEntityIds:\n   - {string.Join("\n   - ", _nightControlEntityIds)}\n"
            );
        }

        private void LogTrace(string message, params object[] param)
        {
            _app.LogTrace($"{_tracePrefix}{message}", param);
        }

        private void LogDebug(string message, params object[] param)
        {
            _app.LogDebug($"{_tracePrefix}{message}", param);
        }

        private bool LuxBelowThreshold()
        {
            if (_roomConfig.LuxEntityId == null)
                return true;

            
            var isBelowThreshold = Lux <= LuxLimit();
            LogDebug("Lux Below Threshold: {b} ({Lux})", isBelowThreshold, Lux);
            return isBelowThreshold;
        }

        private int LuxLimit()
        {
            LogTrace("LuxLimit()");

            if (string.IsNullOrWhiteSpace(_roomConfig.LuxLimitEntityId))
            {
                var roomConfigLuxLimit = _roomConfig.LuxLimit ?? 40;
                LogTrace("Lux Limit from Config");
                LogDebug("Lux Limit: {roomConfigLuxLimit}", roomConfigLuxLimit);
                return roomConfigLuxLimit;
            }

            if (int.TryParse(_app.State(_roomConfig.LuxLimitEntityId)?.State?.ToString(), out int luxEntityVal))
            {
                LogTrace("Lux Limit from Entity");
                LogDebug("Lux Limit: {luxEntityVal}", luxEntityVal);
                return luxEntityVal;
            }

            LogTrace($"Could Not Parse {_roomConfig.LuxLimitEntityId} roomState");
            return 1000;
        }

        private void ResetTimer()
        {
            LogTrace("ResetTimer()");
            Timer?.Dispose();
            Timer = _app.RunIn(_timeout, HandleTimer);
            SetRoomState(RoomState.Active);
        }

        private void SetRoomState(RoomState roomState)
        {

            switch (roomState)
            {
                case RoomState.Idle:
                    _app.SetState(_roomConfig.RoomPresenceEntityId, roomState.ToString().ToLower(), new
                    {
                        PresenceEntityIds = _presenceEntityIds,
                        KeepAliveEntities,
                        ControlEntityIds = _controlEntityIds,
                        NightControlEntityIds = _nightControlEntityIds
                    });
                    break;
                case RoomState.Active:
                    _app.SetState(_roomConfig.RoomPresenceEntityId, roomState.ToString().ToLower(), new
                    {
                        ActiveEntities,
                        PresenceEntityIds = _presenceEntityIds,
                        KeepAliveEntities,
                        ControlEntityIds = _controlEntityIds,
                        NightControlEntityIds = _nightControlEntityIds,
                        Expiry
                    });
                    break;
                case RoomState.Disabled:
                    _app.SetState(_roomConfig.RoomPresenceEntityId, roomState.ToString().ToLower(), null);
                    break;
                case RoomState.Override:
                    _app.SetState(_roomConfig.RoomPresenceEntityId, roomState.ToString().ToLower(), new
                    {
                        ActiveEntities,
                        PresenceEntityIds = _presenceEntityIds,
                        KeepAliveEntities,
                        ControlEntityIds = _controlEntityIds,
                        NightControlEntityIds = _nightControlEntityIds,
                        Expiry
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roomState), roomState, null);
            }
            LogDebug("Set Room State To: {roomState}", roomState.ToString());
        }

        private void SetupSubscriptions()
        {
            LogTrace("SetupSubscriptions()");
            foreach (var entityId in _presenceEntityIds)
                _app.Entity(entityId)
                    .StateAllChanges
                    .Where(e => (e.Old?.State == "off" && e.New?.State == "on") || (e.Old?.State == "on" && e.New?.State == "on"))
                    .Subscribe(s =>
                    {
                        LogDebug("Presence Event: {entityId}", s.New.EntityId);
                        HandleEvent();
                    });

            foreach (var entityId in _keepAliveEntityIds)
                _app.Entity(entityId)
                    .StateChanges
                    .Where(e => (e.Old?.State == "off" && e.New?.State == "on") || (e.Old?.State == "on" && e.New?.State == "on"))
                    .Subscribe(s =>
                    {
                        LogDebug("Keep Alive Event: {entityId}", s.New.EntityId);
                        HandleEvent();
                    });
        }

        private void StartGuardDog()
        {
            _app.RunEvery(TimeSpan.FromMinutes(1), () =>
            {
                LogTrace("GuardDog()");
                if (!NoPresence || !RoomIs(RoomState.Idle)) return;
                foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds).Where(controlEntityId => _app.States.Any(e => e.EntityId == controlEntityId && e.State == "on")))
                {
                    LogDebug("Guard Dog found: {entityId}", entityId);
                    Timer?.Dispose();
                    Timer = _app.RunIn(_timeout, HandleTimer);
                    SetRoomState(RoomState.Override);
                }
            });
        }


        private void TurnOffControlEntities()
        {
            LogTrace("TurnOffControlEntities()");
            foreach (var entityId in GetControlEntities())
            {
                LogDebug("Turn Off Control Entity: {entityId}", entityId);
                _app.Entity(entityId).TurnOff();
            }
            SetRoomState(RoomState.Idle);
        }

        private void TurnOnControlEntities()
        {
            LogTrace("TurnOnControlEntities()");
            foreach (var entityId in GetControlEntities())
                if (_app.State(entityId)?.State == "off")
                {
                    LogDebug("Turn On Control Entity: {entityId}", entityId);
                    _app.Entity(entityId).TurnOn();
                }
                else
                {
                    LogDebug("Entity already on: {entityId}", entityId);
                }
        }

        private void VerifyConfig(RoomConfig roomConfig)
        {
            var entities = _roomConfig.ControlEntityIds
                .Union(_roomConfig.PresenceEntityIds)
                .Union(_roomConfig.KeepAliveEntityIds)
                .Union(_roomConfig.NightControlEntityIds)
                .Union(new List<string> {_roomConfig.LuxEntityId ?? "", _roomConfig.LuxLimitEntityId ?? "", _roomConfig.NightTimeEntityId ?? ""})
                .Where(e => !string.IsNullOrEmpty(e))
                .ToList();


            var invalidEntities = entities.Except(_app.States.Select(s => s.EntityId)).ToList();

            if (invalidEntities.Any())
                _app.LogError($"{_roomConfig.Name} contains the following invalid EntityIds:\n{string.Join("\n  - ", invalidEntities)}");
        }
    }

    public enum RoomState
    {
        Idle,
        Active,
        Disabled,
        Override
    }
}