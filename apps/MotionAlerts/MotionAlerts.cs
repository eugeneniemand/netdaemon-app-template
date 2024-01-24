namespace Niemand;

public class MotionAlertsConfiguration
{
    public IEnumerable<BinarySensorEntity> Sensors { get; set; }
}

//[Focus]
[NetDaemonApp]
public class MotionAlerts
{
    private readonly MotionAlertsConfiguration _config;
    private readonly IEntities                 _entities;
    private readonly IHaContext                _ha;
    private readonly ILogger<MotionAlerts>     _logger;

    public MotionAlerts(IHaContext ha, ILogger<MotionAlerts> logger, IAppConfig<MotionAlertsConfiguration> config, IEntities entities, IServices services)
    {
        _ha       = ha;
        _logger   = logger;
        _entities = entities;
        _config   = config.Value;
        var lastNotification = DateTime.MinValue;


        _config.Sensors.StateChanges()
               .Where(s => s.Old.State == "off" && s.New.State == "on")
               .Subscribe(e =>
               {
                   //if (lastNotification != DateTime.MinValue && ( DateTime.Now - lastNotification ).TotalMinutes < 15) return;
                   if (DateTime.Now.Hour is >= 19 or <= 5 || _entities.InputBoolean.NetdaemonDebugState.IsOn())
                       services.Notify.Eugene($"Motion detected on {e.Entity.EntityId.Replace("binary_sensor.", "", StringComparison.OrdinalIgnoreCase).Replace("_motion", "", StringComparison.OrdinalIgnoreCase)}");
                   //lastNotification = DateTime.Now;
               });
    }
}