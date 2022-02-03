using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;

namespace Ha.Daemons;

public class Rain : INotificationDaemon
{
    private Entities _entities;

    public NotificationConfig Config { get; private set; }

    public event EventHandler<NotificationEventArgs> NotificationRaised;

    public Rain(IHaContext ha)
    {
        _entities = new Entities(ha);

        Config = new NotificationConfig(
            NotificationEnum.Rain,
            "Take an umbrella<break />Rain in {0} minutes. Max intensity is {1}",
            new List<MediaPlayerEntity> { _entities.MediaPlayer.Dining });
    }

    private Dictionary<DateTime, double> ParseRadarData()
    {
        var timeSlotsString = _entities.Sensor.NeerslagBuienradarRegenData.Attributes?.Data.ToString().Split(" ").Take(24);
        var time            = DateTime.Now;
        return ( timeSlotsString ?? Array.Empty<string>() ).Select(slot => slot.Split('|')).ToDictionary(_ => time += TimeSpan.FromMinutes(5), forecast => Convert.ToDouble(forecast[0].Replace(',', '.')));
    }

    public async Task Initialise()
    {
        _entities
            .BinarySensor.FrontDoor
            .StateChanges()
            .Where(s => s.Old.IsOff() && s.New.IsOn())
            .Subscribe(change => { RaiseNotification(); });
    }

    private void RaiseNotification()
    {
        var radarData = ParseRadarData();
        if (!radarData.Any(f => f.Value is > 0 and < 100)) return;
        {
            var minutes   = radarData.Values.ToList().FindIndex(f => f > 0);
            var intensity = Math.Round(Math.Pow(10, ( radarData.Values.Max() - 109 ) / 32), 1);
            NotificationRaised.Invoke(this, new NotificationEventArgs(Config, minutes * 5, intensity));
        }
    }
}