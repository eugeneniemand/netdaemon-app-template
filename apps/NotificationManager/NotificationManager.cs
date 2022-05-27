using System;
using System.Reactive.Concurrency;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using NetDaemon.AppModel;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel;
using Niemand.Routines;
using Stateless;

namespace Niemand;

[Focus]
[NetDaemonApp]
public class NotificationManager : IAsyncInitializable, IDisposable
{
    private readonly Entities                                                                         _entities;
    private readonly IMqttEntityManager                                                               _entityManager;
    private readonly IHaContext                                                                       _haContext;
    private readonly ILogger<DisciplineManager>                                                       _logger;
    private readonly NotificationStateMachine                                                         _nsm = new();
    private readonly IScheduler                                                                       _scheduler;
    private readonly Services                                                                         _services;
    private readonly StateMachine<States, Triggers>                                                   _sm = new(States.None);
    private          StateMachine<States, Triggers>.TriggerWithParameters<NotificationResponseEvent>? _responseTrigger;
    private          IRoutine                                                                         _routine;
    private          IDisposable                                                                      _scheduledAction;


    public NotificationManager(IHaContext haContext, IScheduler scheduler, IMqttEntityManager entityManager, ILogger<DisciplineManager> logger)
    {
        _haContext     = haContext;
        _scheduler     = scheduler;
        _entityManager = entityManager;
        _logger        = logger;
        _entities      = new Entities(haContext);
        _services      = new Services(haContext);

        ConfigureStateMachine();
    }


    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        _haContext.Entity("input_button.jaydenmorningroutine")
                  .StateChanges()
                  .Subscribe(change => StartRoutine(new MorningRoutine(_entities, _logger)));

        _haContext.Events.Filter<NotificationResponseEvent>("alexa_actionable_notification").Subscribe(eventData =>
        {
            _logger.LogInformation("Event(alexa_actionable_notification): {EventId} - {Response} - {ResponseType}", eventData.Data?.EventId, eventData.Data?.Response, eventData.Data?.ResponseType);
            _sm.Fire(_responseTrigger!, eventData.Data);
        });
    }

    public void Dispose()
    {
    }


    private void ConfigureStateMachine()
    {
        _responseTrigger = _sm.SetTriggerParameters<NotificationResponseEvent>(Triggers.Response);

        _sm.Configure(States.None)
           .Permit(Triggers.Prompt, States.Responding)
           .PermitReentry(Triggers.Response)
           .PermitReentry(Triggers.Notify)
           .OnEntry(Notify);

        _sm.Configure(States.No)
           .Permit(Triggers.Notify, States.None)
           .OnEntry(Notify);

        _sm.Configure(States.Yes)
           .Permit(Triggers.Notify, States.None)
           .OnEntry(Notify);

        _sm.Configure(States.Responding)
           .Permit(Triggers.ResponseYes, States.Yes)
           .Permit(Triggers.ResponseNo, States.No)
           .Permit(Triggers.ResponseNone, States.None)
           .PermitReentry(Triggers.Response)
           .OnEntryFrom(_responseTrigger, entryAction =>
           {
               _logger.LogInformation("Trigger: {trigger}, State: {state}, Prompt: {prompt}", Triggers.Response, _sm.State, entryAction.Response);
               _sm.Fire(entryAction.Response switch
               {
                   "ResponseYes"  => Triggers.ResponseYes,
                   "ResponseNone" => Triggers.ResponseNone,
                   _              => Triggers.ResponseNo
               });
           });
    }

    private static string GetAnnouncementSSML(Notification prompt)
    {
        var announcement = prompt.Announcement
                                 .Replace(",", "<break />")
                                 .Replace(".", "<break />");
        return $"<voice name='Emma'>{announcement}</voice>";
    }

    private void Notify()
    {
        switch (_routine.CurrentNotification)
        {
            case NotificationPrompt prompt:
            {
                switch (_sm.State)
                {
                    case States.None:
                        SendNotification(prompt);
                        _sm.Fire(Triggers.Prompt);
                        break;
                    case States.No:
                        SendNotification(prompt.NoResponse);
                        _scheduledAction = _scheduler.Schedule(prompt.NoResponse.Timeout, () => _sm.Fire(Triggers.Notify));
                        break;
                    case States.Yes:
                        SendNotification(prompt.YesResponse);
                        _scheduledAction = _scheduler.Schedule(prompt.YesResponse.Timeout, () =>
                        {
                            _routine.MoveNext();
                            _sm.Fire(Triggers.Notify);
                        });
                        break;
                }

                break;
            }
            case NotificationAnnouncement announcement:
            {
                SendNotification(announcement);
                _scheduledAction = _scheduler.Schedule(announcement.Timeout, () =>
                {
                    _routine.MoveNext();
                    _sm.Fire(Triggers.Notify);
                });
                break;
            }
        }
    }

    private void SendNotification(NotificationPrompt prompt)
    {
        _logger.LogInformation("State: {state}, Prompt: {prompt}", _sm.State, prompt.Announcement);
        _services.Script.ActivateAlexaActionableNotification(GetAnnouncementSSML(prompt), "notification_response", prompt.Target.EntityId);
    }

    private void SendNotification(NotificationAnnouncement prompt)
    {
        _logger.LogInformation("State: {state}, Prompt: {prompt}", _sm.State, prompt.Announcement);
        _services.Notify.AlexaMedia(GetAnnouncementSSML(prompt), null, prompt.Target.EntityId, new { type = "announce" });
    }

    private void StartRoutine(IRoutine routine)
    {
        _routine = routine;
        _logger.LogInformation("Starting Routine");

        if (_routine.MoveNext()) _scheduledAction.Dispose();
        _sm.Fire(Triggers.Notify);
    }
}

public enum Triggers
{
    Notify,
    Response,
    ResponseYes,
    ResponseNo,
    Prompt,
    ResponseNone
}

internal enum States
{
    None,
    Responding,
    Yes,
    No
}

internal record NotificationResponseEvent
{
    [JsonPropertyName("event_id")] public string? EventId { get; init; }

    [JsonPropertyName("event_response")] public string? Response { get; init; }

    [JsonPropertyName("event_response_type")]
    public string? ResponseType { get; init; }
}

public record Notification(string Announcement, MediaPlayerEntity Target)
{
    public bool Complete { get; set; }
}

public record NotificationAnnouncement(string Announcement, TimeSpan Timeout, MediaPlayerEntity Target) : Notification(Announcement, Target);

public record NotificationPrompt(string Announcement, MediaPlayerEntity Target, NotificationAnnouncement YesResponse, NotificationAnnouncement NoResponse) : Notification(Announcement, Target);