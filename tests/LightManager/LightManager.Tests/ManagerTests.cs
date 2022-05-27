// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.ManagerTests
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using FluentAssertions;
using HomeAssistantGenerated;
using LightManagerV2;
using Microsoft.Extensions.Logging;
using Moq;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reactive.Concurrency;
using System.Reflection;
using System.Threading.Tasks;


#nullable enable
namespace LightManager.Tests
{
  public class ManagerTests : TestBase
  {
    private readonly Mock<ILogger<LightsManager>> _logger = new Mock<ILogger<LightsManager>>();

    [Test]
    public void CircadianSwitchTurnOffWhenLightBrightnessIsChangedManually()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      SwitchEntity entity3 = this.HaMock.GetEntity<SwitchEntity>("switch.circadian", "off");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        CircadianSwitchEntity = entity3,
        Name = "TestRoom"
      }.Init(this._logger.Object, "NdUserId", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      }, (object) new LightTurnOnParameters()
      {
        Brightness = new long?(50L)
      });
      this.HaMock.VerifyEntityTurnedOff((Entity) entity3, new Times?(Times.Once()));
    }

    [Test]
    public void CircadianSwitchTurnOffWhenLightColourIsChangedManually()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      SwitchEntity entity3 = this.HaMock.GetEntity<SwitchEntity>("switch.circadian", "off");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        CircadianSwitchEntity = entity3,
        Name = "TestRoom"
      }.Init(this._logger.Object, "NdUserId", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      }, (object) new LightTurnOnParameters()
      {
        ColorTemp = new long?(1000L)
      });
      this.HaMock.VerifyEntityTurnedOff((Entity) entity3, new Times?(Times.Once()));
    }

    [Test]
    public void CircadianSwitchTurnOnWhenLightsAreTurnedOn()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      SwitchEntity entity3 = this.HaMock.GetEntity<SwitchEntity>("switch.circadian", "off");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        CircadianSwitchEntity = entity3,
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity1, "on");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity3, new Times?(Times.Once()));
    }

    [Test]
    public void CreateEnabledSwitchIfNotExists()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      Manager mgr = new Manager()
      {
        Name = "TestRoom"
      };
      string entityId = "switch.light_manager_" + mgr.Name.ToLower();
      mgr.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.EntityManagerMock.Verify<Task>((Expression<Func<IMqttEntityManager, Task>>) (m => m.CreateAsync(entityId, new EntityCreationOptions("switch", Name: string.Format("Light Manager {0}", mgr.Name)))), new Func<Times>(Times.Once));
      this.EntityManagerMock.Verify<Task<IObservable<string>>>((Expression<Func<IMqttEntityManager, Task<IObservable<string>>>>) (m => m.PrepareCommandSubscriptionAsync(entityId)), new Func<Times>(Times.Once));
      this.HaMock.VerifyEntityTurnedOn((Entity) mgr.ManagerEnabled, new Times?(Times.Once()));
    }

    [Test]
    public void DoNotInitRandomManagerWhenRandomStatesExist()
    {
      HaContextMockImpl haContextMockImpl = this.HaMock.Object;
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      new Manager()
      {
        ControlEntities = new List<LightEntity>()
        {
          new LightEntity((IHaContext) haContextMockImpl, "light.dummy")
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      mock.Verify(Expression.Lambda<Action<IRandomManager>>((Expression) Expression.Call(r, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IRandomManager.Init)), new Expression[2]
      {
        (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (It.IsAny)), Array.Empty<Expression>()),
        (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (It.IsAny)), Array.Empty<Expression>())
      }), parameterExpression), new Func<Times>(Times.Never));
    }

    [Test]
    public void DynamicTimeoutIsConfigNightTimeout()
    {
      InputSelectEntity entity = this.HaMock.GetEntity<InputSelectEntity>("input_select.house_mode", "day");
      Manager manager = new Manager()
      {
        NightTimeout = 100,
        NightTimeEntity = entity,
        NightTimeEntityStates = new List<string>()
        {
          "night"
        }
      };
      this.HaMock.TriggerStateChange((Entity) entity, "night");
      manager.DynamicTimeout.Should().Be(TimeSpan.FromSeconds(100.0), "");
    }

    [Test]
    public void DynamicTimeoutIsConfigTimeout() => new Manager()
    {
      Timeout = 100
    }.DynamicTimeout.Should().Be(TimeSpan.FromSeconds(100.0), "");

    [Test]
    public void DynamicTimeoutIsHardcodedNightTimeoutWhenNoNightTimeoutConfigExists()
    {
      InputSelectEntity entity = this.HaMock.GetEntity<InputSelectEntity>("input_select.house_mode", "day");
      Manager manager = new Manager()
      {
        NightTimeEntity = entity,
        NightTimeEntityStates = new List<string>()
        {
          "night"
        }
      };
      this.HaMock.TriggerStateChange((Entity) entity, "night");
      manager.DynamicTimeout.Should().Be(TimeSpan.FromSeconds(90.0), "");
    }

    [Test]
    public void HouseModeChangedDoesNothingWhenAllLightsAreOff()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      LightEntity entity3 = this.HaMock.GetEntity<LightEntity>("light.night_light", "off");
      InputSelectEntity entity4 = this.HaMock.GetEntity<InputSelectEntity>("input_select.house_mode", "day");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2,
          entity3
        },
        NightControlEntities = new List<LightEntity>()
        {
          entity3
        },
        NightTimeEntity = entity4,
        NightTimeEntityStates = new List<string>()
        {
          "night"
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity4, "night");
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
      this.HaMock.VerifyEntityTurnedOff((Entity) entity3, new Times?(Times.Never()));
    }

    [Test]
    public void HouseModeChangedToDayModeDayLightsStayOnAndNightLightsTurnOff()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      LightEntity entity3 = this.HaMock.GetEntity<LightEntity>("light.night_light", "on");
      InputSelectEntity entity4 = this.HaMock.GetEntity<InputSelectEntity>("input_select.house_mode", "night");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        NightControlEntities = new List<LightEntity>()
        {
          entity3
        },
        NightTimeEntity = entity4,
        NightTimeEntityStates = new List<string>()
        {
          "night"
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity4, "day");
      this.HaMock.VerifyEntityTurnedOff((Entity) entity3, new Times?(Times.Once()));
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Once()));
    }

    [Test]
    public void HouseModeChangedToNightModeNightLightsStayOnAndDayLightsTurnOff()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      LightEntity entity3 = this.HaMock.GetEntity<LightEntity>("light.night_light", "on");
      InputSelectEntity entity4 = this.HaMock.GetEntity<InputSelectEntity>("input_select.house_mode", "day");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2,
          entity3
        },
        NightControlEntities = new List<LightEntity>()
        {
          entity3
        },
        NightTimeEntity = entity4,
        NightTimeEntityStates = new List<string>()
        {
          "night"
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity4, "night");
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Once()));
      this.HaMock.VerifyEntityTurnedOff((Entity) entity3, new Times?(Times.Never()));
    }

    [Test]
    public void InitRandomManagerWhenRandomStatesExist()
    {
      HaContextMockImpl haContextMockImpl = this.HaMock.Object;
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      new Manager()
      {
        ControlEntities = new List<LightEntity>()
        {
          new LightEntity((IHaContext) haContextMockImpl, "light.dummy")
        },
        RandomStates = new List<string>()
        {
          "random_cancel"
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      mock.Verify(Expression.Lambda<Action<IRandomManager>>((Expression) Expression.Call(r, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IRandomManager.Init)), new Expression[2]
      {
        (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (It.IsAny)), Array.Empty<Expression>()),
        (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (It.IsAny)), Array.Empty<Expression>())
      }), parameterExpression), new Func<Times>(Times.Once));
    }

    [Test]
    public void IsAnyControlEntitiesOnIsFalseWhenNoEntityIsOn() => new Manager()
    {
      ControlEntities = new List<LightEntity>()
      {
        this.HaMock.GetEntity<LightEntity>("light_dummy", "off")
      }
    }.IsAnyControlEntityOn.Should().BeFalse("");

    [Test]
    public void IsAnyControlEntitiesOnIsTrueWhenAnEntityIsOn() => new Manager()
    {
      ControlEntities = new List<LightEntity>()
      {
        this.HaMock.GetEntity<LightEntity>("light_dummy", "on")
      }
    }.IsAnyControlEntityOn.Should().BeTrue("");

    [Test]
    public void IsNightModeIsFalseWhenEntityStateIsNotInNightTimeStates()
    {
      InputSelectEntity entity = this.HaMock.GetEntity<InputSelectEntity>("input_select.house_mode", "day");
      new Manager()
      {
        NightTimeEntity = entity,
        NightTimeEntityStates = new List<string>()
        {
          "night"
        }
      }.IsNightMode.Should().BeFalse("");
    }

    [Test]
    public void IsNightModeIsTrueWhenEntityStateIsInNightTimeStates()
    {
      InputSelectEntity entity = this.HaMock.GetEntity<InputSelectEntity>("input_select.house_mode", "day");
      Manager manager = new Manager()
      {
        NightTimeEntity = entity,
        NightTimeEntityStates = new List<string>()
        {
          "night"
        }
      };
      this.HaMock.TriggerStateChange((Entity) entity, "night");
      manager.IsNightMode.Should().BeTrue("");
    }

    [Test]
    public void IsOccupiedIsTrueWhenAKeepAliveEntityIsActive() => new Manager()
    {
      KeepAliveEntities = new List<BinarySensorEntity>()
      {
        this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.keep_alive", "on")
      }
    }.IsOccupied.Should().BeTrue("");

    [Test]
    public void IsOccupiedIsTrueWhenAPresenceEntityIsActive() => new Manager()
    {
      PresenceEntities = new List<BinarySensorEntity>()
      {
        this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir", "on")
      }
    }.IsOccupied.Should().BeTrue("");

    [Test]
    public void IsOccupiedIsTrueWhenEntityStateIsInOnStates() => new Manager()
    {
      KeepAliveEntities = new List<BinarySensorEntity>()
      {
        this.HaMock.GetEntity<BinarySensorEntity>("media_player.lg_tv", "playing")
      }
    }.IsOccupied.Should().BeTrue("");

    [Test]
    public void IsTooBrightIsFalseWhenLuxEntityIsBelowHardcodedLuxLimit() => new Manager()
    {
      LuxLimit = new int?(100),
      LuxEntity = this.HaMock.GetEntity<NumericSensorEntity>("sensor.Lux_entity", "50")
    }.IsTooBright.Should().BeFalse("");

    [Test]
    public void IsTooBrightIsFalseWhenLuxEntityIsBelowLuxLimitEntity() => new Manager()
    {
      LuxLimitEntity = this.HaMock.GetEntity<NumericSensorEntity>("sensor.Lux_limit", "100"),
      LuxEntity = this.HaMock.GetEntity<NumericSensorEntity>("sensor.Lux_entity", "50")
    }.IsTooBright.Should().BeFalse("");

    [Test]
    public void IsTooBrightIsFalseWhenNoLuxEntityExists() => new Manager().IsTooBright.Should().BeFalse("");

    [Test]
    public void IsTooBrightIsTrueWhenLuxEntityIsAboveOrEqualLuxLimitEntity() => new Manager()
    {
      LuxLimitEntity = this.HaMock.GetEntity<NumericSensorEntity>("sensor.Lux_limit", "100"),
      LuxEntity = this.HaMock.GetEntity<NumericSensorEntity>("sensor.Lux_entity", "100")
    }.IsTooBright.Should().BeTrue("");

    [Test]
    public void LightsDoNotTurnOffWhenThereIsNoMotionAfterTimeoutPeriodWhenThereIsOccupancy()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "on");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      BinarySensorEntity entity3 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.keep_alive", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        KeepAliveEntities = new List<BinarySensorEntity>()
        {
          entity3
        },
        Timeout = 90,
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity1, "off");
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
    }

    [Test]
    public void LightsDoNotTurnOnWhenThereIsMotionAndItIsTooBright()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        LuxEntity = this.HaMock.GetEntity<NumericSensorEntity>("sensor.lux", "20"),
        LuxLimitEntity = this.HaMock.GetEntity<NumericSensorEntity>("sensor.lux_limit", "10"),
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity1, "on");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Never()));
    }

    [Test]
    public void LightsDontTurnOffWhenNormalTimeoutLapseWhileOverrideIsActive()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "on");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Timeout = 90,
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      }.WithAttributes((object) new LightTurnOnParameters()
      {
        ColorTemp = new long?(1000L)
      }));
      this.HaMock.TriggerStateChange((Entity) entity1, "off");
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
    }

    [Test]
    public void LightsDontTurnOffWhenThereIsNoMotionAfterTimeoutPeriodAndEnabledIsOff()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "on");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Timeout = 90,
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.GetEntity<SwitchEntity>("switch.light_manager_testroom", "off");
      this.HaMock.TriggerStateChange((Entity) entity1, "off");
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
    }

    [Test]
    public void LightsDontTurnOnWhenThereIsMotionAndConditionIsNotMet()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      Entity entity3 = this.HaMock.GetEntity<Entity>("sun.sun", "above_horizon");
      string str = "below_horizon";
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Name = "TestRoom",
        ConditionEntity = entity3,
        ConditionEntityState = str
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity1, "on");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Never()));
    }

    [Test]
    public void LightsDontTurnOnWhenThereIsMotionAndEnabledIsOff()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.GetEntity<SwitchEntity>("switch.light_manager_testroom", "off");
      this.HaMock.TriggerStateChange((Entity) entity1, "on");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Never()));
    }

    [Test]
    public void LightsTurnOffAfterOverrideTimeoutWhenTurnedOnManually()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      SwitchEntity entity3 = this.HaMock.GetEntity<SwitchEntity>("switch.light_manager_testroom", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Timeout = 90,
        Name = "TestRoom",
        ManagerEnabled = entity3
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      });
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1800.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Once()));
    }

    [Test]
    public void LightsTurnOffWhenThereIsNoMotionAfterOverrideTimeoutPeriod()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "on");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Timeout = 90,
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      }.WithAttributes((object) new LightTurnOnParameters()
      {
        ColorTemp = new long?(1000L)
      }));
      this.HaMock.TriggerStateChange((Entity) entity1, "off");
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1800.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Once()));
    }

    [Test]
    public void LightsTurnOffWhenThereIsNoMotionAfterTimeoutPeriod()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "on");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Timeout = 90,
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity1, "off");
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Once()));
    }

    [Test]
    public void LightsTurnOnWhenThereIsMotion()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity1, "on");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Once()));
    }

    [Test]
    public void LightsTurnOnWhenThereIsMotionAndConditionIsMet()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      Entity entity3 = this.HaMock.GetEntity<Entity>("sun.sun", "below_horizon");
      string str = "below_horizon";
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Name = "TestRoom",
        ConditionEntity = entity3,
        ConditionEntityState = str
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity1, "on");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Once()));
    }

    [Test]
    public void NightLightsTurnOnWhenThereIsMotion()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        NightControlEntities = new List<LightEntity>()
        {
          entity2
        },
        NightTimeEntity = this.HaMock.GetEntity<InputSelectEntity>("input_select.house_mode", "night"),
        NightTimeEntityStates = new List<string>()
        {
          "night"
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity1, "on");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Once()));
    }

    [Test]
    public void OverrideDoesNothingWhenCircadianSwitchIsNull()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Name = "TestRoom"
      }.Init(this._logger.Object, "NdUserId", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      SwitchEntity entity3 = this.HaMock.GetEntity<SwitchEntity>("switch.light_manager_testroom", "off");
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      }.WithAttributes((object) new LightTurnOnParameters()
      {
        ColorTemp = new long?(1000L)
      }));
      this.HaMock.VerifyEntityTurnedOn((Entity) entity3, new Times?(Times.Once()));
      this.HaMock.VerifyNoOtherCalls();
    }

    [Test]
    public void TurnOffEntitiesDoesNotExecuteAfterOverrideTimeoutWhenAllTurnedOffManually()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      LightEntity entity3 = this.HaMock.GetEntity<LightEntity>("light.dummy2", "on");
      SwitchEntity entity4 = this.HaMock.GetEntity<SwitchEntity>("switch.light_manager_testroom", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2,
          entity3
        },
        Timeout = 90,
        Name = "TestRoom",
        ManagerEnabled = entity4
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      });
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "off"
      });
      this.HaMock.TriggerStateChange((Entity) entity3, new EntityState()
      {
        State = "off"
      });
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1800.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
      this.HaMock.VerifyEntityTurnedOff((Entity) entity3, new Times?(Times.Never()));
    }

    [Test]
    public void TurnOffEntitiesDoesNotExecuteAfterOverrideTimeoutWhenOnlySomeTurnedOffManually()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      LightEntity entity3 = this.HaMock.GetEntity<LightEntity>("light.dummy2", "off");
      SwitchEntity entity4 = this.HaMock.GetEntity<SwitchEntity>("switch.light_manager_testroom", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2,
          entity3
        },
        Timeout = 90,
        Name = "TestRoom",
        ManagerEnabled = entity4
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      });
      this.HaMock.TriggerStateChange((Entity) entity3, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      });
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "off"
      });
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1800.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
      this.HaMock.VerifyEntityTurnedOff((Entity) entity3, new Times?(Times.Once()));
    }

    [Test]
    public void TurnOffEntityDoesNotExecuteAfterOverrideTimeoutWhenTurnedOffManually()
    {
      Mock<IRandomManager> mock = new Mock<IRandomManager>();
      BinarySensorEntity entity1 = this.HaMock.GetEntity<BinarySensorEntity>("binary_sensor.pir1", "off");
      LightEntity entity2 = this.HaMock.GetEntity<LightEntity>("light.dummy", "off");
      SwitchEntity entity3 = this.HaMock.GetEntity<SwitchEntity>("switch.light_manager_testroom", "on");
      new Manager()
      {
        PresenceEntities = new List<BinarySensorEntity>()
        {
          entity1
        },
        ControlEntities = new List<LightEntity>()
        {
          entity2
        },
        Timeout = 90,
        Name = "TestRoom",
        ManagerEnabled = entity3
      }.Init(this._logger.Object, "", mock.Object, (IScheduler) this.Scheduler, (IHaContext) this.HaMock.Object, this.EntityManagerMock.Object);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "on",
        Context = new NetDaemon.HassModel.Context()
        {
          UserId = "SomeOtherUserId"
        }
      });
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(90.0).Ticks);
      this.HaMock.TriggerStateChange((Entity) entity2, new EntityState()
      {
        State = "off"
      });
      this.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1800.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
    }
  }
}
