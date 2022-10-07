namespace Niemand;

public class Notification
{
    public Notification(string announcement, TtsType ttsType, Volume volume, MediaPlayerEntity? target = null)
    {
        Announcement = announcement;
        Target       = target;
        TtsType      = ttsType;
        VolumeLevel  = volume.ToVolumeLevel();
        Whisper      = Enum.GetName(volume)!.ToUpper().StartsWith("W");
        EventId      = announcement.GetFixedHash();
    }

    public bool Whisper { get; }
    public double VolumeLevel { get; }
    public MediaPlayerEntity? Target { get; }
    public string Announcement { get; }
    public string EventId { get; }
    public TtsType TtsType { get; }
}

public enum TtsType
{
    Announcement,
    TextToSpeech
}

/// <summary>
///     N - Normal and W - Whisper
///     Volume 1 - 10
/// </summary>
public enum Volume
{
    N1,
    N2,
    N3,
    N4,
    N5,
    N6,
    N7,
    N8,
    N9,
    N10,
    W1,
    W2,
    W3,
    W4,
    W5,
    W6,
    W7,
    W8,
    W9,
    W10
}

public class Announcement : Notification
{
    public Announcement(string announcement, TimeSpan timeout, MediaPlayerEntity? target = null, Volume volume = Volume.N3) :
        base(announcement, TtsType.Announcement, volume, target)
    {
        Timeout = timeout;
    }

    public TimeSpan Timeout { get; }
}

public class TextToSpeech : Notification
{
    public TextToSpeech(string announcement, TimeSpan timeout, MediaPlayerEntity? target = null, Volume volume = Volume.N3) :
        base(announcement, TtsType.TextToSpeech, volume, target)
    {
        Timeout = timeout;
    }

    public TimeSpan Timeout { get; }
}

public class Prompt : Notification
{
    public Prompt(string announcement, TextToSpeech yesResponse, TextToSpeech noResponse, MediaPlayerEntity? target = null, TtsType ttsType = TtsType.Announcement, Volume volume = Volume.N3) :
        base(announcement, ttsType, volume, target)
    {
        YesResponse = yesResponse;
        NoResponse  = noResponse;
    }

    public TextToSpeech NoResponse { get; }

    public TextToSpeech YesResponse { get; }
}