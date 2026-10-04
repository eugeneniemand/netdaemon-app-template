using LightManagerV2;
using NetDaemon.Extensions.Testing;
using NetDaemon.HassModel.Entities;
using System.Collections.ObjectModel;

namespace Niemand.Tests.LightManager;

public static class LightManagerTestsExtensions
{
    public static BinarySensorEntity KeepAlive1(this ManagerConfig config) => config.Room().KeepAliveEntities.First();
    public static LightEntity Light(this ManagerConfig config, int index = 1) => config.Room().ControlEntities[index - 1];
    public static SwitchEntity ManagerEnabled(this ManagerConfig config) => config.Room().ManagerEnabled;
    public static LightEntity NightLight(this ManagerConfig config, int index = 1) => config.Room().NightControlEntities[index - 1];
    public static BinarySensorEntity Pir1(this ManagerConfig config) => config.Room().PresenceEntities.First();
    public static Manager Room(this ManagerConfig config, int index = 1) => config.Rooms.ToList()[index - 1];

    public static StateChangeManager TriggerPresence(this StateChangeManager state, BinarySensorEntity entity, bool isOn) =>
        state.Change(entity, isOn ? "on" : "off");

    public static StateChangeManager SetHouseMode(this StateChangeManager state, InputSelectEntity entity, string mode) =>
        state.Change(entity, mode);

    public static StateChangeManager TurnOnManually(this StateChangeManager state, LightEntity light, string userId = "EUGENE", object? attributes = null)
    {
        var entityState = new EntityState
        {
            EntityId = light.EntityId,
            Context = new Context { UserId = userId },
            State = "on"
        };

        if (attributes != null)
            entityState = entityState.WithAttributes(attributes);

        return state.Change(light, entityState);
    }

    public static StateChangeManager TurnOffManually(this StateChangeManager state, LightEntity light, string userId = "EUGENE", object? attributes = null)
    {
        var entityState = new EntityState
        {
            EntityId = light.EntityId,
            Context = new Context { UserId = userId },
            State = "off"
        };

        if (attributes != null)
            entityState = entityState.WithAttributes(attributes);

        return state.Change(light, entityState);
    }

    public static int CountMatching(this IEnumerable<TestServiceCall> calls, Domain domain, string service, string entityId)
    {
        return calls.Count(c =>
            string.Equals(c.Domain, domain.ToString(), StringComparison.OrdinalIgnoreCase)
            && string.Equals(c.Service, service, StringComparison.OrdinalIgnoreCase)
            && c.Target?.EntityIds?.Contains(entityId, StringComparer.OrdinalIgnoreCase) == true);
    }

    public static int CountMatching(this ReadOnlyCollection<TestServiceCall> calls, Domain domain, string service, string entityId) =>
        calls.AsEnumerable().CountMatching(domain, service, entityId);
}