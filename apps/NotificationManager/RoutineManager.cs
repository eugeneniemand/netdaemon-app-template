using Niemand.Routines;
using Stateless;

namespace Niemand;

public class RoutineManager
{
    private readonly Alexa                                                                            _alexa;
    private readonly IScheduler                                                                       _scheduler;
    private readonly StateMachine<States, Triggers>                                                   _sm = new(States.None);
    private          StateMachine<States, Triggers>.TriggerWithParameters<NotificationResponseEvent>? _respondTrigger;
    private          IRoutine                                                                         _routine;
    private          IDisposable?                                                                     _scheduledAction;


    public RoutineManager(IScheduler scheduler, Alexa alexa)
    {
        _scheduler             =  scheduler;
        _alexa                 =  alexa;
        alexa.ResponseReceived += AlexaResponseReceived;
        ConfigureStateMachine();
    }

    public States State => _sm.State;

    public void Start(IRoutine routine)
    {
        _routine = routine ?? throw new ArgumentNullException(nameof(routine), "Routine is not initialized");
        _routine.MoveNext();
        _scheduledAction?.Dispose();
        _sm.Fire(Triggers.Notify);
    }

    private void AlexaResponseReceived(object? sender, NotificationResponseEvent e)
    {
        if (e.EventId?.Length == 8 && int.TryParse(e.EventId, out _))
            _sm.Fire(_respondTrigger!, e);
    }

    private void ConfigureStateMachine()
    {
        _respondTrigger = _sm.SetTriggerParameters<NotificationResponseEvent>(Triggers.Respond);

        _sm.Configure(States.None)
           .Permit(Triggers.Prompt, States.Responding)
           .PermitReentry(Triggers.Respond)
           .PermitReentry(Triggers.Notify)
           .OnEntry(Notify);

        _sm.Configure(States.No)
           .Permit(Triggers.Notify, States.None)
           .OnEntry(Notify);

        _sm.Configure(States.Yes)
           .Permit(Triggers.Notify, States.None)
           .OnEntry(Notify);

        _sm.Configure(States.Responding)
           .Permit(Triggers.RespondYes, States.Yes)
           .Permit(Triggers.RespondNo, States.No)
           .Permit(Triggers.RespondNone, States.None)
           .Permit(Triggers.Notify, States.None)
           .PermitReentry(Triggers.Respond)
           .OnEntryFrom(_respondTrigger, entryAction =>
           {
               //_logger.LogInformation("Trigger: {trigger}, State: {state}, Prompt: {prompt}", Triggers.Response, _sm.State, entryAction.Response);
               _sm.Fire(entryAction.Response switch
               {
                   "ResponseYes" => Triggers.RespondYes,
                   "ResponseNo"  => Triggers.RespondNo,
                   _             => Triggers.RespondNone
               });
           });
    }

    private void Notify()
    {
        switch (_routine.CurrentNotification)
        {
            case Prompt prompt:
            {
                switch (_sm.State)
                {
                    case States.None:
                        _alexa.SendNotification(prompt);
                        _sm.Fire(Triggers.Prompt);
                        break;
                    case States.No:
                        _alexa.SendNotification(prompt.NoResponse);
                        if (prompt.NoResponse.Timeout != TimeSpan.Zero)
                        {
                            _scheduledAction = _scheduler.Schedule(prompt.NoResponse.Timeout, () => _sm.Fire(Triggers.Notify));
                        }
                        else
                        {
                            _routine.MoveNext();
                            _sm.Fire(Triggers.Notify);
                        }

                        break;
                    case States.Yes:
                        _alexa.SendNotification(prompt.YesResponse);
                        _scheduledAction = _scheduler.Schedule(prompt.YesResponse.Timeout, () =>
                        {
                            _routine.MoveNext();
                            _sm.Fire(Triggers.Notify);
                        });
                        break;
                }

                break;
            }
            case Announcement announcement:
            {
                _alexa.SendNotification(announcement);
                _scheduledAction = _scheduler.Schedule(announcement.Timeout, () =>
                {
                    _routine.MoveNext();
                    _sm.Fire(Triggers.Notify);
                });
                break;
            }
            case TextToSpeech announcement:
            {
                _alexa.SendNotification(announcement);
                _scheduledAction = _scheduler.Schedule(announcement.Timeout, () =>
                {
                    _routine.MoveNext();
                    _sm.Fire(Triggers.Notify);
                });
                break;
            }
        }
    }
}