using daemonapp.apps.NotificationsManager;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class NotificationsManager
{
    private readonly IAlexa                        _alexa;
    private readonly IApplianceNotification        _dishwasherNotification;
    private readonly ApplianceNotification         _dryerNotification;
    private readonly IEntities                     _entities;
    private readonly IHaContext                    _haContext;
    private readonly IDictionary<string, DateTime> _lastPrompt = new Dictionary<string, DateTime>();
    private readonly IScheduler                    _scheduler;
    private readonly IApplianceNotification        _washingNotification;

    public NotificationsManager(IHaContext haContext, IEntities entities, IAlexa alexa, IScheduler scheduler)
    {
        _haContext              = haContext;
        _entities               = entities;
        _alexa                  = alexa;
        _scheduler              = scheduler;
        _dishwasherNotification = new ApplianceNotification(scheduler, new DishwasherNotificationConfig(entities));
        _washingNotification    = new ApplianceNotification(scheduler, new WasherNotificationConfig(entities));
        _dryerNotification      = new ApplianceNotification(scheduler, new DryerNotificationConfig(entities));

        MediaPlayerVolume();
        AlarmReminder();
        Appliances();
    }

    private void AlarmReminder()
    {
        _entities.AlarmControlPanel.Alarmo.StateChanges().Subscribe(s =>
        {
            switch (s.New.State)
            {
                case "armed_night":
                    var reminderMessages = new List<string>();
                    if (_entities.InputBoolean.DryerReminder.IsOn())
                        reminderMessages.Add("Dryer");
                    if (_entities.InputBoolean.WashingReminder.IsOn())
                        reminderMessages.Add("Washer");
                    if (_entities.InputBoolean.DryerReminder.IsOn())
                        reminderMessages.Add("Dishwasher");

                    var reminderMessage = reminderMessages.Any() ? string.Join(",", reminderMessages) + " is ready but not turned on." : "";
                    _alexa.TextToSpeech(new Alexa.Config { Entities = new List<string> { "media_player.master", "media_player.office" }, Message = $"{reminderMessage} Alarm armed" });
                    break;
                case "disarmed":
                    break;
            }
        });
    }

    private void Appliances()
    {
        _entities.Sensor.TumbleDryerDryerMachineState.StateChanges().Subscribe(s =>
        {
            switch (_washingNotification.CycleState)
            {
                case CycleState.Running:
                    _entities.InputBoolean.DryerReminder.TurnOff();
                    break;
                case CycleState.Finished:
                    _entities.InputBoolean.DryerAck.TurnOff();
                    break;
                case CycleState.Ready:
                    break;
                case CycleState.Paused:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        _entities.Sensor.WashingMachineWasherMachineState.StateChanges().Subscribe(s =>
        {
            switch (_washingNotification.CycleState)
            {
                case CycleState.Running:
                    _entities.InputBoolean.WashingReminder.TurnOff();
                    break;
                case CycleState.Finished:
                    _entities.InputBoolean.WasherAck.TurnOff();
                    break;
                case CycleState.Ready:
                    break;
                case CycleState.Paused:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        _entities.Sensor.DishwasherOperationState.StateChanges().Subscribe(s =>
        {
            switch (_dishwasherNotification.CycleState)
            {
                case CycleState.Running:
                    _entities.InputBoolean.DishwasherReminder.TurnOff();
                    break;
                case CycleState.Finished:
                    _alexa.Announce(_entities.MediaPlayer.Kitchen.EntityId, "The Dishwasher just finished");
                    _entities.InputBoolean.DishwasherAck.TurnOff();
                    break;
                case CycleState.Ready:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        _entities.BinarySensor.KitchenMotion.StateChanges()
                 .Where(s => s.Old.IsOff() && s.New.IsOn())
                 .Subscribe(s =>
                 {
                     var notification = _dishwasherNotification.GetNotification(_dishwasherNotification.CycleState, LastPrompt(_dishwasherNotification.EventId));
                     SendNotification(notification, _entities.MediaPlayer.Kitchen.EntityId);
                 });

        _entities.BinarySensor.UtilityMotion.StateChanges()
                 .Where(s => s.Old.IsOff() && s.New.IsOn())
                 .Subscribe(s =>
                 {
                     var notification = _washingNotification.GetNotification(_washingNotification.CycleState, LastPrompt(_washingNotification.EventId));
                     SendNotification(notification, _entities.MediaPlayer.Kitchen.EntityId);
                 });

        _entities.BinarySensor.UtilityMotion.StateChanges()
                 .Where(s => s.Old.IsOff() && s.New.IsOn())
                 .Subscribe(s =>
                 {
                     var notification = _dryerNotification.GetNotification(_dryerNotification.CycleState, LastPrompt(_dryerNotification.EventId));
                     SendNotification(notification, _entities.MediaPlayer.Kitchen.EntityId);
                 });

        _alexa.PromptResponses?
              .Where(r => r.EventId == _dishwasherNotification.EventId)
              .Subscribe(r =>
              {
                  var notification = _dishwasherNotification.HandleResponse(r.ResponseType);
                  SendNotification(notification, _entities.MediaPlayer.Kitchen.EntityId);
              });

        _alexa.PromptResponses?
              .Where(r => r.EventId == _washingNotification.EventId)
              .Subscribe(r =>
              {
                  var notification = _washingNotification.HandleResponse(r.ResponseType);
                  SendNotification(notification, _entities.MediaPlayer.Kitchen.EntityId);
              });

        _alexa.PromptResponses?
              .Where(r => r.EventId == _dryerNotification.EventId)
              .Subscribe(r =>
              {
                  var notification = _dryerNotification.HandleResponse(r.ResponseType);
                  SendNotification(notification, _entities.MediaPlayer.Kitchen.EntityId);
              });
    }

    private TimeSpan LastPrompt(string eventId) => _lastPrompt.ContainsKey(eventId) ? _scheduler.Now.LocalDateTime - _lastPrompt[eventId] : TimeSpan.MaxValue;

    private void MediaPlayerVolume()
    {
        _entities.InputSelect.HouseMode.StateChanges().Subscribe(s =>
        {
            switch (s.New.State)
            {
                case "night":
                    foreach (var mediaPlayer in _haContext.GetAllEntities().Where(e => e.EntityId.Contains("media_player."))) new MediaPlayerEntity(_haContext, mediaPlayer.EntityId).VolumeSet(0.1);
                    break;
                case "day":
                    foreach (var mediaPlayer in _haContext.GetAllEntities().Where(e => e.EntityId.Contains("media_player."))) new MediaPlayerEntity(_haContext, mediaPlayer.EntityId).VolumeSet(0.3);
                    break;
            }
        });
    }

    private void SendNotification(Notification? notification, string mediaPlayer)
    {
        if (notification == null || string.IsNullOrEmpty(notification.Message)) return;

        switch (notification.Type)
        {
            case Alexa.NotificationType.Prompt:
                _alexa.Prompt(mediaPlayer, notification.Message, notification.EventId);
                break;
            case Alexa.NotificationType.Announcement:
                _alexa.Announce(mediaPlayer, notification.Message);
                break;
            case Alexa.NotificationType.Tts:
                _alexa.TextToSpeech(mediaPlayer, notification.Message);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _lastPrompt[notification.EventId] = _scheduler.Now.LocalDateTime;
    }
}

public enum CycleState
{
    Running,
    Finished,
    Ready,
    Paused,
    Unknown
}