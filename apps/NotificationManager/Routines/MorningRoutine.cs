namespace Niemand.Routines;

public class MorningRoutine : IRoutine
{
    private readonly IEntities _entities;
    private readonly ILogger   _logger;
    private readonly TimeSpan  _minutes1  = TimeSpan.FromMinutes(1);
    private readonly TimeSpan  _minutes10 = TimeSpan.FromMinutes(10);
    private readonly TimeSpan  _minutes2  = TimeSpan.FromMinutes(2);
    private readonly TimeSpan  _minutes5  = TimeSpan.FromMinutes(5);
    private readonly TimeSpan  _seconds10 = TimeSpan.FromSeconds(10);

    public MorningRoutine(IEntities entities, ILogger logger)
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
        _entities.MediaPlayer.Downstairs.VolumeSet(0.5);
        _entities.MediaPlayer.Playroom.VolumeSet(0.5);

        RoutineSteps = new List<Notification>
        {
            new Announcement("Good morning boys, your morning routine will start in, 5 minutes", _minutes5, _entities.MediaPlayer.Downstairs),
            new Announcement("Boys, its time to start your morning routine", _seconds10, _entities.MediaPlayer.Downstairs),
            new Announcement("Boys, please get your breakfast now. The next instruction will come from the Dining Room", _minutes5, _entities.MediaPlayer.Downstairs),
            new Prompt("Did you get your breakfast", new TextToSpeech("Enjoy, I'll check if you have finished eating in 10 minutes", _minutes10, _entities.MediaPlayer.Dining), new TextToSpeech("Please do it now, I will check again in 1 minute", _minutes1, _entities.MediaPlayer.Dining), _entities.MediaPlayer.Dining),
            new Prompt("Boys are you done eating breakfast", new TextToSpeech("Well done, wait for next instruction", _seconds10, _entities.MediaPlayer.Dining), new TextToSpeech("Ok, I will check again in 2 minutes", _minutes2, _entities.MediaPlayer.Dining), _entities.MediaPlayer.Dining),
            new Announcement("Boys, please go brush your teeth and hair. Next time I speak to you it will be on the Playroom Alexa", _minutes5, _entities.MediaPlayer.Downstairs),
            new Prompt("Have you brushed your teeth and hair yet", new TextToSpeech("Excellent", _seconds10, _entities.MediaPlayer.Playroom), new TextToSpeech("Please do it now, I will check again in 5 minutes", _minutes5, _entities.MediaPlayer.Playroom), _entities.MediaPlayer.Playroom),
            new Announcement("Boys, please get dressed now. Next time I speak to you it will be on the Playroom Alexa", _minutes5, _entities.MediaPlayer.Upstairs),
            new Prompt("Boys, are you dressed", new TextToSpeech("Good Job", _seconds10, _entities.MediaPlayer.Playroom), new TextToSpeech("Please do it now, I will check again in 10 minutes", _minutes10, _entities.MediaPlayer.Playroom), _entities.MediaPlayer.Playroom),
            new Announcement("Boys, well done for your hard work and completing your morning routine", _seconds10, _entities.MediaPlayer.Upstairs),
            new Announcement("Boys, now you can do what you want to do", _seconds10, _entities.MediaPlayer.Everywhere2)
        };
        return false;
    }
}