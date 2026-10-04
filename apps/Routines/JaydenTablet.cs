using Humanizer;
using Niemand.Helpers;
using System.Numerics;
using System.Reactive;
using System.Reactive.Subjects;
using System.Security.Cryptography;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class JaydenTablet
{
    private const string mediaPlayerId = "media_player.dining";
    private readonly IEntities _entities;
    private readonly IServices _services;
    private readonly IScheduler _scheduler;
    private readonly ILogger<JaydenTablet> _logger;
    private AlexaPromptPoller? _alexaPoller;
    private readonly string[] _reminderMessages = new[]
    {
        "Jayden, did you take your tablet today?",
        "Jayden, have you had your tablet yet?",
        "Jayden, don’t forget your tablet—have you taken it?",
        "Jayden, is your tablet taken?",
        "Jayden, quick check—did you take your tablet?"
    };

    private readonly string[] _responseMessages = new[]
    {
        "Please ensure you take it",
        "Don't forget to take it",
        "Make sure you take it",
        "Please take it",
        "Remember to take it"
    };

    public JaydenTablet(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, ILogger<JaydenTablet> logger)
    {
        _entities = entities;
        _services = services;
        _scheduler = scheduler;
        _logger = logger;

        CreateDailyResetTimer(5).Subscribe(_ => _entities.InputBoolean.JaydenTablet.TurnOff());

        // Create the poller (don't subscribe yet)
        _alexaPoller = new AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(entities.InputButton.TestRoutine.StateChanges())
            .AddTrigger(entities.BinarySensor.KonnectedKitchen.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.UtilityMotion.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.BackDoor.StateChanges().Where(e => e.New.IsOn()))
            .WhenPredicateTrue(_entities.InputBoolean.JaydenTablet.IsOff)
            .SetPrompt(new Alexa.Config()
            {
                Message = _reminderMessages[RandomNumberGenerator.GetInt32(_reminderMessages.Length)], //get random message from array
                Entity = mediaPlayerId,
                Whisper = false,
                VolumeLevel = GetVolumeLevel(),
                EventId = "jayden_tablet"
            })
            .WithCooldown(TimeSpan.FromMinutes(3))
            .WithDailyReset(CreateDailyResetTimer(6))
            .OnResponseYes(response =>
            {
                var person = string.Equals(response.ResponsePersonName, "UNKNOWN", StringComparison.OrdinalIgnoreCase) ? "" : response.ResponsePersonName;
                _logger.LogDebug("Tablet acknowledged by {Person}", person);
                _alexaPoller?.Acknowledge();
                _entities.InputBoolean.JaydenTablet.TurnOn();
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = $"Thank you {person}",
                    Entity = "media_player.dining",
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
                    Message = _responseMessages[RandomNumberGenerator.GetInt32(_responseMessages.Length)], //get random message from array
                    Entity = "media_player.dining",
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
        if (_entities.BinarySensor.UkSchoolTermsHampshirePoulnerJuniorSchoolDay.IsOff())
            return 0.3;

        if (DateTime.Now.Hour <= 5 || DateTime.Now.Hour >= 20)
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

    private IObservable<long> CreateDailyResetTimer(int hour)
    {
        return Observable.Timer(NextOccurrenceAtHour(_scheduler.Now, hour), TimeSpan.FromDays(1), _scheduler);
    }

    private DateTimeOffset NextOccurrenceAtHour(DateTimeOffset now, int hour)
    {
        var today = new DateTimeOffset(now.Year, now.Month, now.Day, hour, 0, 0, now.Offset);
        return now < today ? today : today.AddDays(1);
    }

}
