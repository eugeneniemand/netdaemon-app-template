using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Concurrency;
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
        private IDisposable? BrightnessTimer;

        private TimeSpan _timeout => IsNightTime ? _nightTimeout : _normalTimeout;
        private TimeSpan _overrideTimeout;

        private string ActiveEntities => string.Join(", ", _presenceEntityIds.Union(_keepAliveEntityIds).Where(entityId => _app.State(entityId)?.State == "on"));

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

        private bool NoPresence => string.IsNullOrEmpty(ActiveEntities);


        private IDisposable? Timer { get; set; }


        public RoomPresenceImplementation(INetDaemonRxApp app, RoomConfig roomConfig, IScheduler? scheduler = null)
        {
            _app = app;

            try
            {
                _roomConfig = roomConfig;
                Scheduler = scheduler;
                _tracePrefix = $"({_roomConfig.Name}) - ";
                _normalTimeout = TimeSpan.FromSeconds(roomConfig.Timeout != 0 ? roomConfig.Timeout : 300);
                _nightTimeout = TimeSpan.FromSeconds(roomConfig.NightTimeout != 0 ? roomConfig.NightTimeout : 60);
                _overrideTimeout = TimeSpan.FromSeconds(roomConfig.OverrideTimeout != 0 ? roomConfig.OverrideTimeout : 900);
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

            SetRoomState(RoomState.Active);
        }




        public void Initialize()
        {
            try
            {
                LogTrace("Initialize Started");
                VerifyConfig(_roomConfig);
                LogConfig(_roomConfig);
                SetupEnabledSwitchEntity();
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
                LogDebug("Control Entities: {entities}", entities);
                return entities;
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

        private void HandleTimer()
        {
            LogTrace("Is there Presence?");
            if (NoPresence)
            {
                LogTrace("Presence: {p}", false);
                SetRoomState(RoomState.Idle);
            }
            else
            {
                LogTrace("Presence: {p}", true);
                SetRoomState(RoomState.Active);
            }
        }

        private bool IsDisabled()
        {
            LogTrace("IsDisabled()");


            var switchEntity = _app.States.FirstOrDefault(e => e.EntityId == _roomConfig.EnabledSwitchEntityId);
            LogTrace("{switch} is {state}?", _roomConfig.EnabledSwitchEntityId, switchEntity?.State);


            if (switchEntity?.State != "off") return false;
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

        private void LogDebug(string message, params object[] param)
        {
            _app.LogDebug($"{_tracePrefix}{message}", param);
        }

        private void LogTrace(string message, params object[] param)
        {
            _app.LogTrace($"{_tracePrefix}{message}", param);
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


        private void SetRoomState(RoomState roomState)
        {
            switch (roomState)
            {
                case RoomState.Idle:
                    TurnOffControlEntities();
                    BrightnessTimer?.Dispose();
                    BrightnessTimer = null;
                    Timer?.Dispose();
                    Timer = null;
                    _app.SetState(_roomConfig.RoomPresenceEntityId, roomState.ToString().ToLower(), new
                    {
                        PresenceEntityIds = _presenceEntityIds,
                        KeepAliveEntities,
                        ControlEntityIds = _controlEntityIds,
                        NightControlEntityIds = _nightControlEntityIds
                    });
                    _app.Delay(TimeSpan.FromSeconds(1));
                    EnableCircadian();
                    break;
                case RoomState.Active:
                    if (_app.States.Any(e => e.EntityId == _roomConfig.RoomPresenceEntityId) && _app.State(_roomConfig.RoomPresenceEntityId)?.State?.ToLower() == RoomState.Override.ToString().ToLower()) break;
                    Timer?.Dispose();
                    Timer = null;
                    Timer = _app.RunIn(_timeout, HandleTimer);
                    _app.SetState(_roomConfig.RoomPresenceEntityId, roomState.ToString().ToLower(), new
                    {
                        ActiveEntities,
                        PresenceEntityIds = _presenceEntityIds,
                        KeepAliveEntities,
                        ControlEntityIds = _controlEntityIds,
                        NightControlEntityIds = _nightControlEntityIds,
                        Expiry
                    });
                    TurnOnControlEntities();
                    break;
                case RoomState.Disabled:
                    Timer?.Dispose();
                    Timer = null;
                    BrightnessTimer?.Dispose();
                    BrightnessTimer = null;
                    _app.SetState(_roomConfig.RoomPresenceEntityId, roomState.ToString().ToLower(), null);
                    break;
                case RoomState.Override:
                    if (_app.States.Any(e => e.EntityId == _roomConfig.RoomPresenceEntityId) && _app.State(_roomConfig.RoomPresenceEntityId)?.State?.ToLower() == RoomState.Active.ToString().ToLower()) break;
                    Timer?.Dispose();
                    Timer = null;
                    Timer = _app.RunIn(_overrideTimeout, HandleTimer);
                    _app.SetState(_roomConfig.RoomPresenceEntityId, roomState.ToString().ToLower(), new
                    {
                        ActiveEntities,
                        PresenceEntityIds = _presenceEntityIds,
                        KeepAliveEntities,
                        ControlEntityIds = _controlEntityIds,
                        NightControlEntityIds = _nightControlEntityIds,
                        Expiry = DateTime.Now.AddSeconds(_overrideTimeout.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss")
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roomState), roomState, null);
            }

            LogDebug("Set Room State To: {roomState}", roomState.ToString());
        }

        private void SetupEnabledSwitchEntity()
        {
            LogTrace("If {switch} does not exist create it", _roomConfig.EnabledSwitchEntityId);
            var switchEntity = _app.States.FirstOrDefault(e => e.EntityId == _roomConfig.EnabledSwitchEntityId);

            if (switchEntity != null) return;
            LogTrace("{switch} does not exist, creating and turning it on", _roomConfig.EnabledSwitchEntityId);
            _app.SetState(_roomConfig.EnabledSwitchEntityId, "on", null);
        }

        private void SetupSubscriptions()
        {
            LogTrace("SetupSubscriptions()");
            foreach (var entityId in _presenceEntityIds)
                _app.Entity(entityId)
                    .StateAllChanges
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on" || e.Old?.State == "on" && e.New?.State == "on")
                    .Subscribe(s =>
                    {
                        LogDebug("Presence event: {entityId}", s.New.EntityId);
                        HandleEvent();
                    });

            foreach (var entityId in _keepAliveEntityIds)
                _app.Entity(entityId)
                    .StateChanges
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on" || e.Old?.State == "on" && e.New?.State == "on")
                    .Subscribe(s =>
                    {
                        LogDebug("Keep Alive event: {entityId}", s.New.EntityId);
                        HandleEvent();
                    });

            foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds))
                _app.Entity(entityId)
                    .StateChanges
                    .Where(e => e.Old?.State == "on" && e.New?.State == "off")
                    .Subscribe(s =>
                    {
                        LogDebug("Entitiy Manually Turned Off: {entityId}", s.New.EntityId);
                        SetRoomState(RoomState.Idle);
                    });

            foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds))
                _app.Entity(entityId)
                    .StateAllChanges
                    .Where(e => e.Old?.State == "on" && e.New?.State == "on"
                           && e.New?.Context?.UserId != null
                           && (e.Old?.Attribute?.brightness != e.New?.Attribute?.brightness || e.Old?.Attribute?.color_temp != e.New?.Attribute?.color_temp))

                    .Subscribe(s =>
                    {
                        LogDebug("Brightness/Colour Manually Changed: {entityId}", s.New.EntityId);
                        DisableCircadian();
                    });

            foreach (var entityId in _controlEntityIds.Union(_nightControlEntityIds))
                _app.Entity(entityId)
                    .StateChanges
                    .Where(e => e.Old?.State == "off" && e.New?.State == "on")
                    .Subscribe(s =>
                    {
                        LogDebug("Entitiy Manually Turned On: {entityId}", s.New.EntityId);
                        SetRoomState(RoomState.Override);
                    });

            _app.Entity(_roomConfig.EnabledSwitchEntityId)
                .StateChanges
                .Subscribe(s =>
                {
                    LogDebug("Enabled Switch event changed to: {state}", s.New.State);
                    if (s.New.State == "on") SetRoomState(RoomState.Idle);
                    if (s.New.State == "off") SetRoomState(RoomState.Disabled);
                });

            if (_app.States.Any(s => s.EntityId == _roomConfig.NightTimeEntityId))
                _app.Entity(_roomConfig.NightTimeEntityId!)
                   .StateChanges
                   .Subscribe(s =>
                   {
                       LogDebug("Night Mode Switched: {state}", s.New.State);
                       NightModeChanged(s.New.State);
                       
                   });
        }

        private void NightModeChanged(string nightMode)
        {
            if (nightMode == "on")
            {
                foreach (var entityId in _controlEntityIds.Except(_nightControlEntityIds))
                {
                    _app.Entity(entityId).TurnOff();
                }
                foreach (var entityId in _nightControlEntityIds)
                {
                    _app.Entity(entityId).TurnOn();
                }
            }

            if (nightMode == "off")
            {
                foreach (var entityId in _nightControlEntityIds.Except(_controlEntityIds))
                {
                    _app.Entity(entityId).TurnOff();
                }
                foreach (var entityId in _controlEntityIds)
                {
                    _app.Entity(entityId).TurnOn();
                }
            }
        }

        private void DisableCircadian()
        {
            _app.Entity(_roomConfig.CircadianSwitchEntityId).TurnOff();
        }

        private void EnableCircadian()
        {
            _app.Entity(_roomConfig.CircadianSwitchEntityId).TurnOn();
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
        }

        private void TurnOnControlEntities()
        {
            LogTrace("TurnOnControlEntities()");
            foreach (var entityId in GetControlEntities())
                if (_app.State(entityId)?.State == "off")
                {
                    LogDebug("Turn On Control Entity: {entityId}", entityId);

                    if (_roomConfig.SunriseEnabled)
                    {
                        _app.Entity(entityId).TurnOn(new { brightness = _roomConfig.SunriseStartBrightness });
                        _app.Delay(TimeSpan.FromMilliseconds(200));
                        _app.Entity(entityId).TurnOn(new { kelvin = _roomConfig.SunriseStartKelvin });

                        BrightnessTimer = _app.RunEvery(TimeSpan.FromMinutes(_roomConfig.SunriseUpdateInterval), () =>
                        {
                            UpdateBrightness(entityId);
                            UpdateColour(entityId);
                        });
                    }
                    else
                    {
                        _app.Entity(entityId).TurnOn();
                    }
                }
                else
                {
                    LogDebug("Entity already on: {entityId}", entityId);
                }
        }

        public DateTime Now => Scheduler?.Now.DateTime ?? DateTime.Now;
        private IScheduler? Scheduler { get; }

        private void UpdateBrightness(string entityId)
        {
            if (_app.State(entityId)?.State == "off" || !_roomConfig.SunriseBrightnessEnabled) return;
            var startTime = DateTime.ParseExact(_roomConfig.SunriseStartTime, "HH:mm", CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(_roomConfig.SunriseEndTime, "HH:mm", CultureInfo.InvariantCulture);
            var brightness = CalculateFractionBasedOnIntervalAndElapsedTime(startTime, endTime, _roomConfig.SunriseUpdateInterval, _roomConfig.SunriseStartBrightness, _roomConfig.SunriseEndBrightness);
            _app.Entity(entityId).TurnOn(new { brightness });
        }

        private void UpdateColour(string entityId)
        {
            if (_app.State(entityId)?.State == "off" || !_roomConfig.SunriseColourEnabled) return;
            var startTime = DateTime.ParseExact(_roomConfig.SunriseStartTime, "HH:mm", CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(_roomConfig.SunriseEndTime, "HH:mm", CultureInfo.InvariantCulture);
            var kelvin = CalculateFractionBasedOnIntervalAndElapsedTime(startTime, endTime, _roomConfig.SunriseUpdateInterval, _roomConfig.SunriseStartKelvin, _roomConfig.SunriseEndKelvin);
            _app.Entity(entityId).TurnOn(new { kelvin });
        }

        private double CalculateFractionBasedOnIntervalAndElapsedTime(DateTime startTime, DateTime endTime, int interval, int startValue, int endValue)
        {
            var minutes = (endTime - startTime).TotalMinutes;
            var fraction = minutes / interval;
            var range = endValue - startValue;
            var inc = range / fraction;

            if (Now < startTime || Now > endTime) return startValue;

            var minutesElapsed = (Now - startTime).TotalMinutes;
            var intervalsElapsed = minutesElapsed / interval;
            return startValue + (intervalsElapsed * inc);
        }

        private void VerifyConfig(RoomConfig roomConfig)
        {
            var entities = _roomConfig.ControlEntityIds
                .Union(_roomConfig.PresenceEntityIds)
                .Union(_roomConfig.KeepAliveEntityIds)
                .Union(_roomConfig.NightControlEntityIds)
                .Union(new List<string> { _roomConfig.LuxEntityId ?? "", _roomConfig.LuxLimitEntityId ?? "", _roomConfig.NightTimeEntityId ?? "" })
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