using System.Reactive.Concurrency;
//using AwesomeNetdaemon.Services;
using AwesomeNetdaemon.Test.TestUtils;
using LightManagerV2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel;
using Xunit.DependencyInjection.Logging;
using Microsoft.Extensions.Logging.Testing;
using NetDaemon.AppModel;
using NetDaemon.Extensions.MqttEntityManager;
using Niemand.Tests.Mocks;
using Xunit.DependencyInjection;

namespace AwesomeNetdaemon.Test;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(x => x.AddXunitOutput());
        services.AddTransient<Entities>();
        services.AddTransient<IServices, HomeAssistantGenerated.Services>();
        services.AddNetDaemonScheduler();
        services.AddScoped<IHaContext, HaContextMockImpl>();
        services.AddScoped<TestScheduler>();
        services.AddTransient<IScheduler>(s => s.GetRequiredService<TestScheduler>());
        services.AddTransient<StateChangeManager>();
        //services.AddTransient<TestEntityBuilder>();
        //services.AddTransient<IAppConfig<ManagerConfig>, MqttEntityManagerMock>();
        services.AddTransient<IMqttEntityManager, MqttEntityManagerMock>();
        services.AddTransient<ILogger<LightsManager>, FakeLogger<LightsManager>>();
        //services.AddTransient<DetectProgramByPowerUsageBuilder>();
        //services.AddTransient<DetectProgramByPowerUsageBuilderFactory>();
    }
}