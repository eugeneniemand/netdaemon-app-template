using Niemand;

namespace daemonapp.apps.NotificationsManager;

public class DishwasherNotificationConfig : IApplianceNotificationConfig
{
    private readonly IEntities _entities;

    public DishwasherNotificationConfig(IEntities entities)
    {
        _entities = entities;
    }

    public InputBooleanEntity Acknowledge => _entities.InputBoolean.DishwasherAck;

    public Dictionary<string, CycleState> CycleStates => new()
    {
        { "run", CycleState.Running },
        { "finished", CycleState.Finished },
        { "ready", CycleState.Ready }
    };

    public string Name => "Dishwasher";
    public SensorEntity RemainingTime => _entities.Sensor.DishwasherRemainingProgramTime;
    public SensorEntity Status => _entities.Sensor.DishwasherOperationState;
}