namespace Niemand.Helpers;

public interface IAlexa
{
    public Dictionary<string, AlexaPeopleConfig> People { get; }
    public IObservable<PromptResponse> PromptResponses { get; }
    void Announce(Alexa.Config config);
    void Announce(string mediaPlayer, string message);
    /// <summary>
    /// more sounds https://developer.amazon.com/en-US/docs/alexa/custom-skills/ask-soundlibrary.html
    /// </summary>
    /// <param name="mediaPlayer"></param>
    /// <param name="soundName">
    /// amzn_sfx_scifi_alarm_03,
    /// amzn_sfx_scifi_alarm_04,
    /// amzn_sfx_doorbell_chime_01 
    /// </param>
    void PlaySound(MediaPlayerEntity mediaPlayer, string soundName);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediaPlayer"></param>
    /// <param name="command">
    /// cats the musical,
    /// My sunday playlist,
    /// boyz ii men in everywhere group,
    /// shuffle greenday
    /// </param>
    void PlayMusic(MediaPlayerEntity mediaPlayer, string command);
    /// <summary>
    /// Issue a voice command directly to a device as if you're speaking to it.
    /// </summary>
    /// <param name="mediaPlayer"></param>
    /// <param name="command">what time is it</param>
    void SendCommand(MediaPlayerEntity mediaPlayer, string command);
    void Prompt(string mediaPlayer, string message, string eventId);
    void TextToSpeech(Alexa.Config config);
    void TextToSpeech(string mediaPlayer, string message);
}