using Niemand.Helpers;

namespace Niemand.NotificationManager;

public class Notification
{
    public Alexa.NotificationType Type { get; set; }
    public string EventId { get; set; }
    public string Message { get; set; }
}