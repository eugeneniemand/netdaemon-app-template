//using System;
//using System.Collections.Generic;
//using System.Reactive.Linq;
//using System.Threading.Tasks;
//using HomeAssistantGenerated;
//using NetDaemon.HassModel.Entities;

//namespace Ha.Daemons;

//public class LaundryDone : INotificationDaemon
//{
//    private readonly IEntities _entities;

//    public LaundryDone(IEntities entities)
//    {
//        _entities = entities;
//        Config = new NotificationConfig(
//            NotificationEnum.Laundry,
//            "Your laundry is done</ break>Have you checked the machines?",
//            new List<MediaPlayerEntity> { _entities.MediaPlayer.Kitchen });
//    }

//    public NotificationConfig Config { get; }

//    public async Task Initialise()
//    {
//        _entities
//            .BinarySensor.KitchenMotion
//            .StateChanges()
//            .Where(s => s.Old.IsOff() && s.New.IsOn())
//            .Subscribe(change => { RaiseNotification(); });

//        _entities
//            .BinarySensor.Kitchen
//            .StateChanges()
//            .Where(s => s.Old.IsOff() && s.New.IsOn())
//            .Subscribe(change => { RaiseNotification(); });
//    }

//    public event EventHandler<NotificationEventArgs> NotificationRaised;

//    private void RaiseNotification()
//    {
//        NotificationRaised.Invoke(this, new NotificationEventArgs(Config));
//    }
//}

