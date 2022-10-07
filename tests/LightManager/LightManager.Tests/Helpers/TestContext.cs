// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.TestContext
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using System;
using System.Reactive.Concurrency;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Reactive.Testing;
using NetDaemon.HassModel;

#nullable enable
namespace LightManager.Tests;

public class TestContext : IServiceProvider
{
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    private readonly IServiceProvider   _serviceProvider;

    public TestContext()
    {
        _serviceCollection.AddSingleton(_ => new HaContextMock());
        _serviceCollection.AddTransient((Func<IServiceProvider, IHaContext>)( s => s.GetRequiredService<HaContextMock>().Object ));
        _serviceCollection.AddSingleton<TestScheduler>();
        _serviceCollection.AddTransient((Func<IServiceProvider, IScheduler>)( s => s.GetRequiredService<TestScheduler>() ));
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    public object? GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

    public T GetApp<T>() => ActivatorUtilities.GetServiceOrCreateInstance<T>(_serviceProvider);
}