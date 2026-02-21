using System.Text.RegularExpressions;

namespace Niemand.Helpers.Notifications;

/// <summary>
/// Handles formatting of messages for Alexa notifications.
/// Manages SSML construction, voice application, whisper effects, and audio tag cleanup.
/// </summary>
public class MessageFormatter
{
    /// <summary>
    /// Formats a message with voice and whisper effects using SSML markup.
    /// </summary>
    /// <param name="message">The base message text (may contain commas and audio tags)</param>
    /// <param name="voice">The Alexa voice name to apply</param>
    /// <param name="whisper">Whether to apply whisper effect instead of voice</param>
    /// <returns>SSML-formatted message</returns>
    public string FormatMessage(string message, string voice, bool whisper)
    {
        var messageWithBreaks = AddBreaks(message);
        return whisper ? ApplyWhisperEffect(messageWithBreaks) : ApplyVoiceEffect(messageWithBreaks, voice);
    }

    /// <summary>
    /// Concatenates multiple messages into a single message with audio tag-aware separators.
    /// </summary>
    public string ConcatenateMessages(IEnumerable<string> messages)
    {
        var concatenated = string.Join(",,,and,", messages);
        return CleanupAudioTagSeparators(concatenated);
    }

    /// <summary>
    /// Calculates the word count of a message for timing calculations.
    /// </summary>
    public int GetWordCount(string message)
    {
        return message.Split([' ', '.', '-', '—', ','], StringSplitOptions.RemoveEmptyEntries).Length;
    }

    /// <summary>
    /// Adds SSML break tags at comma positions to improve pacing.
    /// </summary>
    private string AddBreaks(string message)
    {
        return message.Replace(",", "<break />");
    }

    /// <summary>
    /// Wraps message in voice effect SSML tag.
    /// </summary>
    private string ApplyVoiceEffect(string message, string voice)
    {
        return $"<voice name='{voice}'>{message}</voice>";
    }

    /// <summary>
    /// Wraps message in Amazon whisper effect SSML tag.
    /// </summary>
    private string ApplyWhisperEffect(string message)
    {
        return $"<amazon:effect name='whispered'>{message}</amazon:effect>";
    }

    /// <summary>
    /// Removes the ",,,and," separator when it appears adjacent to audio tags.
    /// This prevents awkward pauses around audio insertions.
    /// </summary>
    private string CleanupAudioTagSeparators(string message)
    {
        return Regex.Replace(message, AlexaProcessingConfig.AudioTagCleanupPattern, "", RegexOptions.IgnoreCase);
    }
}
