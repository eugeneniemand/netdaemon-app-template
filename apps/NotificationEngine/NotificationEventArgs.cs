namespace Ha.Daemons;

public class NotificationEventArgs
{
    public NotificationConfig Config { get; }

    public NotificationEventArgs(NotificationConfig config, params object[] messageParams)
    {
        Config        = config;
        MessageParams = messageParams;
    }

    public object[] MessageParams { get; }
}