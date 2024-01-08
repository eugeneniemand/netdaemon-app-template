using Niemand;

namespace daemonapp.apps.NotificationsManager;

public class DryerNotificationConfig : IApplianceNotificationConfig
{
    private readonly IEntities _entities;

    public DryerNotificationConfig(IEntities entities)
    {
        _entities = entities;
    }

    public InputBooleanEntity Acknowledge => _entities.InputBoolean.DryerAck;

    public Dictionary<string, CycleState> CycleStates => new()
    {
        { "run", CycleState.Running },
        { "stop", CycleState.Ready },
        { "pause", CycleState.Paused }
    };

    public string Name => "Dryer";
    public SensorEntity RemainingTime => _entities.Sensor.TumbleDryerDryerCompletionTime;
    public SensorEntity Status => _entities.Sensor.TumbleDryerDryerMachineState;
}