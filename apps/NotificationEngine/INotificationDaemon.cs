using System;
using System.Threading.Tasks;

namespace Ha.Daemons;

public interface INotificationDaemon
{
    NotificationConfig Config { get; }

    event EventHandler<NotificationEventArgs> NotificationRaised;
    Task Initialise();
}