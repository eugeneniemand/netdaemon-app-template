using Humanizer;
using Niemand.Helpers;
using System.Numerics;
using System.Reactive;
using System.Reactive.Subjects;
using System.Security.Cryptography;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class HaileyTablet
{
    private const string mediaPlayerId = "media_player.office";
    private readonly IEntities _entities;
    private readonly IServices _services;
    private readonly IScheduler _scheduler;
    private readonly ILogger<HaileyTablet> _logger;
    private AlexaPromptPoller? _alexaPoller;
    private readonly string[] _reminderMessages = new[]
    {
        "Hailey, did you take your tablet today?",
        "Hailey, have you had your tablet yet?",
        "Hailey, don’t forget your tablet—have you taken it?",
        "Hailey, is your tablet taken?",
        "Hailey, quick check—did you take your tablet?"
    };

    private readonly string[] _responseMessages = new[]
    {
        "Please ensure you take it",
        "Don't forget to take it",
        "Make sure you take it",
        "Please take it",
        "Remember to take it"
    };

    public HaileyTablet(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, ILogger<HaileyTablet> logger)
    {
        _entities = entities;
        _services = services;
        _scheduler = scheduler;
        _logger = logger;

        CreateDailyResetTimer(8).Subscribe(_ => _entities.InputBoolean.HaileyTablet.TurnOff());

        // Create the poller (don't subscribe yet)
        _alexaPoller = new AlexaPromptPoller(scheduler, alexa, logger)            
            .AddTrigger(entities.BinarySensor.KonnectedBackOffice.StateChanges().Where(e => e.New.IsOn()))
            .WhenPredicateTrue(_entities.InputBoolean.HaileyTablet.IsOff)
            .SetPrompt(new Alexa.Config()
            {
                Message = _reminderMessages[RandomNumberGenerator.GetInt32(_reminderMessages.Length)], //get random message from array
                Entity = mediaPlayerId,
                Whisper = false,
                VolumeLevel = GetVolumeLevel(),
                EventId = "hailey_tablet"
            })
            .WithCooldown(TimeSpan.FromMinutes(15))
            .WithDailyReset(CreateDailyResetTimer(8))
            .OnResponseYes(response =>
            {
                var person = string.Equals(response.ResponsePersonName, "UNKNOWN", StringComparison.OrdinalIgnoreCase) ? "" : response.ResponsePersonName;
                _logger.LogDebug("Tablet acknowledged by {Person}", person);
                _alexaPoller?.Acknowledge();
                _entities.InputBoolean.HaileyTablet.TurnOn();
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = $"Thank you {person}",
                    Entity = mediaPlayerId,
                    Whisper = false,
                    VolumeLevel = GetVolumeLevel(),
                    EventId = "hailey_tablet"
                });
            })
            .OnResponseNotYes(response =>
            {
                _logger.LogDebug("Not acknowledged: {ResponseType} from {Person}", response.ResponseType, response.ResponsePersonName);
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = _responseMessages[RandomNumberGenerator.GetInt32(_responseMessages.Length)], //get random message from array
                    Entity = mediaPlayerId,
                    Whisper = false,
                    VolumeLevel = GetVolumeLevel(),
                    EventId = "hailey_tablet"
                });
            });

        // Now subscribe
        _alexaPoller.Subscribe();
    }

    private double GetVolumeLevel()
    {        
        return 0.3;
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
