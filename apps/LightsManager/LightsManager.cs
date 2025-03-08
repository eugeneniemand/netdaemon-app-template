#region

using NetDaemon.Extensions.MqttEntityManager;

#endregion

namespace LightManagerV2;

// [Focus]
[NetDaemonApp]
public class LightsManager : IAsyncInitializable
{
    private readonly ManagerConfig          _config;
    private readonly IMqttEntityManager     _entityManager;
    private readonly IHaContext             _haContext;
    private readonly ILogger<LightsManager> _managerLogger;
    //private readonly ILogger<RandomManager> _randomLogger;
    private readonly IScheduler             _scheduler;

    public LightsManager(IScheduler scheduler, IHaContext haContext, IMqttEntityManager entityManager, IAppConfig<ManagerConfig> config, ILogger<LightsManager> managerLogger)
    {
        _scheduler     = scheduler;
        _haContext     = haContext;
        _entityManager = entityManager;
        _config        = config.Value;
        _managerLogger = managerLogger;
    }

    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        ( _config.Rooms.Any(r => r.Debug)
                ? _config.Rooms.Where(r => r.Debug).ToList()
                : _config.Rooms.ToList() )
            .ForEach(async r => await r.Init(_managerLogger, _config.NdUserId, _scheduler, _haContext, _entityManager, _config.GuardTimeout));
        return Task.CompletedTask;
    }
}