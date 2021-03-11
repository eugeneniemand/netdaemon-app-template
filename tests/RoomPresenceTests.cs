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
    public RoomPresenceTests()
    {
        Setup(s => s.RunEvery(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        {
            return Observable.Interval(span, TestScheduler)
                .Subscribe(_ => action());
        });

        Setup(n => n.States).Returns(MockState);
        Setup(e => e.SetState(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<bool>())).Returns<string, object, object, bool>((entityId, state, attributes, waitForResponse) => UpdateMockState(entityId, state.ToString() ?? string.Empty, attributes));
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
            m.Setup(n => n.StateChanges).Returns(StateChangesObservable.Where(f => f.New?.EntityId == entityId && f.New?.State != f.Old?.State));
            m.Setup(n => n.StateAllChanges).Returns(StateChangesObservable.Where(f => f.New?.EntityId == entityId));
            m.Setup(e => e.TurnOn(It.IsAny<object?>())).Callback<object?>(attributes => UpdateMockState(entityId, "on", attributes));
            m.Setup(e => e.TurnOff(It.IsAny<object?>())).Callback<object?>(attributes => UpdateMockState(entityId, "off", attributes));
            m.Setup(e => e.SetState(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<bool>())).Callback<object, object, bool>((state, attributes, waitForResponse) => UpdateMockState(entityId, state.ToString() ?? string.Empty, attributes));
            return m.Object;
        });
    }

    private RoomPresenceImplementation? _app;

    private EntityState UpdateMockState(string entityId, string newState, object? attributes)
    {
        var state = MockState.FirstOrDefault(e => e.EntityId == entityId);
        if (state != null)
            MockState.Remove(state);

        var entityState = new EntityState { EntityId = entityId, State = newState, Attribute = attributes };
        MockState.Add(entityState);
        return entityState;
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

        if (!stateResult && attributes is null) throw new ArgumentOutOfRangeException(entityId, $"State does not match, expected state '{state}' but was '{mockState.State}'");
        if (!stateResult && attributes is not null) throw new ArgumentOutOfRangeException(entityId, $"State does not match, expected state '{state}' but was '{mockState.State}'\nexpected attributes {attributes} but was {mockState.Attribute}");
    }

    public void LightsTurnOnWithCorrectBrightnessAndColorWhenTurnOnBetweenSunsetHours()
    {
        var SunsetStartBrightness = 100;
        var SunsetStartKelvin = 6000;
        var SunsetStartTime = "17:00";
        var SunsetEndTime = "18:00";
        var SunsetEndBrightness = 10;
        var SunsetEndKelvin = 1000;
    }

    [Fact]
    public void AppDoesNotFailSilentlyIfExceptionOccurs()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestEx"
        };
        var app = new RoomPresenceImplementation(Object, config);
        app.Initialize();

        // ACT

        // ASSERT
    }

    [Fact]
    public void LightGuardDoesNotStartTimerForControlEntitiesWhenActivePresenceEntitiesExists()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            Timeout = 2
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "on" });

        app.Initialize();
        // ACT
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(59).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void DisableCircadianWhenControlEntityBrightnessOrColourIsChangedManually()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            CircadianSwitchEntityId = "switch.cl"
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off", Attribute = new { brightness = 10 }, Context = new Context() { UserId = null } });
        MockState.Add(new() { EntityId = config.CircadianSwitchEntityId, State = "on" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        var oldState = new EntityState() { EntityId = config.ControlEntityIds.First(), State = "on", Attribute = (new { brightness = 10, color_temp = 0 }).ToDynamic(), Context = new Context() { UserId = "eugene" } };
        var newState = new EntityState() { EntityId = config.ControlEntityIds.First(), State = "on", Attribute = (new { brightness = 11, color_temp = 0 }).ToDynamic(), Context = new Context() { UserId = "eugene" } };
        TriggerStateChange(newState, oldState);

        // ASSERT
        VerifyState(config.CircadianSwitchEntityId, "off");
    }

    [Fact]
    public void DisableCircadianWhenNightControlEntityBrightnessOrColourIsChangedManually()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            NightControlEntityIds = new List<string> { "light.my_light" },
            CircadianSwitchEntityId = "switch.cl"
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightControlEntityIds.First(), State = "off", Attribute = new { brightness = 10 }, Context = new Context() { UserId = null } });
        MockState.Add(new() { EntityId = config.CircadianSwitchEntityId, State = "on" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        var oldState = new EntityState() { EntityId = config.NightControlEntityIds.First(), State = "on", Attribute = (new { brightness = 10, color_temp = 0 }).ToDynamic(), Context = new Context() { UserId = "eugene" } };
        var newState = new EntityState() { EntityId = config.NightControlEntityIds.First(), State = "on", Attribute = (new { brightness = 11, color_temp = 0 }).ToDynamic(), Context = new Context() { UserId = "eugene" } };
        TriggerStateChange(newState, oldState);

        // ASSERT
        VerifyState(config.CircadianSwitchEntityId, "off");
    }

    [Fact]
    public void LightGuardStartsTimerForControlEntitiesThatWereNotTurnedOnByPresenceEntities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_light_night" },
            Timeout = 300,
            OverrideTimeout = 300
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "on" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "idle" });

        app.Initialize();
        // ACT
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Override.ToString().ToLower());

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.Timeout).Ticks);
        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.Once());
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Idle.ToString().ToLower());
    }

    [Fact]
    public void TurnOffAnyControlEntitySetsToIdle()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TriggerStateChange(config.ControlEntityIds.First(), "on", "off");
        // ASSERT        
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Idle.ToString().ToLower());
    }


    [Fact]
    public void TurnOffAnyNightControlEntitySetsToIdle()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            NightControlEntityIds = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightControlEntityIds.First(), State = "off" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        TriggerStateChange(config.NightControlEntityIds.First(), "on", "off");
        // ASSERT        
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Idle.ToString().ToLower());
    }

    [Fact]
    public void SwitchFromNormalToNightModeDoesNotTurnOffNightEntitiesThatIsAlsoNormalEntities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light_1", "light.my_light_2" },
            NightControlEntityIds = new List<string> { "light.my_light_1" },
            NightTimeEntityId = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        foreach (var entityId in config.ControlEntityIds)
            MockState.Add(new() { EntityId = entityId, State = "on" });

        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "off" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "off", "on");
        // ASSERT        
        VerifyEntityTurnOff(config.ControlEntityIds.Single(e => e == "light.my_light_1"), times: Times.Never());
        VerifyEntityTurnOff(config.ControlEntityIds.Single(e => e == "light.my_light_2"), times: Times.Once());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void SwitchFromNormalToNightModeTurnsOnNightControlEntities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "on" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "off" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "off", "on");
        // ASSERT        
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void SwitchFromNormalToNightModeTurnsOffNormalControlEntities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "on" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "off" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "off", "on");
        // ASSERT        
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void SwitchFromNightToNormalModeTurnsOnNormalControlEntities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "on" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "on", "off");
        // ASSERT        
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void SwitchFromNightToNormalModeTurnsOffNightControlEntities()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId = "switch.night_mode"
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "on" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.NightTimeEntityId, "on", "off");
        // ASSERT        
        VerifyEntityTurnOff(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void LightsDontTurnOffWhenKeepAliveEntityIsOn()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            KeepAliveEntityIds = new List<string> { "binary_sensor.keep_alive" },
            Timeout = 1
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.KeepAliveEntityIds.First(), State = "off" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.KeepAliveEntityIds.First(), "off", "on");
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");

        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void LightsDontTurnOnWhenMotionTriggeredAndLuxAboveThreshold()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            LuxEntityId = "sensor.my_lux",
            LuxLimit = 30
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.LuxEntityId, State = "40" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void LightsDontTurnOnWhenNewAndOldEventStateIsOff()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            Name = "TestRoom"
        };

        _app = new RoomPresenceImplementation(Object, config);
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First() });
        _app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "off");

        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void LightsTurnOffWhenNoPresenceAfterTimeout()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            Timeout = 4,
            NightTimeout = 1
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });

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
    public void LightsTurnOnWhenMotionTriggered()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void LightsTurnOnWhenMotionTriggeredAndLuxBelowThresholdEntityId()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            LuxEntityId = "sensor.my_lux",
            LuxLimit = 60,
            LuxLimitEntityId = "input_number.my_lux_limit"
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.LuxLimitEntityId, State = "30" });
        MockState.Add(new() { EntityId = config.LuxEntityId, State = "10" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void LightsTurnOnWhenMotionTriggeredAndLuxBelowThresholdNumeric()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            LuxEntityId = "sensor.my_lux",
            LuxLimit = 30
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.LuxEntityId, State = "10" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void LightsTurnOnWhenMotionTriggeredOnMoreThanOneSensor()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor", "binary_sensor.my_motion_sensor_2" },
            ControlEntityIds = new List<string> { "light.my_light", "light.my_light_2" },
            Timeout = 1
        };
        var app = new RoomPresenceImplementation(Object, config);

        foreach (var entityId in config.PresenceEntityIds) MockState.Add(new() { EntityId = entityId, State = "off" });
        foreach (var entityId in config.ControlEntityIds) MockState.Add(new() { EntityId = entityId, State = "off" });

        app.Initialize();

        // ACT
        foreach (var entityId in config.PresenceEntityIds) TriggerStateChange(entityId, "off", "on");

        // ASSERT
        foreach (var entityId in config.ControlEntityIds) VerifyEntityTurnOn(entityId, times: Times.Once());
    }

    [Fact]
    public void RoomStateIsSetToDisabledWhenEnabledSwitchIsTurnedOff()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            Timeout = 1
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.EnabledSwitchEntityId, State = "on" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "active" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.EnabledSwitchEntityId, "on", "off");

        // ASSERT
        VerifyState(config.RoomPresenceEntityId, RoomState.Disabled.ToString().ToLower());
    }

    [Fact]
    public void RoomStateIsSetToOverrideWhenControlIsTurnedOnManually()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "idle" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.ControlEntityIds.First(), "off", "on");

        // ASSERT
        VerifyState(config.RoomPresenceEntityId, RoomState.Override.ToString().ToLower());
    }

    [Fact]
    public void RoomStateIsSetToOverrideWhenNightControlIsTurnedOnManually()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            NightControlEntityIds = new List<string> { "light.my_light" },
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "idle" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.NightControlEntityIds.First(), "off", "on");

        // ASSERT
        VerifyState(config.RoomPresenceEntityId, RoomState.Override.ToString().ToLower());
    }

    [Fact]
    public void RoomStateIsSetToActiveWhenControlIsTurnedOnByPresense()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "idle" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyState(config.RoomPresenceEntityId, RoomState.Active.ToString().ToLower());
    }

    [Fact]
    public void ActiveTimerIsDisposedWhenEnabledSwitchIsTurnedOff()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            Timeout = 1
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.EnabledSwitchEntityId, State = "on" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "idle" });
        app.Initialize();

        // ACT
        // Trigger presence to turn on control
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        VerifyState(config.RoomPresenceEntityId, RoomState.Active.ToString().ToLower());

        // turn off presence and enabled
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");
        TriggerStateChange(config.EnabledSwitchEntityId, "on", "off");

        // Trigger presence should not not turn on control
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        TestScheduler.AdvanceBy(TimeSpan.FromSeconds(config.Timeout + 1).Ticks);
        // ASSERT
        VerifyState(config.RoomPresenceEntityId, RoomState.Disabled.ToString().ToLower());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void TimerIsCancelledAndSetToIdleWhenLightIsTurnedOff()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        TriggerStateChange(config.ControlEntityIds.First(), "on", "off");

        // ASSERT
        VerifyState(config.RoomPresenceEntityId, RoomState.Idle.ToString().ToLower());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Once());
    }


    [Fact]
    public void LightsTurnOnWithCorrectBrightnessAndColorWhenTurnOnBetweenSunriseHours()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            SunriseStartBrightness = 10,
            SunriseStartKelvin = 100,
            SunriseStartTime = "06:00",
            SunriseEndTime = "07:00",
            SunriseEndBrightness = 100,
            SunriseEndKelvin = 1000,
            SunriseUpdateInterval = 5,
            SunriseEnabled = true,
            SunriseBrightnessEnabled = true,
            SunriseColourEnabled = true,
            Timeout = 3600
        };

        var app = new RoomPresenceImplementation(Object, config, TestScheduler);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        TestScheduler.AdvanceTo("06:00:00");
        app.Initialize();

        // ACT & ASSERT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 10 }, Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin = 100 }, Times.Once());

        TestScheduler.AdvanceTo("06:05:00");
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 17.5 }, Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin = 175.0 }, Times.Once());

        TestScheduler.AdvanceTo("06:10:00");
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 25.0 }, Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin = 250.0 }, Times.Once());

        TestScheduler.AdvanceTo("06:15:00");
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 32.5 }, Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin = 325.0 }, Times.Once());

        TestScheduler.AdvanceTo("06:20:00");
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 40.0 }, Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin = 400.0 }, Times.Once());

        TestScheduler.AdvanceTo("06:25:00");
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 47.5 }, Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin = 475.0 }, Times.Once());

        TestScheduler.AdvanceTo("06:40:00");
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 55.0 }, Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin = 550.0 }, Times.Once());

        TestScheduler.AdvanceTo("07:00:00");
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { brightness = 100.0 }, Times.Once());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), new { kelvin = 1000.0 }, Times.Once());
    }


    public new void VerifyEntityTurnOn(string entityId, dynamic? attributes = null, Times? times = null)
    {
        if (attributes is not null && attributes is not object)
            throw new NotSupportedException("attributes needs to be an object");

        var t = times ?? Times.Once();

        if (attributes is null)
        {
            Verify(x => x.Entity(entityId).TurnOn(It.IsAny<object>()), t);
        }
        else
        {
            Verify(x => x.Entity(entityId).TurnOn(It.Is<object>(o => TestHelpers.AreEqualObjects((object)attributes, o))), t);
        }
    }

    [Fact]
    public void LuxLimitReturnsDefaultWhenStateCantBeParsed()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            LuxEntityId = "sensor.my_lux",
            LuxLimit = 30,
            LuxLimitEntityId = "input_number.my_lux_limit"
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.LuxEntityId, State = "Unavailable" });
        MockState.Add(new() { EntityId = config.LuxLimitEntityId, State = "Unavailable" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void NightLightsDontTurnOnButLightsDoTurnOnWhenMotionTriggeredWhenNightEntityNotInStates()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId = "binary_sensor.night",
            NightTimeEntityStates = new List<string> { "sleeping", "night" }
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "morning" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void NightLightsTurnOffWhenNoPresenceAfterTimeout()
    {
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId = "binary_sensor.night",
            NightTimeEntityStates = new List<string> { "sleeping", "night" },
            Timeout = 1,
            NightTimeout = 3
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "night" });

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
    public void NightLightsTurnOnWhenMotionTriggeredWhenNightEntityOnAndStateIsInList()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            NightControlEntityIds = new List<string> { "light.my_night_light" },
            NightTimeEntityId = "input_select.house_mode",
            NightTimeEntityStates = new List<string> { "sleeping", "night" }
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "sleeping" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void RoomPresenceOnlyUpdateStateWhenDisabled()
    {
        // ARRANGE 
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            Timeout = 1
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "idle" });
        MockState.Add(new() { EntityId = config.EnabledSwitchEntityId, State = "off" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        // ASSERT
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Disabled.ToString().ToLower());
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void ScenarioTest1()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            Timeout = 300
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "idle" });

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
    public void ScenarioTest2()
    {
        // ARRANGE
        var config = new RoomConfig
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string> { "light.my_light" },
            Timeout = 300
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.RoomPresenceEntityId, State = "idle" });
        TestScheduler.AdvanceTo("20:02:15");

        app.Initialize();

        TriggerStateChange(config.PresenceEntityIds.First(), "off", null, "off", new { last_seen = TestScheduler.Now });
        TriggerStateChange(config.PresenceEntityIds.First(), "off", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());

        TestScheduler.AdvanceTo("20:03:18");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:04:42");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:05:46");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:05:51");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());

        TestScheduler.AdvanceTo("20:05:57");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());

        TestScheduler.AdvanceTo("20:06:00");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());

        TestScheduler.AdvanceTo("20:06:08");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());

        TestScheduler.AdvanceTo("20:06:56");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TestScheduler.AdvanceTo("20:06:57");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:08:04");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "on", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());

        TestScheduler.AdvanceTo("20:09:34");
        TriggerStateChange(config.PresenceEntityIds.First(), "on", null, "off", new { last_seen = TestScheduler.Now });
        VerifyState(config.ControlEntityIds.First(), "on");
        VerifyState(config.RoomPresenceEntityId.ToLower(), RoomState.Active.ToString().ToLower());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }
}