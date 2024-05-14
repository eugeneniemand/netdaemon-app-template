using NetDaemon.HassModel.Entities;

namespace Niemand.Tests.LightManager;

public class RandomManagerFacts(RandomManagerSut sut, StateChangeManager state, TestEntityBuilder entityBuilder)
{
    [Fact]
    public void RandomEntityChangesStartsAndStopsTheQueue()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Scheduler.AdvanceBy(sut.RandomDelay.Ticks);
        state.Change(sut.Config.RandomSwitchEntity, "disarmed");

        // Assert
        state.ServiceCalls.Should().BeEquivalentTo(new[]
            {
                Events.Light.TurnOn(sut.Config.Light())
            }
        );
    }

    [Fact]
    public void RandomEntityChangesStartsAndStopsTheQueueAfterTwoIterations()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Scheduler.AdvanceBy(sut.RandomDelay.Ticks);
        sut.Scheduler.AdvanceBy(sut.RandomDelay.Ticks);
        state.Change(sut.Config.RandomSwitchEntity, "disarmed");

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light())
        );
    }
}