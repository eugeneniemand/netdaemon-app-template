using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;

namespace Ha.Daemons;

public class MorningMode : INotificationDaemon
{
    private Entities _entities;

    public NotificationConfig Config { get; private set; }

    public event EventHandler<NotificationEventArgs> NotificationRaised;

    public MorningMode(IHaContext ha)
    {
        _entities = new Entities(ha);

        Config = new NotificationConfig(
            NotificationEnum.MorningMode,
            "Good morning<break />Should I enable Day Mode",
            new List<MediaPlayerEntity> { _entities.MediaPlayer.Kitchen });
    }

    public async Task Initialise()
    {
        _entities
            .BinarySensor.KitchenMotion
            .StateChanges()
            .Where(s => s.Old.IsOff() && s.New.IsOn())
            .Subscribe(change => { RaiseNotification(); });

        _entities
            .BinarySensor.Kitchen
            .StateChanges()
            .Where(s => s.Old.IsOff() && s.New.IsOn())
            .Subscribe(change => { RaiseNotification(); });
    }

    private void RaiseNotification()
    {
        if (_entities.InputSelect.HouseMode.State!.ToLower() != "day")
            NotificationRaised.Invoke(this, new NotificationEventArgs(Config));
    }
}