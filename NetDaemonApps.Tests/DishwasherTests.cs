using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using HomeAssistantGenerated;
using Moq;
using NetDaemon.HassModel.Entities;
using NetDaemon.HassModel.Mocks.Moq;
using NetDaemonApps.Tests.Helpers;

namespace NetDaemonApps.Tests;

public class DishwasherTests
{
    [Test]
    public void DishwasherStateShouldBeCleanAfterPowerDecreasesForTheFourthTime()
    {
        var _ctx = new HaContextMock();


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
        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Clean.ToString(), Times.Once());
    }

    [Test]
    public void DishwasherStateScenario()
    {
        var states = new List<EntityState>();
        var dataLines = System.IO.File.ReadAllLines("test-data.json");
        foreach( var line in dataLines)
        {
            var state = JsonSerializer.Deserialize<EntityState>(line);
            states.Add(state);
        }

        var _ctx = new HaContextMock();


        // Arrange
        var config = new DishwasherConfiguration
        {
            DishwasherPower = _ctx.GetEntity<NumericSensorEntity>("sensor.dishwasher_power"),
            DishwasherCycleSelect = _ctx.GetEntity<InputSelectEntity>("sensor.dishwasher_state", "Clean")
        };

        _ctx.InitDishwasher(config);
        // Act
        
        foreach (var state in states)
        {
            var time = state.LastChanged;
            _ctx.TriggerStateChange("sensor.dishwasher_power", state);
        }

        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Dirty.ToString(), Times.AtLeastOnce());
        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Wash.ToString(), Times.Once());
        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Rinse.ToString(), Times.Once());
        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Clean.ToString(), Times.Once());
    }


    [Test]
    public void DishwasherStateShouldBeDirtyAfterPowerDecreasesBelowOneWatt()
    {
        var _ctx = new HaContextMock();


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
        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Dirty.ToString(), Times.Once());
    }

    [Test]
    public void DishwasherStateShouldBeRinseAfterPowerIncreasesForTheFourthTime()
    {
        var _ctx = new HaContextMock();


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
        _ctx.TriggerStateChange(config.DishwasherPower, "2000");

        // Assert
        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Rinse.ToString(), Times.Once());
    }

    [Test]
    public void DishwasherStateShouldBeWashingAfterPowerIncreasedAndDecreasedThreeTimes()
    {
        var _ctx = new HaContextMock();


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
        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Wash.ToString(), Times.Once());
    }

    [Test]
    public void DishwasherStateShouldBeWashingWhenPowerIncreases()
    {
        var _ctx = new HaContextMock();


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
        VerifyInputSelect_SelectOption(_ctx, config.DishwasherCycleSelect!.EntityId, Dishwasher.DishwasherCycle.Wash.ToString(), Times.Once());
    }

    public static void VerifyInputSelect_SelectOption(HaContextMock ctx, string entityId, string option, Times times)
    {
        ctx.Verify(c => c.CallService("sensor", "select_option",
                It.Is<ServiceTarget>(x => x.EntityIds != null && x.EntityIds.First() == entityId),
                It.Is<InputSelectSelectOptionParameters>(x => x.Option == option)), times
        );
    }
}

public static class DishwasherAppTestContextInstanceExtensions
{
    public static Dishwasher InitDishwasher(this HaContextMock ctx, DishwasherConfiguration config) => new(ctx.Object, new FakeAppConfig<DishwasherConfiguration>(config));
}