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
            if (entityId == null) return Configurator.UNKNOWN;
            if (app.States.Any(e => e.EntityId == entityId))
                return app.State(entityId)?.State?.ToString() ?? Configurator.UNKNOWN;
            return Configurator.UNKNOWN;
        }

        public static void LogEventHandled(this INetDaemonRxApp app, HassEventArgs args)
        {
            app.LogInformation("{CorrelationId} - Event Handled: {EventType}", args.CorrelationId, args.EventType);
        }
    }
}