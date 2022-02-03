using System;
using System.Threading.Tasks;
using Ha;
using Ha.Daemons;

public partial class NotificationEngineTests
{
    private record TestDaemon : INotificationDaemon
    {
        public Task Initialise()
        {
            return Task.CompletedTask;
        }

        public void SetConfig(NotificationConfig config)
        {
            Config = config;
        }

        public NotificationConfig Config { get; private set; }
        public event EventHandler<NotificationEventArgs>? NotificationRaised;
    }
}