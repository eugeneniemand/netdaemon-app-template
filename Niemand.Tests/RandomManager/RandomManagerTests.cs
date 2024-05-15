using NetDaemon.HassModel.Entities;
using Events = NetDaemon.Extensions.Testing.Events;

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
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(10).Ticks);
        // Assert
        state.ServiceCalls.Should().BeEquivalentTo(new[]
            {
                Events.Light.TurnOn(sut.Config.Light(1)),
                Events.Light.TurnOn(sut.Config.Light(2)),
                Events.Light.TurnOff(sut.Config.Light(1)),
                Events.Light.TurnOff(sut.Config.Light(2))
            }
        );
    }

    [Fact]
    public void RandomEntityChangesStartsAndStopsTheQueueRegardlessOfSleep()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(10).Ticks);
        state.Change(sut.Config.RandomSwitchEntity, "disarmed");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(10).Ticks);
        // Assert
        state.ServiceCalls.Should().BeEquivalentTo(new[]
            {
                Events.Light.TurnOn(sut.Config.Light(1)),
                Events.Light.TurnOn(sut.Config.Light(2)),
                Events.Light.TurnOff(sut.Config.Light(1)),
                Events.Light.TurnOff(sut.Config.Light(2))
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
        state.ServiceCalls.Should().BeEquivalentTo(new[]
            {
                Events.Light.TurnOn(sut.Config.Light(1)),
                Events.Light.TurnOn(sut.Config.Light(2)),
                Events.Light.TurnOff(sut.Config.Light(1)),
                Events.Light.TurnOff(sut.Config.Light(2)),
                Events.Light.TurnOn(sut.Config.Light(1)),
                Events.Light.TurnOn(sut.Config.Light(2)),
                Events.Light.TurnOff(sut.Config.Light(1)),
                Events.Light.TurnOff(sut.Config.Light(2))
            }
        );
    }

    [Fact]
    public void RandomEntityStartsTheQueueAndTestedBeforeSleepIsDone()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Scheduler.AdvanceBy(sut.RandomDelay.Ticks);
        sut.Scheduler.AdvanceBy(sut.RandomDelay.Add(TimeSpan.FromSeconds(-10)).Ticks);

        // Assert
        state.ServiceCalls.Should().BeEquivalentTo(new[]
            {
                Events.Light.TurnOn(sut.Config.Light(1)),
                Events.Light.TurnOn(sut.Config.Light(2)),
                Events.Light.TurnOff(sut.Config.Light(1)),
                Events.Light.TurnOff(sut.Config.Light(2)),
                Events.Light.TurnOn(sut.Config.Light(1)),
                Events.Light.TurnOn(sut.Config.Light(2))
            }
        );
    }

    [Fact]
    public void RandomEntityDoesNotStartQueueWhenStateDoesNotMatchOneOfTheRandomStatesInConfig()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.RandomSwitchEntity, "armed_holiday");
        sut.Scheduler.AdvanceBy(sut.RandomDelay.Ticks);

        // Assert
        state.ServiceCalls.Should().BeEmpty();
    }

    [Fact]
    public void RandomEntityDoesNotStopQueueWhenStateIsNotDisarmed()
    {
        // Arrange
        sut.Init();
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Scheduler.AdvanceBy(sut.RandomDelay.Ticks);

        // Act
        state.Change(sut.Config.RandomSwitchEntity, "not_disarmed");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(10).Ticks);

        // Assert
        state.ServiceCalls.Should().NotBeEmpty();
    }

    [Fact]
    public void RandomEntityDoesNotChangeQueueWhenStateIsUnchanged()
    {
        // Arrange
        sut.Init();
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Scheduler.AdvanceBy(sut.RandomDelay.Ticks);

        // Act
        state.Change(sut.Config.RandomSwitchEntity, "armed_away");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(10).Ticks);

        // Assert
        state.ServiceCalls.Should().HaveCount(2);
    }
}