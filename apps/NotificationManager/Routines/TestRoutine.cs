namespace Niemand.Routines;

public class TestRoutine : IRoutine
{
    private readonly IEntities _entities;
    private readonly ILogger   _logger;
    private readonly TimeSpan  _minutes1  = TimeSpan.FromSeconds(10);
    private readonly TimeSpan  _minutes10 = TimeSpan.FromSeconds(10);
    private readonly TimeSpan  _minutes2  = TimeSpan.FromSeconds(10);
    private readonly TimeSpan  _minutes5  = TimeSpan.FromSeconds(10);
    private readonly TimeSpan  _seconds10 = TimeSpan.FromSeconds(10);

    public TestRoutine(IEntities entities, ILogger logger)
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
        _entities.MediaPlayer.Office.VolumeSet(0.4);


        RoutineSteps = new List<Notification>
        {
            new Announcement("Test Announcement", _minutes5, _entities.MediaPlayer.Office, Volume.N4),
            new Prompt("Please Respond", new TextToSpeech("Thanks", _seconds10, _entities.MediaPlayer.Office, Volume.W5), new TextToSpeech("Try Again", _minutes10, _entities.MediaPlayer.Office), _entities.MediaPlayer.Office)
        };
        return false;
    }
}