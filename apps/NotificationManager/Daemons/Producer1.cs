using System.Reactive.Subjects;
using Timer = System.Timers.Timer;

namespace NotificationEngine.Daemons;

internal class Producer1 : INotificationDaemon
{
    private readonly ILogger<Producer1> _logger;

    public Producer1(ILogger<Producer1> logger)
    {
        _logger = logger;
        _logger.LogInformation("Producer 1 Created");

        var timer = new Timer();
        timer.Interval = 1000;
        timer.Elapsed += (s, e) =>
        {
            _logger.LogInformation("Appending Producer 1");
            Notifications.OnNext(new Notification("Producer 1", "Test", true));
        };
        timer.Start();
    }

    public Subject<INotification> Notifications { get; } = new();
}