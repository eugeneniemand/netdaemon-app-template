#region

using NetDaemon.Extensions.MqttEntityManager;

#endregion

namespace LightManagerV2;

[Focus]
[NetDaemonApp]
public class LightsManager : IAsyncInitializable
{
    private readonly ManagerConfig          _config;
    private readonly IMqttEntityManager     _entityManager;
    private readonly IHaContext             _haContext;
    private readonly ILogger<LightsManager> _managerLogger;
    private readonly ILogger<RandomManager> _randomLogger;
    private readonly IScheduler             _scheduler;
    private          RandomManager          _randomManager;

    public LightsManager(IScheduler scheduler, IHaContext haContext, IMqttEntityManager entityManager, IAppConfig<ManagerConfig> config, ILogger<LightsManager> managerLogger, ILogger<RandomManager> randomLogger)
    {
        _scheduler     = scheduler;
        _haContext     = haContext;
        _entityManager = entityManager;
        _config        = config.Value;
        _managerLogger = managerLogger;
        _randomLogger  = randomLogger;
    }

    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        try
        {
            _randomManager = new RandomManager(_scheduler, _config.RandomSwitchEntity, _config.MinDuration, _config.MaxDuration, _randomLogger);
            ( _config.Rooms.Any(r => r.Debug)
                    ? _config.Rooms.Where(r => r.Debug).ToList()
                    : _config.Rooms.ToList() )
                .ForEach(async r =>
                {
                    //foreach (var controlEntity in r.PresenceEntities)
                    //    _haContext.StateAllChanges().Where(s => s.Entity.EntityId == controlEntity.EntityId).Subscribe(s =>
                    //        _managerLogger.LogDebug("StateChange for {room} : {entity} from {oldSate} to state {newState}", r.Name, s?.New?.EntityId, s?.Old?.State, s?.New?.State)
                    //    );
                    await r.Init(_managerLogger, _config.NdUserId, _randomManager, _scheduler, _haContext, _entityManager);
                });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _managerLogger.LogError(e, "Error Occurred");
        }

        return null;
    }
}