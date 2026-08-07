using Niemand.Helpers;
using System.Reactive;
using System.Reactive.Subjects;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class HouseModePrompts
{
    
    private readonly IEntities _entities;
    private readonly IServices _services;
    private readonly IScheduler _scheduler;
    private readonly ILogger<HouseModePrompts> _logger;
    private AlexaPromptPoller? _morningAlexaPoller;
    private AlexaPromptPoller _nightAlexaPoller;

    public HouseModePrompts(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, ILogger<HouseModePrompts> logger)
    {
        _entities = entities;
        _services = services;
        _scheduler = scheduler;
        _logger = logger;
        SetupMorningPoller(entities, alexa, scheduler, logger);
        SetupNightPoller(entities, alexa, scheduler, logger);
        var midnight = DateTimeOffset.Now.Date.AddMinutes(5);
        scheduler.RunEvery(TimeSpan.FromHours(1), midnight, () =>
        {
            alexa.Prompt(new Alexa.Config() { Entity = "media_player.playroom", Message = "healthcheck", EventId = "healthcheck", VolumeLevel = 0.0 });
        });
    }

    private void SetupMorningPoller(IEntities entities, IAlexa alexa, IScheduler scheduler, ILogger<HouseModePrompts> logger)
    {
        // Create morning poller
        _morningAlexaPoller = new AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(entities.BinarySensor.KonnectedKitchen.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges().Where(e => e.New.IsOn()))
            .AddTrigger(entities.BinarySensor.UtilityMotion.StateChanges().Where(e => e.New.IsOn()))
            .WhenPredicateTrue(() => _entities.InputSelect.HouseMode.State != "day" && _entities.Sun.Sun.IsAboveHorizon() )
            .SetPrompt(new Alexa.Config()
            {
                Message = "Good Morning, would you like to change house mode to Day Mode?",
                Entity = "media_player.dining",
                Whisper = false,
                EventId = "house_mode_day_prompt"
            })
            .WithCooldown(TimeSpan.FromMinutes(30))
            .WithDailyReset(CreateDailyResetTimer(5))
            .OnResponseYes(response =>
            {
                var person = string.Equals(response.ResponsePersonName, "UNKNOWN", StringComparison.OrdinalIgnoreCase) ? "" : response.ResponsePersonName;
                _logger.LogDebug("House mode set by {Person}", person);
                _morningAlexaPoller?.Acknowledge();
                entities.InputSelect.HouseMode.SelectOption("day");
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = $"{person} house mode is now in day mode",
                    Entity = "media_player.dining",
                    Whisper = false,
                    EventId = "house_mode_day_prompt"
                });
            })
            .OnResponseNotYes(response =>
            {
                _logger.LogDebug("Not acknowledged: {ResponseType} from {Person}", response.ResponseType, response.ResponsePersonName);
            });

        // Now subscribe
        _morningAlexaPoller.Subscribe();
    }

    private void SetupNightPoller(IEntities entities, IAlexa alexa, IScheduler scheduler, ILogger<HouseModePrompts> logger)
    {
        // Create night poller
        _nightAlexaPoller = new AlexaPromptPoller(scheduler, alexa, logger)
            .AddTrigger(entities.BinarySensor.MasterMotion.StateChanges().Where(e => e.New.IsOn()))            
            .WhenPredicateTrue(() => _entities.InputSelect.HouseMode.State != "night" && _entities.Sun.Sun.IsBelowHorizon())
            .SetPrompt(new Alexa.Config()
            {
                Message = "Good Evening, would you like to change house mode to Night Mode?",
                Entity = "media_player.master",
                Whisper = false,
                EventId = "house_mode_night_prompt"
            })
            .WithCooldown(TimeSpan.FromMinutes(30))
            .WithDailyReset(CreateDailyResetTimer(17))
            .OnResponseYes(response =>
            {
                var person = string.Equals(response.ResponsePersonName, "UNKNOWN", StringComparison.OrdinalIgnoreCase) ? "" : response.ResponsePersonName;
                _logger.LogDebug("House mode set by {Person}", person);
                _nightAlexaPoller?.Acknowledge();
                entities.InputSelect.HouseMode.SelectOption("night");
                alexa.TextToSpeech(new Alexa.Config()
                {
                    Message = $"{person} house mode is now in night mode",
                    Entity = "media_player.master",
                    Whisper = false,
                    EventId = "house_mode_night_prompt"
                });
            })
            .OnResponseNotYes(response =>
            {
                _logger.LogDebug("Not acknowledged: {ResponseType} from {Person}", response.ResponseType, response.ResponsePersonName);
            });

        // Now subscribe
        _nightAlexaPoller.Subscribe();
    }

    private IObservable<long> CreateDailyResetTimer(int hour)
    {
        return Observable.Timer(NextOccurrenceAtHour(_scheduler.Now, hour), TimeSpan.FromDays(1), _scheduler).StartWith(0L);
    }

    private DateTimeOffset NextOccurrenceAtHour(DateTimeOffset now, int hour)
    {
        var today = new DateTimeOffset(now.Year, now.Month, now.Day, hour, 0, 0, now.Offset);
        return now < today ? today : today.AddDays(1);
    }

}
