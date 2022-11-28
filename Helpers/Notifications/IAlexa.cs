namespace Niemand.Helpers;

public interface IAlexa
{
    void Announce(Alexa.Config config);
    void Announce(string mediaPlayer, string message);
    void TextToSpeech(Alexa.Config config);
    void TextToSpeech(string mediaPlayer, string message);
}