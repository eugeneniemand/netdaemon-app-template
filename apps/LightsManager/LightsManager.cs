#region

using NetDaemon.Extensions.MqttEntityManager;

#endregion

namespace LightManagerV2;

//[Focus]
[NetDaemonApp]
public class LightsManager(IScheduler scheduler, IHaContext haContext, IServices services, IMqttEntityManager entityManager, IAppConfig<ManagerConfig> config, ILogger<LightsManager> managerLogger) : IAsyncInitializable
{
    private readonly ManagerConfig _config = config.Value;

    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        ( _config.Rooms.Any(r => r.Debug)
                ? _config.Rooms.Where(r => r.Debug).ToList()
                : _config.Rooms.ToList() )
            .ForEach(async r => await r.Init(managerLogger, _config.NdUserId, scheduler, haContext, services, entityManager, _config.GuardTimeout));
        return Task.CompletedTask;
    }
}