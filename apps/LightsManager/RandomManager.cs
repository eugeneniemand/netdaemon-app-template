namespace LightManagerV2;

public interface IRandomManager
{
    SwitchEntity RandomSwitchEntity { get; }
    TimeSpan RandomDelay { get; set; }
    void Init(LightEntity entity, IEnumerable<string> randomStates);
    void Init(IEnumerable<LightEntity> entities, IEnumerable<string> randomStates);
    void StartQueue();
    void StopQueue();
}

[NetDaemonApp]
[Focus]
public class RandomManager : IAsyncInitializable, IRandomManager
{
    private readonly ILogger<RandomManager>                                                        _logger;
    private readonly TimeSpan                                                                      _max;
    private readonly TimeSpan                                                                      _min;
    private readonly Random                                                                        _random;
    private readonly bool                                                                          _randomizeList;
    private readonly List<(IEnumerable<LightEntity> _entities, IEnumerable<string> _randomStates)> _rooms = new();
    private readonly IScheduler                                                                    _scheduler;
    private          CancellationToken                                                             _token;
    private          CancellationTokenSource                                                       _tokenSource;
    private          ManagerConfig                                                                 _config;


    public RandomManager(IScheduler scheduler, IAppConfig<ManagerConfig> managerConfig, ILogger<RandomManager> logger, bool randomizeList = true)
    {
        _config            = managerConfig.Value;
        _random            = new Random((int)DateTime.Now.Ticks);
        _scheduler         = scheduler;
        _logger            = logger;
        _randomizeList     = randomizeList;
        _min               = TimeSpan.Parse(_config.MinDuration);
        _max               = TimeSpan.Parse(_config.MaxDuration);
        RandomSwitchEntity = _config.RandomSwitchEntity;
        SubscribeRandomModeEvent();
    }

    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        ( _config.Rooms.Any(r => r.Debug)
                ? _config.Rooms.Where(r => r.Debug).ToList()
                : _config.Rooms.ToList() )
            .ForEach(r => _rooms.Add(( r.ControlEntities, r.RandomStates )));
        return Task.CompletedTask;
    }

    public void Init(LightEntity entity, IEnumerable<string> randomStates)
    {
        Init(new List<LightEntity> { entity }, randomStates);
    }

    public void Init(IEnumerable<LightEntity> entities, IEnumerable<string> randomStates)
    {
        _rooms.Add(( entities, randomStates ));
    }

    public TimeSpan RandomDelay { get; set; }

    public SwitchEntity RandomSwitchEntity { get; }


    public async void StartQueue()
    {
        _logger.LogInformation("StartQueue method started.");

        using var source = _tokenSource = new CancellationTokenSource();
        _token = source.Token;
        try
        {
            while (!_token.IsCancellationRequested)
            {
                var entities_randomStates = BuildQueue();
                foreach (var tuple in entities_randomStates)
                {
                    _token.ThrowIfCancellationRequested();
                    if (_token.IsCancellationRequested) return;
                    
                    SetRandomDuration();
                    await HandleEntities(tuple._entities.ToList());
                }

                await _scheduler.Sleep(TimeSpan.FromSeconds(1), _token);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the StartQueue method.");
        }
        finally
        {
            _logger.LogInformation("StartQueue method ended.");
        }
    }


    /// <summary>
    /// The BuildQueue method in the RandomManager class is responsible for building a queue of entities that have random states configured matching the current random switch entity state. 
    /// </summary>
    private List<(IEnumerable<LightEntity> _entities, IEnumerable<string> _randomStates)> BuildQueue()
    {
        // Log the start of the queue building process
        _logger.LogInformation("Building Queue");

        // Order the rooms list in a random order if _randomizeList is true, otherwise use the rooms list as is
        var orderedRooms = _randomizeList ? _rooms.OrderBy(o => _random.Next()).ToList() : _rooms;

        // Find all entities that have random states configured matching the current random switch entity state
        var entitiesWithRandomStates = orderedRooms.Where(t => t._randomStates.Contains(RandomSwitchEntity.State)).ToList();

        // Log the enabled random entities
        _logger.LogInformation("Random entities enabled: {entities}", string.Join("\n", entitiesWithRandomStates.SelectMany(tuple => tuple._entities.Select(e => e.EntityId))));

        // Return the list of entities with their corresponding random states
        return entitiesWithRandomStates;
    }

    /// <summary>
    /// This method is responsible for turning on the entities, waiting for a random delay, and then turning off the entities. It also logs information about these operations and handles any exceptions that occur during the process.
    /// </summary>
    /// <param name="currentEntities"></param>
    private async Task HandleEntities(List<LightEntity> currentEntities)
    {
        _logger.LogInformation("Handling Entities '{entities}'", string.Join(",", currentEntities.Select(e => e.EntityId)));
        foreach (var entity in currentEntities) entity.TurnOn();
        _logger.LogInformation("Turned On '{entities}' for {randomDuration:T} expiring at {expiry:T}", string.Join(",", currentEntities.Select(e => e.EntityId)), RandomDelay, DateTime.Now + RandomDelay);

        try
        {
            _logger.LogDebug("Waiting for {randomDuration:T}", RandomDelay);
            _token.ThrowIfCancellationRequested();
            await _scheduler.Sleep(RandomDelay, _token);
            _logger.LogDebug("Waited for {randomDuration:T}", RandomDelay);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Cancellation requested. Turning off entities immediately.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing the queue.");
        }

        _logger.LogInformation("Turning Off '{entities}'", string.Join(",", currentEntities.Select(e => e.EntityId)));
        foreach (var entity in currentEntities) entity.TurnOff();
        _logger.LogInformation("Turned Off '{entities}'", string.Join(",", currentEntities.Select(e => e.EntityId)));
    }

    public void StopQueue()
    {
        _logger.LogInformation("StopQueue method started.");
        _tokenSource?.Cancel();
    }

    private void SetRandomDuration() => RandomDelay = TimeSpan.FromMilliseconds(_random.Next((int)_min.TotalMilliseconds, (int)_max.TotalMilliseconds));

    private void SubscribeRandomModeEvent()
    {
        _logger.LogInformation("Subscribed to Random Mode Changed Events");
        RandomSwitchEntity?.StateChanges().Subscribe(e =>
        {
            _logger.LogInformation("Random Mode Changed");
            if (_rooms.Any(t => t._randomStates.Contains(RandomSwitchEntity.State, StringComparer.OrdinalIgnoreCase))) StartQueue();
            else StopQueue();
        });
    }
}