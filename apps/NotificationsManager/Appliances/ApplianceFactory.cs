using Niemand.NotificationManager;

namespace NetDaemon;

public class ApplianceFactory(IEntities entities, INotificationConfigFactory configFactory, IScheduler scheduler, ILogger<NotificationsManager> logger) : IApplianceFactory
{
    public List<Appliance> CreateAppliances(string[] applianceType)
    {
        return applianceType.Select(CreateAppliance).ToList();
    }

    public Appliance CreateAppliance(string applianceType)
    {
        var config                = configFactory.CreateConfig(applianceType, entities);
        var applianceNotification = new ApplianceNotification(scheduler, config, logger);
        return new Appliance(config.MotionSensor, applianceNotification, config.MediaPlayer, config.Status , config.Reminder, config.Acknowledge, config.CycleStateHandler);
    }
}