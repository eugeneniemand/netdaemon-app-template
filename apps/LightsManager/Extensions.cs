using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightManager;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;

namespace LightManager;

public static class Extensions
{
    public static string EntityState(this IHaContext app, string? entityId)
    {
        if (entityId == null) return LightsManager.UNKNOWN;
        return app.GetState(entityId)?.State ?? LightsManager.UNKNOWN;
    }

    public static List<string> ToEntityIds(this IEnumerable<Entity> entities)
    {
        return entities.Select(e => e.EntityId).ToList();
    }

    public static string ToEntityIdsString(this IEnumerable<Entity> entities)
    {
        return string.Join(", ", entities.Select(e => e.EntityId).ToList());
    }
}