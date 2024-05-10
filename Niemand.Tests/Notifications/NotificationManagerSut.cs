using Microsoft.Extensions.Logging;
using Niemand.Tests.Mocks;

namespace Niemand.Tests;

public class NotificationManagerSut(IHaContext haContext, IEntities entities, IAlexa alexa, TestScheduler scheduler, ILogger<NotificationsManager> logger , IApplianceFactory applianceFactory)
{
    public NotificationsManager Init() => new(haContext, entities, alexa, scheduler, logger, applianceFactory);

    public TestScheduler Scheduler => scheduler;

    public AlexaMock Alexa => (AlexaMock)alexa;
}