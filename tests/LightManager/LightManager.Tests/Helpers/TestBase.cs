// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.TestBase
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using HomeAssistantGenerated;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Reactive.Testing;
using NUnit.Framework;

#nullable enable
namespace LightManager.Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class TestBase
{
    public TestBase()
    {
        Context   = new TestContext();
        Scheduler = new TestScheduler();
    }

    public Entities Entities => Context.GetRequiredService<Entities>();

    public HaContextMock HaMock => Context.GetRequiredService<HaContextMock>();

    public TestContext Context { get; }

    public TestScheduler Scheduler { get; }
}