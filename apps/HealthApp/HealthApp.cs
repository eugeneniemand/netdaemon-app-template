using NetDaemon.Helpers;

namespace Niemand.Health;

[NetDaemonApp]
//[Focus]
public class HealthApp
{
    private readonly ILogger<HealthApp> _logger;

    public HealthApp(IHaContext haContext, IEntities entities, ILogger<HealthApp> logger)
    {
        _logger    = logger;

        haContext.GetAllEntities().StateChanges()
                 .Subscribe(e =>
                 {
                     var oldState = e.Old?.State?.ToLower();
                     var newState = e.New?.State?.ToLower();
                     var entityId = e.Entity.EntityId;

                     if (oldState != newState && ( newState == "unavailable" || oldState == "unavailable" ))
                     {
                         _logger.LogWarning("Entity {entityid} has become {state}", entityId, newState);
                     }
                 });

        entities.Sensor.SmartMeterIhdHanStatus.StateChanges()
                .Where(e => e.New?.State != "joined")
                .Subscribe(_ =>
                {
                    _logger.LogError("Smart Meter IHD HAN status is disconnected!");
                });

        entities.BinarySensor.Postbox.StateChanges()
            .Where(e => e.New?.State == "unavailable")
            .Subscribe(e => {
                _logger.LogError("Postbox has become unavailable");
            });

        haContext.GetAllEntities().Where(e => e.EntityId.StartsWith("sensor.wiser_itrv_") && e.EntityId.EndsWith("_battery")).ToList().StateChanges()
            .Where(e => e.New?.State == "unavailable")
            .Subscribe(e => {
                _logger.LogError("Wiser TRV became unavailable");
            });
    }
}