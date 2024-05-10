using NetDaemon;

namespace Niemand.NotificationManager;

public class DryerNotificationConfig : IApplianceNotificationConfig
{
    private readonly IEntities _entities;

    public DryerNotificationConfig(IEntities entities)
    {
        _entities = entities;
    }

    public InputBooleanEntity Acknowledge => _entities.InputBoolean.DryerAck;
    public InputBooleanEntity Reminder => _entities.InputBoolean.DryerReminder;
    
    public MediaPlayerEntity MediaPlayer => _entities.MediaPlayer.Kitchen;
    public BinarySensorEntity MotionSensor => _entities.BinarySensor.UtilityMotion;

    public ICycleStateHandler CycleStateHandler => new DryerCycleStateHandler();
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