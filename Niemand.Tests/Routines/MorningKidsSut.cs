using Microsoft.Extensions.Logging;
using Niemand.Tests.Mocks;

namespace Niemand.Tests;

public class MorningKidsSut(
    IEntities entities,
    IServices services,
    TestScheduler scheduler,
    ILogger<MorningKids> logger,
    StateChangeManager state)
{
    public MorningKids Init() => new(entities, services, scheduler, logger);

    public TestScheduler Scheduler => scheduler;
    public StateChangeManager State => state;
}