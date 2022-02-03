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

public class LaundryDone : INotificationDaemon
{
    private readonly IEntities _entities;

    public NotificationConfig Config { get; private set; }

    public event EventHandler<NotificationEventArgs> NotificationRaised;

    public LaundryDone(IEntities entities)
    {
        _entities = entities;
        Config = new NotificationConfig(
            NotificationEnum.Laundry,
            "Your laundry is done</ break>Have you checked the machines?",
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
        NotificationRaised.Invoke(this, new NotificationEventArgs(Config));
    }
}