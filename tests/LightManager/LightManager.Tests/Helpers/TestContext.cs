// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.TestContext
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Reactive.Testing;
using NetDaemon.HassModel;
using System;
using System.Reactive.Concurrency;


#nullable enable
namespace LightManager.Tests
{
  public class TestContext : IServiceProvider
  {
    private readonly IServiceCollection _serviceCollection = (IServiceCollection) new ServiceCollection();
    private readonly IServiceProvider _serviceProvider;

    public TestContext()
    {
      this._serviceCollection.AddSingleton<HaContextMock>((Func<IServiceProvider, HaContextMock>) (_ => new HaContextMock()));
      this._serviceCollection.AddTransient<IHaContext>((Func<IServiceProvider, IHaContext>) (s => (IHaContext) s.GetRequiredService<HaContextMock>().Object));
      this._serviceCollection.AddSingleton<TestScheduler>();
      this._serviceCollection.AddTransient<IScheduler>((Func<IServiceProvider, IScheduler>) (s => (IScheduler) s.GetRequiredService<TestScheduler>()));
      this._serviceProvider = (IServiceProvider) this._serviceCollection.BuildServiceProvider();
    }

    public object? GetService(Type serviceType) => this._serviceProvider.GetService(serviceType);

    public T GetApp<T>() => ActivatorUtilities.GetServiceOrCreateInstance<T>(this._serviceProvider);
  }
}
