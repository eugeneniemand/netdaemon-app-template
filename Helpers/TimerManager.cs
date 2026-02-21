using HomeAssistantGenerated;
using NetDaemon.AppModel;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using Stateless.Graph;
using System.Globalization;

namespace NetDaemon.Helpers;

public sealed class TimerManager
{
    private readonly IHaContext _ha;
    private readonly IServices _services;

    public TimerManager(IHaContext ha, IServices services)
    {
        _ha = ha;
        _services = services;
    }

    /// <summary>
    /// Start (or restart) a timer at an exact duration.
    /// </summary>
    public async Task StartAsync(string entityId, TimeSpan duration)
    {
        var seconds = Math.Max(0, (int)Math.Round(duration.TotalSeconds));
        if (duration.TotalSeconds <= 0)
        {
            _ha.CallService("timer", "finish", ServiceTarget.FromEntity(entityId));
            return;
        }
        _ha.CallService("timer", "start", ServiceTarget.FromEntity(entityId), new { duration = seconds });        
    }

    /// <summary>
    /// Duration to change to Ex. 60 
    /// </summary>
    public async Task ChangeAsync(string entityId, TimeSpan duration)
    {
        var seconds = Math.Max(0, (int)Math.Round(duration.TotalSeconds));
        var timerState = GetTimerState(entityId);

        if (duration.TotalSeconds <= 0)
        { 
            _ha.CallService("timer", "finish", ServiceTarget.FromEntity(entityId));
            return;
        }

        _ha.CallService("timer", "start", ServiceTarget.FromEntity(entityId), new { duration = seconds });
        
        if (timerState == TimerState.Idle || timerState == TimerState.Paused)
            _ha.CallService("timer", "pause", ServiceTarget.FromEntity(entityId));
    }

    /// <summary>
    /// Add time to a timer by restarting it with remaining + increment if active else only add time
    /// If timer is idle/unknown, it starts at increment.
    /// </summary>
    public async Task AddAsync(string entityId, TimeSpan increment)
    {
        var remaining = GetRemaining(entityId);
        var newDuration = remaining + increment;

        // Guard against weird negatives
        if (newDuration < TimeSpan.Zero) newDuration = TimeSpan.Zero;

        var timerState = GetTimerState(entityId);
        if (timerState == TimerState.Active)
            await StartAsync(entityId, newDuration);
        else        
            await ChangeAsync(entityId, newDuration);

        _services.Logbook.Log(
            entityId: entityId,
            message: $"Duration changed to {newDuration.ToString(@"hh\:mm\:ss")}",
            name: entityId,
            domain: "timer");
    }

    private TimeSpan RoundTimeSpan(TimeSpan duration)
    {
        if (duration.Seconds < 30)
            return TimeSpan.FromMinutes(duration.Minutes);
        
        return TimeSpan.FromMinutes(duration.Minutes + 1);
    }

    /// <summary>
    /// Reads remaining time from timer attributes.
    /// Prefers finishes_at (most accurate), falls back to remaining.
    /// Returns TimeSpan.Zero if not running.
    /// </summary>
    private TimeSpan GetRemaining(string entityId)
    {
        var state = _ha.Entity(entityId);
        if (state is null) return TimeSpan.Zero;

        // Timer states: "active", "paused", "idle"
        var timerState = GetTimerState(entityId);
        if (timerState == TimerState.Unknown || timerState == TimerState.Unavailable)
            return TimeSpan.Zero;

        if (timerState == TimerState.Idle || timerState == TimerState.Paused)
        {
            if (state.Attributes.TryGetValue("remaining", out var durationObj))
            {
                var durationStr = durationObj switch
                {
                    string str => str,
                    JsonElement je when je.ValueKind == JsonValueKind.String => je.GetString(),
                    _ => null
                };

                if (!string.IsNullOrWhiteSpace(durationStr) &&
                    TimeSpan.TryParse(durationStr, CultureInfo.InvariantCulture, out var ts))
                {
                    return ts > TimeSpan.Zero ? RoundTimeSpan(ts) : TimeSpan.Zero;
                }
            }
        }

        // Attributes are usually JsonElement in HassModel
        if (state.Attributes.TryGetValue("finishes_at", out var finishesObj))
        {
            var finishesStr = finishesObj switch
            {
                string str => str,
                JsonElement je when je.ValueKind == JsonValueKind.String => je.GetString(),
                _ => null
            };

            if (!string.IsNullOrWhiteSpace(finishesStr) &&
                DateTimeOffset.TryParse(finishesStr, CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind, out var finishesAt))
            {
                var delta = finishesAt - DateTimeOffset.UtcNow;
                return delta > TimeSpan.Zero ? RoundTimeSpan(delta) : TimeSpan.Zero;
            }
        }

        return TimeSpan.Zero;
    }

    public TimerState GetTimerState(string entityId)
    {
        var state = _ha.Entity(entityId);
        var timerState = state.State ?? "unknown";
        switch (timerState.ToLower())
        {

            case "unknown":
                return TimerState.Unknown;
            case "unavailable":
                return TimerState.Unavailable;
            case "active":
                return TimerState.Active;
            case "paused":
                return TimerState.Paused;
            case "idle":
                return TimerState.Idle;
            default:
                return TimerState.Unknown;
        }
    }

    public TimerState GetTimerStateFromString(string state)
    {        
        var timerState = state ?? "unknown";
        switch (timerState.ToLower())
        {

            case "unknown":
                return TimerState.Unknown;
            case "unavailable":
                return TimerState.Unavailable;
            case "active":
                return TimerState.Active;
            case "paused":
                return TimerState.Paused;
            case "idle":
                return TimerState.Idle;
            default:
                return TimerState.Unknown;
        }
    }

    public enum TimerState
    {
        Idle,
        Active,
        Paused,
        Unknown,
        Unavailable
    }
}
