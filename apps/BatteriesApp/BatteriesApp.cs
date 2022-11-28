namespace Niemand.Energy;

[NetDaemonApp]
//[Focus]
public class BatteriesApp
{
    private readonly IAlexa                _alexa;
    private readonly IHaContext            _haContext;
    private readonly ILogger<BatteriesApp> _logger;
    private readonly IScheduler            _scheduler;
    private readonly IServices             _services;

    public BatteriesApp(IHaContext haContext, IScheduler scheduler, ILogger<BatteriesApp> logger, IServices services, IAlexa alexa)
    {
        _haContext = haContext;
        _scheduler = scheduler;
        _logger    = logger;
        _services  = services;
        _alexa     = alexa;
        scheduler.ScheduleCron("0 7,18 * * *", () => CheckBatteries(haContext, services));
        CheckBatteries(haContext, services);
    }

    private void CheckBatteries(IHaContext haContext, IServices services)
    {
        var prefix    = "sensor.wiser_itrv_";
        var suffix    = "_battery";
        var flat      = new List<string>();
        var low       = new List<string>();
        var batteries = haContext.GetAllEntities().Where(e => e.EntityId.StartsWith(prefix) && e.EntityId.EndsWith(suffix)).ToList();
        foreach (var entity in batteries)
        {
            var batteryLevel = double.Parse(entity.State);
            switch (batteryLevel)
            {
                case 0:
                    flat.Add(CleanEntityId(entity, prefix, suffix));
                    break;
                case <= 10:
                    low.Add(CleanEntityId(entity, prefix, suffix));
                    break;
            }
        }

        if (flat.Count > 0)
        {
            flat.Sort();
            var message = $"The following Radiator batteries are flat, {string.Join(", ", flat)}";
            services.Notify.Twinstead(message);
            _alexa.Announce(new Alexa.Config { Entity = "media_player.downstairs", Message = message });
        }

        if (low.Count > 0)
        {
            low.Sort();
            var message = $"The following Radiator batteries are low, {string.Join(", ", low)}";
            services.Notify.Twinstead(message);
            _alexa.Announce(new Alexa.Config { Entity = "media_player.downstairs", Message = message });
        }
    }

    private static string CleanEntityId(Entity entity, string prefix, string suffix) => entity.EntityId.Replace(prefix, "", StringComparison.OrdinalIgnoreCase).Replace(suffix, "", StringComparison.OrdinalIgnoreCase);
}