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
using Sprinkler;
using Xunit;

/// <summary>
///     Tests the fluent API parts of the daemon
/// </summary>
/// <remarks>
///     Mainly the tests checks if correct underlying call to "CallService"
///     has been made.
/// </remarks>
public class SprinklerTests : RxAppMock
{
    private SprinklerApp? _app;
    

    [Fact]
    public void ZoneInitializesToOneWhenZoneStateIsNull()
    {
        // ARRANGE
        var app = new SprinklerApp(Object);
        MockState.Add(new EntityState() { EntityId = "sensor.zone" });
        MockState.Add(new EntityState() { EntityId = "switch.Sprinkler", State = "off" });
        MockState.Add(new EntityState() { EntityId = "input_number.selected_zone" });
        MockState.Add(new EntityState() { EntityId = "input_number.switch_delay_ms", State = "1" });

        app.Initialize();

        // ACT
        TriggerStateChange("input_number.selected_zone", "0", "1" );

        // ASSERT
        VerifyEntityTurnOn("switch.Sprinkler", times: Times.Once());
        Assert.Equal(1, app._zone);
    }

    [Fact]
    public void ZoneResetsToOneWhenZoneExceedsSix()
    {
        // ARRANGE
        var app = new SprinklerApp(Object);
        MockState.Add(new EntityState() { EntityId = "sensor.zone", State = "6" });
        MockState.Add(new EntityState() { EntityId = "switch.Sprinkler", State = "off" });
        MockState.Add(new EntityState() { EntityId = "input_number.selected_zone", State = "1" });
        MockState.Add(new EntityState() { EntityId = "input_number.switch_delay_ms", State = "1" });
        app.Initialize();

        // ACT
        TriggerStateChange("input_number.selected_zone", "0", "1");

        // ASSERT
        VerifyEntityTurnOff("switch.Sprinkler", times: Times.Once());
        VerifyEntityTurnOn("switch.Sprinkler", times: Times.Exactly(2));
        Assert.Equal(1, app._zone);
    }

    [Fact]
    public void ZoneMovesToProvidedZone()
    {
        // ARRANGE
        var app = new SprinklerApp(Object);
        MockState.Add(new EntityState() { EntityId = "sensor.zone", State = "3" });
        MockState.Add(new EntityState() { EntityId = "switch.Sprinkler", State = "off" });
        MockState.Add(new EntityState() { EntityId = "input_number.selected_zone", State = "2" });
        MockState.Add(new EntityState() { EntityId = "input_number.switch_delay_ms", State = "1" });
        app.Initialize();

        // ACT
        TriggerStateChange("input_number.selected_zone", "0", "2");
        
        // ASSERT
        VerifyEntityTurnOff("switch.Sprinkler", times: Times.Exactly(5));
        VerifyEntityTurnOn("switch.Sprinkler", times: Times.Exactly(6));
        Assert.Equal(2, app._zone);
    }

    [Fact]
    public void ZoneZeroDoesNothing()
    {
        // ARRANGE
        var app = new SprinklerApp(Object);
        MockState.Add(new EntityState() { EntityId = "sensor.zone", State = "3" });
        MockState.Add(new EntityState() { EntityId = "switch.Sprinkler", State = "off" });
        MockState.Add(new EntityState() { EntityId = "input_number.selected_zone", State = "3" });
        MockState.Add(new EntityState() { EntityId = "input_number.switch_delay_ms", State = "1" });
        app.Initialize();

        // ACT
        TriggerStateChange("input_number.selected_zone", "3", "0");

        // ASSERT
        VerifyEntityTurnOff("switch.Sprinkler", times: Times.Never());
        VerifyEntityTurnOn("switch.Sprinkler", times: Times.Never());
        Assert.Equal(3, app._zone);
    }


    [Fact]
    public void SprinklerTurnsOffAfterSpecifiedDuration()
    {
        // ARRANGE
        var app = new SprinklerApp(Object);
        MockState.Add(new EntityState() { EntityId = "sensor.zone", State = "3" });
        MockState.Add(new EntityState() { EntityId = "switch.Sprinkler", State = "off" });
        MockState.Add(new EntityState() { EntityId = "input_number.selected_zone", State = "0" });
        MockState.Add(new EntityState() { EntityId = "input_number.switch_delay_ms", State = "1" });
        MockState.Add(new EntityState() { EntityId = "input_number.zone_4_duration_minutes", State = "10" });
        MockState.Add(new EntityState() { EntityId = "input_number.zone_5_duration_minutes", State = "5" });
        app.Initialize();

        // ACT ZONE 4
        TriggerStateChange("input_number.selected_zone", "0", "4");

        // ASSERT ZONE 4
        VerifyState("switch.Sprinkler", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(10).Ticks);
        VerifyState("switch.Sprinkler", "off");

        // ACT ZONE 5
        TriggerStateChange("input_number.selected_zone", "4", "5");

        // ASSERT ZONE 5
        VerifyState("switch.Sprinkler", "on");
        TestScheduler.AdvanceBy(TimeSpan.FromMinutes(5).Ticks);
        VerifyState("switch.Sprinkler", "off");
    }
}
