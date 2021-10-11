using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using NetDaemon.Common;
using Serilog;

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

        private void LogDebugJson(HassEventArgs args, string message = "", [CallerMemberName] string method = "", [CallerLineNumber] long line = 0, object? data = null)
        {
            var logMessageData = NewLogMessage(args, method, line, message, data);
            _app.LogDebug("{text}", ToJson(logMessageData));
        }

        private void LogVerboseJson(HassEventArgs args, string message = "", [CallerMemberName] string method = "", [CallerLineNumber] long line = 0, object? data = null)
        {
            var logMessageData = NewLogMessage(args, method, line, message, data);
            _app.LogTrace("{text}", ToJson(logMessageData));
        }

        private void LogInfoJson(HassEventArgs args, string message = "", [CallerMemberName] string method = "", [CallerLineNumber] long line = 0, object? data = null)
        {
            var logMessageData = NewLogMessage(args, method, line, message, data);
            _app.LogInformation("{text}", ToJson(logMessageData));
        }

        private void LogWarnJson(HassEventArgs args, string message = "", [CallerMemberName] string method = "", [CallerLineNumber] long line = 0, object? data = null)
        {
            var logMessageData = NewLogMessage(args, method, line, message, data);
            _app.LogWarning("{text}", ToJson(logMessageData));
        }

        private static object NewLogMessage(HassEventArgs args, string method, long line, string message, object? data)
        {
            var logMessageData = new
            {
                TimeStamp = $"{DateTime.Now:O}".Replace("\u002B", "Z"),
                args.RoomName,
                args.EventName,
                args.CorrelationId,
                args.EntityId,
                method,
                line,
                message,
                data
            };
            return logMessageData;
        }

        private void LogCurrentState(string message = "")
        {
            LogVerboseJson(hassEventArgs, message, data: new
            {
                Lux       = EntityState(_roomConfig.LuxEntityId),
                LuxLimit  = EntityState(_roomConfig.LuxLimitEntityId),
                Enabled   = EntityState(_roomConfig.EnabledSwitchEntityId),
                RoomState = EntityState(_roomConfig.RoomPresenceEntityId),
                NightTime = EntityState(_roomConfig.NightTimeEntityId),
                ActiveEntities,
                Timer = Timer != null ? "NotDisposed" : "Disposed",
                IsNightTime
            });
        }

        private void LogConfig(RoomConfig roomConfig)
        {
            LogTrace("LogConfig");

            var jsonConfig = ToJson(roomConfig);
            LogTrace($"{nameof(roomConfig)} for: {roomConfig.Name}");
            LogTrace(jsonConfig, new { });
        }

        private static string ToJson(object obj, bool writeIndented = false)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = writeIndented });
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

        private void LogWarning(string message, params object[] param)
        {
            if (param.Length == 0)
            {
                _app.LogWarning($"{_tracePrefix}{message}");
                return;
            }

            if (param[0].GetType().Name.Contains("Anonymous") || param[0].GetType().Namespace != "System")
            {
                var json = ToJson(param[0]);
                _app.LogWarning($"{_tracePrefix}{{message}}{{json}}", message, json, param.Length > 1 ? param.Skip(1) : Array.Empty<object>());
            }
            else
            {
                _app.LogWarning($"{_tracePrefix}{message}", param);
            }
        }
    }
}