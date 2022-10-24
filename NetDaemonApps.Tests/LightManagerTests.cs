using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HomeAssistantGenerated;
using LightManagerV2;
using Microsoft.Extensions.Logging;
using Moq;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel.Entities;
using NetDaemon.HassModel.Mocks.Moq;
using NetDaemonApps.Tests.Helpers;

namespace NetDaemonApps.Tests;

public class LightManagerTests
{
    [Test]
    public void LightDontTurnOnIfOffAndConditionEntityStateIsNotMet()
    {
        var _ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(_ctx);
        config.Room1().ConditionEntity      = _ctx.GetEntity<SensorEntity>("sensor.condition_entity");
        config.Room1().ConditionEntityState = "under";
        _ctx.InitLightsManager(config);

        // Act
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "on");

        // Assert
        _ctx.VerifyCallService("light.turn_on", config.Light1().EntityId, Times.Never, new LightTurnOnParameters());
    }

    [Test]
    public void LightDontTurnsOffIfOnAndManagerIsDisabled()
    {
        var _ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(_ctx);
        _ctx.HaContextMock.TriggerStateChange(config.Light1(), "on");
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "on");
        _ctx.InitLightsManager(config);

        // Act
        _ctx.HaContextMock.TriggerStateChange(config.ManagerEnabled(), "off");
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "off");
        _ctx.AdvanceTimeBy(TimeSpan.FromSeconds(10).Ticks);

        // Assert
        _ctx.VerifyCallService("light.turn_off", config.Light1().EntityId, Times.Never, new LightTurnOffParameters());
    }

    [Test]
    public void LightDontTurnsOffIfOnAndRoomIsOccupied()
    {
        var _ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(_ctx);
        _ctx.HaContextMock.TriggerStateChange(config.Light1(), "on");
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "on");
        _ctx.HaContextMock.TriggerStateChange(config.KeepAlive1(), "on");
        _ctx.InitLightsManager(config);

        // Act
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "off");
        _ctx.AdvanceTimeBy(TimeSpan.FromSeconds(10).Ticks);

        // Assert
        _ctx.VerifyCallService("light.turn_off", config.Light1().EntityId, Times.Never, new LightTurnOffParameters());
    }

    [Test]
    public void LightDontTurnsOnIfOffAndManagerIsDisabled()
    {
        var _ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(_ctx);
        _ctx.InitLightsManager(config);

        // Act
        _ctx.HaContextMock.TriggerStateChange(config.ManagerEnabled(), "off");
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "on");

        // Assert
        _ctx.VerifyCallService("light.turn_on", config.Light1().EntityId, Times.Never, new LightTurnOnParameters());
    }

    [Test]
    public void LightDontTurnsOnIfOffAndToBright()
    {
        var _ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(_ctx);
        config.Room1().LuxEntity      = _ctx.GetEntity<NumericSensorEntity>("sensor.lux_value", "100");
        config.Room1().LuxLimitEntity = _ctx.GetEntity<NumericSensorEntity>("sensor.lux_Limit", "10");
        _ctx.InitLightsManager(config);

        // Act
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "on");

        // Assert
        _ctx.VerifyCallService("light.turn_on", config.Light1().EntityId, Times.Never, new LightTurnOnParameters());
    }


    [Test]
    public void LightTurnsOffWhenPresenceIsOffAfterTimeout()
    {
        var _ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(_ctx);
        _ctx.HaContextMock.TriggerStateChange(config.Light1(), "on");
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "on");
        _ctx.InitLightsManager(config);

        // Act
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "off");
        _ctx.AdvanceTimeBy(TimeSpan.FromSeconds(10).Ticks);
        // Assert
        _ctx.VerifyCallService("light.turn_off", config.Light1().EntityId, Times.Once, new LightTurnOffParameters());
    }

    [Test]
    public void LightTurnsOnWhenPresenceIsOn()
    {
        var _ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(_ctx);
        _ctx.InitLightsManager(config);

        // Act
        _ctx.HaContextMock.TriggerStateChange(config.Pir1(), "on");

        // Assert
        _ctx.VerifyCallService("light.turn_on", config.Light1().EntityId, Times.Once, new LightTurnOnParameters());
    }


    public static void VerifyInputSelect_SelectOption(HaContextMock ctx, string entityId, string option, Times times)
    {
        ctx.Verify(c => c.CallService("sensor", "select_option",
                It.Is<ServiceTarget>(x => x.EntityIds != null && x.EntityIds.First() == entityId),
                It.Is<InputSelectSelectOptionParameters>(x => x.Option == option)), times
        );
    }

    private static ManagerConfig ManagerConfig(AppTestContext _ctx)
    {
        var cfg = new ManagerConfig
        {
            MinDuration        = "00:05:00",
            MaxDuration        = "00:15:00",
            RandomSwitchEntity = _ctx.GetEntity<SwitchEntity>("switch.random"),
            Rooms = new List<Manager>
            {
                new()
                {
                    Name              = "TestRoom",
                    PresenceEntities  = new List<BinarySensorEntity> { _ctx.GetEntity<BinarySensorEntity>("binary_sensor.pir") },
                    ControlEntities   = new List<LightEntity> { _ctx.GetEntity<LightEntity>("light.bulb_1") },
                    KeepAliveEntities = new List<BinarySensorEntity> { _ctx.GetEntity<BinarySensorEntity>("binary_sensor.keep_alive") },
                    Timeout           = 10
                }
            }
        };

        foreach (var entity in cfg.Rooms.First().PresenceEntities) _ctx.HaContextMock.TriggerStateChange(entity, "off");
        foreach (var entity in cfg.Rooms.First().ControlEntities) _ctx.HaContextMock.TriggerStateChange(entity, "off");
        foreach (var entity in cfg.Rooms.First().KeepAliveEntities) _ctx.HaContextMock.TriggerStateChange(entity, "off");
        cfg.Rooms.First().ManagerEnabled = _ctx.GetEntity<SwitchEntity>("switch.light_manager_test");
        return cfg;
    }
}

public static class LightManagerAppTestContextInstanceExtensions
{
    public static LightsManager InitLightsManager(this AppTestContext ctx, ManagerConfig config)
    {
        var loggerFactory     = LoggerFactory.Create(builder => builder.AddFilter("LightManagerV2.LightsManager", LogLevel.Debug).AddSimpleConsole());
        var initLightsManager = new LightsManager(ctx.Scheduler, ctx.HaContext, new Mock<IMqttEntityManager>().Object, new FakeAppConfig<ManagerConfig>(config), loggerFactory.CreateLogger<LightsManager>(), new Mock<ILogger<RandomManager>>().Object);
        initLightsManager.InitializeAsync(new CancellationToken());
        ctx.HaContextMock.TriggerStateChange(config.ManagerEnabled(), "on");
        return initLightsManager;
    }

    public static BinarySensorEntity KeepAlive1(this ManagerConfig config) => config.Rooms.First().KeepAliveEntities.First();

    public static LightEntity Light1(this ManagerConfig config) => config.Rooms.First().ControlEntities.First();
    public static SwitchEntity ManagerEnabled(this ManagerConfig config) => config.Rooms.First().ManagerEnabled;

    public static BinarySensorEntity Pir1(this ManagerConfig config) => config.Rooms.First().PresenceEntities.First();
    public static Manager Room1(this ManagerConfig config) => config.Rooms.First();
}