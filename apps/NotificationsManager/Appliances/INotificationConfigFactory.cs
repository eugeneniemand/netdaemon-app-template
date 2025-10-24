using Niemand.NotificationManager;

namespace NetDaemon;

public interface INotificationConfigFactory
{
    IApplianceNotificationConfig CreateConfig(string applianceType, IHaContext ha, IEntities entities);
}