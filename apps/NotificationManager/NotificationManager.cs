using Niemand.Routines;

namespace Niemand;

[NetDaemonApp]
public class NotificationManager
{
    public NotificationManager(IHaContext haContext, IScheduler scheduler, ILogger<NotificationManager> logger, IEntities entities, Alexa alexa)
    {
        var rm             = new RoutineManager(scheduler, alexa);
        var morningRoutine = new MorningRoutine(entities, logger);
        var testRoutine    = new TestRoutine(entities, logger);
        var eveningRoutine = new EveningRoutine(entities, logger);
        var kidsOutOfBed   = new KidsOutOfBed(entities, logger);

        haContext.Entity("input_button.jaydenmorningroutine")
                 .StateChanges()
                 .Subscribe(change => rm.Start(morningRoutine));

        haContext.Entity("input_button.test_routine")
                 .StateChanges()
                 .Subscribe(change => rm.Start(testRoutine));

        haContext.Entity("input_button.evening_routine")
                 .StateChanges()
                 .Subscribe(change => rm.Start(eveningRoutine));

        haContext.Events
                 .Where(e => e.EventType == "kids_out_of_bed_event")
                 .Subscribe(change => rm.Start(kidsOutOfBed));
    }
}