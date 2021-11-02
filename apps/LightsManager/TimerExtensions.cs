using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightsManager;
using NetDaemon.Common.Reactive;

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
    }
}