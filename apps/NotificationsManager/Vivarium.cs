using NetDaemon.Helpers;
using Niemand.Helpers;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class Vivarium
{
    private const string MediaPlayer = "media_player.playroom";
    private readonly ILogger<Postbox> _logger;
    private AlexaPromptPoller _poller;

    public Vivarium(IHaContext haContext, IEntities entities, IScheduler scheduler, IAlexa alexa, ILogger<Postbox> logger)
    {
        logger.LogDebug("Subscribing to vivarium Alexa prompt poller");
        _logger = logger;

        _poller = new AlexaPromptPoller(scheduler, alexa, logger)
            //// Vivarium sets a stateful flag that persists until acknowledged
            .AddStatefulTrigger(
                entities.BinarySensor.GarageBackDoor.StateChanges(),
                change => change.New.IsOn()
            )
            // Landing motion is the trigger - fires only if vivarium flag is set
            .AddTrigger(entities.BinarySensor.KonnectedLanding.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.LandingMotion.StateChanges().Where(e => e.New.IsOn()))
            .SetPrompt(new Alexa.Config()
            {
                Message = "The vivarium has been opened,, is it closed and locked?",
                Entity = MediaPlayer,
                Whisper = false,
                VolumeLevel = 0.5,
                EventId = "vivarium_door"
            })
            .WithCooldown(TimeSpan.FromSeconds(60))            
            .OnResponseYes(response =>
            {
                var person = string.Equals(response.ResponsePersonName, "UNKNOWN", StringComparison.OrdinalIgnoreCase) ? "" : response.ResponsePersonName;
                _logger.LogDebug("vivarium acknowledged by {Person}", person);
                _poller.Acknowledge();
                _poller.ResetState();
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = $"Thank you {person}",
                    Entity = MediaPlayer,
                    Whisper = false,
                    VolumeLevel = 0.5,
                    EventId = "vivarium_door"
                });
            })
            .OnResponseNotYes(response =>
            {
                _logger.LogDebug("vivarium not acknowledged: {ResponseType} from {Person}", response.ResponseType, response.ResponsePersonName);
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = "Please ensure vivarium is closed and locked",
                    Entity = MediaPlayer,
                    Whisper = false,
                    VolumeLevel = 0.5,
                    EventId = "vivarium_door"
                });
            });

        _poller.Subscribe();
    }

    private DateTimeOffset Next6am(DateTimeOffset now)
    {
        var todaySix = new DateTimeOffset(now.Year, now.Month, now.Day, 6, 0, 0, now.Offset);
        return now < todaySix ? todaySix : todaySix.AddDays(1);
    }
}