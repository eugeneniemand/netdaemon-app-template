using System;
using System.Reactive.Subjects;
using Ha.Daemons;
using NotificationEngine;
using NotificationEngine.Daemons;

public class NotificationEngineTests
{
    private record TestDaemon(Subject<INotification> Notifications) : INotificationDaemon
    {
        public event EventHandler<NotificationEventArgs>? NotificationRaised;

        public void SetConfig(Notification config)
        {
        }
    }
}