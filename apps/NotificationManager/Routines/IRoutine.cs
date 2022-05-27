namespace Niemand.Routines;

public interface IRoutine
{
    Notification? CurrentNotification { get; }
    bool MoveNext();
}