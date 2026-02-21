using daemonapp.apps.NotificationsManager.Appliances;
using NetDaemon;

namespace Niemand.NotificationManager;

public class DishwasherNotificationConfig : IApplianceNotificationConfig 
{
    private readonly IEntities _entities;

    public DishwasherNotificationConfig(IEntities entities)
    {
        _entities = entities;
    }

    public InputBooleanEntity Acknowledge => _entities.InputBoolean.DishwasherAck;
    public InputBooleanEntity Reminder => _entities.InputBoolean.DishwasherReminder;
    public MediaPlayerEntity MediaPlayer => _entities.MediaPlayer.Kitchen;
    public BinarySensorEntity MotionSensor => _entities.BinarySensor.KitchenMotion;
    
    public ICycleStateHandler CycleStateHandler => new DishwasherCycleStateHandler();

    public Dictionary<string, CycleState> CycleStates => new()
    {
        { "run", CycleState.Running },
        { "finished", CycleState.Finished },
        { "ready", CycleState.Ready }
    };

    public string Name => "Dishwasher";
    public NumericSensorEntity RemainingTime => _entities.Sensor.NeffDishwasherRemainingProgramTime;
    public SensorEntity Status => _entities.Sensor.NeffDishwasherOperationState;
}