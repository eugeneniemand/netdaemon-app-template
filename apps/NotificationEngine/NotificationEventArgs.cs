using NotificationEngine;

namespace Ha.Daemons;

public class NotificationEventArgs
{
    public NotificationEventArgs(Notification config, params object[] messageParams)
    {
        Config        = config;
        MessageParams = messageParams;
    }

    public Notification Config { get; }

    public object[] MessageParams { get; }
}