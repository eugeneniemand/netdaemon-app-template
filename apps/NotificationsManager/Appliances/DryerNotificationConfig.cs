using daemonapp.apps.NotificationsManager.Appliances;
using NetDaemon;

namespace Niemand.NotificationManager;

public class DryerNotificationConfig : IApplianceNotificationConfig
{
    private readonly IHaContext _ha;
    private readonly IEntities _entities;

    public DryerNotificationConfig(IHaContext ha, IEntities entities)
    {
        _ha = ha;
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
    public NumericSensorEntity RemainingTime => new NumericSensorEntity(_ha, _entities.Sensor.TumbleDryerDryerCompletionTime.EntityId); 
    public SensorEntity Status => _entities.Sensor.TumbleDryerDryerMachineState;
}