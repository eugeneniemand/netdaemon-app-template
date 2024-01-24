namespace Niemand;

[NetDaemonApp]
//[Focus]
public class InternetApp
{
    public InternetApp(IEntities entities, IScheduler scheduler, ILogger<InternetApp> logger)
    {
        entities.Switch.PiHole.TurnOn();
            
        entities.Switch.PiHole.StateChanges()
             .Where(change => change.Old.IsOn() && change.New.IsOff())
             .Subscribe(change =>
             {
                 logger.LogInformation("Pi-Hole turned off");
                 scheduler.Schedule(TimeSpan.FromMinutes(5), () => entities.Switch.PiHole.TurnOn());
             });
    }
}