////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using Ha.Daemons;
////using HomeAssistantGenerated;
////using Microsoft.Extensions.DependencyInjection;
////using NetDaemon.Common;
////using NetDaemon.Extensions.Scheduler;
////using NetDaemon.HassModel.Common;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reactive.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using HomeAssistantGenerated;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using NetDaemon.AppModel;
//using NotificationEngine.Daemons;

//namespace NotificationEngine;

///// [Focus]
//[NetDaemonApp]
//public class NotificationEngineApp : IAsyncInitializable
//{
//    private readonly Entities                       _entities;
//    private readonly ILogger<NotificationEngineApp> _logger;
//    private readonly IServiceProvider               _serviceProvider;

//    public NotificationEngineApp(IServiceProvider serviceProvider, ILogger<NotificationEngineApp> logger, Entities entities)
//    {
//        _serviceProvider = serviceProvider;
//        _logger          = logger;
//        _entities        = entities;
//    }

//    //    private bool Whisper => _entities.InputSelect.HouseMode.State == "night";

//    public Task InitializeAsync(CancellationToken token)
//    {
//        var type = typeof(INotificationDaemon);
//        var types = AppDomain.CurrentDomain.GetAssemblies()
//                             .SelectMany(s => s.GetTypes())
//                             .Where(p => type.IsAssignableFrom(p) && p.IsClass);

//        var observables = new List<IObservable<INotification>>();
//        foreach (var t in types)
//        {
//            _logger.LogInformation("Creating Type: {typeName}", t.Name);
//            var instance = ActivatorUtilities.CreateInstance(_serviceProvider, t) as INotificationDaemon;
//            _logger.LogInformation("Get Notifications: {typeName}", t.Name);
//            observables.Add(instance!.Notifications);
//        }

//        _logger.LogInformation("Subscribe To {count} Notification Observables", observables.Count);

//        observables.Merge()
//                   .Buffer(TimeSpan.FromSeconds(1))
//                   .Subscribe(ProcessNotifications);

//        return Task.CompletedTask;
//    }


//    private void Announce()
//    {
//        //var config = args.Config;

//        //if (_lastNotification.ContainsKey(config.Type))
//        //{
//        //    if ((DateTime.Now - _lastNotification[config.Type]).TotalMinutes < config.Snooze)
//        //        return;
//        //}
//        //else
//        //{
//        //    _lastNotification.Add(config.Type, DateTime.Now);
//        //}

//        //foreach (var entity in config.Targets)
//        //    entity.VolumeSet((Whisper ? 1 : 5) / 10.0);
//        //var message = args.Config.Message(args.MessageParams);
//        //var ssmlMessage = !Whisper ? $"<voice name=\"Emma\">{message}</voice>" : $"<amazon:effect name=\"whispered\">{message}</amazon:effect>";
//        //HaServices.Notify.AlexaMedia(ssmlMessage, null, config.Targets.Select(e => e.EntityId).ToList(), new { type = "announce" });
//        //_lastNotification[config.Type] = DateTime.Now;
//    }

//    private void ProcessNotifications(IList<INotification> notifications)
//    {
//        notifications.ToList()
//                     .ForEach(notification =>
//                     {
//                         _logger.LogInformation("Notification from: {producer}",
//                             notification.Name);
//                     });
//    }
//}

