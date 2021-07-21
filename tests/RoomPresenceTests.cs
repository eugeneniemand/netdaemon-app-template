using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Moq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Daemon.Fakes;
using Presence;
using Xunit;

/// <summary>
///     Tests the fluent API parts of the daemon
/// </summary>
/// <remarks>
///     Mainly the tests checks if correct underlying call to "CallService"
///     has been made.
/// </remarks>
public class RoomPresenceTests : RxAppMock
{
    private RoomPresenceImplementation? _app;

    public RoomPresenceTests()
    {
        Setup(s => s.RunEvery(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        {
            return Observable.Interval(span, TestScheduler)
                             .Subscribe(_ => action());
        });

        Setup(n => n.States).Returns(MockState);
        Setup(e => e.SetState(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<bool>()))
            .Returns<string, object, object, bool>((entityId, state, attributes, waitForResponse) =>
                UpdateMockState(entityId, state.ToString() ?? string.Empty, attributes));
        Setup(s => s.RunIn(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        {
            var result = new DisposableTimerResult(new CancellationToken());
            Observable.Timer(span, TestScheduler)
                      .Subscribe(_ => action(), result.Token);
            return result;
        });

        Setup(n => n.Entity(It.IsAny<string>())).Returns<string>(entityId =>
        {
            var m = new Mock<IRxEntityBase>();
            m.Setup(n => n.StateChanges)
             .Returns(StateChangesObservable.Where(f =>
                 f.New?.EntityId == entityId && f.New?.State != f.Old?.State));
            m.Setup(n => n.StateAllChanges).Returns(StateChangesObservable.Where(f => f.New?.EntityId == entityId));
            m.Setup(e => e.TurnOn(It.IsAny<object?>()))
             .Callback<object?>(attributes => UpdateMockState(entityId, "on", attributes));
            m.Setup(e => e.TurnOff(It.IsAny<object?>()))
             .Callback<object?>(attributes => UpdateMockState(entityId, "off", attributes));
            m.Setup(e => e.SetState(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<bool>()))
             .Callback<object, object, bool>((state, attributes, waitForResponse) =>
                 UpdateMockState(entityId, state.ToString() ?? string.Empty, attributes));
            return m.Object;
        });
    }

    [Fact]
    public void active_timer_is_disposed_when_enabled_switch_is_turned_off()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Timeout           = 1
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.EnabledSwitchEntityId, State     = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });
        app.Initialize();

        // ACT
        // Trigger presence to turn on control
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));

        // turn off presence and enabled
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");
        TriggerStateChange(config.EnabledSwitchEntityId, "on", "off");

        // Trigger presence should not not turn on control
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.Timeout + 1).Ticks);
        // ASSERT
        Assert.Equal(true, app.RoomIs(RoomState.Disabled));
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void app_does_not_fail_silently_if_exception_occurs()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestEx"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State = RoomState.Idle });
        app.Initialize();

        // ACT

        // ASSERT
    }


    [Fact]
    public void control_entity_only_turns_on_when_condition_matches_state()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name                 = "TestRoom",
            PresenceEntityIds    = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds     = new List<string> { "light.my_light" },
            ConditionEntityId    = "sun.sun",
            ConditionEntityState = "below_horizon",
            Timeout              = 1
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.EnabledSwitchEntityId, State     = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });
        MockState.Add(new EntityState { EntityId = config.ConditionEntityId, State         = "above_horizon" });
        app.Initialize();

        // ACT

        TriggerStateChange(config.ConditionEntityId, "above_horizon", "below_horizon");
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }


    [Fact]
    public void disable_circadian_when_control_entity_brightness_or_colour_is_changed_manually()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                    = "TestRoom",
            PresenceEntityIds       = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds        = new List<string> { "light.my_light" },
            CircadianSwitchEntityId = "switch.cl"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState
        {
            EntityId  = config.ControlEntityIds.First(),
            State     = "off",
            Attribute = new { brightness     = 10 },
            Context   = new Context { UserId = null }
        });
        MockState.Add(new EntityState { EntityId = config.CircadianSwitchEntityId, State = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State    = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        var oldState = new EntityState
        {
            EntityId  = config.ControlEntityIds.First(),
            State     = "on",
            Attribute = new { brightness = 10, color_temp = 0 }.ToDynamic(),
            Context   = new Context { UserId = "eugene" }
        };
        var newState = new EntityState
        {
            EntityId  = config.ControlEntityIds.First(),
            State     = "on",
            Attribute = new { brightness = 11, color_temp = 0 }.ToDynamic(),
            Context   = new Context { UserId = "eugene" }
        };
        TriggerStateChange(newState, oldState);

        // ASSERT
        VerifyState(config.CircadianSwitchEntityId, "off");
    }

    [Fact]
    public void disable_circadian_when_night_control_entity_brightness_or_colour_is_changed_manually()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                    = "TestRoom",
            PresenceEntityIds       = new List<string> { "binary_sensor.my_motion_sensor" },
            NightControlEntityIds   = new List<string> { "light.my_light" },
            CircadianSwitchEntityId = "switch.cl"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState
        {
            EntityId  = config.NightControlEntityIds.First(),
            State     = "off",
            Attribute = new { brightness     = 10 },
            Context   = new Context { UserId = null }
        });
        MockState.Add(new EntityState { EntityId = config.CircadianSwitchEntityId, State = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State    = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        var oldState = new EntityState
        {
            EntityId  = config.NightControlEntityIds.First(),
            State     = "on",
            Attribute = new { brightness = 10, color_temp = 0 }.ToDynamic(),
            Context   = new Context { UserId = "eugene" }
        };
        var newState = new EntityState
        {
            EntityId  = config.NightControlEntityIds.First(),
            State     = "on",
            Attribute = new { brightness = 11, color_temp = 0 }.ToDynamic(),
            Context   = new Context { UserId = "eugene" }
        };
        TriggerStateChange(newState, oldState);

        // ASSERT
        VerifyState(config.CircadianSwitchEntityId, "off");
    }


    [Fact]
    public void enable_circadian_when_room_state_is_set_to_idle()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                    = "TestRoom",
            PresenceEntityIds       = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds        = new List<string> { "light.my_light" },
            CircadianSwitchEntityId = "switch.cl"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "active" });
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        TriggerStateChange(config.ControlEntityIds.First(), "on", "off");

        // ASSERT
        VerifyState(config.CircadianSwitchEntityId, "on");
    }

    [Fact]
    public void keep_alive_entities_are_polled_and_turns_on_control_entities_regardless_of_presence()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name               = "TestRoom",
            PresenceEntityIds  = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds   = new List<string> { "light.my_light" },
            KeepAliveEntityIds = new List<string> { "sensor.keep_alive" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State   = "off" });
        MockState.Add(new EntityState { EntityId = config.KeepAliveEntityIds.First(), State = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State       = "idle" });

        app.Initialize();
        // ACT
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(5).Ticks);
        TriggerStateChange(config.KeepAliveEntityIds.First(), "on", "on");

        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.AtLeast(1));
    }

    [Fact]
    public void light_guard_does_not_start_timer_for_control_entities_when_active_presence_entities_exists()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Timeout           = 2
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();
        // ACT
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(59).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void light_guard_starts_timer_for_control_entities_that_were_not_turned_on_by_presence_entities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_light_night" },
            Timeout               = 90,
            OverrideTimeout       = 300
        };
        var presenceConfig = new PresenceConfig(config) { GuardTimeout = 1 };
        var app            = new RoomPresenceImplementation(Object, presenceConfig, new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State      = "on" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State          = "idle" });

        app.Initialize();
        // ACT
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(presenceConfig.GuardTimeout).Ticks);
        Assert.Equal(true, app.RoomIs(RoomState.Override));

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.OverrideTimeout).Ticks);
        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.Once());
        Assert.Equal(true, app.RoomIs(RoomState.Idle));
    }

    [Fact]
    public void light_guard_starts_timer_for_control_entities_that_were_not_turned_on_by_presence_entities_when_state_is_override_and_timer_is_null()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_light_night" },
            Timeout               = 90,
            OverrideTimeout       = 300
        };
        var presenceConfig = new PresenceConfig(config);
        var app            = new RoomPresenceImplementation(Object, presenceConfig, new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "override" });

        app.Initialize();
        // ACT
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(presenceConfig.GuardTimeout).Ticks);
        Assert.Equal(true, app.RoomIs(RoomState.Override));

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.OverrideTimeout).Ticks);
        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.Once());
        Assert.Equal(true, app.RoomIs(RoomState.Idle));
    }

    [Fact]
    public void lights_turn_off_when_keep_alive_entity_is_on_and_lux_is_above_threshold()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name               = "TestRoom",
            PresenceEntityIds  = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds   = new List<string> { "light.my_light" },
            KeepAliveEntityIds = new List<string> { "binary_sensor.keep_alive" },
            Timeout            = 10,
            LuxLimit           = 10,
            LuxEntityId        = "sensor.lux"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State   = "off" });
        MockState.Add(new EntityState { EntityId = config.KeepAliveEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.LuxEntityId, State                = 5 });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State       = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.KeepAliveEntityIds.First(), "off", "on");
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");
        TriggerStateChange(config.LuxEntityId, 5, 20);
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.Timeout).Ticks);

        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void lights_dont_turn_off_when_keep_alive_entity_is_on_and_lux_is_below_threshold()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name               = "TestRoom",
            PresenceEntityIds  = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds   = new List<string> { "light.my_light" },
            KeepAliveEntityIds = new List<string> { "binary_sensor.keep_alive" },
            Timeout            = 1
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State   = "off" });
        MockState.Add(new EntityState { EntityId = config.KeepAliveEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State       = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.KeepAliveEntityIds.First(), "off", "on");
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.Timeout).Ticks);
        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void lights_dont_turn_on_when_motion_triggered_and_lux_above_threshold()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            LuxEntityId       = "sensor.my_lux",
            LuxLimit          = 30
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.LuxEntityId, State               = "40" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void lights_dont_turn_on_when_new_and_old_event_state_is_off()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Name              = "TestRoom"
        };

        _app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First() });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State = RoomState.Idle });
        _app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "off");

        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void lights_dont_turn_off_when_presence_turns_off_and_timer_is_still_active()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Name              = "TestRoom",
            Timeout           = 35
        };

        _app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });
        _app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(30).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Exactly(1));
    }

    [Fact]
    public void lights_turn_off_when_no_presence_after_timeout()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Timeout           = 4,
            NightTimeout      = 1
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(4).Ticks);
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void lights_turn_on_when_motion_triggered()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void lights_turn_on_when_motion_triggered_and_lux_below_threshold_entity_id()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            LuxEntityId       = "sensor.my_lux",
            LuxLimit          = 60,
            LuxLimitEntityId  = "input_number.my_lux_limit"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.LuxLimitEntityId, State          = "30" });
        MockState.Add(new EntityState { EntityId = config.LuxEntityId, State               = "10" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void lights_turn_on_when_motion_triggered_and_lux_below_threshold_numeric()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            LuxEntityId       = "sensor.my_lux",
            LuxLimit          = 30
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.LuxEntityId, State               = "10" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void lights_turn_on_when_motion_triggered_on_more_than_one_sensor()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>
                { "binary_sensor.my_motion_sensor", "binary_sensor.my_motion_sensor_2" },
            ControlEntityIds = new List<string> { "light.my_light", "light.my_light_2" },
            Timeout          = 1
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State = RoomState.Idle });
        foreach (var entityId in config.PresenceEntityIds)
            MockState.Add(new EntityState { EntityId = entityId, State = "off" });
        foreach (var entityId in config.ControlEntityIds)
            MockState.Add(new EntityState { EntityId = entityId, State = "off" });

        app.Initialize();

        // ACT
        foreach (var entityId in config.PresenceEntityIds) TriggerStateChange(entityId, "off", "on");

        // ASSERT
        foreach (var entityId in config.ControlEntityIds) VerifyEntityTurnOn(entityId, times: Times.Once());
    }


    [Fact]
    //public void lights_turn_on_with_correct_brightness_and_color_when_turn_on_between_sunrise_hours()
    //{
    //    // ARRANGE
    //    var config = new RoomConfig
    //    {
    //        Name                     = "TestRoom",
    //        PresenceEntityIds        = new List<string> { "binary_sensor.my_motion_sensor" },
    //        ControlEntityIds         = new List<string> { "light.my_light" },
    //        SunriseStartBrightness   = 10,
    //        SunriseStartKelvin       = 100,
    //        SunriseStartTime         = "06:00",
    //        SunriseEndTime           = "07:00",
    //        SunriseEndBrightness     = 100,
    //        SunriseEndKelvin         = 1000,
    //        SunriseUpdateInterval    = 5,
    //        SunriseEnabled           = true,
    //        SunriseBrightnessEnabled = true,
    //        SunriseColourEnabled     = true,
    //        Timeout                  = 3600
    //    };

    //    var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

    //    MockState.Clear();
    //    MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
    //    MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
    //    TestScheduler.AdvanceTo("06:00:00");
    //    app.Initialize();

    //    // ACT & ASSERT
    //    TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 10 }, Times.Once());
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin     = 100 }, Times.Once());

    //    TestScheduler.AdvanceTo("06:05:00");
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 17.5 }, Times.Once());
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin     = 175.0 }, Times.Once());

    //    TestScheduler.AdvanceTo("06:10:00");
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 25.0 }, Times.Once());
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin     = 250.0 }, Times.Once());

    //    TestScheduler.AdvanceTo("06:15:00");
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 32.5 }, Times.Once());
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin     = 325.0 }, Times.Once());

    //    TestScheduler.AdvanceTo("06:20:00");
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 40.0 }, Times.Once());
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin     = 400.0 }, Times.Once());

    //    TestScheduler.AdvanceTo("06:25:00");
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 47.5 }, Times.Once());
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin     = 475.0 }, Times.Once());

    //    TestScheduler.AdvanceTo("06:40:00");
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 55.0 }, Times.Once());
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin     = 550.0 }, Times.Once());

    //    TestScheduler.AdvanceTo("07:00:00");
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 100.0 }, Times.Once());
    //    VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin     = 1000.0 }, Times.Once());
    //}
    public void lights_turn_on_with_correct_brightness_and_color_when_turn_on_between_sunset_hours()
    {
        var SunsetStartBrightness = 100;
        var SunsetStartKelvin     = 6000;
        var SunsetStartTime       = "17:00";
        var SunsetEndTime         = "18:00";
        var SunsetEndBrightness   = 10;
        var SunsetEndKelvin       = 1000;
    }

    [Fact]
    public void lux_limit_returns_default_when_state_cant_be_parsed()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            LuxEntityId       = "sensor.my_lux",
            LuxLimit          = 30,
            LuxLimitEntityId  = "input_number.my_lux_limit"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.LuxEntityId, State               = "Unavailable" });
        MockState.Add(new EntityState { EntityId = config.LuxLimitEntityId, State          = "Unavailable" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void night_lights_dont_turn_on_but_lights_do_turn_on_when_motion_triggered_when_night_entity_not_in_states()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId     = "binary_sensor.night",
            NightTimeEntityStates = new List<string> { "sleeping", "night" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State      = "off" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State             = "morning" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State          = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void night_lights_turn_off_when_no_presence_after_timeout()
    {
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId     = "binary_sensor.night",
            NightTimeEntityStates = new List<string> { "sleeping", "night" },
            Timeout               = 1,
            NightTimeout          = 3
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State      = "off" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State             = "night" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State          = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(2).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(3).Ticks);
        // ASSERT
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.AtLeast(1));
    }

    [Fact]
    public void night_lights_turn_on_when_motion_triggered_when_night_entity_on_and_state_is_in_list()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId     = "input_select.house_mode",
            NightTimeEntityStates = new List<string> { "sleeping", "night" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State      = "off" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State             = "sleeping" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State          = RoomState.Idle });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void room_presence_only_update_state_when_disabled()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Timeout           = 1
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });
        MockState.Add(new EntityState { EntityId = config.EnabledSwitchEntityId, State     = "off" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        Assert.Equal(true, app.RoomIs(RoomState.Disabled));
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void room_state_is_not_set_to_active_when_room_state_is_override()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "override" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        Assert.Equal(true, app.RoomIs(RoomState.Override));
    }

    [Fact]
    public void room_state_is_set_to_active_when_control_is_turned_on_by_presence()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        Assert.Equal(true, app.RoomIs(RoomState.Active));
    }

    [Fact]
    public void room_state_is_set_to_disabled_when_enabled_switch_is_turned_off()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Timeout           = 1
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.EnabledSwitchEntityId, State     = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "active" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.EnabledSwitchEntityId, "on", "off");

        // ASSERT
        Assert.Equal(true, app.RoomIs(RoomState.Disabled));
    }

    [Fact]
    public void room_state_is_set_to_override_when_control_is_turned_on_manually()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.ControlEntityIds.First(), "off", "on");

        // ASSERT
        Assert.Equal(true, app.RoomIs(RoomState.Override));
    }

    [Fact]
    public void room_state_is_set_to_override_when_night_control_is_turned_on_manually()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            NightControlEntityIds = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "off" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State          = "idle" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.NightControlEntityIds.First(), "off", "on");

        // ASSERT
        Assert.Equal(true, app.RoomIs(RoomState.Override));
    }

    [Fact]
    public void scenario_test1()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Timeout           = 300
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });

        app.Initialize();

        // 1st motion trigger and verify that control entity is on after motion stops
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(90).Ticks); // 1:30
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");

        VerifyState(config.ControlEntityIds.First(), "on");

        // 2nd motion trigger and another 60 seconds later and verify that control entity is on after motion stops
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(30).Ticks); // 2:00
        TriggerStateChange(config.PresenceEntityIds.First(), "off", null, "on", new { last_seen = TestScheduler.Now });

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(60).Ticks); // 3:00
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(90).Ticks); // 4:30
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");

        VerifyState(config.ControlEntityIds.First(), "on");

        // No more motion and original timeout expired but control entitiy should remain on as 2nd and 3rd motion should have reset the timeout
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(30).Ticks); // 5:00
        VerifyState(config.ControlEntityIds.First(), "on");

        // Advance to 5 minutes after last motion, now control entity should be off
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(180).Ticks); // 5:00
        VerifyState(config.ControlEntityIds.First(), "off");
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void scenario_test2()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Timeout           = 300
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });
        TestScheduler.AdvanceTo("20:02:15");

        app.Initialize();

        TriggerStateChange(config.PresenceEntityIds.First(), "off", null, "off", new { last_seen = TestScheduler.Now });
        TriggerStateChange(config.PresenceEntityIds.First(), "off", null, "on", new { last_seen  = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));

        TestScheduler.AdvanceTo("20:03:18");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:04:42");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:05:46");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:05:51");
        Assert.Equal(true, app.RoomIs(RoomState.Active));

        TestScheduler.AdvanceTo("20:05:57");
        Assert.Equal(true, app.RoomIs(RoomState.Active));

        TestScheduler.AdvanceTo("20:06:00");
        Assert.Equal(true, app.RoomIs(RoomState.Active));

        TestScheduler.AdvanceTo("20:06:08");
        Assert.Equal(true, app.RoomIs(RoomState.Active));

        TestScheduler.AdvanceTo("20:06:56");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TestScheduler.AdvanceTo("20:06:57");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:08:04");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:09:34");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "off", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        Assert.Equal(true, app.RoomIs(RoomState.Active));
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void timer_is_cancelled_and_set_to_idle_when_light_is_turned_off()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });
        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        TriggerStateChange(config.ControlEntityIds.First(), "on", "off");

        // ASSERT
        Assert.Equal(true, app.RoomIs(RoomState.Idle));
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void turning_off_night_mode_turns_off_night_control_entities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State         = "night" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "night", "day");
        // ASSERT        
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void turning_off_night_mode_turns_on_normal_control_entities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State         = "night" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "night", "day");
        // ASSERT        
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void turning_on_night_mode_does_not_turn_off_night_entities_that_are_also_normal_entities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light_1", "light.my_light_2" },
            NightControlEntityIds = new List<string> { "light.my_light_1" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        foreach (var entityId in config.ControlEntityIds)
            MockState.Add(new EntityState { EntityId = entityId, State = "on" });

        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State    = "day" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "day", "night");
        // ASSERT        
        VerifyEntityTurnOff(config.ControlEntityIds.Single(e => e == "light.my_light_1"), times: Times.Never());
        VerifyEntityTurnOff(config.ControlEntityIds.Single(e => e == "light.my_light_2"), times: Times.Once());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void turning_on_night_mode_turns_off_normal_control_entities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "on" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State         = "day" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "active" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "day", "night");
        // ASSERT        
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void turning_on_night_mode_turns_on_night_control_entities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "on" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State         = "day" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "active" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "day", "night");
        // ASSERT        
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void turning_on_night_mode_when_room_is_idle_does_nothing()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "on" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State         = "day" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "day", "night");
        // ASSERT        
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void turning_off_night_mode_when_room_is_idle_does_nothing()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds      = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityStates = new List<string> { "night" },
            NightTimeEntityId     = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "on" });
        MockState.Add(new EntityState { EntityId = config.NightTimeEntityId, State         = "night" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = "idle" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "night", "day");
        // ASSERT        
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void set_room_state_to_random_when_random_entity_state_matches_config()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            RandomEntityId    = "alarm.panel",
            RandomStates      = new List<string> { "arm_away" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);

        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State = "on" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State     = "idle" });
        MockState.Add(new EntityState { EntityId = config.RandomEntityId, State           = "disarmed" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.RandomEntityId, "disarmed", "arm_away");
        // ASSERT        
        Assert.Equal(true, app.RoomIs(RoomState.RandomWait));
    }

    [Fact]
    public void when_room_state_is_random_start_random_timer_and_turn_on_control_entity_and_off_after_timer_expired()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            RandomEntityId    = "alarm.panel",
            RandomStates      = new List<string> { "arm_away" }
        };


        var randomGenerator = new Mock<IRandomController>();
        randomGenerator.Setup(r => r.GetRandomDuration()).Returns(1);

        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), randomGenerator.Object);

        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State     = "idle" });
        MockState.Add(new EntityState { EntityId = config.RandomEntityId, State           = "disarmed" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.RandomEntityId, "disarmed", "arm_away");
        // ASSERT        
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void when_room_state_is_random_start_random_timer_and_turn_on_control_entity_and_off_after_timer_expired_repeat_until_random_state_not_match()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            RandomEntityId    = "alarm.panel",
            RandomStates      = new List<string> { "arm_away" }
        };


        var randomGenerator = new Mock<IRandomController>();
        randomGenerator.Setup(r => r.GetRandomDuration()).Returns(1);

        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), randomGenerator.Object);

        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State     = "idle" });
        MockState.Add(new EntityState { EntityId = config.RandomEntityId, State           = "disarmed" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.RandomEntityId, "disarmed", "arm_away");

        // ASSERT
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());

        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Exactly(2));
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Exactly(2));

        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Exactly(3));
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Exactly(3));

        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(randomGenerator.Object.GetRandomDuration()).Ticks);
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Exactly(4));

        // Change State - This should cancel all timers and set state to idle
        TriggerStateChange(config.RandomEntityId, "arm_away", "disarmed");
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Exactly(4));
        Assert.Equal(true, app.RoomIs(RoomState.Idle));
    }

    [Fact]
    public void RandomControllerReturnsAllNumbersInRangeOnce()
    {
        var       ctrl = new RandomController(1, 5);
        List<int> list = new();
        for (var i = 0; i < 100; i++)
        {
            list = new List<int>() { 1, 2, 3, 4, 5 };
            for (var l = 0; l < 5; l++) list.Remove(ctrl.GetRandomDuration());
        }

        Assert.Equal(0, list.Count);
    }

    [Fact]
    public void turn_off_any_control_entity_only_fires_for_non_nd_user()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config) { NdUserId = "NetDaemon" }, new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        var oldState = new EntityState
        {
            EntityId  = config.ControlEntityIds.First(),
            State     = "on",
            Attribute = null,
            Context   = new Context { UserId = "" }
        };
        var newNdState = new EntityState
        {
            EntityId  = config.ControlEntityIds.First(),
            State     = "off",
            Attribute = null,
            Context   = new Context { UserId = app.NdUserId }
        };
        var newNonNdState = new EntityState
        {
            EntityId  = config.ControlEntityIds.First(),
            State     = "off",
            Attribute = null,
            Context   = new Context { UserId = "eugene" }
        };


        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT        
        TriggerStateChange(oldState, newNdState);
        Assert.Equal(true, app.RoomIs(RoomState.Active));

        // ASSERT        
        TriggerStateChange(oldState, newNonNdState);
        Assert.Equal(true, app.RoomIs(RoomState.Idle));
    }

    [Fact]
    public void turn_off_any_control_entity_sets_to_idle()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State      = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TriggerStateChange(config.ControlEntityIds.First(), "on", "off");
        // ASSERT        
        Assert.Equal(true, app.RoomIs(RoomState.Idle));
    }


    [Fact]
    public void turn_off_any_night_control_entity_sets_to_idle()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name                  = "TestRoom",
            PresenceEntityIds     = new List<string> { "binary_sensor.my_motion_sensor" },
            NightControlEntityIds = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, new PresenceConfig(config), new Mock<IRandomController>().Object);


        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State     = "off" });
        MockState.Add(new EntityState { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.RoomPresenceEntityId, State          = RoomState.Idle });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TriggerStateChange(config.NightControlEntityIds.First(), "on", "off");
        // ASSERT        
        Assert.Equal(true, app.RoomIs(RoomState.Idle));
    }


    public new void VerifyEntityTurnOn(string entityId, dynamic? attributes = null, Times? times = null)
    {
        if (attributes is not null && attributes is not object)
            throw new NotSupportedException("attributes needs to be an object");

        var t = times ?? Times.Once();

        if (attributes is null)
            Verify(x => x.Entity(entityId).TurnOn(It.IsAny<object>()), t);
        else
            Verify(
                x => x.Entity(entityId).TurnOn(It.Is<object>(o => TestHelpers.AreEqualObjects((object) attributes, o))),
                t);
    }

    public new void VerifyState(string entityId, dynamic? state = null, dynamic? attributes = null)
    {
        var stateResult = false;
        if (attributes is not null && attributes is not object)
            throw new NotSupportedException("attributes needs to be an object");

        if (state is not null && state is not object)
            throw new NotSupportedException("state needs to be an object");

        var mockState = MockState.First(e => e.EntityId == entityId);

        if (state is not null)
        {
            if (attributes is not null)
                stateResult = mockState.State == state && mockState.Attribute == attributes;
            else
                stateResult = mockState.State == state;
        }

        if (attributes is not null)
            stateResult = mockState.Attribute == attributes;

        if (!stateResult && attributes is null)
            throw new ArgumentOutOfRangeException(entityId,
                $"State does not match, expected state '{state}' but was '{mockState.State}'");
        if (!stateResult && attributes is not null)
            throw new ArgumentOutOfRangeException(entityId,
                $"State does not match, expected state '{state}' but was '{mockState.State}'\nexpected attributes {attributes} but was {mockState.Attribute}");
    }

    private EntityState UpdateMockState(string entityId, string newState, object? attributes)
    {
        var state = MockState.FirstOrDefault(e => e.EntityId == entityId);
        if (state != null)
            MockState.Remove(state);

        var entityState = new EntityState { EntityId = entityId, State = newState, Attribute = attributes };
        MockState.Add(entityState);
        return entityState;
    }
}