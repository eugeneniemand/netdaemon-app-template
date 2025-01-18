using NetDaemon.Helpers;

namespace Niemand.Health;

[NetDaemonApp]
//[Focus]
public class HealthApp
{
    private readonly ILogger<HealthApp> _logger;

    public HealthApp(IHaContext haContext, ILogger<HealthApp> logger)
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
    }
}