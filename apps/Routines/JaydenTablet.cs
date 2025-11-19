


using Niemand.Helpers;
using System.Reactive;
using System.Reactive.Subjects;

namespace Niemand;

[NetDaemonApp]
[Focus]
public class JaydenTablet
{
    private const string _mediaPlayer = "media_player.dining";
    private readonly IEntities _entities;
    private readonly IServices _services;
    private readonly IScheduler _scheduler;
    private readonly ILogger<JaydenTablet> _logger;
    private readonly RgbwFrameBuilder fb;

    record ReminderState(DateTimeOffset? LastNotificationTime, bool ShouldNotify);

    public JaydenTablet(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, ILogger<JaydenTablet> logger)
    {
        _entities = entities;
        _services = services;
        _scheduler = scheduler;
        _logger = logger;

        TimeSpan reminderCooldown = TimeSpan.FromMinutes(1);
        Subject<Unit> acknowledgementStream = new();

        // 1. Daily tick at 6am
        var daily6am = Observable
            .Timer(Next6am(_scheduler.Now), TimeSpan.FromDays(1), _scheduler)
            .StartWith(0L);  // start "today" immediately as well

        // 2. For each day, create a fresh reminder stream
        IObservable<Unit> reminderStream =
            daily6am
                .Select(_ =>
                    entities.BinarySensor.KonnectedKitchen.StateChanges().Merge(entities.BinarySensor.KitchenMotion.StateChanges())
                    .Where(s => s.New.IsOn())
                    .Timestamp() // give each motion event a timestamp
                    .Scan(
                        // initial state
                        new ReminderState(LastNotificationTime: null, ShouldNotify: false),
                        (state, motion) =>
                        {
                            var now = motion.Timestamp;

                            DateTimeOffset lastNotification = state.LastNotificationTime ??  _scheduler.Now;
                            bool canNotify =
                                state.LastNotificationTime == null ||
                                (now - lastNotification) >= reminderCooldown;

                            // If we’re allowed, update LastNotificationTime and mark ShouldNotify
                            if (canNotify)
                            {
                                logger.LogDebug($"Can notify: Now: {now} LastNotificationTime: {lastNotification.ToString("G")}");
                                return new ReminderState(
                                    LastNotificationTime: now,
                                    ShouldNotify: true
                                );
                            }
                                
                            return new ReminderState(
                                LastNotificationTime: state.LastNotificationTime,
                                ShouldNotify: false
                            );
                        })
                    .Where(s => s.ShouldNotify)      // only keep “should notify” states
                    .Select(_ => Unit.Default)       // we only care that “a notification should fire”
                    .TakeUntil(acknowledgementStream) // 3. Stop this day's reminders when acknowledged
                 )
                // 4. Only the current day's inner stream is active
                .Switch();

        reminderStream.Subscribe(_ =>
        {
            var _mediaPlayer = JaydenTablet._mediaPlayer;
            logger.LogDebug("Create Prompt");
            alexa.Prompt(_mediaPlayer, "Has Jayden taken his tablet?", "jayden_tablet");            
        });

        alexa.PromptResponses
            .Where(r => r.EventId == "jayden_tablet")
            .Subscribe(x =>
            {
                logger.LogDebug($"Prompt Response {x.ResponseType} from {x.ResponsePersonName}");
                if (x.ResponsePersonName == "UNKNOWN")
                    alexa.TextToSpeech(_mediaPlayer, $"Please let parents answer");
                else
                {
                    if (x.ResponseType != PromptResponseType.ResponseYes)
                        alexa.TextToSpeech(_mediaPlayer, $"{x.ResponsePersonName}, please ensure he takes it");
                    else
                    {
                        logger.LogDebug("Prompt Ack");
                        acknowledgementStream.OnNext(Unit.Default);
                        alexa.TextToSpeech(_mediaPlayer, $"Thank you {x.ResponsePersonName}");
                        logger.LogDebug("acknowledgementStream event created");
                    }
                }
            });
    }

    private DateTimeOffset Next6am(DateTimeOffset now)
    {
        var todaySix = new DateTimeOffset(now.Year, now.Month, now.Day, 6, 0, 0, now.Offset);
        return now < todaySix ? todaySix : todaySix.AddDays(1);
    }

}
