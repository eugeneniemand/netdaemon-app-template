using NetDaemon.Helpers;

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

    public MotionAlerts(IHaContext ha, ILogger<MotionAlerts> logger, IAppConfig<MotionAlertsConfiguration> config, IEntities entities, IServices services, Common common)
    {
        _config = config.Value;
        var lastNotification = DateTime.MinValue;       

        _config.Sensors.StateChanges()
               .Where(s => s.Old.State == "off" && s.New.State == "on")
               .Subscribe(e =>
               {
                   if ((string.Equals(entities.InputSelect.HouseMode.State, "night", StringComparison.OrdinalIgnoreCase) || DateTime.Now.Hour is >= 19 or <= 5) &&
                        (entities.Sensor.EugeneDesktopLastactive.LastChangedNewerThan(TimeSpan.FromMinutes(5))
                        || entities.MediaPlayer.LoungeTv.IsOn()
                        || entities.MediaPlayer.MasterTv2.IsOn()
                        || entities.AlarmControlPanel.Alarmo.IsArmedNight()
                        )
                   )
                       services.Notify.Eugene($"Motion detected on {e.Entity.EntityId.Replace("binary_sensor.", "", StringComparison.OrdinalIgnoreCase).Replace("_motion", "", StringComparison.OrdinalIgnoreCase)}");
               });
    }
}