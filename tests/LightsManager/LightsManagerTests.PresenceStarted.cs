using System;
using LightManager;
using Moq;
using TestStack.BDDfy;
using Xunit;

public partial class LightsManagerTests
{
    [Fact]
    public void on_presence_started_event_doesnt_turns_on_control_entities_when_lux_is_above_limit()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheLuxEntityIs("sensor.lux", 300))
            .And(s => GivenTheLuxLimitIs("input_number.lux_limit", 100))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .Then(s => ThenTheControlEntityTurned(ON, Times.Never()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_event_turns_on_control_entities()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .Then(s => ThenTheControlEntityTurned(ON, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_event_turns_on_control_entities_except_when_state_is_override()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(LightMyLightTwo, OFF))
            .And(s => GivenTheManagerIsInitialised())
            .And(s => WhenControlEntityIsManuallyTurned(ON))
            .Then(s => ThenTheManagerStateIs(ManagerState.Override))
            .When(s => WhenPresenceEntityTurns(ON))
            .Then(s => ThenTheEntityTurnedOnTimes(LightMyLightTwo, Times.Never()))
            .And(s => ThenTheManagerStateIs(ManagerState.Override))
            .BDDfy();
    }


    [Fact]
    public void on_presence_started_event_turns_on_control_entities_when_lux_is_below_limit()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheLuxEntityIs("sensor.lux", 30))
            .And(s => GivenTheLuxLimitIs("input_number.lux_limit", 100))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .Then(s => ThenTheControlEntityTurned(ON, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_event_does_not_reset_timer_when_state_is_override_and_lux_is_above_limit()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheManagerIsInitialised())
            .And(s => GivenTheLuxLimitIs(50))
            .And(s => GivenTheLuxEntityIs(20))
            .When(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenControlEntityIsManuallyTurned(ON))
            .Then(s => ThenManagerTimerSetEventFired(Times.Once(), TimeSpan.FromSeconds(_config.OverrideTimeout)))
            .And(s => ThenTheManagerStateIs(ManagerState.Override))
            .When(s => WhenPresenceEntityTurns(OFF))
            .Then(s => ThenManagerTimerSetEventFired(Times.Once(), TimeSpan.FromSeconds(_config.OverrideTimeout)))
            .When(s => WhenLuxChangesTo(100))
            .And(s => WhenPresenceEntityTurns(ON))
            .Then(s => ThenManagerTimerResetEventFired(Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_event_turns_on_multiple_control_entities()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(SwitchMySwitch, OFF), _theControlEntityIsTemplate)
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .Then(s => ThenTheControlEntityTurned(ON, Times.Once()))
            .Then(s => ThenTheEntityTurnedOnTimes(SwitchMySwitch, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_when_state_is_disabled_then_no_control_entities_turn_on()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheManagerEnabledIs(ON))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenEntityTurns(_config.EnabledSwitchEntityId, OFF), _thenEntityTurnsTemplate)
            .When(s => WhenPresenceEntityTurns(ON))
            .Then(s => ThenTheControlEntityTurned(ON, Times.Never()))
            .BDDfy();
    }
}