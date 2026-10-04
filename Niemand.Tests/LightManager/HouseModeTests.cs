using LightManagerV2;
using NetDaemon.HassModel.Entities;
using NetDaemon.Extensions.Testing;

namespace Niemand.Tests.LightManager;

public class HouseModeTests(LightManagerSut sut, StateChangeManager state, TestEntityBuilder entityBuilder, IHaContext haContext)
{
    // Verifies day mode swaps active entities from night to day lights.
    [Fact]
    public void GivenNightLightsOn_WhenHouseModeChangesToDay_ThenNightLightsTurnOffAndControlLightsTurnOn()
    {
        //Arrange
        state.Change(sut.Config.Light(), "off");
        state.Change(sut.Config.Light(2), "off");
        state.Change(sut.Config.NightLight(), "on");
        state.Change(sut.Config.NightLight(2), "on");
        sut.Init();

        //Act
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "day");

        //Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEquivalentTo(
            [
                Events.Light.TurnOn(sut.Config.Light()),
                Events.Light.TurnOn(sut.Config.Light(2)),
                Events.Light.TurnOff(sut.Config.NightLight()),
                Events.Light.TurnOff(sut.Config.NightLight(2))
            ]
        );
    }

    // Verifies night mode swaps active entities from day to night lights.
    [Fact]
    public void GivenHouseModeChangesToNight_WhenControlLightsAreOn_ThenDayLightsTurnOffAndNightLightsTurnOn()
    {
        // Arrange
        state.Change(sut.Config.Light(), "on");
        state.Change(sut.Config.NightLight(), "on");
        sut.Init();

        // Act
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "night");
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(2).Ticks);

        //Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEquivalentTo(
            [
                Events.Light.TurnOff(sut.Config.Light()),
                Events.Light.TurnOn(sut.Config.NightLight()),
                Events.Light.TurnOn(sut.Config.NightLight(2))
            ]
        );
    }

    // Verifies day brightness parameters are applied for brightness-capable lights.
    [Fact]
    public void GivenDayMode_WhenBrightnessLightsTurnOn_ThenDayBrightnessIsApplied()
    {
        // Arrange
        state.Change(sut.Config.Light(), new
        {
            brightness = 0,
            supported_color_modes = new[] { "brightness" }
        });
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "day");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        //Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEquivalentTo(
            [
                Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters() { BrightnessPct = 100}),
                Events.Light.TurnOn(sut.Config.Light(2)),
            ]
        );
    }

    // Verifies night brightness parameters are applied for brightness-capable lights.
    [Fact]
    public void GivenNightMode_WhenBrightnessLightsTurnOn_ThenNightBrightnessIsApplied()
    {
        // Arrange
        state.Change(sut.Config.NightLight(), new
        {
            brightness = 0,
            supported_color_modes = new[] { "brightness" }
        });
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "night");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        //Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEquivalentTo(
            [
                Events.Light.TurnOn(sut.Config.NightLight(), new LightTurnOnParameters() { BrightnessPct = 2}),
                Events.Light.TurnOn(sut.Config.NightLight(2)),
            ]
        );
    }

    // Verifies day color temperature is applied for color-temp capable lights.
    [Fact]
    public void GivenDayMode_WhenColorTempLightsTurnOn_ThenDayColorTemperatureIsApplied()
    {
        // Arrange
        state.Change(sut.Config.Light(), new
        {
            brightness = 0,
            max_color_temp_kelvin = 4000,
            supported_color_modes = new[] { "color_temp" }
        });
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "day");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        //Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEquivalentTo(
            [
                Events.Light.TurnOn(sut.Config.Light(), new LightTurnOnParameters() { BrightnessPct = 100, ColorTempKelvin = 4000, Transition = 0 }),
                Events.Light.TurnOn(sut.Config.Light(2)),
            ]
        );
    }

    // Verifies night color temperature is applied for color-temp capable lights.
    [Fact]
    public void GivenNightMode_WhenColorTempLightsTurnOn_ThenNightColorTemperatureIsApplied()
    {
        // Arrange
        state.Change(sut.Config.NightLight(), new
        {
            brightness = 0,
            min_color_temp_kelvin = 2000,
            supported_color_modes = new[] { "color_temp" }
        });
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "night");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        //Assert
        state.ServiceCalls.Filter(Domain.Light).Should().BeEquivalentTo(
            [
                Events.Light.TurnOn(sut.Config.NightLight(), new LightTurnOnParameters() { BrightnessPct = 2, ColorTempKelvin = 2000, Transition = 0 }),
                Events.Light.TurnOn(sut.Config.NightLight(2)),
            ]
        );
    }

    // Verifies night mode uses night lights on presence.
    [Fact]
    public void GivenNightMode_WhenPresenceTurnsOn_ThenNightLightTurnsOn()
    {
        // Arrange
        sut.Init();

        // Act
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "night");
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.NightLight())
        );
    }

    // Verifies direct night-light activation path with night mode active.
    [Fact]
    public void GivenNightMode_WhenPresenceIsTriggered_ThenNightControlLightTurnsOn()
    {
        // Arrange
        state.SetHouseMode(sut.Config.Room().NightTimeEntity!, "night");
        sut.Init();

        // Act
        state.TriggerPresence(sut.Config.Pir1(), true);

        // Assert
        state.ServiceCalls.Should().ContainEquivalentOf(
            Events.Light.TurnOn(sut.Config.NightLight())
        );
    }
}
