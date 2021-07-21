using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private const string UNKNOWN = "Unknown";

        private string EntityState(string? entityId)
        {
            if (entityId == null) return UNKNOWN;
            if (_app.States.Any(e => e.EntityId == entityId))
                return _app.State(entityId)?.State?.ToString() ?? UNKNOWN;
            return UNKNOWN;
        }

        private void VerifyConfig(RoomConfig roomConfig)
        {
            var entities = _roomConfig.ControlEntityIds
                                      .Union(_roomConfig.PresenceEntityIds)
                                      .Union(_roomConfig.KeepAliveEntityIds)
                                      .Union(_roomConfig.NightControlEntityIds)
                                      .Union(new List<string> { _roomConfig.LuxEntityId ?? "", _roomConfig.LuxLimitEntityId ?? "", _roomConfig.NightTimeEntityId ?? "", roomConfig.ConditionEntityId })
                                      .Where(e => !string.IsNullOrEmpty(e))
                                      .ToList();

            var invalidEntities = entities.Except(_app.States.Select(s => s.EntityId)).ToList();

            if (invalidEntities.Any())
                _app.LogError($"{_roomConfig.Name} contains the following invalid EntityIds:\n{string.Join("\n  - ", invalidEntities)}");
        }

        private void LogCurrentState(string message)
        {
            var state = ToJson(new
            {
                RoomPresenceState = new
                {
                    Lux,
                    LuxLimit          = LuxLimit(),
                    LuxBelowThreshold = LuxBelowThreshold(),
                    Enabled           = IsDisabled(),
                    ControlEntities   = GetControlEntities(),
                    ActiveEntities,
                    Timer = Timer != null ? "NotDisposed" : "Disposed",
                    IsNightTime
                },
                RawStates = new
                {
                    Lux       = $"{EntityState(_roomConfig.LuxEntityId)} - {_roomConfig.LuxEntityId}",
                    LuxLimit  = $"{EntityState(_roomConfig.LuxLimitEntityId)} - {_roomConfig.LuxLimitEntityId}",
                    Enabled   = $"{EntityState(_roomConfig.EnabledSwitchEntityId)} - {_roomConfig.EnabledSwitchEntityId}",
                    RoomState = $"{EntityState(_roomConfig.RoomPresenceEntityId)} - {_roomConfig.RoomPresenceEntityId}",
                    NightTime = $"{EntityState(_roomConfig.NightTimeEntityId)} - {_roomConfig.NightTimeEntityId}"
                }
            });
            LogTrace("{message}\nState: {state}", message, state);
        }

        private void LogConfig(RoomConfig roomConfig)
        {
            LogTrace("LogConfig");

            var jsonConfig = ToJson(roomConfig);
            LogTrace($"{nameof(roomConfig)} for: {roomConfig.Name}");
            LogTrace(jsonConfig, new { });
        }

        private static string ToJson(object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = false });
        }

        private void LogDebug(string message, params object[] param)
        {
            if (param.Length == 0)
            {
                _app.LogDebug($"{_tracePrefix}{message}");
                return;
            }

            if (param[0].GetType().Name.Contains("Anonymous") || param[0].GetType().Namespace != "System")
            {
                var json = ToJson(param[0]);
                _app.LogDebug($"{_tracePrefix}{{message}}{{json}}", message, json, param.Length > 1 ? param.Skip(1) : Array.Empty<object>());
            }
            else
            {
                _app.LogDebug($"{_tracePrefix}{message}", param);
            }
        }

        private void LogTrace(string message, params object[] param)
        {
            if (param.Length == 0)
            {
                _app.LogTrace($"{_tracePrefix}{message}");
                return;
            }

            if (param[0].GetType().Name.Contains("Anonymous") || param[0].GetType().Namespace != "System")
            {
                var json = ToJson(param[0]);
                _app.LogTrace($"{_tracePrefix}{{message}}{{json}}", message, json, param.Length > 1 ? param.Skip(1) : Array.Empty<object>());
            }
            else
            {
                _app.LogTrace($"{_tracePrefix}{message}", param);
            }
        }
    }
}