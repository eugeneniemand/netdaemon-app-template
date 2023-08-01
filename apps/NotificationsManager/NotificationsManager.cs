namespace Niemand.TestApp;

[NetDaemonApp]
[Focus]
public class NotificationsManager
{
    private readonly IAlexa     _alexa;
    private readonly IEntities  _entities;
    private readonly IHaContext _haContext;
    private readonly IScheduler _scheduler;

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
                    _entities.InputBoolean.DryerAck.TurnOff();
                    break;
                case "stop":
                    break;
            }
        });

        _entities.Sensor.WashingMachineWasherMachineState.StateChanges().Subscribe(s =>
        {
            switch (s.New.State.ToLower())
            {
                case "run":
                    _entities.InputBoolean.WashingReminder.TurnOff();
                    _entities.InputBoolean.WasherAck.TurnOff();
                    break;
                case "stop":
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
                    break;
                case "ready":
                    break;
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
}