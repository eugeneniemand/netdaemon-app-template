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
    private readonly Services _services;
    private bool Whisper => _entities.InputSelect.HouseMode.State == "night";
    private readonly Dictionary<Notification, DateTime> _lastNotification = new();
    private readonly IServiceProvider?                  _serviceProvider;

    public NotificationEngine(IHaContext ha, INetDaemonScheduler scheduler)
    {
        var serviceCollection = new ServiceCollection()
                                .AddSingleton(ha)
                                .AddSingleton(scheduler);
        _serviceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(serviceCollection);

        _entities = new Entities(ha);
        _services = new Services(ha);

        Initialise();
    }

    private void Initialise()
    {
        var type = typeof(INotificationDaemon);
        var types = AppDomain.CurrentDomain.GetAssemblies()
                             .SelectMany(s => s.GetTypes())
                             .Where(p => type.IsAssignableFrom(p) && p.IsClass);

        foreach (var t in types)
        {
            var instance = (INotificationDaemon)ActivatorUtilities.CreateInstance(_serviceProvider!, t)!;
            instance.Initialise();
            ValidateSetup(instance);
            instance.NotificationRaised += Announce;
        }
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
        var data        = new { type = "announce" };
        _services.Notify.AlexaMedia(ssmlMessage, target: config.Targets.Select(e => e.EntityId), data: data);
        _lastNotification[config.Type] = DateTime.Now;
    }
}