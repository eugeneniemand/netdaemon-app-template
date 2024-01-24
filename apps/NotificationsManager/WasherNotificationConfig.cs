using NetDaemon;

namespace Niemand.NotificationManager;

public class WasherNotificationConfig : IApplianceNotificationConfig
{
    private readonly IEntities _entities;

    public WasherNotificationConfig(IEntities entities)
    {
        _entities = entities;
    }

    public InputBooleanEntity Acknowledge => _entities.InputBoolean.WasherAck;

    public Dictionary<string, CycleState> CycleStates => new()
    {
        { "run", CycleState.Running },
        { "stop", CycleState.Ready },
        { "pause", CycleState.Paused }
    };

    public string Name => "Washer";
    public SensorEntity RemainingTime => _entities.Sensor.WashingMachineWasherCompletionTime;
    public SensorEntity Status => _entities.Sensor.WashingMachineWasherMachineState;
}