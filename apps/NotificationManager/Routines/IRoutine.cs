namespace Niemand.Routines;

public interface IRoutine
{
    Notification? CurrentNotification { get; }

    /// <summary>
    ///     Inits the routine steps and if triggered again will move to the next step
    /// </summary>
    /// <returns>true if moved to next step</returns>
    bool MoveNext();
}