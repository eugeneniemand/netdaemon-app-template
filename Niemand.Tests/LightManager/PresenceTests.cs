using LightManagerV2;
using NetDaemon.Extensions.Testing;

namespace Niemand.Tests.LightManager;

public class PresenceTests(LightManagerSut sut, StateChangeManager state, TestEntityBuilder entityBuilder, IHaContext haContext)
{
    // Verifies lights turn off after inactivity timeout.
    [Fact]
    public void GivenPresenceOff_WhenTimeoutElapses_ThenLightTurnsOff()
    {
        // Arrange
        state.Change(sut.Config.Light(), "on");
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    // Verifies motion immediately turns lights on.
    [Fact]
    public void GivenPresenceOn_WhenRoomEvaluates_ThenLightTurnsOn()
    {
        // Arrange
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }

    // Verifies renewed motion cancels pending turn-off timer.
    [Fact]
    public void GivenMotionReturns_WhenOffTimerPending_ThenTurnOffIsCancelled()
    {
        // Arrange
        state.Change(sut.Config.Light(), "on");
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout - 1).Ticks);
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    // Verifies room state transitions to off after timeout.
    [Fact]
    public void GivenPresenceTurnsOff_WhenTimeoutElapses_ThenRoomStateBecomesOff()
    {
        // Arrange
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        sut.Config.Room().RoomState.Should().Be("off");
    }

    // Verifies room state transitions to on when motion is detected.
    [Fact]
    public void GivenPresenceTurnsOn_WhenEvaluated_ThenRoomStateBecomesOn()
    {
        // Arrange
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        sut.Config.Room().RoomState.Should().Be("on");
    }

    // Verifies primary control light activation path.
    [Fact]
    public void GivenPresenceOn_WhenTriggered_ThenControlLightTurnsOn()
    {
        // Arrange
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }
}
