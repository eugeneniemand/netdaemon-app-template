using System.Reactive.Concurrency;
using LightManagerV2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using NetDaemon.AppModel;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.Helpers;
using Niemand.Tests.DisciplineManager;
using Niemand.Tests.LightManager;
using Niemand.Tests.Mocks;
using Xunit.DependencyInjection.Logging;

namespace Niemand.Tests;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(x => x.AddXunitOutput());
        services.AddTransient<IEntities, Entities>();
        services.AddTransient<IServices, Services>();
        services.AddNetDaemonScheduler();
        services.AddScoped<IHaContext, HaContextMockImpl>();
        services.AddScoped<TestScheduler>();
        services.AddTransient<IScheduler>(s => s.GetRequiredService<TestScheduler>());
        services.AddTransient<StateChangeManager>();
        services.AddTransient<TestEntityBuilder>();
        services.AddTransient<IMqttEntityManager, MqttEntityManagerMock>();
        services.AddTransient<AlexaMock>();
        services.AddTransient<IAlexa>(s => s.GetRequiredService<AlexaMock>());
        // Register a single FakeLogCollector and use FakeLogger<T> as the ILogger<T> implementation so tests can inspect logs
        services.AddSingleton<FakeLogCollector>();
        services.AddSingleton(typeof(ILogger<>), typeof(FakeLogger<>));        
        services.AddTransient<IAppConfig<ManagerConfig>>(s => new FakeAppConfig<ManagerConfig>(GetManagerConfig(s.GetRequiredService<StateChangeManager>(), s.GetRequiredService<TestEntityBuilder>())));
        services.AddTransient<INotificationConfigFactory, NotificationConfigFactory>();
        services.AddTransient<IApplianceFactory, ApplianceFactory>();
        services.AddTransient<LightManagerSut>();
        services.AddTransient<KitchenSut>();
        services.AddTransient<MorningKidsSut>();
        services.AddTransient<RoutinesSut>();
        services.AddTransient<NotificationManagerSut>();
        services.AddTransient<People>();
        services.AddTransient<TimerManager>();
        services.AddTransient<ScreenTimeSut>();
        services.AddTransient<KidsChoresManagerSut>();
    }

    private static ManagerConfig GetManagerConfig(StateChangeManager state, TestEntityBuilder entityBuilder)
    {
//#pragma warning disable CS8604
//#pragma warning disable CS8601
        var cfg = new ManagerConfig
        {
            NdUserId           = "ND_USER_ID_1234",
            MinDuration        = "00:05:00",
            MaxDuration        = "00:15:00",
            GuardTimeout       = 301,
            RandomSwitchEntity = entityBuilder.CreateSwitchEntity("switch.random"),
            Rooms =
            [
                new()
                {
                    Name = "TestRoom",

                    PresenceEntities      = [entityBuilder.CreateBinarySensorEntity("binary_sensor.pir")],
                    ControlEntities       = [entityBuilder.CreateLightEntity("light.bulb_1"), entityBuilder.CreateLightEntity("light.bulb_2")],
                    KeepAliveEntities     = [entityBuilder.CreateBinarySensorEntity("binary_sensor.keep_alive")],
                    NightControlEntities  = [entityBuilder.CreateLightEntity("light.bulb_3"), entityBuilder.CreateLightEntity("light.bulb_4")],
                    NightTimeEntity       = entityBuilder.CreateInputSelectEntity("input_select.house_mode"),
                    NightTimeEntityStates = ["night"],
                    Timeout               = 90,
                    NightTimeout          = 30,
                    OverrideTimeout       = 1800,
                    RandomStates          = { "armed_away", "armed_night" }
                }
            ]
        };

        foreach (var entity in cfg.Room().PresenceEntities) state.Change(entity, "off");
        foreach (var entity in cfg.Room().ControlEntities) state.Change(entity, "off");
        foreach (var entity in cfg.Room().NightControlEntities) state.Change(entity, "off");
        foreach (var entity in cfg.Room().KeepAliveEntities) state.Change(entity, "off");
        cfg.Room().ManagerEnabled = entityBuilder.CreateSwitchEntity("switch.light_manager_test");
        return cfg;
//#pragma warning restore CS8601
//#pragma warning restore CS8604
    }
}