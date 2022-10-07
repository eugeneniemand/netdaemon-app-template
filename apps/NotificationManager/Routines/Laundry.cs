namespace Niemand.Routines;

public class Laundry : IRoutine
{
    private readonly IEntities _entities;
    private readonly ILogger   _logger;
    private          DateTime  _triggerTime;

    public Laundry(IEntities entities, ILogger logger)
    {
        _entities = entities;
        _logger   = logger;
    }

    private List<Notification> RoutineSteps { get; set; } = new();

    public Notification? CurrentNotification => RoutineSteps.FirstOrDefault();

    public bool MoveNext()
    {
        var target      = _entities.MediaPlayer.Kitchen;
        var yesResponse = new TextToSpeech("Thank you", TimeSpan.Zero, target);
        var hoursPhrase = ( DateTime.Now - _triggerTime ).Hours != 0 ? $"Its been in the machine for {( DateTime.Now - _triggerTime ).Hours}" : "Ok I'll check again later";
        var noResponse  = new TextToSpeech(hoursPhrase, TimeSpan.FromMinutes(2), target);

        if (RoutineSteps.Any())
        {
            _logger.LogInformation("Force Next Step");
            RoutineSteps.RemoveAt(0);
            return true;
        }

        _triggerTime = DateTime.Now;
        RoutineSteps = new List<Notification>
        {
            new Prompt("Your laundry is done, have you checked the machines?", yesResponse, noResponse, target, TtsType.TextToSpeech)
        };
        return false;
    }
}