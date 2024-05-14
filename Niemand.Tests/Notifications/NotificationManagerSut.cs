using Microsoft.Extensions.Logging;
using Niemand.Tests.Mocks;

namespace Niemand.Tests;

public class NotificationManagerSut(IHaContext haContext, IEntities entities, IServices services, IAlexa alexa, TestScheduler scheduler, ILogger<NotificationsManager> logger , IApplianceFactory applianceFactory)
{
    public NotificationsManager Init() => new(haContext, entities, services, alexa, scheduler, logger, applianceFactory);

    public TestScheduler Scheduler => scheduler;

    public AlexaMock Alexa => (AlexaMock)alexa;
}