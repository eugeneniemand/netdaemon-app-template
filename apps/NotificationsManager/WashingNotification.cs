using Humanizer;
using Humanizer.Localisation;
using NetDaemon;
using Niemand.Helpers;

namespace Niemand.NotificationManager;

public class WashingNotification : IApplianceNotification
{
    private readonly IEntities  _entities;
    private readonly IScheduler _scheduler;

    private readonly Dictionary<string, CycleState> WashingCycleStates = new()
    {
        { "run", CycleState.Running },
        { "stop", CycleState.Ready },
        { "pause", CycleState.Paused }
    };

    public WashingNotification(IEntities entities, IScheduler scheduler)
    {
        _entities  = entities;
        _scheduler = scheduler;
    }

    public CycleState CycleState => WashingCycleStates[_entities.Sensor.WashingMachineWasherMachineState.State.ToLower()];

    public string EventId => "WashingTts";

    public Notification? GetNotification(CycleState cycle, TimeSpan lastPrompt)
    {
        var notification = new Notification
        {
            EventId = EventId
        };

        switch (cycle)
        {
            case CycleState.Running:
                notification.Message = $"The Washing will be done in {TimeRemaining.Humanize(minUnit: TimeUnit.Minute)}";
                notification.Type    = Alexa.NotificationType.Announcement;
                break;
            case CycleState.Finished:
                return null;
            case CycleState.Ready:
                if (_entities.InputBoolean.WasherAck.IsOff() && lastPrompt.TotalMinutes <= 15) return null;

                if (_entities.InputBoolean.WasherAck.IsOff() && lastPrompt.TotalMinutes > 15)
                {
                    notification.Message = $"The Washing finished {TimeFinished.Humanize(minUnit: TimeUnit.Minute)} ago. Has it been unloaded?";
                    notification.Type    = Alexa.NotificationType.Prompt;
                }
                else if (_entities.InputBoolean.WasherAck.IsOn() && lastPrompt.TotalMinutes >= 60)
                {
                    notification.Message = "The Washer is ready";
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
                _entities.InputBoolean.WasherAck.TurnOn();
                notification.Message = "Thanks";
                break;
            default:
                return null;
        }

        return notification;
    }

    public TimeSpan TimeFinished => _scheduler.Now.LocalDateTime - _entities.Sensor.WashingMachineWasherMachineState.EntityState.LastChanged.Value;

    public TimeSpan TimeRemaining => DateTime.Parse(_entities.Sensor.WashingMachineWasherCompletionTime.State ?? _scheduler.Now.LocalDateTime.ToString()) - _scheduler.Now;
}