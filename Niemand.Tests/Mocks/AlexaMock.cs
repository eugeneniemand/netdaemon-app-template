using System.Collections.Generic;
using System.Reactive.Subjects;
using NetDaemon.Helpers;

namespace Niemand.Tests.Mocks;

public class AlexaMock(IServices services) : IAlexa
{
    private readonly Subject<PromptResponse> _promptResponses = new();

    public virtual void Announce(Alexa.Config config)
    {
        services.Notify.AlexaMedia(config.Entity, target: config.Entity, data: new { type = "announce" });
    }

    public virtual void Announce(string mediaPlayer, string message)
    {
        services.Notify.AlexaMedia(message, target: mediaPlayer, data: new { type = "announce" });
    }

    public Dictionary<string, AlexaPeopleConfig> People { get; } = new();

    public virtual void Prompt(string mediaPlayer, string message, string eventId)
    {
    }

    public IObservable<PromptResponse> PromptResponses => _promptResponses;

    public virtual void TextToSpeech(Alexa.Config config)
    {
        services.Notify.AlexaMedia(config.Entity, target: config.Entity, data: new { type = "tts" });
    }

    public virtual void TextToSpeech(string mediaPlayer, string message)
    {
        services.Notify.AlexaMedia(message, target: mediaPlayer, data: new { type = "tts" });
    }

    public void QueueResponse(PromptResponse response)
    {
        _promptResponses.OnNext(response);
    }
}