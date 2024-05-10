using Niemand.NotificationManager;

namespace NetDaemon;

public interface INotificationConfigFactory
{
    IApplianceNotificationConfig CreateConfig(string applianceType, IEntities entities);
}