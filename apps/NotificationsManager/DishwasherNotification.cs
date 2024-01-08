using Humanizer;
using Humanizer.Localisation;
using Niemand;

namespace daemonapp.apps.NotificationsManager;

public interface IApplianceNotificationConfig
{
    Dictionary<string, CycleState> CycleStates { get; }
    InputBooleanEntity Acknowledge { get; }
    SensorEntity RemainingTime { get; }
    SensorEntity Status { get; }
    string Name { get; }
}

public class DishwasherNotification : IApplianceNotification
{
    private readonly IEntities  _entities;
    private readonly IScheduler _scheduler;

    private readonly Dictionary<string, CycleState> DishwasherCycleStates = new()
    {
        { "run", CycleState.Running },
        { "finished", CycleState.Finished },
        { "ready", CycleState.Ready }
    };

    public DishwasherNotification(IEntities entities, IScheduler scheduler)
    {
        _entities  = entities;
        _scheduler = scheduler;
    }

    public CycleState CycleState => DishwasherCycleStates[_entities.Sensor.DishwasherOperationState.State.ToLower()];

    public string EventId => "DishwasherTts";

    public Notification? GetNotification(CycleState cycle, TimeSpan lastPrompt)
    {
        var notification = new Notification
        {
            EventId = EventId
        };

        switch (cycle)
        {
            case CycleState.Running:
                notification.Message = $"The Dishwasher will be done in {TimeRemaining.Humanize(minUnit: TimeUnit.Minute)}";
                notification.Type    = Alexa.NotificationType.Announcement;
                break;
            case CycleState.Finished:
                return null;
            case CycleState.Ready:
                if (_entities.InputBoolean.DishwasherAck.IsOff() && lastPrompt.TotalMinutes <= 15) return null;

                if (_entities.InputBoolean.DishwasherAck.IsOff() && lastPrompt.TotalMinutes > 15)
                {
                    notification.Message = $"The Dishwasher finished {TimeFinished.Humanize(minUnit: TimeUnit.Minute)} ago. Has it been unpacked?";
                    notification.Type    = Alexa.NotificationType.Prompt;
                }
                else if (_entities.InputBoolean.DishwasherAck.IsOn() && lastPrompt.TotalMinutes >= 60)
                {
                    notification.Message = "The Dishwasher is ready";
                    notification.Type    = Alexa.NotificationType.Announcement;
                }

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
                _entities.InputBoolean.DishwasherAck.TurnOn();
                notification.Message = "Thanks";
                break;
            default:
                return null;
        }

        return notification;
    }

    public TimeSpan TimeFinished => _scheduler.Now.LocalDateTime - _entities.Sensor.DishwasherOperationState.EntityState.LastChanged.Value;

    public TimeSpan TimeRemaining => DateTime.Parse(_entities.Sensor.DishwasherRemainingProgramTime.State ?? _scheduler.Now.LocalDateTime.ToString()) - _scheduler.Now;
}