namespace Niemand.Helpers;

/// <summary>
/// Helper for persisting AlexaPromptPoller state to Home Assistant or in-memory cache.
/// 
/// Two approaches are available:
/// 1. PromptPollerStateCache - Simple in-memory cache (no persistence across app reloads)
/// 2. HA Integration - Manual state storage to Home Assistant entities (requires setup)
/// 
/// Example usage with in-memory cache:
/// var stateCache = new PromptPollerStateCache();
/// // Later, to store state:
/// stateCache.Store("jayden_tablet", poller.GetState());
/// </summary>
public class PromptPollerStateHelper
{
    private readonly IHaContext _ha;
    private readonly ILogger<PromptPollerStateHelper> _logger;

    public PromptPollerStateHelper(IHaContext ha, ILogger<PromptPollerStateHelper> logger)
    {
        _ha = ha;
        _logger = logger;
    }

    /// <summary>
    /// Check if an input_datetime entity exists in Home Assistant for storing state.
    /// If not, log an informational message suggesting manual Home Assistant setup.
    /// </summary>
    public bool ValidateStateEntity(string stateKeyPrefix)
    {
        try
        {
            var lastPromptEntityId = $"input_datetime.{stateKeyPrefix}_last_prompt";
            var entity = _ha.Entity(lastPromptEntityId);
            
            if (entity?.State != null)
            {
                _logger.LogInformation(
                    "State entity found for {EventId}: {EntityId}",
                    stateKeyPrefix,
                    lastPromptEntityId
                );
                return true;
            }
            else
            {
                _logger.LogWarning(
                    "State entity not found for {EventId}: {EntityId}. " +
                    "To persist state across app reloads, create this entity in Home Assistant: " +
                    "input_datetime.{StateKeyPrefix}_last_prompt",
                    stateKeyPrefix,
                    lastPromptEntityId,
                    stateKeyPrefix
                );
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to validate state entity for {StateKeyPrefix}", stateKeyPrefix);
            return false;
        }
    }

    /// <summary>
    /// Get diagnostic info about current poller state for logging/debugging.
    /// </summary>
    public void LogPollerState(AlexaPromptPoller poller, string stateKeyPrefix)
    {
        var state = poller.GetState();
        _logger.LogInformation(
            "Poller state for {EventId}: LastPromptTime={LastPromptTime}, " +
            "IsWaitingForResponse={IsWaitingForResponse}, " +
            "CooldownRemaining={CooldownRemaining}ms",
            state.EventId,
            state.LastPromptTime,
            state.IsWaitingForResponse,
            state.CooldownRemaining.TotalMilliseconds
        );
    }
}

/// <summary>
/// Simple in-memory cache for storing AlexaPromptPoller state during this app session.
/// State is lost when the app reloads.
/// 
/// Usage:
/// var cache = new PromptPollerStateCache();
/// cache.Store("jayden_tablet", poller.GetState());
/// 
/// if (cache.TryGet("jayden_tablet", out var state))
/// {
///     _logger.LogInformation("Cached state: {State}", state);
/// }
/// </summary>
public class PromptPollerStateCache
{
    private readonly Dictionary<string, PromptPollerState> _cache = new();
    private readonly ILogger<PromptPollerStateCache> _logger;

    public PromptPollerStateCache(ILogger<PromptPollerStateCache> logger)
    {
        _logger = logger;
    }

    public void Store(string eventId, PromptPollerState state)
    {
        _cache[eventId] = state;
        _logger.LogDebug(
            "Cached poller state for {EventId}: LastPromptTime={LastPromptTime}, CooldownRemaining={CooldownRemaining}ms",
            eventId,
            state.LastPromptTime,
            state.CooldownRemaining.TotalMilliseconds
        );
    }

    public bool TryGet(string eventId, out PromptPollerState? state)
    {
        var found = _cache.TryGetValue(eventId, out state);
        if (found)
        {
            _logger.LogDebug("Retrieved cached state for {EventId}", eventId);
        }
        return found;
    }

    public void Clear(string eventId)
    {
        _cache.Remove(eventId);
        _logger.LogDebug("Cleared cached state for {EventId}", eventId);
    }

    public void ClearAll()
    {
        _cache.Clear();
        _logger.LogDebug("Cleared all cached poller states");
    }
}
