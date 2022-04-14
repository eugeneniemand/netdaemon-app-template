using System.Reactive.Subjects;

namespace NotificationEngine.Daemons;

public interface INotificationDaemon
{
    Subject<INotification> Notifications { get; }
}