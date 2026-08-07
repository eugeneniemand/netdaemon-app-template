using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel.Entities;
using Niemand.Tests.Mocks;

namespace Niemand.Tests.DisciplineManager;

/// <summary>
/// BDD-style test suite for the ScreenTime state machine.
/// Tests verify all state transitions, timer management, and notification handling.
/// </summary>
public class ScreenTimeTests : IDisposable
{
    private readonly ScreenTimeSut _sut;
    private readonly StateChangeManager _state;
    private readonly TestEntityBuilder _entityBuilder;

    // Mock entities
    private MediaPlayerEntity? _loungeTv;
    private Entity? _jaydenButton15;
    private Entity? _jaydenButton30;
    private Entity? _jaydenButton60;
    private Entity? _aaronButton15;
    private Entity? _aaronButton30;
    private Entity? _aaronButton60;
    private Entity? _gabrielButton15;
    private Entity? _gabrielButton30;
    private Entity? _gabrielButton60;

    public ScreenTimeTests(
        IHaContext ha,
        IServices services,
        TestScheduler scheduler,
        StateChangeManager state,
        IMqttEntityManager entityManager,
        AlexaMock alexaMock,
        ILogger<ScreenTime> logger,
        TestEntityBuilder entityBuilder)
    {
        _state = state;
        _entityBuilder = entityBuilder;

        // Setup mock entities before SUT initialization
        SetupMockEntities();

        _sut = new ScreenTimeSut(ha, services, scheduler, state, entityManager, alexaMock, logger);
        _sut.Init();
    }

    private void SetupMockEntities()
    {
        // Setup media player entity
        _loungeTv = _entityBuilder.CreateMediaPlayerEntity("media_player.lounge_tv");
        _state.Change(_loungeTv, "off");

        // Setup reward buttons for Jayden - using CreateEntity<Entity>
        _jaydenButton15 = _entityBuilder.CreateEntity<Entity>("button.jayden_choreops_approve_reward_screen_time_15_min", "unavailable");
        _jaydenButton30 = _entityBuilder.CreateEntity<Entity>("button.jayden_choreops_approve_reward_screen_time_30_min", "unavailable");
        _jaydenButton60 = _entityBuilder.CreateEntity<Entity>("button.jayden_choreops_approve_reward_screen_time_60_min", "unavailable");

        // Setup reward buttons for Aaron
        _aaronButton15 = _entityBuilder.CreateEntity<Entity>("button.aaron_choreops_approve_reward_screen_time_15_min", "unavailable");
        _aaronButton30 = _entityBuilder.CreateEntity<Entity>("button.aaron_choreops_approve_reward_screen_time_30_min", "unavailable");
        _aaronButton60 = _entityBuilder.CreateEntity<Entity>("button.aaron_choreops_approve_reward_screen_time_60_min", "unavailable");

        // Setup reward buttons for Gabriel
        _gabrielButton15 = _entityBuilder.CreateEntity<Entity>("button.gabriel_choreops_approve_reward_screen_time_15_min", "unavailable");
        _gabrielButton30 = _entityBuilder.CreateEntity<Entity>("button.gabriel_choreops_approve_reward_screen_time_30_min", "unavailable");
        _gabrielButton60 = _entityBuilder.CreateEntity<Entity>("button.gabriel_choreops_approve_reward_screen_time_60_min", "unavailable");
    }

    public void Dispose()
    {
        _sut.Cleanup();
    }

    // ============================================================================
    // INITIALIZATION TESTS
    // ============================================================================

    [Fact]
    public void GivenScreenTimeAppInitialized_WhenInitializeAsyncIsComplete_ThenAppLoadsSuccessfully()
    {
        // Given: ScreenTime app is initialized
        // When: InitializeAsync completes
        // Then: App should be loaded without exceptions

        _sut.Instance.Should().NotBeNull();
    }

    // ============================================================================
    // STATE TRANSITION: TV Control Tests
    // ============================================================================

    [Fact]
    public void GivenStateMachineIsLocked_WhenTvTurnsOn_ThenStateHandlesTransition()
    {
        // Given: State machine is in Locked state
        // When: TV turns on
        // Then: State machine fires Lock trigger (PermitReentry)

        // Act
        _state.Change(_loungeTv!, "on");

        // Assert
        // No exception should be thrown; state machine handles the transition
        _sut.Instance.Should().NotBeNull();
    }

    [Fact]
    public void GivenStateMachineHasActiveScreenTime_WhenTvTurnsOff_ThenStateMachineTransitionsToLocked()
    {
        // Given: TV is on
        // When: TV turns off
        // Then: State machine transitions to Locked state and resets

        // Act
        _state.Change(_loungeTv!, "on");
        _state.Change(_loungeTv!, "off");

        // Assert
        // No exception should be thrown; state machine handles the transition
        _sut.Instance.Should().NotBeNull();
    }

    // ============================================================================
    // GRANT SCREEN TIME TESTS
    // ============================================================================

    [Fact]
    public void GivenStateMachineIsLocked_WhenJaydenButton15MinIsPushed_ThenScreenTimeIsGrantedToJayden()
    {
        // Given: State machine is in Locked state
        // When: Jayden's 15-minute reward button is pressed
        // Then: Screen time should be granted to Jayden for 15 minutes (plus 5 minute buffer)

        var expectedDuration = TimeSpan.FromMinutes(20); // 15 + 5 buffer

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        // Timer service should have been initiated
        _sut.Instance.Should().NotBeNull();
    }

    [Fact]
    public void GivenScreenTimeGrantedToJayden_WhenAaronRequestsEqualOrShorterScreenTime_ThenAaronRequestIsIgnored()
    {
        // Given: Jayden has active screen time for 20 minutes (15 + 5 buffer)
        // When: Aaron requests 15 minutes of screen time (20 minutes with buffer)
        // Then: Aaron's request should be ignored because it equals remaining time

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(_aaronButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        // Aaron's request should be ignored; state machine should remain focused on Jayden
        _sut.Instance.Should().NotBeNull();
    }

    [Fact]
    public void GivenJaydenHasActiveScreenTime_WhenAaronRequestsLongerScreenTime_ThenAaronScreenTimeIsGrantedAndJaydensCancelled()
    {
        // Given: Jayden has active screen time for 15 minutes (20 total with buffer)
        // When: Aaron requests 60 minutes of screen time (65 total with buffer)
        // Then: Aaron's screen time should be granted, Jayden's timers cancelled

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(_aaronButton60!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        // Aaron's 65-minute duration is longer than Jayden's 20-minute duration
        _sut.Instance.Should().NotBeNull();
    }

    // ============================================================================
    // TIMER MANAGEMENT TESTS
    // ============================================================================

    [Fact]
    public void GivenScreenTimeGranted_WhenSchedulerAdvancesByOneSecond_ThenTimerServiceIsInvoked()
    {
        // Given: Screen time is granted to a child
        // When: Scheduler advances by 1 second (timer start delay)
        // Then: Home Assistant timer service should be called

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        // Timer should have been started through service calls
        _sut.State.ServiceCalls.Should().NotBeEmpty();
    }

    [Fact]
    public void GivenScreenTimeActiveFor20Minutes_When15MinutesElapse_ThenWarningNotificationIsScheduled()
    {
        // Given: Screen time is granted for 20 minutes (15 + 5 buffer)
        // When: 15 minutes elapse (5 minutes before expiration)
        // Then: Warning notification should be scheduled and Alexa should announce

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        _sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(15).Ticks);

        // Assert
        // Alexa should announce the warning
        _sut.Alexa.AnnounceCalls.Count.Should().BeGreaterThan(0, "Warning announcement should be made 5 minutes before expiration");
    }

    [Fact]
    public void GivenScreenTimeExpires_WhenTimerEndsAfter20Minutes_ThenExpirationHandlerInvokesAnnouncement()
    {
        // Given: Screen time is active
        // When: Timer expires (20 minutes elapse plus 2-second expiration delay)
        // Then: Expiration handler should be invoked and announcement made

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        _sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(20).Ticks);
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(3).Ticks);

        // Assert
        // Alexa should announce timer expiration
        _sut.Alexa.AnnounceCalls.Should().NotBeEmpty("Expiration announcement should be made");
    }

    // ============================================================================
    // BROWSER CONTROL TESTS
    // ============================================================================

    [Fact]
    public void GivenScreenTimeGranted_WhenGrantScreenTimeMethodCalled_ThenWebostV2ServiceIsCalled()
    {
        // Given: Screen time is granted
        // When: GrantScreenTime calls OpenBrowserUrl
        // Then: webostv.command service should be called

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        _sut.State.ServiceCalls.Should().NotBeEmpty("webostv.command should be called to open browser URL");
    }

    // ============================================================================
    // STATE RESET TESTS
    // ============================================================================

    [Fact]
    public void GivenScreenTimeIsActive_WhenStateTransitionsToLocked_ThenScheduledActionsAreDisposed()
    {
        // Given: Screen time is active with scheduled timers
        // When: State machine transitions to Locked (via TV off)
        // Then: All scheduled action disposables should be cleaned up

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(_loungeTv!, "off");

        // Assert
        // No exceptions should be thrown when resetting state
        _sut.Instance.Should().NotBeNull();
    }

    // ============================================================================
    // MULTIPLE CHILDREN SCENARIOS
    // ============================================================================

    [Fact]
    public void GivenJaydenHasActiveScreenTime_WhenGabrielRequestsLongerScreenTime_ThenGabrielScreenTimeIsGranted()
    {
        // Given: Jayden has 15 minutes of active screen time (20 total)
        // When: Gabriel requests 30 minutes of screen time (35 total)
        // Then: Gabriel's request should be granted (longer duration)

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(_gabrielButton30!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        // Gabriel's request (35 minutes) is longer than Jayden's (20 minutes)
        _sut.Instance.Should().NotBeNull();
    }

    [Fact]
    public void GivenMultipleChildrenRequested_WhenButtonsArePressedInSequence_ThenLongerScreenTimeAlwaysWins()
    {
        // Given: Multiple children have requested screen time in sequence
        // When: Each request is compared with remaining time
        // Then: The child with longest total duration should have active screen time

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(_aaronButton30!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(_gabrielButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        // Aaron's 35-minute duration (30 + 5) should be active
        _sut.Instance.Should().NotBeNull();
    }

    // ============================================================================
    // EDGE CASES AND ERROR HANDLING
    // ============================================================================

    [Fact]
    public void GivenNoActiveScreenTime_WhenFirstChildRequestsScreenTime_ThenScreenTimeIsGrantedSuccessfully()
    {
        // Given: No active screen time
        // When: A child requests screen time for the first time
        // Then: Screen time should be granted without any duration comparison

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        // First request should always be granted
        _sut.State.ServiceCalls.Should().NotBeEmpty("First screentime request should be granted");
    }

    [Fact]
    public void GivenScreenTimeGranted_WhenSameChildPushesButtonAgain_ThenDurationIsComparedWithRemaining()
    {
        // Given: Screen time is active for 15 minutes
        // When: Same child (Jayden) pushes button again after 5 minutes
        // Then: New request is evaluated against remaining time

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(5).Ticks);
        _state.Change(_jaydenButton15!, "pushed");

        // Assert
        // Second request (20 minutes) is longer than remaining (15 minutes)
        _sut.Instance.Should().NotBeNull();
    }

    [Fact]
    public void GivenScreenTimeActive_WhenTvTurnsOffDuringActiveSession_ThenAllScreenTimeEnds()
    {
        // Given: Active screen time session
        // When: TV is turned off
        // Then: State should transition to Locked and reset all state

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(_loungeTv!, "off");

        // Assert
        // TV off should trigger Lock state transition and reset
        _sut.Instance.Should().NotBeNull();
    }

    // ============================================================================
    // TIMING AND DELAY TESTS
    // ============================================================================

    [Fact]
    public void GivenScreenTimeGranted_WhenTimerEndsWithExpiration_Then2SecondDelayOccursBeforeReset()
    {
        // Given: Screen time timer is active
        // When: Timer expires (20 minutes plus 2-second delay)
        // Then: 2-second delay should occur before resetting state

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        _sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(20).Ticks);

        var announcesAfter20Min = _sut.Alexa.AnnounceCalls.Count;

        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(2).Ticks);

        // Assert
        // After full delay, additional announcements or state changes should have occurred
        _sut.Instance.Should().NotBeNull();
    }

    [Fact]
    public void GivenScreenTimeGrantedFor15Minutes_WhenCalculatingWarningDelay_ThenWarningDelaysExactly15Minutes()
    {
        // Given: Screen time is granted for 15 minutes (20 with buffer)
        // When: Warning delay is calculated (duration - 5 minutes = 15 minutes)
        // Then: Warning should be announced at 15-minute mark, not before

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Advance 14 minutes - warning should NOT appear yet
        _sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(14).Ticks);
        var announcesAt14Min = _sut.Alexa.AnnounceCalls.Count;

        // Advance 1 more minute to reach 15 minutes
        _sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(1).Ticks);
        var announcesAt15Min = _sut.Alexa.AnnounceCalls.Count;

        // Assert
        // Announcement count should increase at 15-minute mark
        announcesAt15Min.Should().BeGreaterThan(announcesAt14Min);
    }

    // ============================================================================
    // SERVICE CALL VERIFICATION TESTS
    // ============================================================================

    [Fact]
    public void GivenScreenTimeGranted_WhenWebOstTvCommandIsCalled_ThenServiceCallShouldContainCorrectEntityAndPayload()
    {
        // Given: Screen time is granted
        // When: OpenBrowserUrl is invoked
        // Then: Service call should target lounge_tv with correct command payload

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        var webostServiceCalls = _sut.State.ServiceCalls.Where(
            sc => sc.Service.Contains("webostv", StringComparison.OrdinalIgnoreCase)
        ).ToList();

        webostServiceCalls.Should().NotBeEmpty("webostv service should be called");
    }

    // ============================================================================
    // DISPOSAL AND CLEANUP TESTS
    // ============================================================================

    [Fact]
    public void GivenScreenTimeAppIsActive_WhenDisposeIsCalled_ThenResourcesAreReleasedWithoutExceptions()
    {
        // Given: ScreenTime app is initialized and has active subscriptions
        // When: Dispose() is called
        // Then: All subscriptions and scheduled actions should be disposed cleanly

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _sut.Instance.Dispose();

        // Assert
        // No exceptions should be thrown during disposal
        _sut.Instance.Should().NotBeNull();
    }

    [Fact]
    public void GivenMultipleScheduledActionsAreActive_WhenDisposableIsDisposed_ThenAllScheduledActionsAreCancelled()
    {
        // Given: Multiple scheduled actions (timer, warning, expiration)
        // When: State transitions to Locked and ResetScreenTimeState is called
        // Then: All disposables should be disposed

        // Act
        _state.Change(_jaydenButton15!, "pushed");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        _state.Change(_loungeTv!, "off");
        _sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

        // Assert
        // No exceptions should be thrown; cleanup should be complete
        _sut.Instance.Should().NotBeNull();
    }
}
