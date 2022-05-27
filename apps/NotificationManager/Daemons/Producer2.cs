using System.Reactive.Subjects;
using System.Timers;
using Microsoft.Extensions.Logging;

namespace NotificationEngine.Daemons;

internal class Producer2 : INotificationDaemon
{
    private readonly ILogger<Producer2> _logger;

    public Producer2(ILogger<Producer2> logger)
    {
        _logger = logger;
        _logger.LogInformation("Producer 2 Created");

        var timer = new Timer();
        timer.Interval = 1000;
        timer.Elapsed += (s, e) =>
        {
            _logger.LogInformation("Appending Producer 2");
            Notifications.OnNext(new Notification("Producer 2", "Test"));
        };
        timer.Start();
    }

    public Subject<INotification> Notifications { get; } = new();
}