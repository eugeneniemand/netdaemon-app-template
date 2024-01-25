using NetDaemon.HassModel.Entities;

namespace Niemand.Tests.LightManager;

public class LightManagerFacts(LightManagerSut sut, StateChangeManager state, TestEntityBuilder entityBuilder)
{
    // Watchdog should ignore lights while room state is on. It turned off kitchen lights while I was making coffee exactly on the hour 22:00
    // Only turn on Adaptive lights when AL is off


    [Fact]
    public void CircadianSwitchTurnsOnWhenLightTurnsOn()
    {
        // Arrange
        sut.Config.Room().CircadianSwitchEntity = entityBuilder.CreateSwitchEntity("switch.circadian", "off");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Switch.TurnOn(sut.Config.Room().CircadianSwitchEntity!)
        );
    }

    [Fact]
    public void GuardTurnsOffLightsLeftOnWhenRoomStateIsOff()
    {
        // Arrange
        sut.Config.Room().RoomState = "off";
        state.Change(sut.Config.Light(), "on");
        sut.Init();

        // Act  
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.GuardTimeout).Ticks);

        // Assert 
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    [Fact]
    public void ControlEntitiesDontTurnOffWhenConditionIsNotMet()
    {
        // Arrange
        sut.Config.Room().ConditionEntity      = entityBuilder.CreateEntity<SensorEntity>("sensor.sun", "above_horizon");
        sut.Config.Room().ConditionEntityState = "below_horizon";
        state.Change(sut.Config.Light(), "on");
        state.Change(sut.Config.Pir1(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert 
        state.ServiceCalls.Filter(Domain.Light).Should().BeEmpty();
    }

    [Fact]
    public void CircadianSwitchIsTurnedOnWhenLightsTurnOffAfterTimeout()
    {
        // Arrange
        sut.Config.Room().CircadianSwitchEntity = entityBuilder.CreateEntity<SwitchEntity>("switch.adaptive_lighting", "off");
        state.Change(sut.Config.Light(), "on");
        state.Change(sut.Config.Pir1(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert 
        state.ServiceCalls.Filter(Domain.Switch).Should().ContainEquivalentOf(
            Events.Switch.TurnOn(sut.Config.Room().CircadianSwitchEntity!)
        );
    }

    [Fact]
    public void ControlEntitiesDontTurnOffWhenOverrideIsActive()
    {
        // Arrange
        state.Change(sut.Config.Light(), "off");
        state.Change(sut.Config.Pir1(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });

        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert 
        state.ServiceCalls.Light().Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    [Fact]
    public void WatchdogDoesNotRunWhenDisabled()
    {
        // Arrange
        sut.Config.Room().Watchdog = false;
        state.Change(sut.Config.Light(), "on");
        sut.Init();

        // Act  
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.GuardTimeout).Ticks);

        // Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEmpty();
    }

    [Fact]
    public void HouseModeSetToDayTurnsOffAllEntitiesAndTurnOnControlEntities()
    {
        //Arrange
        state.Change(sut.Config.Light(), "off");
        state.Change(sut.Config.Light(2), "off");
        state.Change(sut.Config.NightLight(), "on");
        state.Change(sut.Config.NightLight(2), "on");
        sut.Init();

        //Act
        state.Change(sut.Config.Room().NightTimeEntity!, "night");

        //Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEquivalentTo(new[]
            {
                Events.Light.TurnOff(sut.Config.Light()),
                Events.Light.TurnOff(sut.Config.Light(2)),
                Events.Light.TurnOff(sut.Config.NightLight()),
                Events.Light.TurnOff(sut.Config.NightLight(2))
            }
        );
    }

    [Fact]
    public void HouseModeSetToDayTurnsOffCircadianSleepMode()
    {
        //Arrange
        sut.Config.Room().CircadianSwitchEntity = entityBuilder.CreateEntity<SwitchEntity>("switch.adaptive_lighting_testroom", "on");
        sut.Init();

        //Act
        state.Change(sut.Config.Room().NightTimeEntity!, "day");

        //Assert
        state.ServiceCalls.Switch().Should().ContainEquivalentOf(
            Events.Switch.TurnOff(entityBuilder.CreateSwitchEntity("switch.adaptive_lighting_sleep_mode_testroom"))
        );
    }

    [Fact]
    public void HouseModeSetToNightTurnsOnCircadianSleepMode()
    {
        //Arrange
        sut.Config.Room().CircadianSwitchEntity = entityBuilder.CreateEntity<SwitchEntity>("switch.adaptive_lighting_testroom", "on");
        sut.Init();

        //Act
        state.Change(sut.Config.Room().NightTimeEntity!, "night");

        //Assert
        state.ServiceCalls.Switch().Should().ContainEquivalentOf(
            Events.Switch.TurnOn(entityBuilder.CreateSwitchEntity("switch.adaptive_lighting_sleep_mode_testroom"))
        );
    }

    [Fact]
    public void OverrideEventTurnsOffCircadianSwitch()
    {
        //Arrange
        sut.Config.Room().CircadianSwitchEntity = entityBuilder.CreateEntity<SwitchEntity>("switch.adaptive_lighting_testroom", "on");
        sut.Init();

        //Act
        var oldState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            },
            AttributesJson = new LightTurnOnParameters
            {
                Brightness = 0
            }.AsJsonElement()
        };

        var newState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            },
            AttributesJson = new LightTurnOnParameters
            {
                Brightness = 100
            }.AsJsonElement()
        };

        state.Change(sut.Config.Light(), oldState, newState);

        //Assert
        state.ServiceCalls.Switch().Should().ContainEquivalentOf(
            Events.Switch.TurnOff(sut.Config.Room().CircadianSwitchEntity!)
        );
    }

    [Fact]
    public void OverrideEventTurnsDoesNotAttemptToTurnOffCircadianSwitchIfItDoesNotExist()
    {
        //Arrange
        sut.Init();

        //Act
        var oldState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            },
            AttributesJson = new LightTurnOnParameters
            {
                Brightness = 0
            }.AsJsonElement()
        };

        var newState = new EntityState
        {
            State = "on",
            Context = new Context
            {
                UserId = "EUGENE"
            },
            AttributesJson = new LightTurnOnParameters
            {
                Brightness = 100
            }.AsJsonElement()
        };

        state.Change(sut.Config.Light(), oldState, newState);

        //Assert
        state.ServiceCalls.Switch().Should().BeEquivalentTo(new[]
            { Events.Switch.TurnOn(sut.Config.Room().ManagerEnabled) }
        );
    }

    [Fact]
    public void HouseModeSetToNightTurnsOffAllNonNightControlEntitiesAndTurnsOnNightControlEntities()
    {
        // Arrange

        state.Change(sut.Config.Light(), "on");
        state.Change(sut.Config.NightLight(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Room().NightTimeEntity!, "night");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(2).Ticks);

        //Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEquivalentTo(new[]
            {
                Events.Light.TurnOff(sut.Config.Light()),
                Events.Light.TurnOff(sut.Config.Light(2)),
                Events.Light.TurnOff(sut.Config.NightLight()),
                Events.Light.TurnOff(sut.Config.NightLight(2)),
                Events.Light.TurnOn(sut.Config.NightLight(2))
            }
        );
    }

    // [Fact]
    // public void LightBrightnessChangedManuallyTurnsOffCircadianSwitch()
    // {
    //     // Arrange
    //     sut.Config.Room().CircadianSwitchEntity = entityBuilder.CreateSwitchEntity("switch.circadian");
    //     //sut.Init(sut.Config);
    //
    //     // Act
    //     state.Change(sut.Config.Pir1(), "on");
    //
    //
    //     var oldState = new EntityState
    //     {
    //         State = "on",
    //         Context = new Context
    //         {
    //             UserId = "EUGENE"
    //         }
    //     }.WithAttributes(new LightTurnOnParameters
    //     {
    //         Brightness = 0
    //     });
    //
    //     var newState = new EntityState
    //     {
    //         State = "on",
    //         Context = new Context
    //         {
    //             UserId = "EUGENE"
    //         }
    //     }.WithAttributes(new LightTurnOnParameters
    //     {
    //         Brightness = 100
    //     });
    //
    //     state.Change(sut.Config.Light(), oldState, newState);
    //
    //     // Assert
    //     state.ServiceCalls.Should().ContainEquivalentOf(
    //         Events.Switch.TurnOff(sut.Config.Room().CircadianSwitchEntity)
    //     );
    // }

    [Fact]
    public void LightDontTurnOnIfOffAndConditionEntityStateIsNotMet()
    {
        // Arrange
        sut.Config.Room().ConditionEntity      = entityBuilder.CreateSensorEntity("sensor.condition_entity");
        sut.Config.Room().ConditionEntityState = "under";
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    [Fact]
    public void LightDontTurnsOffIfOnAndManagerIsDisabled()
    {
        // Arrange
        state.Change(sut.Config.Light(), "on");
        state.Change(sut.Config.Pir1(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.ManagerEnabled(), "off");
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    [Fact]
    public void LightDontTurnsOffIfOnAndRoomIsOccupied()
    {
        // Arrange
        state.Change(sut.Config.Light(), "on");
        state.Change(sut.Config.Pir1(), "on");
        state.Change(sut.Config.KeepAlive1(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    [Fact]
    public void LightDontTurnsOnIfOffAndManagerIsDisabled()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.ManagerEnabled(), "off");
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    [Fact]
    public void LightDontTurnsOnIfOffAndToBright()
    {
        // Arrange
        sut.Config.Room().LuxEntity = entityBuilder.CreateNumericEntity("sensor.lux_value");
        state.Change(sut.Config.Room().LuxEntity!, "100");
        sut.Config.Room().LuxLimitEntity = entityBuilder.CreateNumericEntity("sensor.lux_Limit");
        state.Change(sut.Config.Room().LuxLimitEntity!, "10");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    [Fact]
    public void LightTurnedOffManuallyCancelsOverrideTimeoutOnlyIfAllControlEntitiesAreOff()
    {
        // Arrange
        state.Change(sut.Config.Light(2), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });

        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "off"
        });
        state.Change(sut.Config.Light(2), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "off"
        });
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light(2))
        );
    }

    [Fact]
    public void LightTurnedOffManuallyDoesNotCancelsOverrideTimeoutIfSomeControlEntitiesAreOn()
    {
        // Arrange

        state.Change(sut.Config.Light(2), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });

        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "off"
        });
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light(2))
        );
    }

    [Fact]
    public void LightTurnedOnManuallyActivatesOverrideTimeout()
    {
        // Arrange

        sut.Init();

        // Act
        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }


    [Fact]
    public void LightTurnsOffWhenPresenceIsOffAfterTimeout()
    {
        // Arrange

        state.Change(sut.Config.Light(), "on");
        state.Change(sut.Config.Pir1(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);
        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    [Fact]
    public void LightTurnsOnWhenPresenceIsOn()
    {
        // Arrange

        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    [Fact]
    public void MotionAfterLastOffEventCancelsOffEventTimer()
    {
        // Arrange

        state.Change(sut.Config.Light(), "on");
        state.Change(sut.Config.Pir1(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout - 1).Ticks);
        state.Change(sut.Config.Pir1(), "on");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }


    [Fact]
    public void NightLightTurnsOnWhenPresenceIsOnAndNightModeIsActive()
    {
        // Arrange

        sut.Init();

        // Act
        state.Change(sut.Config.Room().NightTimeEntity!, "night");
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.NightLight())
        );
    }

    [Fact]
    public void OverriddenLightsObeyOverrideTimeoutRegardlessOfNormalTimeoutAndPresence()
    {
        // Arrange
        state.Change(sut.Config.Room().NightTimeEntity!, "night");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "on");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(20).Ticks);
        state.Change(sut.Config.Pir1(), "off");
        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });

        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().NightTimeout).Ticks);

        // Assert normal timer does nothing
        state.ServiceCalls.Filter(Domain.Light).Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.NightLight())
        );

        var never = new[]
        {
            Events.Light.TurnOff(sut.Config.NightLight()),
            Events.Light.TurnOff(sut.Config.Light())
        };
        foreach (var e in never) state.ServiceCalls.Filter(Domain.Light).Should().NotContainEquivalentOf(e);

        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert override timer turns light off
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    [Fact]
    public void RoomStateIsOffWhenEntitiesTurnOff()
    {
        // Arrange
        state.Change(sut.Config.Pir1(), "on");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        sut.Config.Room().RoomState.Should().Be("off");
    }

    [Fact]
    public void RoomStateIsOnWhenEntitiesTurnOn()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        sut.Config.Room().RoomState.Should().Be("on");
    }

    [Fact]
    public void TurnOnLight()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    [Fact]
    public void TurnOnNightLight()
    {
        // Arrange
        state.Change(sut.Config.Room().NightTimeEntity!, "night");
        sut.Init();

        // Act
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.NightLight())
        );
    }

    [Fact]
    public void WhenOverrideActiveOtherLightsThatAreOffDontTurnOnForPresence()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });
        state.Change(sut.Config.Pir1(), "on");

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light(2))
        );
    }

    [Fact]
    public void WhenOverrideActivePresenceResetsOverrideTimeout()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(900).Ticks);
        state.Change(sut.Config.Pir1(), "on");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(900).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );

        // Act
        state.Change(sut.Config.Pir1(), "off");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }
}