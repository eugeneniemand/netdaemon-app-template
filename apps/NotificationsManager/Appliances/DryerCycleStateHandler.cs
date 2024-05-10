namespace NetDaemon;

public class DryerCycleStateHandler : ICycleStateHandler
{
    public void HandleCycleState(CycleState cycleState, InputBooleanEntity applianceReminder, InputBooleanEntity applianceAcknowledge)
    {
        switch (cycleState)
        {
            case CycleState.Running:
                applianceReminder.TurnOff();
                break;
            case CycleState.Finished:
                break;
            case CycleState.Ready:
                applianceAcknowledge.TurnOff();
                break;
            case CycleState.Paused:
                break;
            case CycleState.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}