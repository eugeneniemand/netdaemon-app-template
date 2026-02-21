using Niemand.Helpers;
using System.Reactive;
using System.Reactive.Subjects;

namespace Niemand;

[NetDaemonApp]
[Focus]
public class JaydenTablet
{
    private const string _mediaPlayer = "media_player.office";
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
            .AddTrigger(entities.InputButton.TestRoutine.StateChanges())
            .AddTrigger(entities.BinarySensor.KonnectedKitchen.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.UtilityMotion.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.BackDoor.StateChanges().Where(e => e.New.IsOn()))
            .SetPrompt(new Alexa.Config()
            {
                Message = "Jayden, have you taken your tablet?",
                Entity = _mediaPlayer,
                Whisper = false,
                VolumeLevel = GetVolumeLevel(),
                EventId = "jayden_tablet"
            })
            .WithCooldown(TimeSpan.FromMinutes(3))
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
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = $"Thank you {person}",
                    Entity = _mediaPlayer,
                    Whisper = false,
                    VolumeLevel = GetVolumeLevel(),
                    EventId = "jayden_tablet"
                });
            })
            .OnResponseNotYes(response =>
            {
                _logger.LogDebug("Not acknowledged: {ResponseType} from {Person}", response.ResponseType, response.ResponsePersonName);
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = "Please ensure you take it",
                    Entity = _mediaPlayer,
                    Whisper = false,
                    VolumeLevel = GetVolumeLevel(),
                    EventId = "jayden_tablet"
                });
            });

        // Now subscribe
        _alexaPoller.Subscribe();
    }

    private double GetVolumeLevel()
    {
        if (DateTime.Now.Hour <= 5)
            return 0.2;

        if (DateTime.Now.Hour == 6 && DateTime.Now.Minute <= 30)
            return 0.3;

        if (DateTime.Now.Hour == 6 && DateTime.Now.Minute > 30)
            return 0.4;

        if (DateTime.Now.Hour == 7 && DateTime.Now.Minute <= 30)
            return 0.6;

        if (DateTime.Now.Hour >= 7 && DateTime.Now.Minute >= 30)
            return 0.7;

        return 0.5;
    }

    private DateTimeOffset Next6am(DateTimeOffset now)
    {
        var todaySix = new DateTimeOffset(now.Year, now.Month, now.Day, 6, 0, 0, now.Offset);
        return now < todaySix ? todaySix : todaySix.AddDays(1);
    }

}
