using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private string EntityState(string entityId)
        {
            if (_app.States.Any(e => e.EntityId == entityId))
                return _app.State(entityId)?.State?.ToString() ?? string.Empty;
            return string.Empty;
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

        private void LogConfig(RoomConfig roomConfig)
        {
            LogTrace("LogConfig");

            var jsonConfig = JsonConvert.SerializeObject(roomConfig, Formatting.Indented);
            LogTrace($"{nameof(roomConfig)} for: {roomConfig.Name}");
            LogTrace(jsonConfig, new { });
        }

        private void LogDebug(string message, params object[] param)
        {
            _app.LogDebug($"{_tracePrefix}{message}", param);
        }

        private void LogTrace(string message, params object[] param)
        {
            _app.LogTrace($"{_tracePrefix}{message}", param);
        }
    }
}
