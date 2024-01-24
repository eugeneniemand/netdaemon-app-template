using LightManagerV2;
using Microsoft.Extensions.Logging;
using NetDaemon.AppModel;
using NetDaemon.Extensions.MqttEntityManager;

namespace Niemand.Tests.LightManager;

public class LightManagerSut(IHaContext ha, TestScheduler scheduler, StateChangeManager state, IMqttEntityManager entityManager, IAppConfig<ManagerConfig> config, ILogger<LightsManager> managerLogger, ILogger<RandomManager> randomLogger)
{
    
    public void Init(ManagerConfig? configOverride = null)
    {
        var cfg      =  configOverride == null ? config : new FakeAppConfig<ManagerConfig>(configOverride);
        var instance = new LightsManager(scheduler, ha, entityManager, cfg, managerLogger, randomLogger);
        instance.InitializeAsync(new CancellationToken());
        state.Change(config.Value.ManagerEnabled(), "on");
        AssertionOptions.FormattingOptions.MaxLines = 1000;
    }

    public ManagerConfig Config => config.Value;
    public TestScheduler Scheduler => scheduler;
}