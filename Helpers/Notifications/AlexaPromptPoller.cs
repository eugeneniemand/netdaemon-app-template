using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Subjects;
using Microsoft.Extensions.Logging;

namespace Niemand.Helpers;

/// <summary>
/// Generic Alexa prompt poller with cooldown management, multiple trigger support,
/// and response-type-specific action handlers.
/// 
/// Handles a state machine:
/// Idle ? Listening for Triggers ? WaitingForResponse (cooldown) ? Idle
/// 
/// Supports:
/// - Multiple observable triggers merged together
/// - Per-response-type action handlers
/// - Daily reset or event-triggered reset
/// - Structured logging throughout
/// </summary>
public class AlexaPromptPoller
{
    private readonly IScheduler _scheduler;
    private readonly IAlexa _alexa;
    private readonly ILogger _logger;

    private readonly List<IObservable<Unit>> _triggers = new();
    private string _mediaPlayer = "";
    private string _promptMessage = "";
    private string _eventId = "";
    private TimeSpan _cooldown = TimeSpan.FromMinutes(1);
    
    private readonly Dictionary<PromptResponseType, Action<PromptResponse>> _responseHandlers = new();
    private Action<PromptResponse>? _defaultResponseHandler;
    
    private IObservable<Unit>? _dailyResetTrigger;
    private Subject<Unit>? _externalResetTrigger;
    private Subject<Unit>? _acknowledgmentStream;
    private DateTimeOffset _lastPromptTime = DateTimeOffset.MinValue;
    private bool _isWaitingForResponse = false;
    private bool _isAcknowledged = false;

    // For managing disposables
    private readonly List<IDisposable> _subscriptions = new();

    public AlexaPromptPoller(IScheduler scheduler, IAlexa alexa, ILogger logger)
    {
        _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        _alexa = alexa ?? throw new ArgumentNullException(nameof(alexa));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Add a trigger observable. Can be called multiple times to add multiple triggers.
    /// They will be merged together.
    /// </summary>
    public AlexaPromptPoller AddTrigger(IObservable<Unit> trigger)
    {
        if (trigger == null) throw new ArgumentNullException(nameof(trigger));
        _triggers.Add(trigger);
        _logger.LogDebug("Added trigger to AlexaPromptPoller");
        return this;
    }

    /// <summary>
    /// Add a generic trigger observable of any type, converting it to Unit.
    /// Useful for observables that emit values (e.g., StateChange, numbers, strings, etc.)
    /// 
    /// Examples:
    /// .AddTrigger(entities.BinarySensor.KitchenMotion.StateChanges())  // StateChange -> Unit
    /// .AddTrigger(Observable.Interval(TimeSpan.FromSeconds(5)))        // long -> Unit
    /// .AddTrigger(myNumberSequence)                                    // int -> Unit
    /// .AddTrigger(stateChanges.Where(s => s.New.IsOn()))              // StateChange -> Unit
    /// </summary>
    public AlexaPromptPoller AddTrigger<T>(IObservable<T> trigger)
    {
        if (trigger == null) throw new ArgumentNullException(nameof(trigger));
        // Convert any observable to Unit by selecting Unit.Default for each emission
        var unitTrigger = trigger.Select(_ => Unit.Default);
        _triggers.Add(unitTrigger);
        _logger.LogDebug("Added generic trigger of type {TriggerType} to AlexaPromptPoller", typeof(T).Name);
        return this;
    }

    /// <summary>
    /// Add a trigger that fires when a predicate is true.
    /// Useful for conditional triggers.
    /// 
    /// Example:
    /// .AddTrigger(entities.BinarySensor.Motion.StateChanges().Where(s => s.New.IsOn()))
    /// </summary>
    public AlexaPromptPoller AddTrigger<T>(IObservable<T> trigger, Func<T, bool> predicate)
    {
        if (trigger == null) throw new ArgumentNullException(nameof(trigger));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        // Apply predicate and convert to Unit
        var unitTrigger = trigger
            .Where(predicate)
            .Select(_ => Unit.Default);
        _triggers.Add(unitTrigger);
        _logger.LogDebug("Added conditional trigger of type {TriggerType} to AlexaPromptPoller", typeof(T).Name);
        return this;
    }

    /// <summary>
    /// Add a trigger that fires when a predicate is true on the value.
    /// Useful for extracting a boolean condition from the trigger value.
    /// 
    /// Example:
    /// .AddTrigger(entities.BinarySensor.Motion.StateChanges(), s => s.New.IsOn())
    /// </summary>
    public AlexaPromptPoller AddTrigger<T>(IObservable<T> trigger, Func<T, bool?> booleanExtractor)
    {
        if (trigger == null) throw new ArgumentNullException(nameof(trigger));
        if (booleanExtractor == null) throw new ArgumentNullException(nameof(booleanExtractor));
        // Extract boolean and filter to true, then convert to Unit
        var unitTrigger = trigger
            .Where(value => booleanExtractor(value) == true)
            .Select(_ => Unit.Default);
        _triggers.Add(unitTrigger);
        _logger.LogDebug("Added boolean-extracted trigger of type {TriggerType} to AlexaPromptPoller", typeof(T).Name);
        return this;
    }

    /// <summary>
    /// Add multiple triggers at once.
    /// </summary>
    public AlexaPromptPoller AddTriggers(params IObservable<Unit>[] triggers)
    {
        foreach (var trigger in triggers)
        {
            AddTrigger(trigger);
        }
        return this;
    }

    /// <summary>
    /// Set the media player entity ID (e.g., "media_player.dining").
    /// </summary>
    public AlexaPromptPoller SetMediaPlayer(string mediaPlayer)
    {
        _mediaPlayer = mediaPlayer ?? throw new ArgumentNullException(nameof(mediaPlayer));
        _logger.LogDebug("AlexaPromptPoller media player set to {MediaPlayer}", _mediaPlayer);
        return this;
    }

    /// <summary>
    /// Set the prompt message and unique event ID.
    /// </summary>
    public AlexaPromptPoller SetPrompt(string message, string eventId)
    {
        _promptMessage = message ?? throw new ArgumentNullException(nameof(message));
        _eventId = eventId ?? throw new ArgumentNullException(nameof(eventId));
        _logger.LogDebug("AlexaPromptPoller prompt set: {EventId} - {Message}", _eventId, _promptMessage);
        return this;
    }

    /// <summary>
    /// Set the cooldown duration between prompts (default: 1 minute).
    /// </summary>
    public AlexaPromptPoller WithCooldown(TimeSpan cooldown)
    {
        _cooldown = cooldown;
        _logger.LogDebug("AlexaPromptPoller cooldown set to {Cooldown}", _cooldown);
        return this;
    }

    /// <summary>
    /// Add a handler for a specific response type.
    /// </summary>
    public AlexaPromptPoller OnResponse(PromptResponseType responseType, Action<PromptResponse> handler)
    {
        _responseHandlers[responseType] = handler ?? throw new ArgumentNullException(nameof(handler));
        _logger.LogDebug("Added response handler for type {ResponseType}", responseType);
        return this;
    }

    /// <summary>
    /// Add a handler for any response type EXCEPT ResponseYes.
    /// Useful for handling all "not yes" scenarios with the same logic.
    /// </summary>
    public AlexaPromptPoller OnResponseNotYes(Action<PromptResponse> handler)
    {
        return OnResponse(PromptResponseType.ResponseNo, handler)
            .OnResponse(PromptResponseType.ResponseNone, handler)
            .OnResponse(PromptResponseType.ResponseUnknown, handler)
            .OnResponse(PromptResponseType.ResponseSelect, handler)
            .OnResponse(PromptResponseType.ResponseNumeric, handler)
            .OnResponse(PromptResponseType.ResponseDuration, handler);
    }

    /// <summary>
    /// Add a handler for any response type EXCEPT ResponseNo.
    /// Useful for handling all "not no" scenarios with the same logic.
    /// </summary>
    public AlexaPromptPoller OnResponseNotNo(Action<PromptResponse> handler)
    {
        return OnResponse(PromptResponseType.ResponseYes, handler)
            .OnResponse(PromptResponseType.ResponseNone, handler)
            .OnResponse(PromptResponseType.ResponseUnknown, handler)
            .OnResponse(PromptResponseType.ResponseSelect, handler)
            .OnResponse(PromptResponseType.ResponseNumeric, handler)
            .OnResponse(PromptResponseType.ResponseDuration, handler);
    }

    /// <summary>
    /// Add a handler for any response type EXCEPT the specified type.
    /// Useful for handling all "not X" scenarios with the same logic.
    /// </summary>
    public AlexaPromptPoller OnResponseNot(PromptResponseType excludeType, Action<PromptResponse> handler)
    {
        // Register handler for all types except the excluded one
        foreach (PromptResponseType type in Enum.GetValues(typeof(PromptResponseType)))
        {
            if (type != excludeType)
            {
                OnResponse(type, handler);
            }
        }
        return this;
    }

    /// <summary>
    /// Add a handler for ResponseYes.
    /// </summary>
    public AlexaPromptPoller OnResponseYes(Action<PromptResponse> handler)
    {
        return OnResponse(PromptResponseType.ResponseYes, handler);
    }

    /// <summary>
    /// Add a handler for ResponseNo.
    /// </summary>
    public AlexaPromptPoller OnResponseNo(Action<PromptResponse> handler)
    {
        return OnResponse(PromptResponseType.ResponseNo, handler);
    }

    /// <summary>
    /// Add a handler for ResponseUnknown (when person cannot be identified).
    /// </summary>
    public AlexaPromptPoller OnResponseUnknown(Action<PromptResponse> handler)
    {
        return OnResponse(PromptResponseType.ResponseUnknown, handler);
    }

    /// <summary>
    /// Add a handler for ResponseNone (timeout/no response).
    /// </summary>
    public AlexaPromptPoller OnResponseNone(Action<PromptResponse> handler)
    {
        return OnResponse(PromptResponseType.ResponseNone, handler);
    }

    /// <summary>
    /// Add a handler for ResponseSelect.
    /// </summary>
    public AlexaPromptPoller OnResponseSelect(Action<PromptResponse> handler)
    {
        return OnResponse(PromptResponseType.ResponseSelect, handler);
    }

    /// <summary>
    /// Add a default handler for response types without explicit handlers.
    /// </summary>
    public AlexaPromptPoller OnResponseDefault(Action<PromptResponse> handler)
    {
        _defaultResponseHandler = handler ?? throw new ArgumentNullException(nameof(handler));
        _logger.LogDebug("Added default response handler");
        return this;
    }

    /// <summary>
    /// Set a daily reset trigger (observable that fires once per day to reset state).
    /// Example: Observable.Timer(Next6am(), TimeSpan.FromDays(1), scheduler)
    /// </summary>
    public AlexaPromptPoller WithDailyReset(IObservable<Unit> dailyTrigger)
    {
        _dailyResetTrigger = dailyTrigger ?? throw new ArgumentNullException(nameof(dailyTrigger));
        _logger.LogDebug("Daily reset trigger configured");
        return this;
    }

    /// <summary>
    /// Set a daily reset trigger with a generic observable type.
    /// The value is discarded and converted to Unit.
    /// 
    /// Example:
    /// .WithDailyReset(Observable.Timer(Next6am(), TimeSpan.FromDays(1), scheduler))
    /// .WithDailyReset(myNumberSequence)
    /// </summary>
    public AlexaPromptPoller WithDailyReset<T>(IObservable<T> dailyTrigger)
    {
        if (dailyTrigger == null) throw new ArgumentNullException(nameof(dailyTrigger));
        // Convert to Unit
        _dailyResetTrigger = dailyTrigger.Select(_ => Unit.Default);
        _logger.LogDebug("Daily reset trigger configured with type {TriggerType}", typeof(T).Name);
        return this;
    }

    /// <summary>
    /// Get an observable that can be used to externally trigger a reset.
    /// Call .ResetState() to send a reset signal.
    /// </summary>
    public IObservable<Unit> GetExternalResetTrigger()
    {
        _externalResetTrigger ??= new Subject<Unit>();
        return _externalResetTrigger;
    }

    /// <summary>
    /// Externally trigger a reset (clears cooldown and waiting state).
    /// </summary>
    public void ResetState()
    {
        _externalResetTrigger ??= new Subject<Unit>();
        _lastPromptTime = DateTimeOffset.MinValue;
        _isWaitingForResponse = false;
        _isAcknowledged = false;
        _logger.LogDebug("AlexaPromptPoller state reset externally");
        _externalResetTrigger.OnNext(Unit.Default);
    }

    /// <summary>
    /// Build and subscribe to the prompt poller. Returns disposable to manage subscription lifecycle.
    /// </summary>
    public IDisposable Subscribe()
    {
        ValidateConfiguration();

        if (_triggers.Count == 0)
            throw new InvalidOperationException("At least one trigger must be added via AddTrigger()");

        // Merge all triggers
        var mergedTriggers = _triggers[0];
        for (int i = 1; i < _triggers.Count; i++)
        {
            mergedTriggers = mergedTriggers.Merge(_triggers[i]);
        }

        // Create acknowledgment stream if not already created
        _acknowledgmentStream ??= new Subject<Unit>();

        // Apply cooldown logic using throttling, stop when acknowledged
        var throttledTriggers = mergedTriggers
            .TakeUntil(_acknowledgmentStream)
            .Where(_ =>
            {
                var elapsed = _scheduler.Now - _lastPromptTime;
                bool canPrompt = elapsed >= _cooldown;
                
                if (!canPrompt)
                {
                    _logger.LogDebug(
                        "Trigger ignored: cooldown active. Elapsed: {Elapsed}ms, Cooldown: {Cooldown}ms",
                        elapsed.TotalMilliseconds,
                        _cooldown.TotalMilliseconds
                    );
                }
                
                return canPrompt;
            })
            .Do(_ =>
            {
                _lastPromptTime = _scheduler.Now;
                _isWaitingForResponse = true;
                _logger.LogDebug("Cooldown elapsed, sending prompt");
            });

        // Subscribe to triggers
        var triggerSubscription = throttledTriggers
            .Subscribe(_ =>
            {
                _logger.LogDebug("Sending prompt: {EventId} to {MediaPlayer}", _eventId, _mediaPlayer);
                _alexa.Prompt(_mediaPlayer, _promptMessage, _eventId);
            });

        // Subscribe to responses
        var responseSubscription = _alexa.PromptResponses
            .Where(r => r.EventId == _eventId)
            .Do(r => _logger.LogDebug(
                "Received response for {EventId}: Type={ResponseType}, Person={ResponsePersonName}",
                r.EventId,
                r.ResponseType,
                r.ResponsePersonName
            ))
            .Subscribe(response =>
            {
                HandleResponse(response);
                _isWaitingForResponse = false;
            });

        _subscriptions.Add(triggerSubscription);
        _subscriptions.Add(responseSubscription);

        // Subscribe to reset triggers if configured
        if (_dailyResetTrigger != null)
        {
            var resetSubscription = _dailyResetTrigger
                .Do(_ => _logger.LogDebug("Daily reset triggered for {EventId}", _eventId))
                .Subscribe(_ => ResetState());
            
            _subscriptions.Add(resetSubscription);
        }

        if (_externalResetTrigger != null)
        {
            var externalResetSubscription = _externalResetTrigger
                .Do(_ => _logger.LogDebug("External reset triggered for {EventId}", _eventId))
                .Subscribe(_ =>
                {
                    _lastPromptTime = DateTimeOffset.MinValue;
                    _isWaitingForResponse = false;
                    _isAcknowledged = false;
                });
            
            _subscriptions.Add(externalResetSubscription);
        }

        _logger.LogInformation(
            "AlexaPromptPoller initialized: EventId={EventId}, MediaPlayer={MediaPlayer}, Cooldown={Cooldown}ms, Triggers={TriggerCount}",
            _eventId,
            _mediaPlayer,
            _cooldown.TotalMilliseconds,
            _triggers.Count
        );

        return new CompositeDisposable(_subscriptions);
    }

    private void ValidateConfiguration()
    {
        if (string.IsNullOrWhiteSpace(_mediaPlayer))
            throw new InvalidOperationException("MediaPlayer must be set via SetMediaPlayer()");
        
        if (string.IsNullOrWhiteSpace(_promptMessage))
            throw new InvalidOperationException("Prompt message must be set via SetPrompt()");
        
        if (string.IsNullOrWhiteSpace(_eventId))
            throw new InvalidOperationException("Event ID must be set via SetPrompt()");
    }

    /// <summary>
    /// Acknowledge the prompt and stop all further prompting.
    /// Once called, triggers will no longer fire until ResetState() is called.
    /// </summary>
    public void Acknowledge()
    {
        _acknowledgmentStream ??= new Subject<Unit>();
        _isAcknowledged = true;
        _logger.LogDebug("AlexaPromptPoller acknowledged - no further prompts will be sent until reset");
        _acknowledgmentStream.OnNext(Unit.Default);
    }

    /// <summary>
    /// Check if the poller has been acknowledged.
    /// </summary>
    public bool IsAcknowledged => _isAcknowledged;

    private void HandleResponse(PromptResponse response)
    {
        if (_responseHandlers.TryGetValue(response.ResponseType, out var handler))
        {
            _logger.LogDebug("Executing specific handler for response type {ResponseType}", response.ResponseType);
            handler(response);
        }
        else if (_defaultResponseHandler != null)
        {
            _logger.LogDebug("No specific handler found, executing default handler for {ResponseType}", response.ResponseType);
            _defaultResponseHandler(response);
        }
        else
        {
            _logger.LogWarning("No handler found for response type {ResponseType} and no default handler configured", response.ResponseType);
        }
    }

    /// <summary>
    /// Get current state snapshot for diagnostics/persistence.
    /// </summary>
    public PromptPollerState GetState()
    {
        return new PromptPollerState
        {
            EventId = _eventId,
            LastPromptTime = _lastPromptTime,
            IsWaitingForResponse = _isWaitingForResponse,
            CooldownRemaining = (_lastPromptTime + _cooldown) - _scheduler.Now,
            IsAcknowledged = _isAcknowledged
        };
    }
}

/// <summary>
/// State snapshot for diagnostics or persistence to Home Assistant.
/// </summary>
public record PromptPollerState
{
    public string EventId { get; init; } = "";
    public DateTimeOffset LastPromptTime { get; init; }
    public bool IsWaitingForResponse { get; init; }
    public TimeSpan CooldownRemaining { get; init; }
    public bool IsAcknowledged { get; init; }
}

/// <summary>
/// CompositeDisposable helper - manages a collection of disposables.
/// </summary>
internal class CompositeDisposable : IDisposable
{
    private readonly List<IDisposable> _disposables;
    private bool _disposed;

    public CompositeDisposable(List<IDisposable> disposables)
    {
        _disposables = disposables;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        foreach (var disposable in _disposables)
        {
            disposable?.Dispose();
        }
        _disposables.Clear();
    }
}
