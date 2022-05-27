using System;
using System.Collections.Generic;
using System.Linq;
using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;

namespace Niemand.Routines;

public class MorningRoutine : IRoutine
{
    private readonly Entities _entities;
    private readonly ILogger  _logger;
    private readonly TimeSpan minutes1  = TimeSpan.FromMinutes(1);
    private readonly TimeSpan minutes10 = TimeSpan.FromMinutes(10);
    private readonly TimeSpan minutes2  = TimeSpan.FromMinutes(2);

    private readonly TimeSpan minutes5 = TimeSpan.FromMinutes(5);
    //private readonly TimeSpan minutes1  = TimeSpan.FromSeconds(10);
    //private readonly TimeSpan minutes10 = TimeSpan.FromSeconds(10);
    //private readonly TimeSpan minutes2  = TimeSpan.FromSeconds(10);
    //private readonly TimeSpan minutes5  = TimeSpan.FromSeconds(10);
    //private readonly TimeSpan seconds10 = TimeSpan.FromSeconds(10);

    private readonly TimeSpan seconds10 = TimeSpan.FromSeconds(10);

    public MorningRoutine(Entities entities, ILogger logger)
    {
        _entities = entities;
        _logger   = logger;
    }

    private List<Notification> RoutineSteps { get; set; } = new();

    public Notification? CurrentNotification => RoutineSteps.FirstOrDefault();

    /// <summary>
    ///     Inits the routine steps and if triggered again will move to the next step
    /// </summary>
    /// <returns>true if moved to next step</returns>
    public bool MoveNext()
    {
        if (RoutineSteps.Any())
        {
            _logger.LogInformation("Force Next Step");
            RoutineSteps.RemoveAt(0);
            return true;
        }

        _logger.LogInformation("Init Morning Routine");
        _entities.MediaPlayer.Downstairs.VolumeSet(0.5);
        _entities.MediaPlayer.Playroom.VolumeSet(0.5);

        RoutineSteps = new List<Notification>
        {
            new NotificationAnnouncement("Good morning boys, your morning routine will start in, 5 minutes", minutes5, _entities.MediaPlayer.Downstairs),
            new NotificationAnnouncement("Boys, its time to start your morning routine", seconds10, _entities.MediaPlayer.Downstairs),
            new NotificationAnnouncement("Boys, please get your breakfast now. The next instruction will come from the Dining Room", minutes5, _entities.MediaPlayer.Downstairs),
            new NotificationPrompt("Did you get your breakfast", _entities.MediaPlayer.Dining, new NotificationAnnouncement("Enjoy, I'll check if you have finished eating in 10 minutes", minutes10, _entities.MediaPlayer.Dining), new NotificationAnnouncement("Please do it now, I will check again in 1 minute", minutes1, _entities.MediaPlayer.Dining)),
            new NotificationPrompt("Boys are you done eating breakfast", _entities.MediaPlayer.Dining, new NotificationAnnouncement("Well done, wait for next instruction", seconds10, _entities.MediaPlayer.Dining), new NotificationAnnouncement("Ok, I will check again in 2 minutes", minutes2, _entities.MediaPlayer.Dining)),
            new NotificationAnnouncement("Boys, please go brush your teeth and hair. Next time I speak to you it will be on the Playroom Alexa", minutes5, _entities.MediaPlayer.Downstairs),
            new NotificationPrompt("Have you brushed your teeth and hair yet", _entities.MediaPlayer.Playroom, new NotificationAnnouncement("Excellent", seconds10, _entities.MediaPlayer.Playroom), new NotificationAnnouncement("Please do it now, I will check again in 5 minutes", minutes5, _entities.MediaPlayer.Playroom)),
            new NotificationAnnouncement("Boys, please get dressed now. Next time I speak to you it will be on the Playroom Alexa", minutes5, _entities.MediaPlayer.Upstairs),
            new NotificationPrompt("Boys, are you dressed", _entities.MediaPlayer.Playroom, new NotificationAnnouncement("Good Job", seconds10, _entities.MediaPlayer.Office), new NotificationAnnouncement("Please do it now, I will check again in 10 minutes", minutes10, _entities.MediaPlayer.Office)),
            new NotificationAnnouncement("Boys, well done for your hard work and completing your morning routine", seconds10, _entities.MediaPlayer.Upstairs),
            new NotificationAnnouncement("Boys, now you can do what you want to do", seconds10, _entities.MediaPlayer.Everywhere2)
        };
        return false;
    }
}