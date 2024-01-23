using LightManagerV2;

namespace Niemand.Tests.LightManager;

public static class LightManagerTestsExtensions
{
    public static BinarySensorEntity KeepAlive1(this ManagerConfig config) => config.Room().KeepAliveEntities.First();
    public static LightEntity Light(this ManagerConfig config, int index = 1) => config.Room().ControlEntities[index - 1];
    public static SwitchEntity ManagerEnabled(this ManagerConfig config) => config.Room().ManagerEnabled;
    public static LightEntity NightLight(this ManagerConfig config, int index = 1) => config.Room().NightControlEntities[index - 1];
    public static BinarySensorEntity Pir1(this ManagerConfig config) => config.Room().PresenceEntities.First();
    public static Manager Room(this ManagerConfig config, int index = 1) => config.Rooms.ToList()[index - 1];
}