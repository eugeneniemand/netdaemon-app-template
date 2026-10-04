using LightManagerV2;
using NetDaemon.Extensions.Testing;

namespace Niemand.Tests.LightManager;

public class WatchdogTests(LightManagerSut sut, StateChangeManager state, TestEntityBuilder entityBuilder, IHaContext haContext)
{
    // Ensures watchdog turns off lights when room state indicates inactive occupancy.
    [Fact]
    public void GivenRoomStateOff_WhenWatchdogRuns_ThenLightsTurnOff()
    {
        // Arrange
        sut.Config.Room().RoomState = "off";
        state.TurnOnManually(sut.Config.Light());
        sut.Init();

        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.GuardTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    // Ensures watchdog scheduling does nothing when the feature is disabled.
    [Fact]
    public void GivenWatchdogDisabled_WhenGuardIntervalElapses_ThenNoLightCallsAreMade()
    {
        // Arrange
        sut.Config.Room().Watchdog = false;
        state.TurnOnManually(sut.Config.Light());
        sut.Init();

        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.GuardTimeout).Ticks);

        // Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEmpty();
    }
}
