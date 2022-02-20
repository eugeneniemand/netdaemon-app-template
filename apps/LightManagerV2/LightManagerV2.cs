#region

using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;
using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using NetDaemon.AppModel;

#endregion

namespace LightManagerV2;

[Focus]
[NetDaemonApp]
public class LightsManager : IAsyncInitializable
{
    private readonly ILogger<LightsManager> _managerLogger;
    private readonly ILogger<RandomManager> _randomLogger;
    private readonly IScheduler             _scheduler;

    private RandomManager _randomManager;

    public LightsManager()
    {
    }

    public LightsManager(IScheduler scheduler, ILogger<LightsManager> managerLogger, ILogger<RandomManager> randomLogger)
    {
        _scheduler     = scheduler;
        _managerLogger = managerLogger;
        _randomLogger  = randomLogger;
    }

    public IEnumerable<Manager>? Rooms { get; set; } = new List<Manager>();
    public int GuardTimeout { get; set; } = 900;
    public string MaxDuration { get; set; }
    public string MinDuration { get; set; }
    public string NdUserId { get; set; }
    public SwitchEntity RandomSwitchEntity { get; set; }

    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        _randomManager = new RandomManager(_scheduler, RandomSwitchEntity, MinDuration, MaxDuration, _randomLogger);
        ( Rooms.Any(r => r.Debug) ? Rooms.Where(r => r.Debug).ToList() : Rooms.ToList() ).ForEach(r => r.Init(_managerLogger, NdUserId, _randomManager, _scheduler));
        return null;
    }
}