using Niemand.Helpers;
using System.Reactive;
using System.Reactive.Subjects;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class JaydenTablet
{
    private const string _mediaPlayer = "media_player.dining";
    private readonly IEntities _entities;
    private readonly IServices _services;
    private readonly IScheduler _scheduler;
    private readonly ILogger<JaydenTablet> _logger;
    private AlexaPromptPoller? _alexaPoller;

    public JaydenTablet(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, ILogger<JaydenTablet> logger)
    {
        _entities = entities;
        _services = services;
        _scheduler = scheduler;
        _logger = logger;

        // Create the poller (don't subscribe yet)
        _alexaPoller = new AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(entities.BinarySensor.KonnectedKitchen.StateChanges())
            .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges())
            .SetPrompt("Has Jayden taken his tablet?", "jayden_tablet")
            .SetMediaPlayer(_mediaPlayer)
            .WithCooldown(TimeSpan.FromMinutes(1))
            .WithDailyReset(Observable.Timer(
                Next6am(_scheduler.Now), 
                TimeSpan.FromDays(1)
                , _scheduler
                ).StartWith(0L))
            .OnResponseYes(response =>
            {
                var person = string.Equals(response.ResponsePersonName, "UNKNOWN", StringComparison.OrdinalIgnoreCase) ? "" : response.ResponsePersonName;
                _logger.LogDebug("Tablet acknowledged by {Person}", person);
                _alexaPoller?.Acknowledge();
                alexa.TextToSpeech(_mediaPlayer, $"Thank you {person}");
            })
            .OnResponseNotYes(response =>
            {
                _logger.LogDebug("Not acknowledged: {ResponseType} from {Person}", response.ResponseType, response.ResponsePersonName);
                alexa.TextToSpeech(_mediaPlayer, $"Please ensure he takes it");
            });

        // Now subscribe
        _alexaPoller.Subscribe();
    }

    private DateTimeOffset Next6am(DateTimeOffset now)
    {
        var todaySix = new DateTimeOffset(now.Year, now.Month, now.Day, 6, 0, 0, now.Offset);
        return now < todaySix ? todaySix : todaySix.AddDays(1);
    }

}
