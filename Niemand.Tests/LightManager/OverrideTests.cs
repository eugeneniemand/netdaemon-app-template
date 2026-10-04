using LightManagerV2;
using NetDaemon.HassModel.Entities;
using NetDaemon.Extensions.Testing;
using System.Text.Json;

namespace Niemand.Tests.LightManager;

public class OverrideTests(LightManagerSut sut, StateChangeManager state, TestEntityBuilder entityBuilder, IHaContext haContext)
{
    // Verifies override mode prevents automatic turn-off during presence timeout.
    [Fact]
    public void GivenOverrideActive_WhenPresenceTurnsOff_ThenControlLightsDoNotTurnOff()
    {
        // Arrange
        state.Change(sut.Config.Light(), "off");
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Init();

        // Act
        state.TurnOnManually(sut.Config.Light());

        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().Timeout).Ticks);

        // Assert
        state.ServiceCalls.Light().Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    // Verifies manual turn-off cancels override timeout when all controlled lights are off.
    [Fact]
    public void GivenAllControlLightsOff_WhenUserTurnsLightsOff_ThenOverrideTimeoutIsCancelled()
    {
        // Arrange
        state.Change(sut.Config.Light(2), "on");
        sut.Init();

        // Act
        state.TurnOnManually(sut.Config.Light());

        state.TurnOffManually(sut.Config.Light());
        state.TurnOffManually(sut.Config.Light(2));
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light(2))
        );
    }

    // Verifies override timeout remains active while any controlled light stays on.
    [Fact]
    public void GivenSomeControlLightsStillOn_WhenUserTurnsOneLightOff_ThenOverrideTimeoutRemainsActive()
    {
        // Arrange
        state.Change(sut.Config.Light(2), "on");
        sut.Init();

        // Act
        state.TurnOnManually(sut.Config.Light());
        sut.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(150).Ticks);
        state.TurnOffManually(sut.Config.Light());
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light(2))
        );
    }

    // Verifies manual light-on starts override timeout lifecycle.
    [Fact]
    public void GivenManualTurnOn_WhenOverrideTimeoutElapses_ThenLightTurnsOff()
    {
        // Arrange
        sut.Init();

        // Act
        state.TurnOnManually(sut.Config.Light());
        sut.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(150).Ticks);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    // Verifies manual day turn-on without explicit brightness gets manager day brightness.
    [Fact]
    public void GivenManualTurnOnWithoutBrightnessInDayMode_WhenOverrideStarts_ThenDayBrightnessIsApplied()
    {
        // Arrange
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "day");
        sut.Init();

        // Act
        state.TurnOnManually(
            sut.Config.Light(),
            attributes: new
            {
                supported_color_modes = new[] { "brightness" }
            });
        sut.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(150).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters() { BrightnessPct = 100 })
        );
    }

    // Verifies explicit manual brightness is preserved and not overwritten by manager defaults.
    [Fact]
    public void GivenManualTurnOnWithExplicitBrightness_WhenOverrideStarts_ThenManagerDoesNotOverrideBrightness()
    {
        // Arrange
        sut.Init();

        // Act
        state.TurnOnManually(
            sut.Config.Light(),
            attributes: new
            {
                brightness = 0,
                supported_color_modes = new[] { "brightness" }
            });
        sut.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(50).Ticks);
        state.TurnOnManually(
            sut.Config.Light(),
            attributes: new
            {
                brightness = 50,
                supported_color_modes = new[] { "brightness" }
            });
        sut.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(150).Ticks);

        // Assert
        state.ServiceCalls.Filter(Domain.Light).Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters() { BrightnessPct = 100 })
        );
    }

    // Verifies override timeout behavior takes precedence over normal occupancy timeout.
    [Fact]
    public void GivenOverrideActive_WhenNormalTimeoutElapses_ThenOnlyOverrideTimeoutTurnsLightsOff()
    {
        // Arrange
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "night");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(20).Ticks);
        state.TriggerPresence(sut.Config.Pir1(), false);
        state.TurnOnManually(sut.Config.Light());

        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().NightTimeout).Ticks);

        // Assert normal timer does nothing
        state.ServiceCalls.Filter(Domain.Light).Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.NightLight())
        );

        var never = new[]
        {
            Events.Light.TurnOff(sut.Config.NightLight()),
            Events.Light.TurnOff(sut.Config.Light())
        };
        foreach (var e in never) state.ServiceCalls.Filter(Domain.Light).Should().NotContainEquivalentOf(e);

        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert override timer turns light off
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    // Verifies override mode does not auto-enable additional lights on presence events.
    [Fact]
    public void GivenOverrideActive_WhenPresenceTurnsOn_ThenOtherControlLightsDoNotTurnOn()
    {
        // Arrange
        sut.Init();

        // Act
        state.TurnOnManually(sut.Config.Light());
        sut.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(150).Ticks);
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Filter(Domain.Light).Should().NotContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.Light(2))
        );
    }

    // Verifies presence events reset override timeout while override is active.
    [Fact]
    public void GivenOverrideActive_WhenPresenceReturns_ThenOverrideTimeoutIsReset()
    {
        // Arrange
        sut.Init();

        // Act
        state.Change(sut.Config.Light(), new EntityState
        {
            Context = new Context
            {
                UserId = "EUGENE"
            },
            State = "on"
        });
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(900).Ticks);
        state.TriggerPresence(sut.Config.Pir1(), true);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(900).Ticks);

        // Assert
        state.ServiceCalls.Should().NotContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );

        // Act
        state.TriggerPresence(sut.Config.Pir1(), false);
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(sut.Config.Room().OverrideTimeout).Ticks);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOff(sut.Config.Light())
        );
    }

    public static IEnumerable<object[]> VoiceBrightnessScenarios()
    {
        yield return ["day", (int?)50, (int?)null, false, (int?)127, true, false, false];
        yield return ["day", (int?)null, (int?)200, false, (int?)200, true, false, false];
        yield return ["day", (int?)null, (int?)null, false, (int?)null, false, true, false];
        yield return ["day", (int?)75, (int?)null, true, (int?)null, true, false, false];
        yield return ["night", (int?)50, (int?)null, false, (int?)null, false, false, true];
    }

    // Verifies voice turn-on payload variants preserve user intent across day/night contexts.
    [Theory]
    [MemberData(nameof(VoiceBrightnessScenarios))]
    public void GivenVoiceTurnOnCommands_WhenProcessed_ThenUserIntentIsPreserved(
        string mode,
        int? brightnessPct,
        int? brightness,
        bool useEntityArray,
        int? stateBrightness,
        bool shouldNotForceDayBrightness,
        bool shouldNotTurnOff,
        bool shouldNotForceNightBrightness)
    {
        // Arrange
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, mode);
        sut.Init();

        // Act
        var ev = useEntityArray
            ? CreateLightTurnOnEventWithArrayEntities(new[] { sut.Config.Light().EntityId, sut.Config.Light(2).EntityId }, brightnessPct, brightness, "alexa")
            : CreateLightTurnOnEvent(sut.Config.Light().EntityId, brightnessPct, brightness, "alexa");
        RaiseEvent(ev, haContext);

        if (stateBrightness.HasValue)
        {
            state.TurnOnManually(
                sut.Config.Light(),
                "alexa",
                new { brightness = stateBrightness.Value });
        }
        else
        {
            state.TurnOnManually(sut.Config.Light(), "alexa");
        }

        if (useEntityArray)
        {
            state.TurnOnManually(sut.Config.Light(2), "alexa");
        }

        sut.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(150).Ticks);

        // Assert
        if (shouldNotForceDayBrightness)
        {
            state.ServiceCalls.Filter(Domain.Light).Should().NotContainEquivalentOf(
                Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters { BrightnessPct = 100 })
            );
        }

        if (shouldNotTurnOff)
            state.ServiceCalls.CountMatching(Domain.Light, "turn_off", sut.Config.Light().EntityId).Should().Be(0);

        if (shouldNotForceNightBrightness)
        {
            state.ServiceCalls.Filter(Domain.Light).Should().NotContainEquivalentOf(
                Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters { BrightnessPct = 2 })
            );
        }
    }

    private Event CreateCallServiceEvent(string domain, string service, object serviceData, string? userId = null)
    {
        var eventData = new
        {
            domain = domain,
            service = service,
            service_data = serviceData,
            context = new { user_id = userId }
        };

        return new Event
        {
            EventType = "call_service",
            DataElement = JsonSerializer.SerializeToElement(eventData)
        };
    }

    private Event CreateLightTurnOnEvent(string entityId, int? brightnessPct = null, int? brightness = null, string? userId = null)
    {
        object serviceData = brightness.HasValue
            ? new { entity_id = entityId, brightness = brightness.Value }
            : brightnessPct.HasValue
                ? new { entity_id = entityId, brightness_pct = brightnessPct.Value }
                : new { entity_id = entityId };

        return CreateCallServiceEvent("light", "turn_on", serviceData, userId);
    }

    private Event CreateLightTurnOnEventWithArrayEntities(string[] entityIds, int? brightnessPct = null, int? brightness = null, string? userId = null)
    {
        object serviceData = brightness.HasValue
            ? new { entity_id = entityIds, brightness = brightness.Value }
            : brightnessPct.HasValue
                ? new { entity_id = entityIds, brightness_pct = brightnessPct.Value }
                : new { entity_id = entityIds };

        return CreateCallServiceEvent("light", "turn_on", serviceData, userId);
    }

    private void RaiseEvent(Event ev, IHaContext haContext)
    {
        ((HaContextMockImpl)haContext).EventsSubject.OnNext(ev);
    }
}
