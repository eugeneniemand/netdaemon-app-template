using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Common;

namespace Niemand.Daemons;

public class Calendar : INotificationDaemon
{
    private INetDaemonScheduler _scheduler;
    private Entities            _entities;

    public Calendar(IHaContext ha, INetDaemonScheduler scheduler)
    {
        _scheduler = scheduler;
        _entities  = new Entities(ha);

        Config = new NotificationConfig(
            Notification.Calendar,
            "You have {0} <break /> starting at {1}",
            new List<MediaPlayerEntity> { _entities.MediaPlayer.Downstairs });
    }

    public NotificationConfig Config { get; private set; }

    public event EventHandler<NotificationEventArgs> NotificationRaised;

    private async Task RaiseNotification()
    {
        var nextEntry = ( await GetCalendarEntries() ).FirstOrDefault();
        if (nextEntry == null) return;
        if (( nextEntry.Start - DateTime.Now ).TotalMinutes is > 0 and <= 60) NotificationRaised.Invoke(this, new NotificationEventArgs(Config, nextEntry.Summary, nextEntry.Start.ToShortTimeString()));
    }

    private static async Task<List<CalendarEntry>> GetCalendarEntries()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiI2Mzk3Njc4NGI3M2M0MzM5ODllNTFmNTkyYzI3MjJjMCIsImlhdCI6MTYxMzA4MzA4NSwiZXhwIjoxOTI4NDQzMDg1fQ.r1BzMO3_i5qlsYcX_njyqSSMwogpLK-zsgCZUrHQ3bs");
        var httpResponseMessage = await client.GetAsync("http://192.168.1.3:8123/api/calendars/calendar.home?start=2021-12-08T00:00:00Z&end=2021-12-09T00:00:00Z");
        var results             = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<CalendarEntry>>(results) ?? new List<CalendarEntry>();
    }

    public async Task Initialise()
    {
        var startTime = DateTimeOffset.Parse($"{DateTime.Now.AddHours(1):yyyy-MM-dd HH:00:00}");
        await RaiseNotification();
        _scheduler.RunEvery(TimeSpan.FromHours(1), startTime, async () => await RaiseNotification());
    }

    private record CalendarEntry(
        Guid Uid
        , string Summary
        , DateTime Start
        , DateTime End
        , string Location
        , string Description
    );
}