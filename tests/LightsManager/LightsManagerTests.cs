using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using LightManager;
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
    private const string                                    _andAfterSecondsTemplate         = "and after <b>{0}</b> seconds";
    private const string                                    _stateChangedTemplate            = "when <b>{0}</b> changes from <b>{1}</b> to <b>{2}</b>";
    private const string                                    _theControlEntityIsTemplate      = "and the <b>control</b> entity <b>{0}</b> is <b>{1}</b>";
    private const string                                    _theKeepAliveEntityIsTemplate    = "and the <b>keep alive</b> entity <b>{0}</b> is <b>{1}</b>";
    private const string                                    _thenEntityTurnedOffTemplate     = "then <b>{0}</b> has <b>{1}</b> turned <b>off</b>";
    private const string                                    _thenEntityTurnedOnTemplate      = "then <b>{0}</b> has <b>{1}</b> turned <b>on</b>";
    private const string                                    _thenEntityTurnsTemplate         = "then <b>{0}</b> turns <b>{1}</b>";
    private const string                                    _theNightControlEntityIsTemplate = "and the <b>night control</b> entity <b>{0}</b> is <b>{1}</b>";
    private const string                                    _thePresenceEntityIsTemplate     = "and the <b>presence</b> entity <b>{0}</b> is <b>{1}</b>";
    private const string                                    _whenEntityTurnsTemplate         = "when <b>{0}</b> turns <b>{1}</b>";
    private const string                                    BinarySensorHouseMode            = "binary_sensor.house_mode";
    private const string                                    BinarySensorMyMotionSensor       = "binary_sensor.my_motion_sensor";
    private const string                                    SensorLux                        = "sensor.my_lux";
    private const string                                    InputLuxLimit                    = "input_number.my_lux_limit";
    private const string                                    LightMyLight                     = "light.my_light";
    private const string                                    LightMyNightLight                = "light.my_night_light";
    private const string                                    SensorKeepAlive                  = "sensor.keep_alive";
    private const string                                    SwitchMySwitch                   = "switch.my_switch";
    private const string                                    ON                               = "on";
    private const string                                    OFF                              = "off";
    private const string                                    LightMyLightTwo                  = "light.my_light_2";
    private       LightsManagerConfig                       _config;
    private       Manager                                   _manager;
    private       List<(object sender, HassEventArgs args)> _managerFiredEvents;

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

        Setup(s => s.RunEvery(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        {
            var result = new DisposableTimerResult(new CancellationToken());
            Observable.Interval(span, TestScheduler)
                      .Subscribe(_ => action(), result.Token);
            return result;
        });
    }

    [Fact]
    public void on_control_entity_override_to_on_sets_state_active()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenControlEntityIsManuallyTurned(ON))
            .Then(s => ThenTheManagerStateIs(ManagerState.Override))
            .BDDfy();
    }

    [Fact]
    public void on_guard_dog_patrols_entities_that_are_on_with_no_timer_are_turned_off()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(ON))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenTheGuardDogPatrols())
            .Then(s => ThenTheManagerStateIs(ManagerState.Override))
            .When(s => WhenAfterSeconds(_config.OverrideTimeout))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_guard_dog_patrols_entities_that_are_on_when_state_is_active_are_not_turned_off()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheGuardTimeoutIsSeconds(10))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .Then(s => ThenTheManagerStateIs(ManagerState.Active))
            .When(s => WhenTheGuardDogPatrols())
            .Then(s => ThenTheManagerStateIs(ManagerState.Active))
            .Then(s => ThenTheEntityStateIs(LightMyLight, OFF, Times.Never()))
            .BDDfy();
    }

    [Fact]
    public void on_init_if_enabled_switch_off_state_is_disabled()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheManagerEnabledIs(OFF))
            .And(s => GivenTheManagerIsInitialised())
            .Then(s => ThenTheManagerStateIs(ManagerState.Disabled))
            .BDDfy();
    }

    [Fact]
    public void on_enabled_switch_changed_from_off_to_on_the_state_is_idle()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheManagerEnabledIs(OFF))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenManagerIsTurned(ON))
            .Then(s => ThenTheManagerStateIs(ManagerState.Idle))
            .BDDfy();
    }

    [Fact]
    public void on_control_entity_override_for_two_entities_timer_is_still_active()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(LightMyLightTwo, OFF))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenControlEntityIsManuallyTurned(ON))
            .When(s => WhenControlEntityIsManuallyTurned(LightMyLightTwo, ON))
            .Then(s => ThenTheManagerStateIs(ManagerState.Override))
            .And(s => ThenManagerTimerSetEventFired(Times.Exactly(2), TimeSpan.FromSeconds(_config.OverrideTimeout)))
            .And(s => ThenThereIsAnActiveTimer())
            .BDDfy();
    }

    [Fact]
    public void on_house_mode_changed_to_day_turns_off_night_entities_and_turns_on_day_entities()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(ON))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheNightControlEntityIs(ON))
            .And(s => GivenTheNightTimeEntityStatesAre("night"))
            .And(s => GivenTheNightTimeEntityIs("night"))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenHouseModeChangesTo("day"))
            .Then(s => ThenTheEntityTurnedOffTimes(LightMyNightLight, Times.Once()))
            .Then(s => ThenTheControlEntityTurned(ON, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_house_mode_changed_to_night_turns_on_night_entities_and_turns_off_day_entities()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(ON))
            .And(s => GivenTheControlEntityIs(ON))
            .And(s => GivenTheNightControlEntityIs(OFF))
            .And(s => GivenTheNightTimeEntityStatesAre("night"))
            .And(s => GivenTheNightTimeEntityIs("day"))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenHouseModeChangesTo("night"))
            .Then(s => ThenTheNightControlEntityTurned(ON, Times.Once()))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_house_mode_changed_to_night_turns_on_night_entities_and_turns_off_day_entities_even_when_state_is_active()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheKeepAliveEntityIs(OFF))
            .And(s => GivenTheNightControlEntityIs(OFF))
            .And(s => GivenTheNightTimeEntityStatesAre("night"))
            .And(s => GivenTheNightTimeEntityIs("day"))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheNightTimeoutIsSeconds(60))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenKeepAliveEntityTurns(ON))
            .Then(s => ThenTheControlEntityTurned(ON, Times.AtLeastOnce()))
            .When(s => WhenPresenceEntityTurns(OFF))
            .And(s => WhenAfterSeconds(_config.Timeout))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .When(s => WhenHouseModeChangesTo("night"))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Once()))
            .Then(s => ThenTheNightControlEntityTurned(ON, Times.Once()))
            .BDDfy();
    }

    [Fact]
    public void on_house_mode_changed_when_not_active_still_performs_reconfiguration()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(LightMyLightTwo, OFF), _theControlEntityIsTemplate)
            .And(s => GivenTheNightControlEntityIs(LightMyLightTwo, OFF), _theControlEntityIsTemplate)
            .And(s => GivenTheNightControlEntityIs(OFF))
            .And(s => GivenTheNightTimeEntityStatesAre("night"))
            .And(s => GivenTheNightTimeEntityIs("day"))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenHouseModeChangesTo("night"))
            .Then(s => ThenTheControlEntitiesAre(LightMyLight, LightMyLightTwo))
            .Then(s => ThenTheNightControlEntitiesAre(LightMyNightLight, LightMyLightTwo))
            .BDDfy();
    }


    [Fact]
    public void on_keep_alive_entity_turns_off_control_entities_turns_off_after_timeout()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheKeepAliveEntityIs(OFF))
            .And(s => GivenTheTimeoutIsSeconds(60))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .When(s => WhenKeepAliveEntityTurns(ON))
            .Then(s => ThenTheControlEntityTurned(ON, Times.AtLeastOnce()))
            .When(s => WhenPresenceEntityTurns(OFF))
            .And(s => WhenAfterSeconds(_config.Timeout))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .When(s => WhenKeepAliveEntityTurns(OFF))
            .And(s => WhenAfterSeconds(_config.Timeout))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Once()))
            .BDDfy();
    }


    [Fact]
    public void when_enabled_switch_is_turned_off_then_manager_state_is_disabled()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheManagerEnabledIs(ON))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenManagerIsTurned(OFF))
            .Then(s => ThenTheManagerStateIs(ManagerState.Disabled))
            .BDDfy();
    }

    [Fact]
    public void on_control_entity_override_to_off_state_stays_override_if_other_control_entities_are_on()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(LightMyLightTwo, OFF))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenControlEntityIsManuallyTurned(LightMyLightTwo, OFF))
            .Then(s => ThenTheControlEntityTurned(OFF, Times.Never()))
            .And(s => ThenTheManagerStateIs(ManagerState.Override))
            .BDDfy();
    }

    [Fact]
    public void on_control_entity_override_to_off_sets_state_idle_if_all_control_entities_are_off()
    {
        this.Given(s => GivenTheRoom())
            .And(s => GivenThePresenceEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(OFF))
            .And(s => GivenTheControlEntityIs(LightMyLightTwo, OFF))
            .And(s => GivenTheManagerIsInitialised())
            .When(s => WhenPresenceEntityTurns(ON))
            .And(s => WhenControlEntityIsManuallyTurned(OFF))
            .And(s => WhenControlEntityIsManuallyTurned(LightMyLightTwo, OFF))
            .Then(s => ThenAllControlEntitiesAreOff())
            .And(s => ThenTheManagerStateIs(ManagerState.Idle))
            .BDDfy();
    }
}