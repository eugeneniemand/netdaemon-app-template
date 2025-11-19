using FluentAssertions;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;

namespace Niemand.Tests;

public class MorningKidsTests(MorningKidsSut sut, IEntities entities, StateChangeManager state, TestEntityBuilder entityBuilder, FakeLogCollector logs)
{
    private void SetTime(string time)
    {
        var parsed = TimeOnly.Parse(time, CultureInfo.InvariantCulture);        
        sut.Scheduler.AdvanceTo(DateTime.Today.Add(parsed.ToTimeSpan()).Ticks);
    }

    [Fact]
    public void LedsTurnOffOutsideActiveHours()
    {
        // Arrange
        var leds = entityBuilder.CreateLightEntity(entities.Light.PicoWLedsPicoWLeds.EntityId);
        state.Change(leds, "on");
        var instance = sut.Init();

        // Act - Set time outside active hours
        SetTime("08:01");
        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(entities.Light.PicoWLedsPicoWLeds)
        );
    }

    [Fact]
    public void LedsTurnOnDuringActiveHoursIfOff()
    {
        // Arrange
        var leds = entityBuilder.CreateEntity<LightEntity>(entities.Light.PicoWLedsPicoWLeds.EntityId, "off");
        var instance = sut.Init();

        // Act - Set time within active hours
        SetTime("07:00");
        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(entities.Light.PicoWLedsPicoWLeds, new LightTurnOnParameters { Effect = "MQTT Frame" })
        );
    }

    

    [Fact]
    public void CountdownProgressionIsCorrect()
    {
        // Arrange
        var leds = entityBuilder.CreateLightEntity(entities.Light.PicoWLedsPicoWLeds.EntityId);
        var instance = sut.Init();

        // Act - Set starting time and advance to check LED updates
        SetTime("07:00");
        
        // Verify initial state - all LEDs should be lit (30 minutes remaining)
        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
        state.ServiceCalls.Count.Should().Be(2); // Turn on + initial frame

        // Advance halfway - should see about half the LEDs lit
        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(30).Ticks);
        state.ServiceCalls.Count.Should().BeGreaterThan(30); // Should have multiple frame updates

        // Verify final state
        SetTime("08:00");
        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(entities.Light.PicoWLedsPicoWLeds)
        );
    }

    [Fact]
    public void EsphomeServiceReceivesFrameUpdates()
    {
        // Arrange
        var leds = entityBuilder.CreateEntity<LightEntity>(entities.Light.PicoWLedsPicoWLeds.EntityId, "off");
        var instance = sut.Init();

        // Act
        SetTime("07:00");
        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);

        // Assert
        // Verify that the ESPHome service received frame updates with hex strings
        state.ServiceCalls
            .Where(call => call.Domain == "esphome" && call.Service == "pico_w_leds_push_frame_hex")
            .Should().NotBeEmpty();
    }

    [Fact]
    public void Update_Logs_TimeRemainingAndLedCount()
    {
        // Arrange - ensure the light entity exists and app is initialized
        var leds = entityBuilder.CreateEntity<LightEntity>(entities.Light.PicoWLedsPicoWLeds.EntityId, "off");
        var instance = sut.Init();

        // Act - trigger update inside active hours
        SetTime("07:00");
        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);

        // Assert - verify a debug log with the expected content was produced
        logs.GetSnapshot().Should().Contain(e => 
            e.Message != null && e.Message.Contains("Time Remaining:") && e.Message.Contains("LEDs Lit")
        );
    }
}