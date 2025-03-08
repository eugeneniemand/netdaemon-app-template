using Niemand.Helpers;
using System.Text.Json.Serialization;
using Cronos;

namespace daemonapp.apps.NotificationsManager;

[NetDaemonApp]
[Focus]
public class BatteryNotifications(IHaContext ha, IEntities entities, IServices services, IHaContext context, IScheduler scheduler, IAlexa alexa, ILogger<BatteryNotifications> logger) : IAsyncInitializable
{
    private readonly IList<BatteryNotificationsConfig> _config = new List<BatteryNotificationsConfig>
    {
        new(entities.Sensor.JaydensIpadBatteryLevel , entities.Sensor.JaydensIpadBatteryState, entities.Switch.JaydenBlockXbox, "mobile_app_jaydens_ipad"),
        new(entities.Sensor.JaydenSIphoneBatteryLevel, entities.Sensor.JaydenSIphoneBatteryState, entities.Switch.JaydenBlockXbox, "mobile_app_jayden_s_iphone"),
        new(entities.Sensor.AaronsIpadBatteryLevel, entities.Sensor.AaronsIpadBatteryState,entities.Switch.AaronBlockXbox, "mobile_app_aarons_ipad"),
        new(entities.Sensor.AaronsIphoneBatteryLevel, entities.Sensor.AaronsIphoneBatteryState, entities.Switch.AaronBlockXbox,"mobile_app_aarons_iphone"),
        new(entities.Sensor.GabrielsIpadBatteryLevel, entities.Sensor.GabrielsIpadBatteryState,entities.Switch.GabrielBlockXbox, "mobile_app_gabriels_ipad")
    };

    public class MobileBatteryEvent
    {
        [JsonPropertyName("device")] public string? Device { get; init; }
        [JsonPropertyName("battery_level")] public int? BatteryLevel { get; init; }
        [JsonPropertyName("charging")] public string? ChargingDescription { get; init; }

        public MobileBatteryEvent()
        {
            
        }
    }


    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        CronExpression cron = CronExpression.Parse("0 9-19 * * SAT-SUN");
        ha.Events.Filter<MobileBatteryEvent>("mobile_battery_event").Subscribe(e => {
            var data = e.Data;

            // Get the current time in UTC
            DateTime now = DateTime.UtcNow;

            // Get the previous occurrence before the current time
            DateTime? previousUtc = cron.GetNextOccurrence(now, false);

            // Get the next occurrence after the current time
            DateTime? nextUtc = cron.GetNextOccurrence(now, true);
            
            // Check if the current time is within the range specified by the cron expression
            bool isWithinRange = previousUtc.HasValue && nextUtc.HasValue && now >= previousUtc.Value && now < nextUtc.Value;
    
            if (isWithinRange) 
                alexa.Announce(entities.MediaPlayer.Everywhere2.EntityId, $"{e.Data.Device}'s battery is at {e.Data.BatteryLevel}%");
        });

        //scheduler.ScheduleCron("0 16-19 * * 1-5", SendNotifications); // Weekdays
        //scheduler.ScheduleCron("0 9-19 * * 0,6", SendNotifications); // Weekends
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
                    config.XboxBlock.TurnOn();
                    entities.Switch.LgLounge.TurnOff();
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
            if (totalHours > 12)
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