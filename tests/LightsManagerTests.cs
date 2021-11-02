using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using LightsManager;
using Moq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Daemon.Fakes;
using TestStack.BDDfy;
using Xunit;

/// <summary>
///     Tests the fluent API parts of the daemon
/// </summary>
/// <remarks>
///     Mainly the tests checks if correct underlying call to "CallService"
///     has been made.
/// </remarks>
public partial class LightsManagerTests : RxAppMock
{
    public LightsManagerTests()
    {
        Setup(n => n.States).Returns(MockState);

        Setup(s => s.RunIn(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        {
            var result = new DisposableTimerResult(new CancellationToken());
            Observable.Timer(span, TestScheduler)
                      .Subscribe(_ => action(), result.Token);
            return result;
        });
    }


    [Fact]
    public void on_house_mode_changed_to_day_turns_off_night_entities_and_turns_on_day_entities()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "on"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheNightControlEntityIs("light.my_night_light", "on"), _theNightControlEntityIsTemplate)
            .And(s => TheNightTimeEntityStatesAre("night"))
            .And(s => TheNightTimeEntityIs("binary_sensor.house_mode", "night"))
            .And(s => TheManagerIsInitialised())
            .When(s => TriggerStateChange("binary_sensor.house_mode", "night", "day"), _stateChangedTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_night_light", Times.Once()), _thenEntityTurnedOffTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_house_mode_changed_to_night_turns_on_night_entities_and_turns_off_day_entities()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "on"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "on"), _theControlEntityIsTemplate)
            .And(s => TheNightControlEntityIs("light.my_night_light", "off"), _theNightControlEntityIsTemplate)
            .And(s => TheNightTimeEntityStatesAre("night"))
            .And(s => TheNightTimeEntityIs("binary_sensor.house_mode", "day"))
            .And(s => TheManagerIsInitialised())
            .When(s => TriggerStateChange("binary_sensor.house_mode", "day", "night"), _stateChangedTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_night_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Once()), _thenEntityTurnedOffTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_house_mode_changed_when_not_active_still_performs_reconfiguration()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light2", "off"), _theControlEntityIsTemplate)
            .And(s => TheNightControlEntityIs("light.my_light2", "off"), _theControlEntityIsTemplate)
            .And(s => TheNightControlEntityIs("light.my_night_light", "off"), _theNightControlEntityIsTemplate)
            .And(s => TheNightTimeEntityStatesAre("night"))
            .And(s => TheNightTimeEntityIs("binary_sensor.house_mode", "day"))
            .And(s => TheManagerIsInitialised())
            .When(s => TriggerStateChange("binary_sensor.house_mode", "day", "night"), _stateChangedTemplate)
            .Then(s => TheControlEntitiesAre("light.my_light", "light.my_light2"))
            .Then(s => TheNightControlEntitiesAre("light.my_night_light", "light.my_light2"))
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_event_doesnt_turns_on_control_entities_when_lux_is_above_limit()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheLuxEntityIs("sensor.lux", 300))
            .And(s => TheLuxLimitEntityIs("input_number.lux_limit", 100))
            .And(s => TheManagerIsInitialised())
            .When(s => TriggerStateChange("binary_sensor.my_motion_sensor", "off", "on"), _stateChangedTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Never()), _thenEntityTurnedOnTemplate)
            .BDDfy();
    }


    [Fact]
    public void on_presence_started_event_turns_on_control_entities()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheManagerIsInitialised())
            .When(s => TriggerStateChange("binary_sensor.my_motion_sensor", "off", "on"), _stateChangedTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_event_turns_on_control_entities_when_lux_is_below_limit()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheLuxEntityIs("sensor.lux", 30))
            .And(s => TheLuxLimitEntityIs("input_number.lux_limit", 100))
            .And(s => TheManagerIsInitialised())
            .When(s => TriggerStateChange("binary_sensor.my_motion_sensor", "off", "on"), _stateChangedTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_event_turns_on_multiple_control_entities()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheControlEntityIs("switch.my_switch", "off"), _theControlEntityIsTemplate)
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "on"))
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .Then(s => TheEntityTurnedOnTimes("switch.my_switch", Times.Once()), _thenEntityTurnedOnTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_does_not_set_timer_if_active_presence_is_on()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "on"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "on"), _theControlEntityIsTemplate)
            .And(s => TheKeepAliveEntityIs("binary_sensor.my_keep_alive_sensor", "on"))
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _thenEntityTurnsTemplate)
            .And(s => AfterSeconds(120), _andAfterSecondsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
            .BDDfy();
    }

    [Fact]
    public void when_enabled_switch_is_turned_off_then_manager_state_is_disabled()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheManagerEnabledIs("on"))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns(_config.EnabledSwitchEntityId, "off"), _thenEntityTurnsTemplate)
            .Then(s => TheManagerStateIs(ManagerState.Disabled))
            .BDDfy();
    }

    [Fact]
    public void on_presence_started_when_state_is_disabled_then_no_control_entities_turn_on()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheManagerEnabledIs("on"))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns(_config.EnabledSwitchEntityId, "off"), _thenEntityTurnsTemplate)
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _thenEntityTurnsTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Never()), _thenEntityTurnedOnTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_when_state_is_changed_from_active_to_disabled_when_timer_exists_then_no_control_entities_turn_off()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "on"), _theControlEntityIsTemplate)
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheManagerEnabledIs("on"))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _thenEntityTurnsTemplate)
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _thenEntityTurnsTemplate)
            .When(s => EntityTurns(_config.EnabledSwitchEntityId, "off"), _thenEntityTurnsTemplate)
            .When(s => AfterSeconds(70))
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_when_state_is_changed_from_active_to_disabled_when_timer_doesnt_exists_then_no_control_entities_turn_off()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "on"), _theControlEntityIsTemplate)
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheManagerEnabledIs("on"))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _thenEntityTurnsTemplate)
            .When(s => EntityTurns(_config.EnabledSwitchEntityId, "off"), _thenEntityTurnsTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_when_state_is_changed_from_idle_to_disabled_then_no_control_entities_turn_off()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "on"), _theControlEntityIsTemplate)
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheManagerEnabledIs("on"))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns(_config.EnabledSwitchEntityId, "off"), _thenEntityTurnsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
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
        MockState.Add(new EntityState { EntityId = room1.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = room2.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = room1.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = room2.ControlEntityIds.First(), State  = "off" });

        var room1App = new Manager(Object, room1);
        var room2App = new Manager(Object, room2);

        // ACT
        TriggerStateChange(room1.PresenceEntityIds.First(), "off", "on");
        TriggerStateChange(room2.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyEntityTurnOn(room1.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOn(room2.ControlEntityIds.First(), times: Times.Once());

        TriggerStateChange(room1.PresenceEntityIds.First(), "on", "off");
        TriggerStateChange(room2.PresenceEntityIds.First(), "on", "off");

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(60).Ticks);

        VerifyEntityTurnOff(room1.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(room2.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void on_presence_stopped_turns_off_control_entities_after_multiple_presence_events_and_timeout()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "on"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "on"), _theControlEntityIsTemplate)
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _whenEntityTurnsTemplate)
            .And(s => AfterSeconds(30), _andAfterSecondsTemplate)
            .Then(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _thenEntityTurnsTemplate)
            .Then(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _thenEntityTurnsTemplate)
            .And(s => AfterSeconds(20), _andAfterSecondsTemplate)
            .Then(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _thenEntityTurnsTemplate)
            .Then(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _thenEntityTurnsTemplate)
            .And(s => AfterSeconds(30), _andAfterSecondsTemplate)
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _whenEntityTurnsTemplate)
            // Total time since turning event 1 is 80 seconds, timeout is 60 seconds but it should reset every time there is a new presence event.
            // At this point (after "off" event 3) the light should not have turned off.
            // It should only turn off after the TestScheduler has advanced the full timeout after "off" event 3
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
            .When(s => TimeoutExpired())
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Once()), _thenEntityTurnedOffTemplate)
            .And(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_turns_off_control_entities_after_night_timeout()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "on"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "on"), _theControlEntityIsTemplate)
            .And(s => TheNightTimeEntityIs("binary_sensor.house_mode", "night"))
            .And(s => TheNightTimeEntityStatesAre("night"))
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheNightTimeoutIsSeconds(30))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _thenEntityTurnsTemplate)
            .And(s => AfterSeconds(10), _andAfterSecondsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
            .And(s => AfterSeconds(_config.NightTimeout), _andAfterSecondsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Once()), _thenEntityTurnedOffTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_presence_stopped_turns_off_control_entities_after_timeout()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "on"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "on"), _theControlEntityIsTemplate)
            .And(s => TheControlEntityIs("switch.my_switch", "on"), _theControlEntityIsTemplate)
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _thenEntityTurnsTemplate)
            .And(s => AfterSeconds(10), _andAfterSecondsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
            .Then(s => TheEntityTurnedOffTimes("switch.my_switch", Times.Never()), _thenEntityTurnedOffTemplate)
            .And(s => AfterSeconds(_config.Timeout), _andAfterSecondsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Once()), _thenEntityTurnedOffTemplate)
            .Then(s => TheEntityTurnedOffTimes("switch.my_switch", Times.Once()), _thenEntityTurnedOffTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_keep_alive_entity_turns_off_control_entities_turns_off_after_timeout()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheKeepAliveEntityIs("sensor.keep_alive", "off"), _theKeepAliveEntityIsTemplate)
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _thenEntityTurnsTemplate)
            .When(s => EntityTurns("sensor.keep_alive", "on"), _thenEntityTurnsTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _thenEntityTurnsTemplate)
            .And(s => AfterSeconds(_config.Timeout), _andAfterSecondsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
            .When(s => EntityTurns("sensor.keep_alive", "off"), _thenEntityTurnsTemplate)
            .And(s => AfterSeconds(_config.Timeout), _andAfterSecondsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Once()), _thenEntityTurnedOffTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_house_mode_changed_to_night_turns_on_night_entities_and_turns_off_day_entities_even_when_state_is_active()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheKeepAliveEntityIs("sensor.keep_alive", "off"), _theKeepAliveEntityIsTemplate)
            .And(s => TheNightControlEntityIs("light.my_night_light", "off"), _theNightControlEntityIsTemplate)
            .And(s => TheNightTimeEntityStatesAre("night"))
            .And(s => TheNightTimeEntityIs("binary_sensor.house_mode", "day"))
            .And(s => TheTimeoutIsSeconds(60))
            .And(s => TheNightTimeoutIsSeconds(60))
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _thenEntityTurnsTemplate)
            .And(s => EntityTurns("sensor.keep_alive", "on"), _thenEntityTurnsTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "off"), _thenEntityTurnsTemplate)
            .And(s => AfterSeconds(_config.Timeout), _andAfterSecondsTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Never()), _thenEntityTurnedOffTemplate)
            .When(s => TriggerStateChange("binary_sensor.house_mode", "day", "night"), _stateChangedTemplate)
            .Then(s => TheEntityTurnedOffTimes("light.my_light", Times.Once()), _thenEntityTurnedOffTemplate)
            .Then(s => TheEntityTurnedOnTimes("light.my_night_light", Times.Once()), _thenEntityTurnedOnTemplate)
            .BDDfy();
    }

    [Fact]
    public void on_control_entity_override_to_on_sets_state_active()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheManagerIsInitialised())
            .When(s => OverrideEntity("light.my_light", "on"), _thenEntityTurnsTemplate)
            .Then(s => TheManagerStateIs(ManagerState.Override))
            .BDDfy();
    }

    [Fact]
    public void on_control_entity_override_to_off_sets_state_idle()
    {
        this.Given(s => GivenTheRoom("Room1"))
            .And(s => ThePresenceEntityIs("binary_sensor.my_motion_sensor", "off"), _thePresenceEntityIsTemplate)
            .And(s => TheControlEntityIs("light.my_light", "off"), _theControlEntityIsTemplate)
            .And(s => TheManagerIsInitialised())
            .When(s => EntityTurns("binary_sensor.my_motion_sensor", "on"), _thenEntityTurnsTemplate)
            .Then(s => TheManagerStateIs(ManagerState.Active))
            .When(s => OverrideEntity("light.my_light", "off"), _thenEntityTurnsTemplate)
            .Then(s => TheManagerStateIs(ManagerState.Idle))
            .BDDfy();
    }
}