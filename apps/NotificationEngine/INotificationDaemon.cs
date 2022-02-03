using System;
using System.Threading.Tasks;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Common;

namespace Ha.Daemons;

public interface INotificationDaemon
{
    Task Initialise();
    NotificationConfig Config { get; }

    event EventHandler<NotificationEventArgs> NotificationRaised;
}