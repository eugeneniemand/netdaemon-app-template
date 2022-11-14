using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HomeAssistantGenerated;
using LightManagerV2;
using Microsoft.Extensions.Logging;
using Moq;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using NetDaemon.HassModel.Mocks;
using NetDaemon.HassModel.Mocks.Moq;
using NetDaemonApps.Tests.Helpers;

namespace NetDaemonApps.Tests;

public class LightManagerTests
{
    [Test]
    public void CircadianSwitchTurnsOnWhenLightTurnsOn()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        config.Room().CircadianSwitchEntity = ctx.GetEntity<SwitchEntity>("switch.circadian");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");

        // Assert
        ctx.VerifyCallService("switch.turn_on", config.Room().CircadianSwitchEntity.EntityId, Times.Once);
    }

    [Test]
    public void GuardTurnsOffLightsLeftOnWhenRoomStateIsOff()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        config.Room().RoomState = "off";
        ctx.TriggerStateChange(config.Light(), "on");
        ctx.InitLightsManager(config);

        // Act        
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.GuardTimeout).Ticks);

        // Assert
        ctx.VerifyLightTurnOff(config.Light(), Times.Once);
    }

    [Test]
    public void HouseModeSetToDayTurnsOffAllNonControlEntitiesAndTurnsOnAnyControlEntitiesThatAreOff()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.NightLight(), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Room().NightTimeEntity, "day");

        // Assert
        ctx.VerifyLightTurnOff(config.NightLight(), Times.Once);
        ctx.VerifyLightTurnOff(config.NightLight(2), Times.Never);
        ctx.VerifyLightTurnOn(config.Light(), Times.Once);
        ctx.VerifyLightTurnOn(config.Light(2), Times.Once);
    }

    [Test]
    public void HouseModeSetToNightTurnsOffAllNonNightControlEntitiesAndTurnsOnAnyNightControlEntitiesThatAreOff()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Light(), "on");
        ctx.TriggerStateChange(config.NightLight(), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Room().NightTimeEntity, "night");

        // Assert
        ctx.VerifyLightTurnOff(config.Light(), Times.Once);
        ctx.VerifyLightTurnOff(config.NightLight(), Times.Never);
        ctx.VerifyLightTurnOn(config.NightLight(), Times.Never);
        ctx.VerifyLightTurnOn(config.NightLight(2), Times.Once);
    }

    [Test]
    public void LightBrightnessChangedManuallyTurnsOffCircadianSwitch()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        config.Room().CircadianSwitchEntity = ctx.GetEntity<SwitchEntity>("switch.circadian");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");


        var oldState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            }
        }.WithAttributes(new LightTurnOnParameters
        {
            Brightness = config.Room().Timeout
        });

        var newState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            }
        }.WithAttributes(new LightTurnOnParameters
        {
            Brightness = 100
        });

        ctx.TriggerStateChange(config.Light().EntityId, oldState, newState);

        // Assert
        ctx.VerifyCallService("switch.turn_off", config.Room().CircadianSwitchEntity.EntityId, Times.Once);
    }

    [Test]
    public void LightBrightnessChangedManuallyTurnsOffCircadianSwitchIfExists()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");


        var oldState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            }
        }.WithAttributes(new LightTurnOnParameters
        {
            Brightness = 10
        });

        var newState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            }
        }.WithAttributes(new LightTurnOnParameters
        {
            Brightness = 100
        });

        ctx.TriggerStateChange(config.Light().EntityId, oldState, newState);

        // Assert
        ctx.VerifyCallService("switch.turn_off", It.IsAny<string>(), Times.Never);
    }

    [Test]
    public void LightColourChangedManuallyTurnsOffCircadianSwitch()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        config.Room().CircadianSwitchEntity = ctx.GetEntity<SwitchEntity>("switch.circadian");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");


        var oldState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            }
        }.WithAttributes(new LightTurnOnParameters
        {
            ColorTemp = 4000L
        });

        var newState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            }
        }.WithAttributes(new LightTurnOnParameters
        {
            ColorTemp = 1000L
        });

        ctx.TriggerStateChange(config.Light().EntityId, oldState, newState);

        // Assert
        ctx.VerifyCallService("switch.turn_off", config.Room().CircadianSwitchEntity.EntityId, Times.Once);
    }


    [Test]
    public void LightDontTurnOnIfOffAndConditionEntityStateIsNotMet()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        config.Room().ConditionEntity      = ctx.GetEntity<SensorEntity>("sensor.condition_entity");
        config.Room().ConditionEntityState = "under";
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");

        // Assert
        ctx.VerifyLightTurnOn(config.Light(), Times.Never);
    }

    [Test]
    public void LightDontTurnsOffIfOnAndManagerIsDisabled()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Light(), "on");
        ctx.TriggerStateChange(config.Pir1(), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.ManagerEnabled(), "off");
        ctx.TriggerStateChange(config.Pir1(), "off");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().Timeout).Ticks);

        // Assert
        ctx.VerifyLightTurnOff(config.Light(), Times.Never);
    }

    [Test]
    public void LightDontTurnsOffIfOnAndRoomIsOccupied()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Light(), "on");
        ctx.TriggerStateChange(config.Pir1(), "on");
        ctx.TriggerStateChange(config.KeepAlive1(), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "off");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().Timeout).Ticks);

        // Assert
        ctx.VerifyLightTurnOff(config.Light(), Times.Never);
    }

    [Test]
    public void LightDontTurnsOnIfOffAndManagerIsDisabled()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.ManagerEnabled(), "off");
        ctx.TriggerStateChange(config.Pir1(), "on");

        // Assert
        ctx.VerifyLightTurnOn(config.Light(), Times.Never);
    }

    [Test]
    public void LightDontTurnsOnIfOffAndToBright()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        config.Room().LuxEntity      = ctx.GetEntity<NumericSensorEntity>("sensor.lux_value", "100");
        config.Room().LuxLimitEntity = ctx.GetEntity<NumericSensorEntity>("sensor.lux_Limit", "10");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");

        // Assert
        ctx.VerifyLightTurnOn(config.Light(), Times.Never);
    }

    [Test]
    public void LightTurnedOffManuallyCancelsOverrideTimeoutOnlyIfAllControlEntitiesAreOff()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Light(2), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });

        ctx.TriggerStateChange(config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "off"
        });
        ctx.TriggerStateChange(config.Light(2), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "off"
        });
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().OverrideTimeout).Ticks);

        // Assert
        ctx.VerifyLightTurnOff(config.Light(2), Times.Never);
    }


    [Test]
    public void LightTurnedOffManuallyDoesNotCancelsOverrideTimeoutIfSomeControlEntitiesAreOn()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Light(2), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });

        ctx.TriggerStateChange(config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "off"
        });
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().OverrideTimeout).Ticks);

        // Assert
        ctx.VerifyLightTurnOff(config.Light(2), Times.Once);
    }

    [Test]
    public void LightTurnedOnManuallyActivatesOverrideTimeout()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().OverrideTimeout).Ticks);

        // Assert
        ctx.VerifyLightTurnOff(config.Light(), Times.Once);
    }


    [Test]
    public void LightTurnsOffWhenPresenceIsOffAfterTimeout()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Light(), "on");
        ctx.TriggerStateChange(config.Pir1(), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "off");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().Timeout).Ticks);
        // Assert
        ctx.VerifyLightTurnOff(config.Light(), Times.Once);
    }

    [Test]
    public void LightTurnsOnWhenPresenceIsOn()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");

        // Assert
        ctx.VerifyLightTurnOn(config.Light(), Times.Once);
    }

    [Test]
    public void MotionAfterLastOffEventCancelsOffEventTimer()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Light(), "on");
        ctx.TriggerStateChange(config.Pir1(), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "off");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().Timeout - 1).Ticks);
        ctx.TriggerStateChange(config.Pir1(), "on");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(1).Ticks);
        // Assert
        ctx.VerifyLightTurnOff(config.Light(), Times.Never);
    }


    [Test]
    public void NightLightTurnsOnWhenPresenceIsOnAndNightModeIsActive()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Room().NightTimeEntity, "night");
        ctx.TriggerStateChange(config.Pir1(), "on");

        // Assert
        ctx.VerifyLightTurnOn(config.NightLight(), Times.Once);
    }

    [Test]
    public void OverriddenLightsObeyOverrideTimeoutRegardlessOfNormalTimeoutAndPresence()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Room().NightTimeEntity, "night");

        ctx.InitLightsManager(config);


        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(20).Ticks);
        ctx.TriggerStateChange(config.Pir1(), "off");
        ctx.TriggerStateChange(config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });

        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().NightTimeout).Ticks);

        // Assert normal timer does nothing
        ctx.VerifyLightTurnOn(config.NightLight(), Times.Once);
        ctx.VerifyLightTurnOff(config.NightLight(), Times.Never);
        ctx.VerifyLightTurnOff(config.Light(), Times.Never);

        // Act
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().OverrideTimeout).Ticks);

        // Assert override timer turns light off
        ctx.VerifyLightTurnOff(config.Light(), Times.Once);
    }

    [Test]
    public void RoomStateIsOffWhenEntitiesTurnOff()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.TriggerStateChange(config.Pir1(), "on");
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "off");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().Timeout).Ticks);

        // Assert
        config.Room().RoomState.Should().Be("off");
    }

    [Test]
    public void RoomStateIsOnWhenEntitiesTurnOn()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.InitLightsManager(config);

        // Act
        ctx.TriggerStateChange(config.Pir1(), "on");

        // Assert
        config.Room().RoomState.Should().Be("on");
    }


    public static void VerifyInputSelect_SelectOption(HaContextMock ctx, string entityId, string option, Times times)
    {
        ctx.Verify(c => c.CallService("sensor", "select_option",
                It.Is<ServiceTarget>(x => x.EntityIds != null && x.EntityIds.First() == entityId),
                It.Is<InputSelectSelectOptionParameters>(x => x.Option == option)), times
        );
    }

    [Test]
    public void WhenOverrideActiveOtherLightsThatAreOffDontTurnOnForPresence()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.InitLightsManager(config);


        // Act
        ctx.TriggerStateChange(config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });
        ctx.TriggerStateChange(config.Pir1(), "on");

        // Assert
        ctx.VerifyLightTurnOn(config.Light(2), Times.Never);
    }

    [Test]
    public void WhenOverrideActivePresenceResetsOverrideTimeout()
    {
        var ctx = new AppTestContext();

        // Arrange
        var config = ManagerConfig(ctx);
        ctx.InitLightsManager(config);


        // Act
        ctx.TriggerStateChange(config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });
        ctx.TriggerStateChange(config.Pir1(), "off");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(900).Ticks);
        ctx.TriggerStateChange(config.Pir1(), "on");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(900).Ticks);
        ctx.VerifyLightTurnOff(config.Light(), Times.Never);
        ctx.TriggerStateChange(config.Pir1(), "off");
        ctx.AdvanceTimeBy(TimeSpan.FromSeconds(config.Room().OverrideTimeout).Ticks);
        // Assert
        ctx.VerifyLightTurnOff(config.Light(), Times.Once);
    }

    private static ManagerConfig ManagerConfig(AppTestContext ctx)
    {
#pragma warning disable CS8604
#pragma warning disable CS8601
        var cfg = new ManagerConfig
        {
            NdUserId           = "ND_USER_ID_1234",
            MinDuration        = "00:05:00",
            MaxDuration        = "00:15:00",
            GuardTimeout       = 301,
            RandomSwitchEntity = ctx.GetEntity<SwitchEntity>("switch.random"),
            Rooms = new List<Manager>
            {
                new()
                {
                    Name = "TestRoom",

                    PresenceEntities      = new List<BinarySensorEntity> { ctx.GetEntity<BinarySensorEntity>("binary_sensor.pir") },
                    ControlEntities       = new List<LightEntity> { ctx.GetEntity<LightEntity>("light.bulb_1"), ctx.GetEntity<LightEntity>("light.bulb_2") },
                    KeepAliveEntities     = new List<BinarySensorEntity> { ctx.GetEntity<BinarySensorEntity>("binary_sensor.keep_alive") },
                    NightControlEntities  = new List<LightEntity> { ctx.GetEntity<LightEntity>("light.bulb_3"), ctx.GetEntity<LightEntity>("light.bulb_4") },
                    NightTimeEntity       = ctx.GetEntity<InputSelectEntity>("input_select.house_mode"),
                    NightTimeEntityStates = new List<string> { "night" },
                    Timeout               = 90,
                    NightTimeout          = 30,
                    OverrideTimeout       = 1800
                }
            }
        };

        foreach (var entity in cfg.Room().PresenceEntities) ctx.TriggerStateChange(entity, "off");
        foreach (var entity in cfg.Room().ControlEntities) ctx.TriggerStateChange(entity, "off");
        foreach (var entity in cfg.Room().NightControlEntities) ctx.TriggerStateChange(entity, "off");
        foreach (var entity in cfg.Room().KeepAliveEntities) ctx.TriggerStateChange(entity, "off");
        cfg.Room().ManagerEnabled = ctx.GetEntity<SwitchEntity>("switch.light_manager_test");
        return cfg;
#pragma warning restore CS8601
#pragma warning restore CS8604
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

    public static BinarySensorEntity KeepAlive1(this ManagerConfig config) => config.Room().KeepAliveEntities.First();
    public static LightEntity Light(this ManagerConfig config, int index = 1) => config.Room().ControlEntities[index - 1];
    public static SwitchEntity ManagerEnabled(this ManagerConfig config) => config.Room().ManagerEnabled;
    public static LightEntity NightLight(this ManagerConfig config, int index = 1) => config.Room().NightControlEntities[index - 1];
    public static BinarySensorEntity Pir1(this ManagerConfig config) => config.Room().PresenceEntities.First();
    public static Manager Room(this ManagerConfig config, int index = 1) => config.Rooms.ToList()[index - 1];
}