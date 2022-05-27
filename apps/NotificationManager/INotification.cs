namespace NotificationEngine;

public interface INotification
{
    bool TimeSensitive { get; }
    string Message { get; }
    string Name { get; }
}