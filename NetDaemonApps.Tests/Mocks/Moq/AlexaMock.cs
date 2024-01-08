using System.Collections.Generic;
using System.Reactive.Subjects;
using Niemand.Helpers;

namespace Niemand.Mocks;

public class AlexaMock : IAlexa
{
    private readonly Subject<PromptResponse> _promptResponses = new();

    public virtual void Announce(Alexa.Config config)
    {
    }

    public virtual void Announce(string mediaPlayer, string message)
    {
    }

    public Dictionary<string, AlexaPeopleConfig> People { get; } = new();

    public virtual void Prompt(string mediaPlayer, string message, string eventId)
    {
    }

    public IObservable<PromptResponse> PromptResponses => _promptResponses;

    public virtual void TextToSpeech(Alexa.Config config)
    {
    }

    public virtual void TextToSpeech(string mediaPlayer, string message)
    {
    }

    public void QueueResponse(PromptResponse response)
    {
        _promptResponses.OnNext(response);
    }
}