using System;
using System.Collections.Generic;
using System.Linq;
using LightsManager;
using Moq;
using NetDaemon.Common;
using TestStack.BDDfy;
using Xunit;

public partial class LightsManagerTests
{
    [Fact]
    public void on_presence_stopped_does_not_set_timer_if_active_presence_is_on()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(ON))
            .And(s => GivenTheControlEntityIs(ON))
            .And(s => GivenTheKeepAliveEntityIs(ON))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(OFF))
            .And(s => WhenAfterSeconds(120))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_only_entities_for_presence_active_room_is_on()
    {
        // ARRANGE
        var room1 = new LightsManagerConfig
        {
            Name              = "Room1",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor_room1" },
            ControlEntityIds  = new List<string> { "light.my_light_room1" },
            Timeout           = 60
        };

        var room2 = new LightsManagerConfig
        {
            Name              = "Room2",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor_room2" },
            ControlEntityIds  = new List<string> { "light.my_light_room2" },
            Timeout           = 120
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = room1.PresenceEntityIds.First(), State = OFF });
        MockState.Add(new EntityState { EntityId = room2.PresenceEntityIds.First(), State = OFF });
        MockState.Add(new EntityState { EntityId = room1.ControlEntityIds.First(), State  = OFF });
        MockState.Add(new EntityState { EntityId = room2.ControlEntityIds.First(), State  = OFF });

        var room1App = new Manager(Object, room1);
        var room2App = new Manager(Object, room2);

        // ACT
        TriggerStateChange(room1.PresenceEntityIds.First(), OFF, ON);
        TriggerStateChange(room2.PresenceEntityIds.First(), OFF, ON);

        // ASSERT
        VerifyEntityTurnOn(room1.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOn(room2.ControlEntityIds.First(), times: Times.Once());

        TriggerStateChange(room1.PresenceEntityIds.First(), ON, OFF);
        TriggerStateChange(room2.PresenceEntityIds.First(), ON, OFF);

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(60).Ticks);

        VerifyEntityTurnOff(room1.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(room2.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void on_presence_stopped_turns_off_control_entities_after_multiple_presence_events_and_timeout()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(ON))
            .And(s => GivenTheControlEntityIs(ON))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenAfterSeconds(30))
            .Then(s => WhenPresenceEntityTurns(OFF))
            .Then(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenAfterSeconds(20))
            .Then(s => WhenPresenceEntityTurns(OFF))
            .Then(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenAfterSeconds(30))
            .When(s => WhenPresenceEntityTurns(OFF))
            // Total time since turning event 1 is 80 seconds, timeout is 60 seconds but it should reset every time there is a new presence event.
            // At this point (after OFF event 3) the light should not have turned off.
            // It should only turn off after the TestScheduler has advanced the full timeout after OFF event 3
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .When(s => WhenTimeoutExpired())
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Once()))
            .And(s => ThenTheControlEntityTurned(ON, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_turns_off_control_entities_after_night_timeout()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(ON))
            .And(s => GivenTheControlEntityIs(ON))
            .And(s => GivenTheNightTimeEntityIs("night"))
            .And(s => GivenTheNightTimeEntityStatesAre("night"))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheNightTimeoutIsSeconds(30))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(OFF))
            .And(s => WhenAfterSeconds(10))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .And(s => WhenAfterSeconds(_config.NightTimeout))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_turns_off_control_entities_after_timeout()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(ON))
            .And(s => GivenTheControlEntityIs(ON))
            .And(s => GivenTheControlEntityIs(SwitchMySwitch, ON), _theControlEntityIsTemplate)
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(OFF))
            .And(s => WhenAfterSeconds(10))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .Then(s => ThenTheEntityTurnedOffTimes(SwitchMySwitch, Times.Never()))
            .And(s => WhenAfterSeconds(_config.Timeout))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Once()))
            .Then(s => ThenTheEntityTurnedOffTimes(SwitchMySwitch, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_when_state_is_changed_from_active_to_disabled_when_timer_doesnt_exists_then_no_control_entities_turn_off()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(ON))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheManagerEnabledIs(ON))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenManagerIsTurned(OFF))
            .Then(s => ThenTheControlEntityTurned(ON, Times.Once()))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_when_state_is_changed_from_active_to_disabled_when_timer_exists_then_no_control_entities_turn_off()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheManagerEnabledIs(ON))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenPresenceEntityTurns(OFF))
            .Then(s => ThenTheManagerStateIs(ManagerState.Active))
            .And(s => ThenThereIsAnActiveTimer())
            .When(s => WhenManagerIsTurned(OFF))
            .And(s => WhenTimeoutExpired())
            .Then(s => ThenTheControlEntityTurned(ON, Times.Once()))
            .And(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_when_state_is_changed_from_idle_to_disabled_then_no_control_entities_turn_off()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(ON))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheManagerEnabledIs(ON))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenManagerIsTurned(OFF))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .BDDfy();
    }
}