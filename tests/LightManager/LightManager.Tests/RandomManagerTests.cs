// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.RandomManagerTests
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using HomeAssistantGenerated;
using LightManagerV2;
using Microsoft.Extensions.Logging;
using Microsoft.Reactive.Testing;
using Moq;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;


#nullable enable
namespace LightManager.Tests
{
  public class RandomManagerTests : TestBase
  {
    private readonly Mock<ILogger<RandomManager>> _logger = new Mock<ILogger<RandomManager>>();

    [Test]
    public void ImmediatelyStopProcessingQueueAndTurnsOffEntitiesThatWereTurnedOn()
    {
      SwitchEntity randomSwitchEntity = new SwitchEntity((IHaContext) this.HaMock.Object, "switch.random");
      LightEntity entity1 = new LightEntity((IHaContext) this.HaMock.Object, "light.living");
      LightEntity entity2 = new LightEntity((IHaContext) this.HaMock.Object, "light.dining");
      List<string> randomStates = new List<string>()
      {
        "random_cancel"
      };
      TestScheduler testScheduler = new TestScheduler();
      RandomManager randomManager = new RandomManager((IScheduler) testScheduler, randomSwitchEntity, "0.00:00:00.100", "0.00:00:00.300", this._logger.Object, false);
      randomManager.Init(entity1, (IEnumerable<string>) randomStates);
      randomManager.Init(entity2, (IEnumerable<string>) randomStates);
      this.HaMock.TriggerStateChange((Entity) randomSwitchEntity, "random_cancel");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity1);
      this.HaMock.TriggerStateChange((Entity) randomSwitchEntity, "off");
      testScheduler.AdvanceBy(TimeSpan.FromSeconds(1.0).Ticks);
      this.HaMock.VerifyEntityTurnedOff((Entity) entity1);
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Never()));
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.Never()));
    }

    [Test]
    public void QueueOnlyProcessesNextEntitiesAfterRandomDelay()
    {
      SwitchEntity randomSwitchEntity = new SwitchEntity((IHaContext) this.HaMock.Object, "switch.random");
      LightEntity entity1 = new LightEntity((IHaContext) this.HaMock.Object, "light.living");
      LightEntity entity2 = new LightEntity((IHaContext) this.HaMock.Object, "light.dining");
      List<string> randomStates = new List<string>()
      {
        "random_delay"
      };
      TestScheduler testScheduler = new TestScheduler();
      RandomManager randomManager = new RandomManager((IScheduler) testScheduler, randomSwitchEntity, "0.00:00:00.100", "0.00:00:00.300", this._logger.Object, false);
      randomManager.Init(entity1, (IEnumerable<string>) randomStates);
      randomManager.Init(entity2, (IEnumerable<string>) randomStates);
      this.HaMock.TriggerStateChange((Entity) randomSwitchEntity, "random_delay");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity1);
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.Never()));
      testScheduler.AdvanceBy(randomManager.RandomDelay.Ticks);
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2);
      this.HaMock.TriggerStateChange((Entity) randomSwitchEntity, "off");
    }

    [Test]
    public void QueueRestartsWhenEndIsReached()
    {
      SwitchEntity randomSwitchEntity = new SwitchEntity((IHaContext) this.HaMock.Object, "switch.random");
      LightEntity entity1 = new LightEntity((IHaContext) this.HaMock.Object, "light.living");
      LightEntity entity2 = new LightEntity((IHaContext) this.HaMock.Object, "light.dining");
      List<string> randomStates = new List<string>()
      {
        "random_loop"
      };
      TestScheduler testScheduler = new TestScheduler();
      RandomManager randomManager = new RandomManager((IScheduler) testScheduler, randomSwitchEntity, "0.00:00:00.100", "0.00:00:00.300", this._logger.Object, false);
      randomManager.Init(entity1, (IEnumerable<string>) randomStates);
      randomManager.Init(entity2, (IEnumerable<string>) randomStates);
      this.HaMock.TriggerStateChange((Entity) randomSwitchEntity, "random_loop");
      for (int index = 0; index < 5; ++index)
        testScheduler.AdvanceBy(randomManager.RandomDelay.Ticks);
      this.HaMock.TriggerStateChange((Entity) randomSwitchEntity, "off");
      this.HaMock.VerifyEntityTurnedOn((Entity) entity1, new Times?(Times.AtLeast(3)));
      this.HaMock.VerifyEntityTurnedOn((Entity) entity2, new Times?(Times.AtLeast(3)));
      this.HaMock.VerifyEntityTurnedOff((Entity) entity1, new Times?(Times.AtLeast(3)));
      this.HaMock.VerifyEntityTurnedOff((Entity) entity2, new Times?(Times.AtLeast(3)));
    }
  }
}
