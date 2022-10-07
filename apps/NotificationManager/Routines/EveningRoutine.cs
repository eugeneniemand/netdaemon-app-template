namespace Niemand.Routines;

public class EveningRoutine : IRoutine
{
    private readonly IEntities _entities;
    private readonly ILogger   _logger;
    private readonly TimeSpan  _minutes1  = TimeSpan.FromMinutes(1);
    private readonly TimeSpan  _minutes10 = TimeSpan.FromMinutes(10);
    private readonly TimeSpan  _minutes2  = TimeSpan.FromMinutes(2);
    private readonly TimeSpan  _minutes5  = TimeSpan.FromMinutes(5);
    private readonly TimeSpan  _seconds10 = TimeSpan.FromSeconds(10);

    public EveningRoutine(IEntities entities, ILogger logger)
    {
        _entities = entities;
        _logger   = logger;
    }

    private List<Notification> RoutineSteps { get; set; } = new();

    public Notification? CurrentNotification => RoutineSteps.FirstOrDefault();

    public bool MoveNext()
    {
        if (RoutineSteps.Any())
        {
            _logger.LogInformation("Force Next Step");
            RoutineSteps.RemoveAt(0);
            return true;
        }

        _logger.LogInformation("Init Morning Routine");
        _entities.MediaPlayer.Everywhere2.VolumeSet(0.7);
        _entities.MediaPlayer.Playroom.VolumeSet(0.5);

        RoutineSteps = new List<Notification>
        {
            //new NotificationAnnouncement("Good evening boys, your evening routine will start in, 5 minutes", _minutes5, _entities.MediaPlayer.Everywhere2),
            //new NotificationAnnouncement("Good evening boys, your evening routine will start in, 5 minutes", _minutes5, _entities.MediaPlayer.LoungeSonos),
            new Announcement("Boys, its time to start your evening routine", _seconds10, _entities.MediaPlayer.Everywhere2),
            //new NotificationAnnouncement("Boys, its time to start your evening routine", _seconds10, _entities.MediaPlayer.Dining),
            //new NotificationAnnouncement("Boys, please go brush your teeth. Next time I speak to you it will be on the Playroom Alexa", _minutes5, _entities.MediaPlayer.Everywhere2),
            new Announcement("Boys, please go shower. Next time I speak to you it will be on the Playroom Alexa", _minutes5, _entities.MediaPlayer.Dining),
            new Prompt("Have you finished your shower", new TextToSpeech("Excellent", _seconds10, _entities.MediaPlayer.Playroom), new TextToSpeech("Please do it now, I will check again in 5 minutes", _minutes5, _entities.MediaPlayer.Playroom), _entities.MediaPlayer.Playroom),
            new Announcement("Boys, please go brush your teeth. Next time I speak to you it will be on the Playroom Alexa", _minutes5, _entities.MediaPlayer.Dining),
            new Prompt("Have you brushed your teeth yet", new TextToSpeech("Excellent", _seconds10, _entities.MediaPlayer.Playroom), new TextToSpeech("Please do it now, I will check again in 5 minutes", _minutes5, _entities.MediaPlayer.Playroom), _entities.MediaPlayer.Playroom),
            new Announcement("Boys, please get your pajamas now. Next time I speak to you it will be on the Playroom Alexa", _minutes5, _entities.MediaPlayer.Upstairs),
            new Prompt("Boys, do you have your pajamas", new TextToSpeech("Good Job", _seconds10, _entities.MediaPlayer.Playroom), new TextToSpeech("Please do it now, I will check again in 10 minutes", _minutes10, _entities.MediaPlayer.Playroom), _entities.MediaPlayer.Playroom),
            new Announcement("Boys, well done for your hard work and completing your evening routine", _seconds10, _entities.MediaPlayer.Upstairs),
            new Announcement("Boys, now you can read a book and get to bed", _seconds10, _entities.MediaPlayer.Upstairs)
        };
        return false;
    }
}