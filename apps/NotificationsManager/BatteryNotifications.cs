using Niemand.Helpers;

namespace daemonapp.apps.NotificationsManager;

[NetDaemonApp]
//[Focus]
public class BatteryNotifications(IEntities entities, IServices services, IHaContext context, IScheduler scheduler, IAlexa alexa, ILogger<BatteryNotifications> logger) : IAsyncInitializable
{
    private readonly IList<BatteryNotificationsConfig> _config = new List<BatteryNotificationsConfig>
    {
        new(entities.Sensor.JaydenSIpadBatteryLevel, entities.Sensor.JaydenSIpadBatteryState, "mobile_app_jayden_s_ipad"),
        new(entities.Sensor.JaydenSIphoneBatteryLevel, entities.Sensor.JaydenSIphoneBatteryState, "mobile_app_jayden_s_iphone"),
        new(entities.Sensor.AaronsIpadBatteryLevel, entities.Sensor.AaronsIpadBatteryState, "mobile_app_aarons_ipad"),
        new(entities.Sensor.AaronsIphoneBatteryLevel, entities.Sensor.AaronsIphoneBatteryState, "mobile_app_aarons_iphone")
    };


    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        scheduler.ScheduleCron("0 8-19 * * *", SendNotifications);
        return Task.CompletedTask;
    }

    private List<string> CheckBatteryLevel()
    {
        var deviceNames = new List<string>();
        foreach (var config in _config)
        {
            var deviceName = config.BatteryLevel.Attributes.FriendlyName.Replace(" Battery Level", "");
            if (config.BatteryState.State == "Charging") continue;

            switch (config.BatteryLevel.State)
            {
                case <= 30:
                    deviceNames.Add(deviceName);
                    break;
            }
        }

        return deviceNames;
    }

    private List<string> CheckLastUpdated()
    {
        var deviceNames = new List<string>();
        foreach (var config in _config)
        {
            var totalHours = ( DateTime.Now - config.BatteryLevel.EntityState.LastUpdated.Value ).TotalHours;
            if (totalHours < 12)
            {
                ForceUpdate(config.MobileApp);
                return [];
            }

            var deviceName = config.BatteryLevel.Attributes.FriendlyName.Replace(" Battery Level", "");
            deviceNames.Add(deviceName);
            break;
        }

        return deviceNames;
    }

    private void SendNotifications()
    {
        var deviceNamesFromBatteryCheck     = CheckBatteryLevel();
        var deviceNamesFromLastUpdatedCheck = CheckLastUpdated().Except(deviceNamesFromBatteryCheck).ToList();

        if (deviceNamesFromBatteryCheck.Count != 0)
        {
            var batteryCheckMessage = $"{string.Join(" and ", deviceNamesFromBatteryCheck)} needs charging";
            services.Notify.Twinstead(batteryCheckMessage);
            alexa.Announce(entities.MediaPlayer.Everywhere2.EntityId, batteryCheckMessage);
        }

        if (deviceNamesFromLastUpdatedCheck.Count != 0)
        {
            var lastUpdateCheckMessage = $"{string.Join(" and ", deviceNamesFromLastUpdatedCheck)} has not been seen";
            services.Notify.Twinstead(lastUpdateCheckMessage);
            alexa.Announce(entities.MediaPlayer.Everywhere2.EntityId, lastUpdateCheckMessage);
        }
    }

    private void ForceUpdate(string mobileApp)
    {
        context.CallService("notify", mobileApp, null, new { Message = "request_location_update" });
    }
}