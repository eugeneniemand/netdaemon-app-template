//using System;
//using System.Collections.Generic;
//using System.Reactive.Linq;
//using System.Threading.Tasks;
//using HomeAssistantGenerated;
//using NetDaemon.HassModel.Common;
//using NetDaemon.HassModel.Entities;

//namespace Ha.Daemons;

//public class MorningMode : INotificationDaemon
//{
//    private readonly Entities _entities;

//    public MorningMode(IHaContext ha)
//    {
//        _entities = new Entities(ha);

//        Config = new NotificationConfig(
//            NotificationEnum.MorningMode,
//            "Good morning<break />Should I enable Day Mode",
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
//        if (_entities.InputSelect.HouseMode.State!.ToLower() != "day")
//            NotificationRaised.Invoke(this, new NotificationEventArgs(Config));
//    }
//}