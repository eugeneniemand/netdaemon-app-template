using HomeAssistantGenerated;
using Humanizer;
using Humanizer.Localisation;
using NetDaemon.Client;
using NetDaemon.Helpers;
using Niemand.Helpers;
using Niemand.HistoryReader;

namespace Niemand;

[NetDaemonApp]
[Focus]
public class KidsChores(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, IHomeAssistantApiManager apiManager) : IAsyncInitializable
{
    //private readonly IAlexa _alexa;
    //private readonly IEntities _entities;
    //private readonly IScheduler _scheduler;
    //private IDisposable? _alarmScheduler;
    TvUsageCalculator TvUsage;

    private async Task CalcScreentime()
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"); // UK time
        var now = DateTimeOffset.Now;
        var todayMidnight = new DateTimeOffset(DateTime.Today, timeZone.GetUtcOffset(DateTime.Today));
        string isoString = todayMidnight.ToString("yyyy-MM-ddTHH:mm:sszzz");

        var usageCalc = new TvUsageCalculator();
        var history = await apiManager.GetApiCallAsync<List<List<MediaPlayerHistory>>>($"history/period/{isoString}?filter_entity_id=media_player.lounge_tv", CancellationToken.None);
        usageCalc.CalculateUsage(history.First(), DateTime.Now);
        TvUsage = usageCalc;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        //entities.MediaPlayer.LoungeTv.StateAllChanges().SubscribeAsync(async s => {
        //    TvUsage = await CalcScreentime();
        //});
        //

        //alexa.Announce(new Alexa.Config() { Entities = ["media_player.downstairs", "media_player.lounge_tv", "media_player.master"], Message = "Aaron, Snake Food is overdue", VolumeLevel = 0.4, Whisper = false });

        scheduler.SchedulePeriodic(TimeSpan.FromMinutes(5), () =>
        {
            //CalcScreentime().GetAwaiter().GetResult();
            //if (TvUsage.TotalOnTime >= TimeSpan.FromMinutes(60))
            //{

            //}

            if (entities.MediaPlayer.LoungeTv.IsOff())
                return;

            var kc_statuses = entities.Sensor.EnumerateAll().Where(e => e.EntityId.Contains("chore_status"));
            var kc_overdue = kc_statuses.Where(e => e.EntityState.State == "overdue");
            if (kc_overdue.Any())
            {
                var kids = new List<string>() { "Jayden", "Aaron", "Gabriel" };
                var kc_summary = kc_overdue.Where(e => kids.Contains(e.Attributes.KidName)).CountBy(e => e?.Attributes?.KidName ?? "Unknown");
                var kc_messages = kc_summary.Select(kvp => $"{kvp.Key}");

                services.Notify.LoungeTv(new NotifyLoungeTvParameters() { Message = $"TV TURNING OFF!!! - {string.Join(" and ", kc_messages)}, you have overdue chores." });
                var tasks = new[]
                {
                        Task.Run(async () => await scheduler.Sleep(TimeSpan.FromSeconds(20)))
                    };
                Task.WaitAll(tasks);

                entities.MediaPlayer.LoungeTv.TurnOff();
            }
            
        });

        entities.InputButton.ChoreStatus.StateChanges().Subscribe(_ =>
        {
            try
            {
                var kc_statuses = entities.Sensor.EnumerateAll().Where(e => e.EntityId.Contains("chore_status"));
                var kc_morningRoutine = kc_statuses.Where(e => e.EntityState.State != "approved" && (e.Attributes?.Labels?.Any(label => string.Equals(label, "Morning Routine", StringComparison.OrdinalIgnoreCase)) ?? false));
                var kc_summary = kc_morningRoutine.CountBy(e => e.Attributes.KidName);
                var kc_messages = kc_summary.Select(kvp => $"{kvp.Key} has {kvp.Value}");

                foreach (var msg in kc_messages)
                {
                    alexa.Announce(new Alexa.Config() { Entities = ["media_player.downstairs", "media_player.master"], Message = msg, VolumeLevel = 0.3, Whisper = false });
                }

            }
            catch (Exception ex)
            {
                var e = ex;
            }
        });
    }

}

public class TvUsageCalculator
{
    public TimeSpan TotalOnTime { get; private set; } = TimeSpan.Zero;
    public Dictionary<string, TimeSpan> TimePerInput { get; private set; } = new();

    public void CalculateUsage(List<MediaPlayerHistory> history, DateTime endTime)
    {
        if (history == null || !history.Any()) return;

        history = history.OrderBy(h => h.last_changed).ToList();

        for (int i = 0; i < history.Count - 1; i++)
        {
            var current = history[i];
            var next = history[i + 1];
            var duration = next.last_changed - current.last_changed;

            if (current.state == "on")
            {
                TotalOnTime += duration;

                if (!string.IsNullOrEmpty(current.attributes.Source))
                {
                    string source = current.attributes.Source;

                    if (!TimePerInput.ContainsKey(source))
                        TimePerInput[source] = TimeSpan.Zero;

                    TimePerInput[source] += duration;
                }
            }
        }

        // Handle final record if it's still "on" up to now
        var last = history.Last();
        if (last.state == "on")
        {
            var duration = endTime - last.last_changed;
            TotalOnTime += duration;

            if (!string.IsNullOrEmpty(last.attributes.Source))
            {
                string source = last.attributes.Source;

                if (!TimePerInput.ContainsKey(source))
                    TimePerInput[source] = TimeSpan.Zero;

                TimePerInput[source] += duration;
            }
        }
    }
}
