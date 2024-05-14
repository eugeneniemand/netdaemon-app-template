using LightManagerV2;
using Microsoft.Extensions.Logging;
using NetDaemon.AppModel;
using NetDaemon.Extensions.MqttEntityManager;

namespace Niemand.Tests.LightManager;

public class RandomManagerSut(IHaContext ha, TestScheduler scheduler, StateChangeManager state, IMqttEntityManager entityManager, IAppConfig<ManagerConfig> config, ILogger<LightsManager> managerLogger, ILogger<RandomManager> randomLogger)
{
    
    public void Init(ManagerConfig? configOverride = null)
    {
        var cfg      =  configOverride == null ? config : new FakeAppConfig<ManagerConfig>(configOverride);
        Instance = new RandomManager(scheduler, cfg, randomLogger);
        Instance.InitializeAsync(CancellationToken.None);
        AssertionOptions.FormattingOptions.MaxLines = 1000;
    }

    public ManagerConfig Config => config.Value;
    public TestScheduler Scheduler => scheduler;
    
    public RandomManager Instance { get; private set; }
    
    public TimeSpan RandomDelay
    {
        get => Instance.RandomDelay;
        set => Instance.RandomDelay = value;
    }
}