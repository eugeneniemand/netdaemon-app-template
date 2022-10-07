namespace Niemand.Routines;

public class KidsOutOfBed : IRoutine
{
    private readonly IEntities _entities;
    private readonly ILogger   _logger;

    public KidsOutOfBed(IEntities entities, ILogger logger)
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

        var target      = _entities.MediaPlayer.Master;
        var yesResponse = new TextToSpeech("Ok", TimeSpan.Zero, target, Volume.W1);
        var noResponse  = new TextToSpeech("Waiting", TimeSpan.FromMinutes(2), target, Volume.W1);
        RoutineSteps = new List<Notification>
        {
            new TextToSpeech("Boys are out of bed", TimeSpan.FromSeconds(10), target, Volume.W1),
            new Prompt("Are boys in bed", yesResponse, noResponse, target, TtsType.TextToSpeech, Volume.W1)
        };
        return false;
    }
}