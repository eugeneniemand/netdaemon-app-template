using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NetDaemon.Common;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using NetDaemon.Extensions.Scheduler;
using Niemand.Daemons;

namespace Niemand;

[Focus]
[NetDaemonApp]
public class NotificationEngine
{
    private readonly Entities _entities;
    public Services HaServices { get; set; }
    private bool Whisper => _entities.InputSelect.HouseMode.State == "night";
    private readonly Dictionary<NotificationEnum, DateTime> _lastNotification = new();
    private readonly IServiceProvider?                      _serviceProvider;

    public NotificationEngine(IHaContext ha, INetDaemonScheduler scheduler, INotificationDaemon? testDaemon = null)
    {
        _entities  = new Entities(ha);
        HaServices = new Services(ha);
        var serviceCollection = new ServiceCollection()
                                .AddSingleton(ha)
                                .AddSingleton(scheduler)
                                .AddSingleton<IEntities>(_entities)
                                .AddSingleton(HaServices);

        _serviceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(serviceCollection);

        if (testDaemon == null)
            Initialise().GetAwaiter().GetResult();
        else
            Initialise(testDaemon).GetAwaiter().GetResult();
    }

    public async Task Initialise()
    {
        var type = typeof(INotificationDaemon);
        var types = AppDomain.CurrentDomain.GetAssemblies()
                             .SelectMany(s => s.GetTypes())
                             .Where(p => type.IsAssignableFrom(p) && p.IsClass);

        foreach (var t in types)
        {
            var instance = (INotificationDaemon)ActivatorUtilities.CreateInstance(_serviceProvider!, t)!;
            await Initialise(instance);
        }
    }

    public async Task Initialise(INotificationDaemon instance)
    {
        await instance.Initialise();
        ValidateSetup(instance);
        instance.NotificationRaised += Announce;
    }

    private void ValidateSetup(INotificationDaemon instance)
    {
        if (instance.Config == null) throw new ArgumentException("Null Notification Config", nameof(instance.Config));
        if (!instance.Config.Targets.Any()) throw new ArgumentException("Target list is empty", nameof(instance.Config.Targets));
    }

    private void Announce(object? sender, NotificationEventArgs args)
    {
        var config = args.Config;

        if (_lastNotification.ContainsKey(config.Type))
        {
            if (( DateTime.Now - _lastNotification[config.Type] ).TotalMinutes < config.Snooze)
                return;
        }
        else
        {
            _lastNotification.Add(config.Type, DateTime.Now);
        }

        foreach (var entity in config.Targets)
            entity.VolumeSet(( Whisper ? 1 : 5 ) / 10.0);
        var message     = args.Config.Message(args.MessageParams);
        var ssmlMessage = !Whisper ? $"<voice name=\"Emma\">{message}</voice>" : $"<amazon:effect name=\"whispered\">{message}</amazon:effect>";
        HaServices.Notify.AlexaMedia(ssmlMessage, null, config.Targets.Select(e => e.EntityId).ToList(), new { type = "announce" });
        _lastNotification[config.Type] = DateTime.Now;
    }
}