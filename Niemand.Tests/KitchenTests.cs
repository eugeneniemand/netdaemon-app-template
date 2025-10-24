using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NetDaemon.AppModel;
using Niemand.Helpers.Notifications;
using Niemand;
using NetDaemon.Extensions.Testing;
using Xunit;

namespace Niemand.Tests;

public class KitchenSut
{
    public IHaContext Ha { get; }
    public IServices Services { get; }
    public TestScheduler Scheduler { get; }
    public StateChangeManager State { get; }
    public TestEntityBuilder EntityBuilder { get; }
    public IEntities Entities { get; }
    public IAlexa Alexa { get; }
    public ILogger<Kitchen> Logger { get; }
    public PushNotifier PushNotifier { get; }
    public KitchenConfiguration Config { get; private set; }
    public Kitchen Instance { get; private set; }

    public KitchenSut(IHaContext ha, IServices services, TestScheduler scheduler, StateChangeManager state, TestEntityBuilder entityBuilder, IEntities entities, IAlexa alexa, ILogger<Kitchen> logger)
    {
        Ha = ha;
        Services = services;
        Scheduler = scheduler;
        State = state;
        EntityBuilder = entityBuilder;
        Entities = entities;
        Alexa = alexa;
        Logger = logger;
        PushNotifier = new PushNotifier(ha, services);
    }

    public void Init(KitchenConfiguration? configOverride = null)
    {
        Config = configOverride ?? new KitchenConfiguration
        {
            CoffeeMachineLight = EntityBuilder.CreateEntity<LightEntity>("light.coffee_machine_light", "off"),
            CoffeeMachinePower = EntityBuilder.CreateNumericEntity("sensor.coffee_machine_power"),
            CoffeeMachineAdaptiveLighting = EntityBuilder.CreateSwitchEntity("switch.coffee_machine_adaptive_lighting", "off")
        };
        Instance = new Kitchen(Ha, Scheduler, new FakeAppConfig<KitchenConfiguration>(Config), Logger, Services, Entities, Alexa, PushNotifier);
    }
}

public class KitchenFacts(KitchenSut sut)
{
    [Fact]
    public void CheapEnergyActive_TriggersNotification()
    {
        // Arrange
        sut.State.Change(sut.Entities.BinarySensor.OctopusEnergyTargetThreeHour, "on");
        sut.State.Change(sut.Entities.InputBoolean.DishwasherReminder, "off");
        sut.Init();
        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        // Assert
        sut.State.ServiceCalls.Should().ContainEquivalentOf(
            NetDaemon.Extensions.Testing.Events.Notify.Twinstead("3 Hour Cheap Energy Started: "));
    }

    [Fact]
    public void Dishwasher_DoorOpen_DoesNotStart()
    {
        // Arrange
        sut.State.Change(sut.Entities.BinarySensor.OctopusEnergyTargetThreeHour, "on");
        sut.State.Change(sut.Entities.InputBoolean.DishwasherReminder, "on");
        sut.State.Change(sut.Entities.BinarySensor.NeffDishwasherDoor, "on");
        sut.Init();
        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        // Assert
        sut.State.ServiceCalls.Should().BeEmpty();
    }

    [Fact]
    public void Dishwasher_StartsAndNotifies()
    {
        // Arrange
        sut.State.Change(sut.Entities.BinarySensor.OctopusEnergyTargetThreeHour, "on");
        sut.State.Change(sut.Entities.InputBoolean.DishwasherReminder, "on");
        sut.State.Change(sut.Entities.BinarySensor.NeffDishwasherDoor, "off");
        sut.State.Change(sut.Entities.Switch.NeffDishwasherPower, "off");
        sut.Init();
        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        // Assert
        sut.State.ServiceCalls.Should().BeEmpty();
    }

    [Fact]
    public void CoffeeMachinePower_TurnsOnLight()
    {
        // Arrange
        var config = new KitchenConfiguration
        {
            CoffeeMachineLight = sut.EntityBuilder.CreateEntity<LightEntity>("light.coffee_machine_light", "off"),
            CoffeeMachinePower = sut.EntityBuilder.CreateNumericEntity("sensor.coffee_machine_power"),
            CoffeeMachineAdaptiveLighting = sut.EntityBuilder.CreateSwitchEntity("switch.coffee_machine_adaptive_lighting", "off")
        };
        sut.Init(config);
        sut.State.Change(config.CoffeeMachinePower!, 10);
        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        // Assert
        sut.State.ServiceCalls.Should().BeEmpty();
    }

    [Fact]
    public void CoffeeMachineReady_SendsNotification()
    {
        // Arrange
        var config = new KitchenConfiguration
        {
            CoffeeMachineLight = sut.EntityBuilder.CreateEntity<LightEntity>("light.coffee_machine_light", "on"),
            CoffeeMachinePower = sut.EntityBuilder.CreateNumericEntity("sensor.coffee_machine_power"),
            CoffeeMachineAdaptiveLighting = sut.EntityBuilder.CreateSwitchEntity("switch.coffee_machine_adaptive_lighting", "off")
        };
        sut.State.Change(config.CoffeeMachinePower!, 1300);
        sut.Init(config);
        sut.State.Change(config.CoffeeMachinePower!, 6);
        // Act
        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        // Assert
        sut.State.ServiceCalls.Should().ContainEquivalentOf(
            NetDaemon.Extensions.Testing.Events.Notify.Twinstead("Coffee machine is ready"));
    }
}
