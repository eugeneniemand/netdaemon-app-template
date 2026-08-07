using Microsoft.Extensions.Logging;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.Helpers;
using Niemand.Tests.Mocks;

namespace Niemand.Tests.DisciplineManager;

public class ScreenTimeSut(
    IHaContext ha,
    IServices services,
    TestScheduler scheduler,
    StateChangeManager state,
    IMqttEntityManager entityManager,
    AlexaMock alexaMock,
    ILogger<ScreenTime> logger)
{
    private TimerManager? _timerManager;
    private ScreenTime? _screenTime;

    public void Init()
    {
        _timerManager = new TimerManager(ha, services);
        _screenTime = new ScreenTime(ha, entityManager, _timerManager, scheduler, alexaMock, services, logger);
        _screenTime.InitializeAsync(CancellationToken.None).Wait();
    }

    public ScreenTime Instance
    {
        get => _screenTime ?? throw new InvalidOperationException("SUT not initialized. Call Init() first.");
    }

    public TestScheduler Scheduler => scheduler;
    public StateChangeManager State => state;
    public AlexaMock Alexa => alexaMock;

    public void Cleanup()
    {
        _screenTime?.Dispose();
    }
}
