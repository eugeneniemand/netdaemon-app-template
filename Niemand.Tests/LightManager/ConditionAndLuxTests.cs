using LightManagerV2;
using NetDaemon.HassModel.Entities;
using NetDaemon.Extensions.Testing;

namespace Niemand.Tests.LightManager;

public class ConditionAndLuxTests(LightManagerSut sut, StateChangeManager state, TestEntityBuilder entityBuilder, IHaContext haContext)
{
    // Verifies circadian switch is re-enabled when automation turns lights on.
    [Fact]
    public void GivenCircadianSwitchOff_WhenLightTurnsOn_ThenCircadianSwitchTurnsOn()
    {
        // Arrange
        sut.Config.Room().CircadianSwitchEntity = entityBuilder.CreateSwitchEntity("switch.circadian", "off");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Switch.TurnOn(sut.Config.Room().CircadianSwitchEntity!)
        );
    }

    // Verifies condition checks do not prevent lights from turning off once already on.
    [Fact]
    public void GivenConditionNotMet_WhenPresenceTimeoutExpires_ThenLightsAreNotTurnedOffByConditionCheck()
    {
        // Arrange
        sut.Config.Room().ConditionEntity = entityBuilder.CreateEntity<SensorEntity>("sensor.sun", "above_horizon");
        sut.Config.Room().ConditionEntityState = "below_horizon";
        state.Change(sut.Config.Light(), "on");
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEmpty();
    }

    // Verifies circadian switch is turned back on after timeout-based turn-off.
    [Fact]
    public void GivenCircadianSwitchOff_WhenLightsTurnOffAfterTimeout_ThenCircadianSwitchTurnsOn()
    {
        // Arrange
        sut.Config.Room().CircadianSwitchEntity = entityBuilder.CreateEntity<SwitchEntity>("switch.adaptive_lighting", "off");
        state.Change(sut.Config.Light(), "on");
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        state.ServiceCalls.Filter(Domain.Switch).Should().ContainEquivalentOf(
            Events.Switch.TurnOn(sut.Config.Room().CircadianSwitchEntity!)
        );
    }

    // Verifies condition mismatch blocks automatic turn-on.
    [Fact]
    public void GivenConditionStateMismatch_WhenPresenceTurnsOn_ThenControlLightsDoNotTurnOn()
    {
        // Arrange
        sut.Config.Room().ConditionEntity = entityBuilder.CreateSensorEntity("sensor.condition_entity");
        sut.Config.Room().ConditionEntityState = "under";
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    // Verifies disabled manager prevents automatic turn-off.
    [Fact]
    public void GivenManagerDisabled_WhenPresenceTurnsOff_ThenControlLightsDoNotTurnOff()
    {
        // Arrange
        state.Change(sut.Config.Light(), "on");
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Init();

        // Act
        state.Change(sut.Config.ManagerEnabled(), "off");
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    // Verifies occupancy keeps lights on even when primary presence goes off.
    [Fact]
    public void GivenRoomOccupied_WhenPresenceTurnsOff_ThenControlLightsDoNotTurnOff()
    {
        // Arrange
        state.Change(sut.Config.Light(), "on");
        state.TriggerPresence(sut.Config.Pir1(), true);
        state.Change(sut.Config.KeepAlive1(), "on");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    // Verifies disabled manager prevents automatic turn-on.
    [Fact]
    public void GivenManagerDisabled_WhenPresenceTurnsOn_ThenControlLightsDoNotTurnOn()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.ManagerEnabled(), "off");
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    // Verifies high lux blocks automatic turn-on.
    [Fact]
    public void GivenLuxAboveLimit_WhenPresenceTurnsOn_ThenControlLightsDoNotTurnOn()
    {
        // Arrange
        sut.Config.Room().LuxEntity = entityBuilder.CreateNumericEntity("sensor.lux_value");
        state.Change(sut.Config.Room().LuxEntity!, "100");
        sut.Config.Room().LuxLimitEntity = entityBuilder.CreateNumericEntity("sensor.lux_Limit");
        state.Change(sut.Config.Room().LuxLimitEntity!, "10");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    // Verifies day mode still applies automatic day brightness on motion.
    [Fact]
    public void GivenDayMode_WhenMotionTurnsLightOn_ThenAutomaticDayBrightnessIsUsed()
    {
        // Arrange
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "day");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(150).Ticks);

        // Assert
        state.ServiceCalls.Filter(Domain.Light).Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters { BrightnessPct = 100 })
        );
    }
}
