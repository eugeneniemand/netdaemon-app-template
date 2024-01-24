using Humanizer;
using Humanizer.Localisation;
using NetDaemon;
using Niemand.Helpers;

namespace Niemand.NotificationManager;

public class ApplianceNotification : IApplianceNotification
{
    private readonly InputBooleanEntity _acknowledge;

    private readonly string                         _appliance;
    private readonly Dictionary<string, CycleState> _cycleStates;
    private readonly SensorEntity                   _remainingTime;
    private readonly IScheduler                     _scheduler;
    private readonly SensorEntity                   _status;

    public ApplianceNotification(IScheduler scheduler, IApplianceNotificationConfig config)
    {
        _scheduler     = scheduler;
        _appliance     = config.Name;
        _status        = config.Status;
        _remainingTime = config.RemainingTime;
        _acknowledge   = config.Acknowledge;
        _cycleStates   = config.CycleStates;
    }

    public CycleState CycleState => _status.State != null ? _cycleStates[_status.State.ToLower()] : CycleState.Unknown;

    public string EventId => $"{_appliance}Tts";

    public Notification? GetNotification(CycleState cycle, TimeSpan lastPrompt)
    {
        var notification = new Notification
        {
            EventId = EventId
        };

        switch (cycle)
        {
            case CycleState.Running:
                switch (TimeRemaining.TotalMinutes)
                {
                    case >= 60 when lastPrompt.TotalMinutes <= 15:
                    case >= 30 when lastPrompt.TotalMinutes <= 10:
                    case >= 10 when lastPrompt.TotalMinutes <= 5:
                        return null;
                }

                notification.Message = $"The {_appliance} will be done in {TimeRemaining.Humanize(minUnit: TimeUnit.Minute)}";
                notification.Type    = Alexa.NotificationType.Announcement;
                break;
            case CycleState.Finished:
                return null;
            case CycleState.Ready:
                if (_acknowledge.IsOff() && lastPrompt.TotalMinutes <= 15) return null;

                if (_acknowledge.IsOff() && lastPrompt.TotalMinutes > 15)
                {
                    notification.Message = $"The {_appliance} finished {TimeFinished.Humanize(minUnit: TimeUnit.Minute)} ago. Has it been unloaded?";
                    notification.Type    = Alexa.NotificationType.Prompt;
                }
                else if (_acknowledge.IsOn() && lastPrompt.TotalMinutes >= 60)
                {
                    notification.Message = $"The {_appliance} is ready";
                    notification.Type    = Alexa.NotificationType.Announcement;
                }

                break;
            case CycleState.Unknown:
                throw new ArgumentOutOfRangeException();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return notification;
    }

    public Notification? HandleResponse(PromptResponseType? responseType)
    {
        if (responseType == null) return null;

        var notification = new Notification
        {
            EventId = EventId,
            Type    = Alexa.NotificationType.Tts
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

    public TimeSpan TimeFinished => _scheduler.Now.LocalDateTime - _status.EntityState.LastChanged.Value;

    public TimeSpan TimeRemaining => DateTime.Parse(_remainingTime.State ?? _scheduler.Now.LocalDateTime.ToString()) - _scheduler.Now;
}