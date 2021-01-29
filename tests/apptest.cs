using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Reactive.Testing;
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
public class AppTests : RxAppMock
{
    private RoomPresenceImplementation? _app;
    private readonly TestScheduler _testScheduler = new();

    public AppTests()
    {

        Setup(n => n.Entity(It.IsAny<string>())).Returns<string>(entityId =>
        {
            var m = new Mock<IRxEntityBase>();
            m.Setup(n => n.StateChanges).Returns(StateChangesObservable.Where(f => f.New?.EntityId == entityId && f.New?.State != f.Old?.State));
            m.Setup(n => n.StateAllChanges).Returns(StateChangesObservable.Where(f => f.New?.EntityId == entityId));
            m.Setup(e => e.TurnOn(null)).Callback(() =>  UpdateMockState(entityId, "on"));
            return m.Object;
        });

        Setup(s => s.RunIn(It.IsAny<TimeSpan>(), It.IsAny<Action>()))
            .Callback<TimeSpan, Action>((span, action) =>
            {
                Observable.Timer(span, _testScheduler)
                    .Subscribe(_ => action());
            });
    }

    private void UpdateMockState(string entityId, string newState)
    {
        var state = MockState.First(e => e.EntityId == entityId);
        MockState.Remove(state);
        MockState.Add(new EntityState() {EntityId = entityId, State = newState});
    }

    [Fact]
    public void LightsDontTurnOnWhenEventStateNewAndOldIsOff()
    {
        // ARRANGE
        var config = new RoomConfig()
        {
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" }
        };

        _app = new RoomPresenceImplementation(Object, new());
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First() });
        _app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "off");

        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void LightsTurnOnWhenMotionTriggered()
    {

        // ARRANGE
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" }
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First());
    }

    [Fact]
    public void NightLightsTurnOnWhenMotionTriggeredWhenNightEntityOn()
    {

        // ARRANGE
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" },
            NightControlEntityIds = new List<string>() { "light.my_night_light" },
            NightTimeEntityId = "binary_sensor.night"
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "on" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Never());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Once());
    }

    [Fact]
    public void NightLightsDontTurnOnWhenMotionTriggeredWhenNightEntityOff()
    {

        // ARRANGE
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" },
            NightControlEntityIds = new List<string>() { "light.my_night_light" },
            NightTimeEntityId = "binary_sensor.night"
        };
        var app = new RoomPresenceImplementation(Object, config);

        MockState.Clear();
        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightControlEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.NightTimeEntityId, State = "off" });

        app.Initialize();

        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        // ASSERT
        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOn(config.NightControlEntityIds.First(), times: Times.Never());
    }

    [Fact]
    public void LightsTurnOnWhenMotionTriggeredAndLuxBelowThresholdEntityId()
    {

        // ARRANGE
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" },
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
        VerifyEntityTurnOn(config.ControlEntityIds.First());
    }

    [Fact]
    public void LightsTurnOnWhenMotionTriggeredAndLuxBelowThresholdNumeric()
    {

        // ARRANGE
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" },
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
        VerifyEntityTurnOn(config.ControlEntityIds.First());
    }

    [Fact]
    public void LightsDontTurnOnWhenMotionTriggeredAndLuxAboveThreshold()
    {

        // ARRANGE
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" },
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
    public void LightsTurnOnWhenMotionTriggeredOnMoreThanOneSensor()
    {
        // ARRANGE
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor", "binary_sensor.my_motion_sensor_2" },
            ControlEntityIds = new List<string>() { "light.my_light", "light.my_light_2" },
            Timeout = 1
        };
        var app = new RoomPresenceImplementation(Object, config);

        foreach (var entityId in config.PresenceEntityIds) MockState.Add(new() { EntityId = entityId, State = "off" });
        foreach (var entityId in config.ControlEntityIds) MockState.Add(new() { EntityId = entityId, State = "off" });

        app.Initialize();

        // ACT
        foreach (var entityId in config.PresenceEntityIds) TriggerStateChange(entityId, "off", "on");

        // ASSERT
        foreach (var entityId in config.ControlEntityIds) VerifyEntityTurnOn(entityId, times: Times.Exactly(1));
    }

    [Fact]
    public void LightsTurnOffWhenNoPresenceAfterTimeout()
    {
        // ARRANGE 
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" },
            Timeout = 1
        };
        var app = new RoomPresenceImplementation(Object, config);


        MockState.Add(new() { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new() { EntityId = config.ControlEntityIds.First(), State = "off" });

        app.Initialize();
        // ACT
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");
        _testScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");
        _testScheduler.AdvanceBy(TimeSpan.FromSeconds(2).Ticks);
        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.AtLeast(1));
    }

    [Fact]
    public async Task LightsDontTurnOffWhenKeepAliveEnityIsOn()
    {
        // ARRANGE
        var config = new RoomConfig()
        {
            Name = "TestRoom",
            PresenceEntityIds = new List<string>() { "binary_sensor.my_motion_sensor" },
            ControlEntityIds = new List<string>() { "light.my_light" },
            KeepAliveEntityIds = new List<string>() { "binary_sensor.keep_alive" },
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

        _testScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        TriggerStateChange(config.PresenceEntityIds.First(), "on", "off");

        // ASSERT
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }
}
