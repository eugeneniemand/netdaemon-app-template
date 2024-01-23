using Niemand.Tests.Mocks;

namespace Niemand.Tests;

public class NotificationManagerSut(IHaContext haContext, IEntities entities, IAlexa alexa, TestScheduler scheduler)
{
    public NotificationsManager Init() => new(haContext, entities, alexa, scheduler);

    public TestScheduler Scheduler => scheduler;

    public AlexaMock Alexa => (AlexaMock)alexa;
}