using Humanizer;
using Humanizer.Localisation;
using NetDaemon;
using Niemand.Helpers;

namespace Niemand.NotificationManager;

public class ApplianceNotification : IApplianceNotification
{
    // Acknowledge boolean to track if the user has confirmed unloading the appliance
    private readonly InputBooleanEntity _acknowledge;

    // Name of the appliance (e.g., "washing machine")
    private readonly string _appliance;

    // Maps status strings (e.g., "running") to CycleState enums
    private readonly Dictionary<string, CycleState> _cycleStates;

    // Sensor for remaining time until the appliance finishes
    private readonly SensorEntity _remainingTime;

    // Scheduler for timing-related operations
    private readonly IScheduler _scheduler;

    // Logger for errors and debugging information
    private readonly ILogger<NotificationsManager> _logger;

    // Sensor for the current status of the appliance (e.g., "running", "finished")
    private readonly SensorEntity _status;

    public ApplianceNotification(IScheduler scheduler, IApplianceNotificationConfig config, ILogger<NotificationsManager> logger)
    {
        _scheduler = scheduler;
        _logger = logger;
        _appliance = config.Name;
        _status = config.Status;
        _remainingTime = config.RemainingTime;
        _acknowledge = config.Acknowledge;
        _cycleStates = config.CycleStates;
    }

    // Gets the current cycle state based on the status sensor
    public CycleState CycleState => _status.State != null && _cycleStates.TryGetValue(_status.State.ToLower(), out var state)
        ? state
        : CycleState.Unknown;

    // Unique event ID for notifications
    public string EventId => $"{_appliance}Tts";

    // Prompt intervals in minutes for notifications based on remaining time
    private const int PromptInterval1 = 15; // When remaining time >= 60 minutes
    private const int PromptInterval2 = 10; // When remaining time >= 30 minutes
    private const int PromptInterval3 = 5;  // When remaining time >= 10 minutes
    private const int AcknowledgeInterval = 60; // Reminder interval after acknowledgment

    // Determines the appropriate notification based on the cycle state and time since last prompt
    public Notification? GetNotification(CycleState cycle, TimeSpan lastPrompt)
    {
        switch (cycle)
        {
            case CycleState.Running:
                return TimeRemaining != TimeSpan.Zero ? GetRunningNotification(lastPrompt) : null;
            case CycleState.Finished:
            case CycleState.Ready:
                return GetReadyNotification(lastPrompt);
            case CycleState.Unknown:
                _logger.LogWarning("Unknown cycle state for {_appliance}", _appliance);
                return null;
            default:
                _logger.LogError("Unexpected cycle state for {_appliance}: {Cycle}", _appliance, cycle);
                return null;
        }
    }

    // Handles notifications when the appliance is running
    private Notification? GetRunningNotification(TimeSpan lastPrompt)
    {        

        if (ShouldNotPromptForRunning(TimeRemaining, lastPrompt))
            return null;

        return CreateNotification(
            $"The {_appliance} will be done in {TimeRemaining.Humanize(minUnit: TimeUnit.Minute)}",
            Alexa.NotificationType.Announcement);
    }

    // Checks if a notification should be skipped based on remaining time and last prompt
    private bool ShouldNotPromptForRunning(TimeSpan remaining, TimeSpan lastPrompt)
    {
        if (remaining.TotalMinutes >= 60 && lastPrompt.TotalMinutes <= PromptInterval1) return true;
        if (remaining.TotalMinutes >= 30 && lastPrompt.TotalMinutes <= PromptInterval2) return true;
        if (remaining.TotalMinutes >= 10 && lastPrompt.TotalMinutes <= PromptInterval3) return true;
        return false;
    }

    // Handles notifications when the appliance is finished or ready
    private Notification? GetReadyNotification(TimeSpan lastPrompt)
    {
        var timeSinceFinished = (_scheduler.Now.LocalDateTime - _status.EntityState?.LastChanged) ?? TimeSpan.MaxValue;

        // Announce when the appliance just finished and hasn't been acknowledged
        if (_acknowledge.IsOff() && timeSinceFinished.TotalMinutes <= PromptInterval1)
            return CreateNotification($"The {_appliance} just finished", Alexa.NotificationType.Announcement);

        // Prompt if it’s been a while and still not acknowledged
        if (_acknowledge.IsOff() && lastPrompt.TotalMinutes > PromptInterval1)
            return CreateNotification(
                $"The {_appliance} finished {TimeFinished.Humanize(minUnit: TimeUnit.Minute)} ago. Has it been unloaded?",
                Alexa.NotificationType.Prompt);

        // Remind after acknowledgment if enough time has passed
        if (_acknowledge.IsOn() && lastPrompt.TotalMinutes >= AcknowledgeInterval)
            return CreateNotification($"The {_appliance} is ready", Alexa.NotificationType.Announcement);

        return null;
    }

    // Creates a notification with the specified message and type
    private Notification CreateNotification(string message, Alexa.NotificationType type)
    {
        return new Notification
        {
            EventId = EventId,
            Message = message,
            Type = type
        };
    }

    // Processes the user’s response to a prompt
    public Notification? HandleResponse(PromptResponseType? responseType)
    {
        if (responseType == null) return null;

        var notification = new Notification
        {
            EventId = EventId,
            Type = Alexa.NotificationType.Tts
        };

        switch (responseType)
        {
            case PromptResponseType.ResponseNo:
                notification.Message = "Ok";
                break;
            case PromptResponseType.ResponseYes:
                _acknowledge.TurnOn();
                notification.Message = "Thanks";
                break;
            default:
                return null;
        }

        return notification;
    }

    // Time elapsed since the appliance finished
    public TimeSpan TimeFinished => _status.EntityState?.LastChanged != null
        ? _scheduler.Now.LocalDateTime - _status.EntityState.LastChanged.Value
        : TimeSpan.Zero;

    // Time remaining until the appliance finishes
    public TimeSpan TimeRemaining
    {
        get
        {
            if (string.IsNullOrEmpty(_remainingTime.State))
                return TimeSpan.Zero;

            if (DateTime.TryParse(_remainingTime.State, out var finishTime))
                return finishTime > _scheduler.Now.LocalDateTime ? finishTime - _scheduler.Now.LocalDateTime : TimeSpan.Zero; // if the state is stuck and finsih time is in the past then return zero i.e. invalid

            _logger.LogWarning("Invalid remaining time state for {_appliance}: {_remainingTime.State}", _appliance, _remainingTime.State);
            return TimeSpan.Zero;
        }
    }
}