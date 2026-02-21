namespace Niemand.Helpers.Notifications;

/// <summary>
/// Configuration constants for Alexa notification processing behavior.
/// Centralizes timing, buffer sizes, and regex patterns used in message processing.
/// </summary>
public static class AlexaProcessingConfig
{
    /// <summary>
    /// Time window to buffer notification messages before processing them as a batch.
    /// </summary>
    public static readonly TimeSpan MessageBufferDelay = TimeSpan.FromMilliseconds(500);

    /// <summary>
    /// Delay after setting volume and before sending notifications to allow device to stabilize.
    /// </summary>
    public static readonly TimeSpan VolumeSetupDelay = TimeSpan.FromSeconds(2);

    /// <summary>
    /// Base delay per word in a message (seconds). Used to calculate how long to wait before reverting volume.
    /// </summary>
    public const double WordDelay = 0.5d;

    /// <summary>
    /// Regex pattern to clean up audio tag separators in concatenated messages.
    /// Removes the ",,,and," separator when it appears adjacent to &lt;audio/&gt; tags.
    /// </summary>
    public static readonly string AudioTagCleanupPattern =
        @"(?:(?<=<audio[^>]*\/>)\s*,{3}and\s*,?)" +
        @"|(?:\s*,{3}and\s*,?(?=<audio[^>]*\/>))";
}
