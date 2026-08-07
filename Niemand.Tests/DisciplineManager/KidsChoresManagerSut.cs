using Microsoft.Extensions.Logging;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.Helpers;
using Niemand.Helpers;
using Niemand.Tests.Mocks;

namespace Niemand.Tests.DisciplineManager;

/// <summary>
/// System Under Test (SUT) fixture for KidsChoresManager.
/// Provides test setup and teardown for the KidsChoresManager app with mocked dependencies.
/// </summary>
public class KidsChoresManagerSut(
    IHaContext ha,
    IServices services,
    TestScheduler scheduler,
    StateChangeManager state,
    IMqttEntityManager entityManager,
    AlexaMock alexaMock,
    ILogger<KidsChoresManager> logger,
    TestEntityBuilder entityBuilder)
{
    private NetDaemon.Helpers.TimerManager? _timerManager;
    private KidsChoresManager? _kidsChoresManager;

    /// <summary>
    /// Initializes the KidsChoresManager app with all dependencies.
    /// </summary>
    public void Init()
    {
        _timerManager = new NetDaemon.Helpers.TimerManager(ha, services);
        _kidsChoresManager = new KidsChoresManager(ha, entityManager, _timerManager, scheduler, alexaMock, logger);
        _kidsChoresManager.InitializeAsync(CancellationToken.None).Wait();
    }

    /// <summary>
    /// Gets the initialized KidsChoresManager instance.
    /// Throws if Init() has not been called.
    /// </summary>
    public KidsChoresManager Instance
    {
        get => _kidsChoresManager ?? throw new InvalidOperationException("SUT not initialized. Call Init() first.");
    }

    /// <summary>
    /// Gets the test scheduler for advancing time in tests.
    /// </summary>
    public TestScheduler Scheduler => scheduler;

    /// <summary>
    /// Gets the StateChangeManager for simulating entity state changes.
    /// </summary>
    public StateChangeManager State => state;

    /// <summary>
    /// Gets the AlexaMock for verifying announcements and text-to-speech calls.
    /// </summary>
    public AlexaMock Alexa => alexaMock;

    /// <summary>
    /// Gets the TestEntityBuilder for creating mock entities.
    /// </summary>
    public TestEntityBuilder EntityBuilder => entityBuilder;

    /// <summary>
    /// Gets the HaContext for the test environment.
    /// </summary>
    public IHaContext HaContext => ha;

    /// <summary>
    /// Cleans up resources when the test completes.
    /// </summary>
    public void Cleanup()
    {
        _kidsChoresManager?.Dispose();
    }
}
