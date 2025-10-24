#region

using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.Extensions.Scheduler;
using HomeAssistantGenerated;
using System.Linq;

#endregion

namespace LightManagerV2;

[Focus]
[NetDaemonApp]
public class LightsManager(IScheduler scheduler, IHaContext haContext, IServices services, IMqttEntityManager entityManager, IAppConfig<ManagerConfig> config, ILogger<LightsManager> managerLogger) : IAsyncInitializable
{
    private readonly ManagerConfig _config = config.Value;

    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        (_config.Rooms.Any(r => r.Debug)
                ? _config.Rooms.Where(r => r.Debug).ToList()
                : _config.Rooms.ToList())
            .ForEach(async r => await InitRoom(r));

        scheduler.ScheduleCron("0 7,15 * * *", () => haContext.GetAllEntities()
            .Where(e => e.EntityId.Contains("switch.light_manager_"))
            .ToList()
            .ForEach(e => ResetLightManagers(e)));

        return Task.CompletedTask;
    }

    private void ResetLightManagers(Entity e)
    {
        managerLogger.LogInformation("Resetting all light managers");
        ((ISwitchEntityCore)e).TurnOn();
    }

    private async Task InitRoom(Manager r)
    {
        await r.Init(managerLogger, _config.NdUserId, scheduler, haContext, services, entityManager, _config.GuardTimeout);
    }
}