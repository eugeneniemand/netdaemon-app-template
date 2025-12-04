using NetDaemon.Helpers;
using Niemand.Helpers;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class Postbox
{
    private const string MediaPlayer = "media_player.playroom";
    private readonly ILogger<Postbox> _logger;
    private AlexaPromptPoller _poller;

    public Postbox(IHaContext haContext, IEntities entities, IScheduler scheduler, IAlexa alexa, ILogger<Postbox> logger)
    {
        logger.LogDebug("Subscribing to Postbox Alexa prompt poller");
        _logger = logger;

        _poller = new AlexaPromptPoller(scheduler, alexa, logger)
            //// Postbox vibration sets a stateful flag that persists until acknowledged
            .AddStatefulTrigger(
                entities.BinarySensor.Postbox.StateChanges(),
                change => change.New.IsOn()
            )
            // Hallway motion is the trigger - fires only if postbox flag is set
            .AddTrigger(entities.BinarySensor.KonnectedHallway.StateChanges().Where(e => e.New.IsOn()))
            .SetPrompt(new Alexa.Config()
            {
                Message = "You have mail in the postbox. Have you collected it?",
                Entity = MediaPlayer,
                Whisper = false,
                VolumeLevel = 0.5,
                EventId = "postbox_mail"
            })
            .WithCooldown(TimeSpan.FromSeconds(30))
            .WithDailyReset(Observable.Timer(
                Next6am(scheduler.Now),
                TimeSpan.FromDays(1)
                , scheduler
                ).StartWith(0L))
            .OnResponseYes(response =>
            {
                var person = string.Equals(response.ResponsePersonName, "UNKNOWN", StringComparison.OrdinalIgnoreCase) ? "" : response.ResponsePersonName;
                _logger.LogDebug("Postbox mail acknowledged by {Person}", person);
                _poller.Acknowledge();
                alexa.TextToSpeech(MediaPlayer, $"Thank you {person}");
            })
            .OnResponseNotYes(response =>
            {
                _logger.LogDebug("Postbox mail not acknowledged: {ResponseType} from {Person}", response.ResponseType, response.ResponsePersonName);
                alexa.TextToSpeech(MediaPlayer, $"Please remember to collect your mail");
            });

        _poller.Subscribe();
    }

    private DateTimeOffset Next6am(DateTimeOffset now)
    {
        var todaySix = new DateTimeOffset(now.Year, now.Month, now.Day, 6, 0, 0, now.Offset);
        return now < todaySix ? todaySix : todaySix.AddDays(1);
    }
}