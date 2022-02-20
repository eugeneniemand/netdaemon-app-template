﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Reactive.Linq;
//using System.Text.Json;
//using System.Threading.Tasks;
//using HomeAssistantGenerated;
//using NetDaemon.Extensions.Scheduler;
//using NetDaemon.HassModel.Common;
//using NetDaemon.HassModel.Entities;

//namespace Ha.Daemons;

//public class Calendar : INotificationDaemon
//{
//    private readonly Entities            _entities;
//    private readonly INetDaemonScheduler _scheduler;

//    public Calendar(IHaContext ha, INetDaemonScheduler scheduler)
//    {
//        _scheduler = scheduler;
//        _entities  = new Entities(ha);

//        Config = new NotificationConfig(
//            NotificationEnum.Calendar,
//            "Reminder<break />{0}<break /> starting at {1}",
//            new List<MediaPlayerEntity> { _entities.MediaPlayer.Downstairs });
//    }

//    public NotificationConfig Config { get; }

//    public async Task Initialise()
//    {
//        var startTime = DateTimeOffset.Parse($"{DateTime.Now:yyyy-MM-dd HH:00:00}");
//        await Notify();
//        _scheduler.RunEvery(TimeSpan.FromMinutes(15), startTime, async () => await Notify());
//        _entities
//            .BinarySensor.FrontDoor
//            .StateChanges()
//            .Where(s => s.Old.IsOff() && s.New.IsOn())
//            .Subscribe(async change => { await Notify(); });
//    }

//    public event EventHandler<NotificationEventArgs> NotificationRaised;

//    private static async Task<List<CalendarEntry>> GetCalendarEntries()
//    {
//        var client = new HttpClient();
//        client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiI2Mzk3Njc4NGI3M2M0MzM5ODllNTFmNTkyYzI3MjJjMCIsImlhdCI6MTYxMzA4MzA4NSwiZXhwIjoxOTI4NDQzMDg1fQ.r1BzMO3_i5qlsYcX_njyqSSMwogpLK-zsgCZUrHQ3bs");
//        var httpResponseMessage = await client.GetAsync($"http://192.168.1.3:8123/api/calendars/calendar.home?start={DateTime.Today:yyyy-MM-dd}T00:00:00Z&end={DateTime.Today.AddDays(1):yyyy-MM-dd}T00:00:00Z");
//        var results             = await httpResponseMessage.Content.ReadAsStringAsync();
//        return JsonSerializer.Deserialize<List<CalendarEntry>>(results) ?? new List<CalendarEntry>();
//    }

//    private async Task Notify()
//    {
//        var nextEntry = ( await GetCalendarEntries() ).Where(c => c.start > DateTime.Now).OrderBy(c => c.start).FirstOrDefault();
//        if (nextEntry == null) return;
//        if (( nextEntry.start - DateTime.Now ).TotalMinutes is > 0 and <= 15) NotificationRaised.Invoke(this, new NotificationEventArgs(Config, nextEntry.summary, nextEntry.start.ToShortTimeString()));
//    }

//    private record CalendarEntry(
//        Guid uid
//        , string summary
//        , DateTime start
//        , DateTime end
//        , string location
//        , string description
//    );
//}