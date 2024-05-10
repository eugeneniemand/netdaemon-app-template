namespace NetDaemon;

public class DishwasherCycleStateHandler : ICycleStateHandler
{
    public void HandleCycleState(CycleState cycleState, InputBooleanEntity applianceReminder, InputBooleanEntity applianceAcknowledge)
    {
        switch (cycleState)
        {
            case CycleState.Running:
                applianceReminder.TurnOff();
                break;
            case CycleState.Finished:
                applianceAcknowledge.TurnOff();
                break;
            case CycleState.Ready:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}