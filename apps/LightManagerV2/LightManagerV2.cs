#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using NetDaemon.Common;

#endregion

namespace LightManagerV2;

[Focus]
[NetDaemonApp]
public class LightsManager : IInitializable
{
    private readonly ILogger<LightsManager> _managerLogger;
    private readonly ILogger<RandomManager> _randomLogger;

    private RandomManager _randomManager;

    public LightsManager(ILogger<LightsManager> managerLogger, ILogger<RandomManager> randomLogger)
    {
        _managerLogger = managerLogger;
        _randomLogger  = randomLogger;
    }

    public IEnumerable<Manager>? Rooms { get; set; } = new List<Manager>();
    public int GuardTimeout { get; set; } = 900;
    public string MaxDuration { get; set; }
    public string MinDuration { get; set; }
    public string NdUserId { get; set; }
    public SwitchEntity RandomSwitchEntity { get; set; }

    public void Initialize()
    {
        _randomManager = new RandomManager(RandomSwitchEntity, MinDuration, MaxDuration, _randomLogger);
        ( Rooms.Any(r => r.Debug) ? Rooms.Where(r => r.Debug).ToList() : Rooms.ToList() ).ForEach(r => r.Init(_managerLogger, NdUserId, _randomManager));
    }
}

public class RandomManager
{
    private readonly List<List<LightEntity>> _entities = new();
    private readonly ILogger<RandomManager>  _logger;
    private readonly TimeSpan                _max;
    private readonly TimeSpan                _min;
    private readonly CancellationToken       _token;
    private readonly CancellationTokenSource _tokenSource;
    private          List<LightEntity>       _currentEntities;
    private          IEnumerable<string>     _randomStates;


    public RandomManager(SwitchEntity randomSwitchEntity, string min, string max, ILogger<RandomManager> logger)
    {
        RandomSwitchEntity = randomSwitchEntity;
        _logger            = logger;
        _min               = TimeSpan.Parse(min);
        _max               = TimeSpan.Parse(max);
        _tokenSource       = new CancellationTokenSource();
        _token             = _tokenSource.Token;
        SubscribeRandomModeEvent();
    }

    public SwitchEntity RandomSwitchEntity { get; }

    public TimeSpan RandomDuration { get; set; }
    private bool IsRandomMode => _randomStates.Contains(RandomSwitchEntity.State);

    public void AddControlEntities(List<LightEntity> entities, IEnumerable<string> randomStates)
    {
        _entities.Add(entities);
        _randomStates = randomStates;
    }

    public async void StartQueue(bool randomizeList = true)
    {
        while (!_token.IsCancellationRequested)
        {
            _logger.LogInformation("Building Queue");
            var orderedEnumerable = randomizeList ? _entities.OrderBy(o => new Random().Next()).ToList() : _entities;
            foreach (var entities in orderedEnumerable)
            {
                _currentEntities = entities;
                SetRandomDuration();
                foreach (var entity in _currentEntities) entity.TurnOn();
                _logger.LogInformation("Turned On '{entities}' for {randomDuration:T} expiring at {expiry:T}", string.Join(",", entities.Select(e => e.EntityId)), RandomDuration, DateTime.Now + RandomDuration);
                await Task.Delay(RandomDuration, _token).ContinueWith(tsk => { });
                foreach (var entity in _currentEntities) entity.TurnOff();
                if (_token.IsCancellationRequested) return;
            }
        }
    }

    public void StopQueue()
    {
        _tokenSource.Cancel();
        foreach (var entity in _currentEntities) entity.TurnOff();
    }

    private void SetRandomDuration() => RandomDuration = TimeSpan.FromMilliseconds(new Random().Next((int)_min.TotalMilliseconds, (int)_max.TotalMilliseconds));

    private void SubscribeRandomModeEvent()
    {
        _logger.LogInformation("Subscribed to Random Mode Changed Events");
        RandomSwitchEntity?.StateChanges().Subscribe(e =>
        {
            _logger.LogInformation("Random Mode Changed");
            if (IsRandomMode) StartQueue();
            else StopQueue();
        });
    }
}