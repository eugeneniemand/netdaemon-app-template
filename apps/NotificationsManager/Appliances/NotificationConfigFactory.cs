using Niemand.NotificationManager;

namespace NetDaemon;

public class NotificationConfigFactory : INotificationConfigFactory
{
    public IApplianceNotificationConfig CreateConfig(string applianceType, IHaContext ha, IEntities entities)
    {
        switch (applianceType)
        {
            case "Dishwasher":
                return new DishwasherNotificationConfig(entities);
            case "Washer":
                return new WasherNotificationConfig(ha, entities);
            case "Dryer":
                return new DryerNotificationConfig(ha, entities);
            default:
                throw new ArgumentException($"Invalid appliance type: {applianceType}");
        }
    }
}