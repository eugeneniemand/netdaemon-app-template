using NetDaemon;

namespace Niemand.NotificationManager;

public class WasherNotificationConfig : IApplianceNotificationConfig
{
    private readonly IHaContext _ha;
    private readonly IEntities _entities;

    public WasherNotificationConfig(IHaContext ha, IEntities entities)
    {
        _ha = ha;
        _entities = entities;
    }

    public InputBooleanEntity Acknowledge => _entities.InputBoolean.WasherAck;
    public InputBooleanEntity Reminder => _entities.InputBoolean.WashingReminder;
    
    public MediaPlayerEntity MediaPlayer => _entities.MediaPlayer.Kitchen;
    public BinarySensorEntity MotionSensor => _entities.BinarySensor.UtilityMotion;
    public ICycleStateHandler CycleStateHandler => new WasherCycleStateHandler();
    public Dictionary<string, CycleState> CycleStates => new()
    {
        { "run", CycleState.Running },
        { "stop", CycleState.Ready },
        { "pause", CycleState.Paused }
    };

    public string Name => "Washer";
    public NumericSensorEntity RemainingTime => new NumericSensorEntity(_ha, _entities.Sensor.WashingMachineWasherCompletionTime.EntityId);
    public SensorEntity Status => _entities.Sensor.WashingMachineWasherMachineState;
}