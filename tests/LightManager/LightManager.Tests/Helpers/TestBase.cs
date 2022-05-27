// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.TestBase
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using HomeAssistantGenerated;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Reactive.Testing;
using Moq;
using NetDaemon.Extensions.MqttEntityManager;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading.Tasks;


#nullable enable
namespace LightManager.Tests
{
  [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
  public class TestBase
  {
    public Mock<IMqttEntityManager> EntityManagerMock = new Mock<IMqttEntityManager>();

    public TestBase()
    {
      this.Context = new TestContext();
      this.Scheduler = new TestScheduler();
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      this.EntityManagerMock.Setup<Task<IObservable<string>>>(Expression.Lambda<Func<IMqttEntityManager, Task<IObservable<string>>>>((Expression) Expression.Call(m, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IMqttEntityManager.PrepareCommandSubscriptionAsync)), new Expression[1]
      {
        (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (It.IsAny)), Array.Empty<Expression>())
      }), parameterExpression)).ReturnsAsync<IMqttEntityManager, IObservable<string>>((Func<IObservable<string>>) (() => (IObservable<string>) new Subject<string>()));
    }

    public Entities Entities => this.Context.GetRequiredService<Entities>();

    public HaContextMock HaMock => this.Context.GetRequiredService<HaContextMock>();

    public TestContext Context { get; }

    public TestScheduler Scheduler { get; }
  }
}
