using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Niemand.Helpers.Notifications;

/// <summary>
/// Handles Home Assistant events for Alexa prompt responses.
/// Subscribes to alexa_actionable_notification events and publishes them as observable responses.
/// </summary>
public class PromptResponseHandler
{
    private static bool _eventSubscriptionInitialized = false;
    private static readonly object _lockObject = new object();
    private static Dictionary<string, AlexaPeopleConfig> _people = new();

    private readonly Subject<PromptResponse> _promptResponses;
    private readonly ILogger<PromptResponseHandler> _logger;

    public IObservable<PromptResponse> PromptResponses => _promptResponses;

    public PromptResponseHandler(
        Subject<PromptResponse> promptResponses,
        ILogger<PromptResponseHandler> logger)
    {
        _promptResponses = promptResponses;
        _logger = logger;
    }

    /// <summary>
    /// Sets up subscription to Home Assistant alexa_actionable_notification events.
    /// This method is thread-safe and only initializes the subscription once.
    /// </summary>
    public void SetupEventSubscription(IHaContext haContext, Dictionary<string, AlexaPeopleConfig> people)
    {
        // Ensure event subscription is only set up once, even if called from multiple instances
        lock (_lockObject)
        {
            if (_eventSubscriptionInitialized)
                return;

            _eventSubscriptionInitialized = true;
            _people = people;
        }

        haContext.Events.Filter<PromptResponseEvent>("alexa_actionable_notification")
            .Do(e => _logger.LogDebug("Received alexa_actionable_notification event {eventData}", e))
            .Select(e => ConvertEventToResponse(e.Data))
            .DistinctUntilChanged(e => new { e.EventId, e.ResponseType })
            .Do(e => _logger.LogDebug("Distinct PromptResponse {PromptResponse}", e))
            .Subscribe(responseEvent =>
                {
                    _logger.LogInformation(
                        "Event(alexa_actionable_notification): {EventId} - {Response} - {ResponseType} by {ResponsePersonId}",
                        responseEvent.EventId,
                        responseEvent.Response?.ToString(),
                        responseEvent.ResponseType,
                        responseEvent?.ResponsePersonId);

                    if (responseEvent == null) return;

                    _promptResponses.OnNext(responseEvent);
                });
    }

    /// <summary>
    /// Converts a Home Assistant event to a PromptResponse DTO.
    /// Looks up the person name from the configured people dictionary.
    /// </summary>
    private PromptResponse ConvertEventToResponse(PromptResponseEvent? eventData)
    {
        var personName = "UNKNOWN";
        if (eventData?.ResponsePersonId != null && _people.TryGetValue(eventData.ResponsePersonId, out var person))
        {
            personName = person.Name;
        }

        return new PromptResponse
        {
            EventId = eventData?.EventId ?? "",
            Response = eventData?.Response!,
            ResponsePersonId = eventData?.ResponsePersonId ?? "",
            ResponsePersonName = personName,
            ResponseType = eventData?.ResponseType ?? PromptResponseType.ResponseUnknown
        };
    }
}
