using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightsManager;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;

namespace LightsManager
{
    public static class Extensions
    {
        public static string EntityState(this INetDaemonRxApp app, string? entityId)
        {
            if (entityId == null) return StartUp.UNKNOWN;
            if (app.States.Any(e => e.EntityId == entityId))
                return app.State(entityId)?.State?.ToString() ?? StartUp.UNKNOWN;
            return StartUp.UNKNOWN;
        }

        public static List<string> ToEntityIds(this IEnumerable<RxEntityBase> entities)
        {
            return entities.Select(e => e.EntityId).ToList();
        }

        public static string ToEntityIdsString(this IEnumerable<RxEntityBase> entities)
        {
            return string.Join(", ", entities.Select(e => e.EntityId).ToList());
        }
    }
}