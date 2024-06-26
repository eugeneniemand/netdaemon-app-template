namespace NotificationEngine;

//public class NotificationConfig
//{
//    public Notification Type { get; }
//    public string MessageTemplate { get; }
//    public List<MediaPlayerEntity> Targets { get; }
//    public double Snooze { get; }

//    public NotificationConfig(Notification type, string messageTemplate, List<MediaPlayerEntity> targets, double snooze = 5)
//    {
//        Type            = type;
//        MessageTemplate = messageTemplate;
//        Targets         = targets;
//        Snooze          = snooze;
//    }

//    public object[] MessageParams { get; set; }
//    public string Message => MessageParams.Any() ? string.Format(MessageTemplate, MessageParams) : MessageTemplate;
//};

//public record Notification(NotificationEnum Type, string MessageTemplate, List<MediaPlayerEntity> Targets, double Snooze = 5)
//{
//    public string Message(object[] messageParams) => messageParams.Any() ? string.Format(MessageTemplate, messageParams) : MessageTemplate;
//}

public record Notification(string Name, string Message, bool TimeSensitive = false) : INotification;