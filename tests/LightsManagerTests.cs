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
using Xunit;

/// <summary>
///     Tests the fluent API parts of the daemon
/// </summary>
/// <remarks>
///     Mainly the tests checks if correct underlying call to "CallService"
///     has been made.
/// </remarks>
public class LightsManagerTests : RxAppMock
{
    public LightsManagerTests()
    {
        //    Setup(s => s.RunEvery(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        //    {
        //        return Observable.Interval(span, TestScheduler)
        //                         .Subscribe(_ => action());
        //    });


        //    Setup(s => s.RunIn(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        //    {
        //        var result = new DisposableTimerResult(new CancellationToken());
        //        Observable.Timer(span, TestScheduler)
        //                  .Subscribe(_ => action(), result.Token);
        //        return result;
        //    });
        Setup(n => n.States).Returns(MockState);
        //Setup(n => n.Entity(It.IsAny<string>())).Returns<string>(entityId =>
        //{
        //    var m = new Mock<IRxEntityBase>();
        //    m.Setup(n => n.StateChanges)
        //     .Returns(StateChangesObservable.Where(f =>
        //         f.New?.EntityId == entityId && f.New?.State != f.Old?.State));
        //    m.Setup(n => n.StateAllChanges).Returns(StateChangesObservable.Where(f => f.New?.EntityId == entityId));
        //    m.Setup(e => e.TurnOn(It.IsAny<object?>()))
        //     .Callback<object?>(attributes => UpdateMockState(entityId, "on", attributes));
        //    m.Setup(e => e.TurnOff(It.IsAny<object?>()))
        //     .Callback<object?>(attributes => UpdateMockState(entityId, "off", attributes));
        //    m.Setup(e => e.SetState(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<bool>()))
        //     .Callback<object, object, bool>((state, attributes, waitForResponse) =>
        //         UpdateMockState(entityId, state.ToString() ?? string.Empty, attributes));
        //    return m.Object;
        //});
    }


    [Fact]
    public void on_presence_started_event_turns_on_control_entities()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" }
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void on_presence_started_event_turns_on_multiple_control_entities()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light", "switch.my_switch" }
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.ToList()[0], times: Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.ToList()[1], times: Times.Once());
    }

    [Fact]
    public void on_presence_started_event_turns_on_control_entities_when_lux_is_below_limit()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            LuxLimitEntityId  = "input_number.lux_limit",
            LuxEntityId       = "sensor.lux"
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.LuxLimitEntityId, State          = 100 });
        MockState.Add(new EntityState { EntityId = config.LuxEntityId, State               = 30 });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void on_presence_started_event_doesnt_turns_on_control_entities_when_lux_is_above_limit()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            LuxLimitEntityId  = "input_number.lux_limit",
            LuxEntityId       = "sensor.lux"
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.LuxLimitEntityId, State          = 100 });
        MockState.Add(new EntityState { EntityId = config.LuxEntityId, State               = 300 });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void on_presence_stopped_turns_off_control_entities_after_timeout()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light", "switch.my_switch" },
            Timeout           = 60
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "on" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "on" });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");

        // ASSERT
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(10).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.ToList()[0], times: Times.Never());
        VerifyEntityTurnOff(config.ControlEntityIds.ToList()[1], times: Times.Never());

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.Timeout).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.ToList()[0], times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.ToList()[1], times: Times.Once());
    }

    [Fact]
    public void on_presence_stopped_turns_off_control_entities_after_night_timeout()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "binary_sensor.house_mode",
            Timeout               = 60,
            NightTimeout          = 30
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "on" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "on" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State         = "night" });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");

        // ASSERT
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(10).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.NightTimeout).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void on_presence_stopped_does_not_set_timer_if_presence_sensor_is_on()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name               = "TestRoom",
            PresenceEntityIds  = new List<string> { "binary_sensor.my_motion_sensor" },
            KeepAliveEntityIds = new List<string> { "binary_sensor.my_keep_alive_sensor" },
            ControlEntityIds   = new List<string> { "light.my_light" },
            Timeout            = 60
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State  = "on" });
        MockState.Add(new EntityState { EntityId = config.KeepAliveEntityIds.First(), State = "on" });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");

        // ASSERT
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.Timeout * 2).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void on_house_mode_changed_to_day_turns_off_night_entities_and_turns_on_day_entities()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "binary_sensor.house_mode"
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "on" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State      = "off" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "on" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State             = "night" });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.NightTimeEntityId, "night", "day");

        // ASSERT
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void on_house_mode_changed_to_night_turns_on_night_entities_and_turns_off_day_entities()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "binary_sensor.house_mode"
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "on" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State      = "on" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State             = "day" });

        var app = new Manager(Object, new Configurator(config));

        // ACT
        TriggerStateChange(config.NightTimeEntityId, "day", "night");

        // ASSERT
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void on_house_mode_changed_when_not_active_still_performs_reconfiguration()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "binary_sensor.house_mode"
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State      = "off" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State             = "day" });

        var configurator = new Configurator(config);
        var app          = new Manager(Object, configurator);

        // ACT
        Assert.Equal(config.ControlEntityIds.First(), configurator.Lights.First().EntityId);
        TriggerStateChange(config.NightTimeEntityId, "day", "night");

        // ASSERT
        Assert.Equal(config.NightControlEntityIds.First(), configurator.Lights.First().EntityId);
    }
}