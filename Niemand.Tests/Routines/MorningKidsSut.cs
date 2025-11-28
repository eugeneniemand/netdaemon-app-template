using Microsoft.Extensions.Logging;
using Niemand.Tests.Mocks;

namespace Niemand.Tests;

public class MorningKidsSut(
    IEntities entities,
    IServices services,
    TestScheduler scheduler,
    AlexaMock alexa,
    ILogger<MorningKids> logger,
    StateChangeManager state)
{
    public MorningKids Init() => new(entities, services, alexa, scheduler, logger);

    public TestScheduler Scheduler => scheduler;
    public StateChangeManager State => state;
}