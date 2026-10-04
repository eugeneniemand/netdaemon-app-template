//using System;
//using System.Reactive.Concurrency;
//using System.Security.Cryptography;
//using AutomationPipelines;
//using HomeAssistantGenerated;
//using NetDaemon.Extensions.Observables;

[NetDaemonApp]
//[Focus]
public class LoungeApp(Entities entities, Services services, ILogger<LoungeApp> logger, IScheduler scheduler) : IAsyncInitializable
{
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        entities.MediaPlayer.Lounge.StateChanges()
            .WhenStateIsFor(s => s.State == "idle", TimeSpan.FromMinutes(15), scheduler)
            .Subscribe(s =>
            {
                entities.MediaPlayer.LoungeTv.TurnOff();
            });
    }
}

//public class VolumeChangedNode : PipelineNode<bool>
//{
//    private readonly ILogger<VolumeChangedNode> logger;

//    public VolumeChangedNode(Entities entities, ILogger<VolumeChangedNode> logger)
//    {
//        var tv = entities.MediaPlayer.LoungeTv;
//        tv.StateAllChanges().Subscribe(s =>
//        {
//            if (s.Old?.Attributes == null || s.New?.Attributes == null)
//            {
//                logger.LogDebug("Attributes are null, disable node");
//                DisableNode();
//            }
//            else if (s.Old.Attributes.VolumeLevel != s.New.Attributes.VolumeLevel)
//            {
//                logger.LogDebug($"Volume changed from {s.Old.Attributes.VolumeLevel} to {s.New.Attributes.VolumeLevel}");
//                Output = false;
//            }
//            else
//                DisableNode();
//        });
//        this.logger = logger;
//    }

//    protected override void InputReceived(bool state)
//    {
//        logger.LogDebug($"Input received: {state}");
//        base.InputReceived(state);
//    }
//}

//public class LightChangedNode : PipelineNode<bool>
//{
//    private readonly ILogger<LightChangedNode> logger;

//    public LightChangedNode(LightEntities lights, ILogger<LightChangedNode> logger)
//    {
//        lights.OfficeSkylight.SubscribeOnOff(
//            () =>
//            {
//                logger.LogInformation("Light turned on. Output set to true");
//                Output = true;
//            }, () =>
//            {
//                logger.LogInformation("Light turned off. Output set to false");
//                Output = false;
//            });
//        this.logger = logger;
//    }

//    protected override void InputReceived(bool state)
//    {
//        logger.LogDebug($"Input received: {state}");
//        base.InputReceived(state);
//    }
//}

//public class OfficeLightChangedNode : PipelineNode<bool>
//{
//    private readonly ILogger<OfficeLightChangedNode> logger;

//    public OfficeLightChangedNode(LightEntities lights, ILogger<OfficeLightChangedNode> logger)
//    {
//        lights.Office.SubscribeOnOff(
//            () =>
//            {
//                logger.LogInformation("Light turned on. Output set to true");
//                Output = true;
//            }, () =>
//            {
//                logger.LogInformation("Light turned off. Output set to false");
//                Output = false;
//            });
//        this.logger = logger;
//    }

//    protected override void InputReceived(bool state)
//    {
//        logger.LogDebug($"Input received: {state}");
//        base.InputReceived(state);
//    }
//}

//public class LoungeApp(Entities entities, Services services, ILogger<LoungeApp> logger, IScheduler scheduler) : IAsyncInitializable
//{
//    public async Task InitializeAsync(CancellationToken cancellationToken)
//    {
//        var tv = entities.MediaPlayer.LoungeTv;
//        var source = tv.Attributes.Source;
//        var gameConsoles = new List<string>() { "PS3", "XBOX", "Nintendo" };
//        IDisposable turnOffTimer = null;

//        var rxRate = entities.Sensor.LgLoungeRx;
//        var txRate = entities.Sensor.LgLoungeTx;
//        // Combine rx and tx rates into total bandwidth
//        var totalBandwidth = rxRate.StateChanges()
//            .CombineLatest(txRate.StateChanges(), (rx, tx) =>
//            {
//                double? rxValue = rx?.New?.State ?? 0f;
//                double? txValue = tx?.New?.State ?? 0f;
//                return (double)(rxValue + txValue);
//            })
//            .DistinctUntilChanged(bandwidth => Math.Round(bandwidth, 3));

//        // Detect idle state: bandwidth < 0.1 Mbps for 15 minutes
//        List<double>? bufferList = new();
//        totalBandwidth
//            .Buffer(TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(5))
//            .Where(buffer => buffer.Count > 0 && buffer.All(b => b < 0.1))
//            .Do(buffer => bufferList = (List<double>)buffer)
//            .Subscribe(_ =>
//            {
//                var tv = entities.MediaPlayer.LoungeTv;
//                // Check if TV is on and not in use by games consoles
//                if (tv.IsOn() && !gameConsoles.Contains(source))
//                {
//                    logger.LogDebug($"Buffer contents: [{string.Join(", ", bufferList.Select(b => b.ToString("F3")))}]");
//                    tv.TurnOff();
//                    logger.LogInformation("TV turning off due to idle state (bandwidth < 0.1 Mbps for 15 minutes).");
//                }
//            });

//        scheduler.SchedulePeriodic(TimeSpan.FromMinutes(30), () =>
//        {
//            if (tv.IsOn() && gameConsoles.Contains(source))
//            {
//                services.Notify.LoungeTv(new NotifyLoungeTvParameters() { Message = "TV turning off in 1 minutes. Adjust volume to cancel" });
//                logger.LogInformation("Schedule TV to be turned off due to idle state");
//                turnOffTimer = scheduler.Schedule(TimeSpan.FromMinutes(1), () =>
//                {
//                    tv.TurnOff();
//                    logger.LogInformation("TV turned off due to idle state");
//                });
//            }
//        });

//        tv.StateAllChanges().Subscribe(s =>
//        {
//            source = tv.Attributes.Source;
//            if (s.Old.Attributes.VolumeLevel != s.New.Attributes.VolumeLevel)
//            {
//                logger.LogDebug($"TV Turn Off Timer Cancelled. Volume changed from {s.Old.Attributes.VolumeLevel} to {s.New.Attributes.VolumeLevel}");
//                turnOffTimer?.Dispose();
//            }
//        });
//    }
//}

//public class LoungeApp(Entities entities, Services services, ILogger<LoungeApp> logger, IScheduler scheduler, IPipeline<bool> TurnTvOffPipeline) : IAsyncInitializable
//{`
//    public async Task InitializeAsync(CancellationToken cancellationToken)
//    {
//        IDisposable turnOffTimer = null;
//        TurnTvOffPipeline
//            .SetDefault(true)
//            //.RegisterNode<BandwidthUsageNode>()
//            //.RegisterNode<GameConsoleExclusionNode>()
//            .RegisterNode<VolumeChangedNode>()
//            .SetOutputHandler(turnOff =>
//            {
//                logger.LogDebug("Ouput Handler: turnOff={turnOff}", turnOff);
//                if (turnOff)
//                {
//                    services.Notify.LoungeTv(new NotifyLoungeTvParameters() { Message = "Turning off in 1 minute, change volume to cancel." });
//                    turnOffTimer = scheduler.Schedule(TimeSpan.FromMinutes(1), () =>
//                    {
//                        var tv = entities.MediaPlayer.LoungeTv;
//                        //tv.TurnOff();
//                        logger.LogInformation("TV turned off due to idle state");
//                    });
//                }
//                else
//                {
//                    turnOffTimer?.Dispose();
//                    logger.LogDebug($"TV turn off cancelled due to volume change");
//                }
//            });
//    }
//}

//public class GameConsoleExclusionNode : PipelineNode<bool>
//{
//    private readonly IEnumerable<string> _gameConsoles = new List<string>() { "PS3", "XBOX", "Nintendo" };

//    public GameConsoleExclusionNode(Entities entities, IScheduler scheduler, ILogger<GameConsoleExclusionNode> logger)
//    {
//        void IsUsingGamesConsole(Entities entities, ILogger<GameConsoleExclusionNode> logger)
//        {
//            var tv = entities.MediaPlayer.LoungeTv;
//            if (_gameConsoles.Contains(tv.Attributes?.Source))
//            {
//                logger.LogInformation("Schedule TV to be turned off due to idle state");
//                Output = false;
//            }
//            else
//            {
//                logger.LogDebug($"Source {tv.Attributes?.Source} not in games consoles, disable node");
//                DisableNode();
//            }
//        }
//        IsUsingGamesConsole(entities, logger);
//        scheduler.SchedulePeriodic(TimeSpan.FromMinutes(1), () =>
//        {
//            IsUsingGamesConsole(entities, logger);
//        });
//    }
//}



//public class BandwidthUsageNode : PipelineNode<bool>
//{
//    public BandwidthUsageNode(Entities entities, ILogger<BandwidthUsageNode> logger)
//    {
//        var rxRate = entities.Sensor.LgLoungeRx;
//        var txRate = entities.Sensor.LgLoungeTx;
//        // Combine rx and tx rates into total bandwidth
//        var totalBandwidth = rxRate.StateChanges()
//            .CombineLatest(txRate.StateChanges(), (rx, tx) =>
//            {
//                double? rxValue = rx?.New?.State ?? 0f;
//                double? txValue = tx?.New?.State ?? 0f;
//                return (double)(rxValue + txValue);
//            })
//            .DistinctUntilChanged(bandwidth => Math.Round(bandwidth, 3));

//        // Detect idle state: bandwidth < 0.1 Mbps for 15 minutes
//        List<double>? bufferList = new();
//        totalBandwidth
//            .Buffer(TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(5))
//            .Where(buffer => buffer.Count > 0 && buffer.All(b => b < 0.1))
//            .Do(buffer => bufferList = (List<double>)buffer)
//            .Subscribe(_ =>
//            {
//                //var tv = entities.MediaPlayer.LoungeTv;
//                //// Check if TV is on and not in use by games consoles
//                //if (tv.IsOn())
//                //{
//                //logger.LogDebug($"Buffer contents: [{string.Join(", ", bufferList.Select(b => b.ToString("F3")))}]");
//                Output = true;
//                logger.LogInformation("TV turning off due to idle state (bandwidth < 0.1 Mbps for 15 minutes).");
//                //}
//            });
//    }
//}

