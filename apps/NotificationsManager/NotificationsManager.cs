namespace Niemand.TestApp;

[NetDaemonApp]
[Focus]
public class NotificationsManager
{
    private const    string                        DishwasherDoneEventId = "DishwasherDone";
    private readonly IAlexa                        _alexa;
    private readonly IEntities                     _entities;
    private readonly IHaContext                    _haContext;
    private readonly IDictionary<string, DateTime> _lastPrompt = new Dictionary<string, DateTime>();
    private readonly IScheduler                    _scheduler;

    public NotificationsManager(IHaContext haContext, IEntities entities, IAlexa alexa, IScheduler scheduler)
    {
        _haContext = haContext;
        _entities  = entities;
        _alexa     = alexa;
        _scheduler = scheduler;

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
                    var reminders = new List<string>();
                    if (_entities.InputBoolean.DryerReminder.IsOn())
                        reminders.Add("Dryer");
                    if (_entities.InputBoolean.WashingReminder.IsOn())
                        reminders.Add("Washer");
                    if (_entities.InputBoolean.DryerReminder.IsOn())
                        reminders.Add("Dishwasher");

                    var message = string.Join(",", reminders) + " is ready but not turned on.";
                    _alexa.TextToSpeech(new Alexa.Config { Entities = new List<string> { "media_player.master", "media_player.office" }, Message = message });
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
            switch (s.New.State.ToLower())
            {
                case "run":
                    _entities.InputBoolean.DryerReminder.TurnOff();
                    break;
                case "stop":
                    _entities.InputBoolean.DryerAck.TurnOff();
                    break;
            }
        });

        _entities.Sensor.WashingMachineWasherMachineState.StateChanges().Subscribe(s =>
        {
            switch (s.New.State.ToLower())
            {
                case "run":
                    _entities.InputBoolean.WashingReminder.TurnOff();
                    break;
                case "stop":
                    _entities.InputBoolean.WasherAck.TurnOff();
                    break;
                case "pause":
                    break;
            }
        });

        _entities.Sensor.DishwasherOperationState.StateChanges().Subscribe(s =>
        {
            switch (s.New.State.ToLower())
            {
                case "run":
                    _entities.InputBoolean.DishwasherReminder.TurnOff();
                    break;
                case "finished":
                    _entities.InputBoolean.DishwasherAck.TurnOff();
                    break;
                case "ready":
                    break;
            }
        });

        _entities.BinarySensor.KitchenMotion.StateChanges()
                 .Where(s => s.Old.IsOff() && s.New.IsOn())
                 .Subscribe(s =>
                 {
                     if (_entities.InputBoolean.DishwasherAck.IsOff() && PromptLessThanMinutesAgo(DishwasherDoneEventId, 15)) return;

                     if (_entities.InputBoolean.DishwasherAck.IsOff() && !PromptLessThanMinutesAgo(DishwasherDoneEventId, 15))
                     {
                         _alexa.Prompt(_entities.MediaPlayer.Kitchen.EntityId, "The Dishwasher is done. Has it been unpacked?", DishwasherDoneEventId);
                         _lastPrompt[DishwasherDoneEventId] = DateTime.Now;
                     }

                     if (_entities.InputBoolean.DishwasherAck.IsOn() && !PromptLessThanMinutesAgo(DishwasherDoneEventId, 60))
                     {
                         _alexa.Announce(_entities.MediaPlayer.Kitchen.EntityId, "The Dishwasher is ready");
                         _lastPrompt[DishwasherDoneEventId] = DateTime.Now;
                     }
                 });

        _alexa.PromptResponses.Where(r => r.EventId == DishwasherDoneEventId).Subscribe(r =>
        {
            if (r.ResponseType == PromptResponseType.ResponseNo) _alexa.TextToSpeech(_entities.MediaPlayer.Kitchen.EntityId, "Ok");
            if (r.ResponseType == PromptResponseType.ResponseYes)
            {
                _entities.InputBoolean.DishwasherAck.TurnOn();
                _alexa.TextToSpeech(_entities.MediaPlayer.Kitchen.EntityId, "Thanks");
            }
        });
    }

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

    private bool PromptLessThanMinutesAgo(string eventId, int minutes) => _lastPrompt.ContainsKey(eventId) && ( DateTime.Now - _lastPrompt[eventId] ).TotalMinutes <= minutes;
}