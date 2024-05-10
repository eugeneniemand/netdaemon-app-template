using System.Reactive.Concurrency;
using Microsoft.Extensions.Logging;

namespace Niemand.Tests;

public class RoutineTests(RoutinesSut sut, StateChangeManager state, IEntities entities)
{
    [Fact]
    public void AlarmArmedWhenEveryoneIsAway()
    {
        // Arrange
        state.Change(entities.Person.Eugene, "home");
        state.Change(entities.Person.Hailey, "home");
        state.Change(entities.Person.Aubrecia, "home");
        state.Change(entities.AlarmControlPanel.Alarmo, "disarmed");

        // Act
        var _ = sut.Instance;
        state.Change(entities.Person.Eugene, "not_home");
        state.Change(entities.Person.Hailey, "not_home");
        state.Change(entities.Person.Aubrecia, "mum_home");

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(Events.AlarmPanel.ArmedAway(entities.AlarmControlPanel.Alarmo));
    }

    [Fact]
    public void AlarmDisarmsWhenAnyoneArrivesHome()
    {
        // Arrange
        state.Change(entities.Person.Eugene, "not_home");

        // Act
        var _ = sut.Instance;
        state.Change(entities.Person.Eugene, "home");

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(Events.AlarmPanel.Disarmed(entities.AlarmControlPanel.Alarmo));
    }
    
    [Fact]
    public void AlarmDisarmsWhenSomeoneComesDownTheStairs()
    {
        // Arrange
        state.Change(entities.AlarmControlPanel.Alarmo, "armed_night");

        // Act
        var _ = sut.Instance;
        state.Change(entities.BinarySensor.Hallway, "on");
        state.Change(entities.BinarySensor.LandingMotion, "on");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(Events.AlarmPanel.Disarmed(entities.AlarmControlPanel.Alarmo));
    }
}

public class RoutinesSut(IHaContext haContext, IEntities entities, IServices services, TestScheduler scheduler, People people, ILogger<Routines> logger)
{
    public Routines Instance => new(haContext, entities, services, scheduler, people, logger);
    public TestScheduler Scheduler => scheduler;
}