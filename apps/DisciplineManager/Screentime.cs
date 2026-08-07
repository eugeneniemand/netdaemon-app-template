using Humanizer;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel.Integration;
using Niemand.Helpers;
using Niemand.Helpers.Notifications;
using Stateless;
using Stateless.Graph;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Niemand;

[Focus]
[NetDaemonApp]
public class ScreenTime : IAsyncInitializable, IDisposable
{
    private enum ScreenTimeState
    {
        Locked,     // Waiting for screentime approval
        Unlocked    // Child has active screentime
    }

    private enum ScreenTimeTrigger
    {
        Unlock,             // Transition from Locked to Unlocked
        Lock,               // Transition from Unlocked to Locked
        TvTurnedOn,         // TV turned on event
        TvSourceChanged,    // TV source changed event
        GrantScreenTime,    // Grant screen time with parameters
        OpenScreenTimePage, // Open the screentime request page
        TimerExpired,        // Screentime timer expired
        ResetTimer
    }

    private readonly Entities _entities;
    private readonly IMqttEntityManager _entityManager;
    private readonly TimerManager _timerManager;
    private readonly IScheduler _scheduler;
    private readonly IAlexa _alexa;
    private readonly IHaContext _haContext;
    private readonly IServices _services;
    private readonly ILogger<ScreenTime> _logger;

    private readonly string _screenTimeRequestUrl = "http://10.10.40.14:8000/screentime";
    private IDisposable? _warningNotificationSubscription;

    // State machine
    private StateMachine<ScreenTimeState, ScreenTimeTrigger> _stateMachine;
    private StateMachine<ScreenTimeState, ScreenTimeTrigger>.TriggerWithParameters<string, int> _grantScreenTimeTrigger;
    private string? _activeChild;
    private DateTime _expirationTime = DateTime.MinValue;

    // Scheduled action disposables for cancellation
    private IDisposable? _timerStartDisposable;
    private IDisposable? _warningNotificationDisposable;
    private IDisposable? _expirationDisposable;
    private string? _newSource;

    public ScreenTime(IHaContext haContext, IMqttEntityManager entityManager, TimerManager timerManager, IScheduler scheduler, IAlexa alexa, IServices services, ILogger<ScreenTime> logger)
    {
        _haContext = haContext;
        _entityManager = entityManager;
        _timerManager = timerManager;
        _scheduler = scheduler;
        _alexa = alexa;
        _services = services;
        _logger = logger;
        _entities = new Entities(haContext);

        // Initialize state machine
        _stateMachine = new StateMachine<ScreenTimeState, ScreenTimeTrigger>(ScreenTimeState.Locked);

        // Set up parameterized trigger for granting screen time
        _grantScreenTimeTrigger = _stateMachine.SetTriggerParameters<string, int>(ScreenTimeTrigger.GrantScreenTime);

        _stateMachine.Configure(ScreenTimeState.Locked)
            .OnEntry(OpenScreenTimeRequestPage)
            .OnEntry(ResetScreenTimeState)
            .Permit(ScreenTimeTrigger.Unlock, ScreenTimeState.Unlocked)
            // permit unlock and  grant screentime from locked state
            .Permit(ScreenTimeTrigger.GrantScreenTime, ScreenTimeState.Unlocked)
            .PermitReentry(ScreenTimeTrigger.Lock)
            //.PermitReentry(ScreenTimeTrigger.TvSourceChanged)
            .OnEntryFrom(_grantScreenTimeTrigger, (kidName, minutes) => GrantScreenTime(kidName, minutes));

        _stateMachine.Configure(ScreenTimeState.Unlocked)
            .Permit(ScreenTimeTrigger.Lock, ScreenTimeState.Locked)            
            .PermitReentry(ScreenTimeTrigger.Unlock)
            //.PermitReentry(ScreenTimeTrigger.TvSourceChanged)
            .PermitReentry(ScreenTimeTrigger.TvTurnedOn)
            .OnEntryFrom(_grantScreenTimeTrigger, (kidName, minutes) => GrantScreenTime(kidName, minutes));
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        // When TV turns on, fire the state machine transition
        _entities.MediaPlayer.LoungeTv.StateChanges()
            .Where(s => s.New.IsOn())
            .Subscribe(_ => _stateMachine.Fire(ScreenTimeTrigger.Lock));

        // When source changes and new source is not "Web Browser" store the intened source to allow switching to it after PIN validation
        _entities.MediaPlayer.LoungeTv.StateAllChanges()
            .Where(e => e.Entity.IsOn() && e.New?.Attributes?.Source != e.Old?.Attributes?.Source)
            .Where(e => e.New?.Attributes?.Source != "Web Browser")
            .Subscribe(e =>
            {
                _logger.LogInformation("TV source changed to {NewSource} from {OldSource}", e.New?.Attributes?.Source, e.Old?.Attributes?.Source);
                _newSource = e.New?.Attributes?.Source;
            });

        // When source changes, fire the state machine transition
        _entities.MediaPlayer.LoungeTv.StateAllChanges()
            .Where(e => e.Entity.IsOn() && e.New?.Attributes?.Source != e.Old?.Attributes?.Source)            
            .Where(_ => _activeChild == null || DateTime.Now >= _expirationTime)
            .Subscribe(_ => _stateMachine.Fire(ScreenTimeTrigger.Lock));

        // When TV turns off, fire the state machine transition
        _entities.MediaPlayer.LoungeTv.StateChanges()
            .Where(s => s.New.IsOff())
            .Subscribe(_ => _stateMachine.Fire(ScreenTimeTrigger.Lock));

        // Subscribe to reward approvals for Jayden
        SubscribeToScreenTimeReward("Jayden", new[]
        {
            (_entities.Button.JaydenChoreopsApproveRewardScreenTime15Min, 15),
            (_entities.Button.JaydenChoreopsApproveRewardScreenTime30Min, 30),
            (_entities.Button.JaydenChoreopsApproveRewardScreenTime60Min, 60),
        });

        // Subscribe to reward approvals for Aaron
        SubscribeToScreenTimeReward("Aaron", new[]
        {
            (_entities.Button.AaronChoreopsApproveRewardScreenTime15Min, 15),
            (_entities.Button.AaronChoreopsApproveRewardScreenTime30Min, 30),
            (_entities.Button.AaronChoreopsApproveRewardScreenTime60Min, 60),
        });

        // Subscribe to reward approvals for Gabriel
        SubscribeToScreenTimeReward("Gabriel", new[]
        {
            (_entities.Button.GabrielChoreopsApproveRewardScreenTime15Min, 15),
            (_entities.Button.GabrielChoreopsApproveRewardScreenTime30Min, 30),
            (_entities.Button.GabrielChoreopsApproveRewardScreenTime60Min, 60),
        });

        _haContext.Events.Filter<ScreenTimeEventData>("screentime_request")
            .Subscribe(e =>
            {
                _stateMachine.Fire(_grantScreenTimeTrigger, "PIN", e.Data.ScreentimeMinutes);
                _logger.LogInformation("Unlocked by PIN");
            });

        _stateMachine.Fire(ScreenTimeTrigger.Lock);

        await Task.CompletedTask;
    }
   

    private void SubscribeToScreenTimeReward(string kidName, (ButtonEntity button, int minutes)[] rewards)
    {
        foreach (var (button, minutes) in rewards)
        {
            button.StateChanges().Subscribe(_ =>
            {
                _logger.LogInformation("{kidName} received {minutes} minutes of screen time", kidName, minutes);
                _stateMachine.Fire(_grantScreenTimeTrigger, kidName, minutes);
            });
        }
    }

    private bool GrantScreenTime(string kidName, int minutes)
    {
        var duration = TimeSpan.FromMinutes(minutes + 5);
        var now = DateTime.Now;
        var newExpirationTime = now.Add(duration);

        // Check if there's an active timer
        if (_activeChild != null && _expirationTime != DateTime.MinValue)
        {
            var remainingTime = _expirationTime - now;

            // Only grant new screentime if the new duration is longer than the remaining time
            if (duration <= remainingTime)
            {
                _logger.LogInformation("Screen time request from {kidName} for {minutes} minutes ignored. {activeChild} has {remainingSeconds}s remaining which is longer than the new request.", kidName, minutes, _activeChild, remainingTime.TotalSeconds);
                return false;
            }

            // New duration is longer, so cancel existing schedules
            _logger.LogInformation("Cancelling existing schedules for {activeChild} to grant longer screen time to {kidName}. Old remaining: {oldRemaining}s, New duration: {newDuration}s", _activeChild, kidName, remainingTime.TotalSeconds, duration.TotalSeconds);
            _timerStartDisposable?.Dispose();
            _warningNotificationDisposable?.Dispose();
            _expirationDisposable?.Dispose();
        }

        _expirationTime = newExpirationTime;
        _activeChild = kidName;

        // Redirect from request page to normal content
        //OpenBrowserUrl("http://10.10.40.14:8000/");

        // Set source to _newsource
        _services.MediaPlayer.SelectSource(ServiceTarget.FromEntity(_entities.MediaPlayer.LoungeTv.EntityId), new MediaPlayerSelectSourceParameters() { Source = _newSource });

        // Start the screentime timer
        _timerStartDisposable = _scheduler.Schedule(TimeSpan.FromSeconds(1), async () =>
        await _timerManager.StartAsync("timer.screentime_remaining", duration));

        // Calculate warning time (5 minutes before end)
        var warningDelay = duration - TimeSpan.FromMinutes(5);

        if (warningDelay.TotalSeconds > 0)
        {
            // Schedule warning notification
            _warningNotificationDisposable = _scheduler.Schedule(warningDelay, async () =>
            {
                await SendWarningNotification(kidName, 5);
            });
        }

        // Schedule redirect back to request page when timer ends
        _expirationDisposable = _scheduler.Schedule(duration + TimeSpan.FromSeconds(2), async () =>
        {
            await TimerExpired(kidName);
        });

        _logger.LogInformation("Screen time granted to {kidName} for {minutes} minutes. State: {State}", kidName, minutes, _stateMachine.State);
        return true;
    }

    private void OpenScreenTimeRequestPage()
    {
        if (!_entities.MediaPlayer.LoungeTv.IsOn())
            return;

        _logger.LogInformation("Opening screen time request page");
        OpenBrowserUrl(_screenTimeRequestUrl);
    }

    private void OpenBrowserUrl(string url)
    {
        try
        {
            _services.Webostv.Command(new()
            {
                EntityId = _entities.MediaPlayer.LoungeTv.EntityId,
                Command = "system.launcher/open",
                Payload = new
                {
                    target = url
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to open browser URL: {url}", url);
        }
    }

    private async Task SendWarningNotification(string kidName, int minutesRemaining)
    {
        try
        {
            var message = $"{kidName}, you have {minutesRemaining} minute{(minutesRemaining > 1 ? "s" : "")} of screen time remaining";

            _alexa.Announce(new Alexa.Config()
            {
                Entity = _entities.MediaPlayer.LoungeTv.EntityId,
                Message = message
            });

            _services.Notify.LoungeTv( new NotifyLoungeTvParameters()
            {
                Message = message
            });

            _logger.LogInformation("Sent warning to {kidName}: {message}", kidName, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send warning notification for {kidName}", kidName);
        }
    }

    private async Task TimerExpired(string kidName)
    {
        try
        {
            _logger.LogInformation("Screen time expired for {kidName}", kidName);
            var message = $"{kidName}, your screen time has ended. Please request more time if you would like to continue.";
            // Notify the child
            _alexa.Announce(new Alexa.Config()
            {
                Entity = _entities.MediaPlayer.LoungeTv.EntityId,
                Message = message,
            });

            _services.Notify.LoungeTv(new NotifyLoungeTvParameters()
            {
                Message = message
            });

            // Redirect back to request page
            await _scheduler.Sleep(TimeSpan.FromSeconds(2));
            _stateMachine.Fire(ScreenTimeTrigger.Lock);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to handle timer expiration for {kidName}", kidName);
        }
    }

    private void ResetScreenTimeState()
    {
        _logger.LogInformation("Resetting screentime state from {CurrentChild}", _activeChild ?? "None");
        _activeChild = null;
        _expirationTime = DateTime.MinValue;

        // Dispose of all scheduled actions
        _timerStartDisposable?.Dispose();
        _warningNotificationDisposable?.Dispose();
        _expirationDisposable?.Dispose();        
    }

    public void Dispose()
    {
        _warningNotificationSubscription?.Dispose();
    }
}

internal class ScreenTimeEventData
{
    public string Device { get; set; }
    public DateTime Timestamp { get; set; }
    // get the json property "screentime_minutes" and map it to ScreentimeMinutes
    [JsonPropertyName("screentime_minutes")]
    public int ScreentimeMinutes { get; set; }
}