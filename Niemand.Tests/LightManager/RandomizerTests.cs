using LightManagerV2;
using NetDaemon.HassModel.Entities;
using NetDaemon.Extensions.Testing;

namespace Niemand.Tests.LightManager;

public class RandomizerTests(LightManagerSut sut, StateChangeManager state, TestEntityBuilder entityBuilder, IHaContext haContext)
{
    // Verifies randomizer starts after sunset and matching alarm state.
    [Fact]
    public void GivenSunSetsAndAlarmStateMatches_WhenRandomizerEvaluates_ThenManagerDisablesAndRandomLightTurnsOn()
    {
        // Arrange
        var sun = entityBuilder.CreateEntity<Entity>("sun.sun", "above_horizon");
        sut.Config.Room().ControlEntities.RemoveRange(1, sut.Config.Room().ControlEntities.Count - 1);
        sut.Config.Room().NightControlEntities.Clear();
        state.Change(sut.Config.Light(), new
        {
            brightness = 0,
            supported_color_modes = new[] { "brightness" }
        });
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Init();

        // Act
        state.Change(sun, "below_horizon");

        // Assert
        state.ServiceCalls.Filter(Domain.Switch).Should().ContainEquivalentOf(
            Events.Switch.TurnOff(sut.Config.Room().ManagerEnabled)
        );
        state.ServiceCalls.Filter(Domain.Light).Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters { BrightnessPct = 100 })
        );
    }

    // Verifies randomizer candidate lights come from union of day and night control sets.
    [Fact]
    public void GivenOnlyNightControlLightsAvailable_WhenRandomizerRuns_ThenNightControlLightCanBeSelected()
    {
        // Arrange
        entityBuilder.CreateEntity<Entity>("sun.sun", "below_horizon");
        sut.Config.Room().ControlEntities.Clear();
        sut.Config.Room().NightControlEntities.RemoveRange(1, sut.Config.Room().NightControlEntities.Count - 1);
        state.Change(sut.Config.NightLight(), new
        {
            brightness = 0,
            supported_color_modes = new[] { "brightness" }
        });
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");

        // Act
        sut.Init();

        // Assert
        state.ServiceCalls.Filter(Domain.Light).Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.NightLight(), new LightTurnOnParameters { BrightnessPct = 100 })
        );
    }

    // Verifies randomizer stop restores only managers it disabled.
    [Fact]
    public void GivenMixedRoomStates_WhenRandomizerStops_ThenOnlyMatchingManagersAreRestored()
    {
        // Arrange
        var sun = entityBuilder.CreateEntity<Entity>("sun.sun", "above_horizon");
        entityBuilder.CreateSwitchEntity("switch.light_manager_otherroom", "on");
        var otherRoom = new Manager
        {
            Name = "OtherRoom",
            PresenceEntities = [entityBuilder.CreateBinarySensorEntity("binary_sensor.other_pir")],
            ControlEntities = [entityBuilder.CreateLightEntity("light.other_room")],
            NightControlEntities = [entityBuilder.CreateLightEntity("light.other_room_night")],
            NightTimeEntity = entityBuilder.CreateInputSelectEntity("input_select.house_mode_other"),
            NightTimeEntityStates = ["night"],
            Timeout = 90,
            NightTimeout = 30,
            OverrideTimeout = 1800,
            RandomStates = { "armed_home" }
        };
        sut.Config.Rooms.Add(otherRoom);
        state.Change(otherRoom.PresenceEntities.First(), "off");
        state.Change(otherRoom.ControlEntities.First(), "off");
        state.Change(otherRoom.NightControlEntities.First(), "off");
        state.Change(otherRoom.NightTimeEntity!, "day");
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Init();

        var matchingTurnOnBeforeStop = state.ServiceCalls.CountMatching(Domain.Switch, "turn_on", sut.Config.Room().ManagerEnabled.EntityId);
        var otherTurnOnBeforeStop = state.ServiceCalls.CountMatching(Domain.Switch, "turn_on", otherRoom.ManagerEnabled.EntityId);

        // Act
        state.Change(sun, "below_horizon");
        state.Change(sut.Config.RandomSwitchEntity, "disarmed");

        // Assert
        state.ServiceCalls.Filter(Domain.Switch).Should().ContainEquivalentOf(
            Events.Switch.TurnOff(sut.Config.Room().ManagerEnabled)
        );
        state.ServiceCalls.CountMatching(Domain.Switch, "turn_off", otherRoom.ManagerEnabled.EntityId).Should().Be(0);
        state.ServiceCalls.CountMatching(Domain.Switch, "turn_on", sut.Config.Room().ManagerEnabled.EntityId)
            .Should().BeGreaterThan(matchingTurnOnBeforeStop);
        state.ServiceCalls.CountMatching(Domain.Switch, "turn_on", otherRoom.ManagerEnabled.EntityId)
            .Should().Be(otherTurnOnBeforeStop);
    }

    // Verifies progressive room activation does not turn off already active random lights.
    [Fact]
    public void GivenTargetRequiresMultipleActiveRooms_WhenRandomizerAddsRooms_ThenExistingRandomLightsStayOn()
    {
        // Arrange
        entityBuilder.CreateEntity<Entity>("sun.sun", "below_horizon");
        sut.Config.RandomizerMinActiveRooms = 2;
        sut.Config.RandomizerMaxActiveRooms = 2;
        sut.Config.RandomizerMinShuffleInterval = "00:00:01";
        sut.Config.RandomizerMaxShuffleInterval = "00:00:01";
        sut.Config.RandomizerMinOnDuration = "00:30:00";
        sut.Config.RandomizerMaxOnDuration = "00:30:00";
        sut.Config.Room().ControlEntities.RemoveRange(1, sut.Config.Room().ControlEntities.Count - 1);
        sut.Config.Room().NightControlEntities.Clear();
        state.Change(sut.Config.Light(), new
        {
            brightness = 0,
            supported_color_modes = new[] { "brightness" }
        });

        entityBuilder.CreateSwitchEntity("switch.light_manager_otherroom", "on");
        var otherRoom = new Manager
        {
            Name = "OtherRoom",
            PresenceEntities = [entityBuilder.CreateBinarySensorEntity("binary_sensor.other_pir")],
            ControlEntities = [entityBuilder.CreateLightEntity("light.other_room")],
            NightControlEntities = [],
            NightTimeEntity = entityBuilder.CreateInputSelectEntity("input_select.house_mode_other"),
            NightTimeEntityStates = ["night"],
            Timeout = 90,
            NightTimeout = 30,
            OverrideTimeout = 1800,
            RandomStates = { "armed_away" }
        };
        sut.Config.Rooms.Add(otherRoom);
        state.Change(otherRoom.PresenceEntities.First(), "off");
        state.Change(otherRoom.ControlEntities.First(), new
        {
            brightness = 0,
            supported_color_modes = new[] { "brightness" }
        });
        state.Change(otherRoom.NightTimeEntity!, "day");
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Init();

        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        state.ServiceCalls.Filter(Domain.Light).Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters { BrightnessPct = 100 })
        );
        state.ServiceCalls.Filter(Domain.Light).Should().ContainEquivalentOf(
            Events.Light.TurnOn(otherRoom.ControlEntities.First(), new LightTurnOnParameters { BrightnessPct = 100 })
        );
        state.ServiceCalls.CountMatching(Domain.Light, "turn_off", sut.Config.Light().EntityId).Should().Be(0);
        state.ServiceCalls.CountMatching(Domain.Light, "turn_off", otherRoom.ControlEntities.First().EntityId).Should().Be(0);
    }
}
