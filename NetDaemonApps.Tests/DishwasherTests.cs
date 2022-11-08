using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HomeAssistantGenerated;
using Moq;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using NetDaemonApps.Tests.Helpers;
using Niemand.Helpers;

namespace NetDaemonApps.Tests;

public class DishwasherTests
{
    [Test]
    public void DishwasherStateScenario()
    {
        var states    = new List<EntityState>();
        var dataLines = File.ReadAllLines("test-data.json");
        foreach (var line in dataLines)
        {
            var state = JsonSerializer.Deserialize<EntityState>(line);
            states.Add(state);
        }

        var _ctx = new AppTestContext();


        // Arrange
        var config = new DishwasherConfiguration
        {
            DishwasherPower       = _ctx.GetEntity<NumericSensorEntity>("sensor.dishwasher_power"),
            DishwasherCycleSelect = _ctx.GetEntity<InputSelectEntity>("sensor.dishwasher_state", "Clean")
        };

        _ctx.InitDishwasher(config);
        // Act

        foreach (var state in states)
        {
            var time = state.LastChanged;
            _ctx.TriggerStateChange(config.DishwasherPower, state);
        }

        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Dirty.ToString(), Times.AtLeastOnce());
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Wash.ToString(), Times.Once());
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Rinse.ToString(), Times.Once());
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Clean.ToString(), Times.Once());
    }

    [Test]
    public void DishwasherStateShouldBeCleanAfterPowerDecreasesForTheFourthTime()
    {
        var _ctx = new AppTestContext();


        // Arrange
        var config = new DishwasherConfiguration
        {
            DishwasherPower       = _ctx.GetEntity<NumericSensorEntity>("sensor.dishwasher_power"),
            DishwasherCycleSelect = _ctx.GetEntity<InputSelectEntity>("sensor.dishwasher_state")
        };

        _ctx.InitDishwasher(config);
        // Act
        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");
        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");
        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");
        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");
        _ctx.TriggerStateChange(config.DishwasherPower, "70");

        // Assert
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Clean.ToString(), Times.Once());
    }


    [Test]
    public void DishwasherStateShouldBeDirtyAfterPowerDecreasesBelowOneWatt()
    {
        var _ctx = new AppTestContext();


        // Arrange
        var config = new DishwasherConfiguration
        {
            DishwasherPower       = _ctx.GetEntity<NumericSensorEntity>("sensor.dishwasher_power"),
            DishwasherCycleSelect = _ctx.GetEntity<InputSelectEntity>("sensor.dishwasher_state", "Clean")
        };

        _ctx.InitDishwasher(config);
        // Act

        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        _ctx.TriggerStateChange(config.DishwasherPower, "0.34");

        // Assert
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Dirty.ToString(), Times.Once());
    }


    [Test]
    public void DishwasherStateShouldBeWashingAfterPowerIncreasedAndDecreasedThreeTimes()
    {
        var _ctx = new AppTestContext();


        // Arrange
        var config = new DishwasherConfiguration
        {
            DishwasherPower       = _ctx.GetEntity<NumericSensorEntity>("sensor.dishwasher_power"),
            DishwasherCycleSelect = _ctx.GetEntity<InputSelectEntity>("sensor.dishwasher_state")
        };

        _ctx.InitDishwasher(config);
        // Act
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");
        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");
        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");
        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        // Assert
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Wash.ToString(), Times.Once());
    }

    [Test]
    public void DishwasherStateShouldBeWashingWhenPowerIncreases()
    {
        var _ctx = new AppTestContext();


        // Arrange
        var config = new DishwasherConfiguration
        {
            DishwasherPower       = _ctx.GetEntity<NumericSensorEntity>("sensor.dishwasher_power"),
            DishwasherCycleSelect = _ctx.GetEntity<InputSelectEntity>("sensor.dishwasher_state")
        };

        _ctx.InitDishwasher(config);
        // Act
        _ctx.TriggerStateChange(config.DishwasherPower, "70");
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");
        // Assert
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Wash.ToString(), Times.Once());
    }

    [Test]
    public void DishwasherTurnsOnAtStartOf3HourWindow()
    {
        var _ctx = new AppTestContext();

        // Arrange
        var config = new DishwasherConfiguration
        {
            DishwasherPower       = _ctx.GetEntity<NumericSensorEntity>("sensor.dishwasher_power"),
            DishwasherCycleSelect = _ctx.GetEntity<InputSelectEntity>("sensor.dishwasher_state"),
            DishwasherSwitch      = _ctx.GetEntity<SwitchEntity>("switch.dishwasher", "off")
        };

        _ctx.InitDishwasher(config);
        // Act
        _ctx.HaContextMock.TriggerEvent(new Event { EventType = Shared.Events.Cheap3HourWindowStarted.ToString() });
        // Assert
        _ctx.VerifySwitchTurnOn(config.DishwasherSwitch, Times.Once);
    }

    [Test]
    public void FullTest()
    {
        var _ctx = new AppTestContext();


        // Arrange
        var config = new DishwasherConfiguration
        {
            DishwasherPower       = _ctx.GetEntity<NumericSensorEntity>("sensor.dishwasher_power"),
            DishwasherCycleSelect = _ctx.GetEntity<InputSelectEntity>("sensor.dishwasher_state")
        };

        _ctx.InitDishwasher(config);
        // Act

        _ctx.TriggerStateChange(config.DishwasherPower, "30");
        _ctx.TriggerStateChange(config.DishwasherPower, "60");
        _ctx.TriggerStateChange(config.DishwasherPower, "90");
        _ctx.TriggerStateChange(config.DishwasherPower, "1832");
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Wash.ToString(), Times.Once());
        _ctx.TriggerStateChange(config.DishwasherPower, "1783");
        _ctx.TriggerStateChange(config.DishwasherPower, "1832");
        _ctx.TriggerStateChange(config.DishwasherPower, "75");
        _ctx.TriggerStateChange(config.DishwasherPower, "7");

        _ctx.TriggerStateChange(config.DishwasherPower, "60");
        _ctx.TriggerStateChange(config.DishwasherPower, "90");
        _ctx.TriggerStateChange(config.DishwasherPower, "1832");
        _ctx.TriggerStateChange(config.DishwasherPower, "1783");
        _ctx.TriggerStateChange(config.DishwasherPower, "1832");
        _ctx.TriggerStateChange(config.DishwasherPower, "75");

        _ctx.TriggerStateChange(config.DishwasherPower, "60");
        _ctx.TriggerStateChange(config.DishwasherPower, "90");
        _ctx.TriggerStateChange(config.DishwasherPower, "1832");
        _ctx.TriggerStateChange(config.DishwasherPower, "1783");
        _ctx.TriggerStateChange(config.DishwasherPower, "1832");
        _ctx.TriggerStateChange(config.DishwasherPower, "75");

        _ctx.TriggerStateChange(config.DishwasherPower, "75");
        _ctx.TriggerStateChange(config.DishwasherPower, "7");

        _ctx.TriggerStateChange(config.DishwasherPower, "75");
        _ctx.TriggerStateChange(config.DishwasherPower, "7");

        _ctx.TriggerStateChange(config.DishwasherPower, "75");
        _ctx.TriggerStateChange(config.DishwasherPower, "7");

        _ctx.TriggerStateChange(config.DishwasherPower, "60");
        _ctx.TriggerStateChange(config.DishwasherPower, "90");
        _ctx.TriggerStateChange(config.DishwasherPower, "1832");
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Rinse.ToString(), Times.Once());
        _ctx.TriggerStateChange(config.DishwasherPower, "1783");
        _ctx.TriggerStateChange(config.DishwasherPower, "177");
        _ctx.TriggerStateChange(config.DishwasherPower, "1832");

        _ctx.TriggerStateChange(config.DishwasherPower, "75");
        _ctx.TriggerStateChange(config.DishwasherPower, "7");
        _ctx.TriggerStateChange(config.DishwasherPower, "0");
        _ctx.VerifyInputSelect_SelectOption(config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Clean.ToString(), Times.Once());
    }
}

public static class DishwasherAppTestContextInstanceExtensions
{
    public static Dishwasher InitDishwasher(this AppTestContext ctx, DishwasherConfiguration config) => new(ctx.HaContextMock.Object, new FakeAppConfig<DishwasherConfiguration>(config));
}