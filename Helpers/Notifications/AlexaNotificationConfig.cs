namespace Niemand.Helpers.Notifications;

/// <summary>
/// Configuration for an Alexa notification.
/// Allows customization of message, entities, volume, timing, and effects.
/// </summary>
public class AlexaNotificationConfig
{
    /// <summary>
    /// Gets or sets whether to apply whisper effect (null = use device defaults).
    /// </summary>
    public bool? Whisper { get; set; } = null;

    /// <summary>
    /// Gets or sets the volume level (0-1) override (null = use device defaults).
    /// </summary>
    public double? VolumeLevel { get; set; } = null;

    /// <summary>
    /// Gets or sets the delay (in seconds) before reverting volume.
    /// If null, calculated based on word count.
    /// </summary>
    public int? VolumeResetDelay { get; set; } = null;

    /// <summary>
    /// Gets or sets the notification message text.
    /// </summary>
    public string Message { get; set; } = "";

    /// <summary>
    /// Gets or sets the event ID for prompt notifications (used to correlate responses).
    /// </summary>
    public string EventId { get; set; } = "";

    /// <summary>
    /// Gets or sets the notification type (tts, announce, prompt).
    /// </summary>
    public string NotifyType { get; set; } = "tts";

    /// <summary>
    /// Gets or sets a single media player entity ID.
    /// Auto-adds to Entities list when set.
    /// </summary>
    public string Entity { get; set; } = "";

    private List<string> _entities = new();

    /// <summary>
    /// Gets or sets the list of media player entity IDs to notify.
    /// Entity property is automatically included if set.
    /// </summary>
    public List<string> Entities
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Entity) && !_entities.Contains(Entity))
                _entities.Add(Entity);
            return _entities;
        }
        set => _entities = value;
    }
}
