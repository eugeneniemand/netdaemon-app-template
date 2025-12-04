using System.Collections.Generic;
using System.Reactive.Subjects;
using NetDaemon.Helpers;

namespace Niemand.Tests.Mocks;

public class AlexaMock(IServices services) : IAlexa
{
    private readonly Subject<PromptResponse> _promptResponses = new();
    private readonly List<(string mediaPlayer, string message, string eventId)> _promptHistory = new();
    private readonly List<Alexa.Config> _promptConfigHistory = new();

    public virtual void Announce(Alexa.Config config)
    {
        services.Notify.AlexaMedia(config.Entity, target: config.Entity, data: new { type = "announce" });
    }

    public virtual void Announce(string mediaPlayer, string message)
    {
        services.Notify.AlexaMedia(message, target: mediaPlayer, data: new { type = "announce" });
    }

    public Dictionary<string, AlexaPeopleConfig> People { get; } = [];

    public virtual void Prompt(string mediaPlayer, string message, string eventId)
    {
        _promptHistory.Add((mediaPlayer, message, eventId));
    }

    public virtual void Prompt(Alexa.Config config)
    {
        _promptConfigHistory.Add(config);
    }

    public int PromptCallCount => _promptHistory.Count + _promptConfigHistory.Count;
    public IReadOnlyList<(string mediaPlayer, string message, string eventId)> PromptHistory => _promptHistory.AsReadOnly();
    public IReadOnlyList<Alexa.Config> PromptConfigHistory => _promptConfigHistory.AsReadOnly();

    public IObservable<PromptResponse> PromptResponses => _promptResponses;

    IObservable<PromptResponse> IAlexa.PromptResponses => _promptResponses;

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

    void IAlexa.PlaySound(MediaPlayerEntity mediaPlayer, string soundName)
    {
        throw new NotImplementedException();
    }

    void IAlexa.PlayMusic(MediaPlayerEntity mediaPlayer, string command)
    {
        throw new NotImplementedException();
    }

    void IAlexa.SendCommand(MediaPlayerEntity mediaPlayer, string command)
    {
        throw new NotImplementedException();
    }
}