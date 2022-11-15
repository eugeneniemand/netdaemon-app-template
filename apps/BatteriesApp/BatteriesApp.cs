namespace Niemand.Energy;

[NetDaemonApp]
//[Focus]
public class BatteriesApp
{
    //private readonly Alexa      _alexa;

    private readonly IHaContext                                         _haContext;
    private readonly ILogger<BatteriesApp>                                 _logger;
    private readonly IScheduler                                         _scheduler;
    private readonly IServices                                          _services;
    
    public BatteriesApp(IHaContext haContext, IScheduler scheduler, ILogger<BatteriesApp> logger) //, Alexa alexa)
    {
        _haContext = haContext;
        _scheduler = scheduler;
        _logger    = logger;
        //_alexa     = alexa;
        scheduler.ScheduleCron("0 7 * * *", () =>
        {
            var batteries = haContext.GetAllEntities().Where(e=> e.EntityId.StartsWith("sensor.wiser_itrv_") && e.EntityId.EndsWith("_battery")).ToList();
            foreach(var entity in batteries)
            {
                double.TryParse(entity.State, out var batteryLevel);
                if (batteryLevel == 0 )
                {
                    haContext.CallService("notify", "twinstead", null, new { Message = "Radiator batteries are flat" });
                }
                if (batteryLevel <= 10)
                {
                    haContext.CallService("notify", "twinstead", null, new { Message = "Radiator batteries are almost flat" });
                }
            }
        });
    }

}