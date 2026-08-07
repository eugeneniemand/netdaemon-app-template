using System.Collections.Generic;
using System.Reactive.Subjects;
using NetDaemon.Helpers;

namespace Niemand.Tests.Mocks;

public class AlexaMock() : IAlexa
{
    private readonly Subject<PromptResponse> _promptResponses = new();
    
    private readonly List<Alexa.Config> _promptConfigHistory = new();
    private readonly List<(string mediaPlayer, string message, string eventId)> _promptHistory = new();
    
    private readonly List<Alexa.Config> _announceConfigHistory = new();
    private readonly List<(string mediaPlayer, string message)> _announceHistory = new();

    private readonly List<Alexa.Config> _ttsConfigHistory = new();
    private readonly List<(string mediaPlayer, string message)> _ttsHistory = new();

    private readonly Dictionary<string, List<MediaPlayerEntity>> _mediaPlayers = new();

    public virtual void Announce(Alexa.Config config)
    {
        _announceConfigHistory.Add(config);        
    }

    public virtual void Announce(string mediaPlayer, string message)
    {
        _announceHistory.Add((mediaPlayer, message));        
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

    public void AddMockMediaPlayer(string entityId, string label)
    {
        var mediaPlayer = new MediaPlayerEntity(null!, entityId);
        var mediaPlayers = _mediaPlayers.GetValueOrDefault(label, []);
        if (mediaPlayers.Contains(mediaPlayer))
            return;
        mediaPlayers.Add(mediaPlayer);

        _mediaPlayers[label] = mediaPlayers;
    }

    public int PromptCallCount => _promptHistory.Count + _promptConfigHistory.Count;
    public IReadOnlyList<(string mediaPlayer, string message, string eventId)> PromptHistory => _promptHistory.AsReadOnly();
    public IReadOnlyList<Alexa.Config> PromptConfigHistory => _promptConfigHistory.AsReadOnly();
    public IReadOnlyList<Alexa.Config> AnnounceConfigCalls => _announceConfigHistory.AsReadOnly();
    public IReadOnlyList<(string mediaPlayer, string message)> AnnounceCalls => _announceHistory.AsReadOnly();
    
    public IObservable<PromptResponse> PromptResponses => _promptResponses;

    IObservable<PromptResponse> IAlexa.PromptResponses => _promptResponses;

    public virtual void TextToSpeech(Alexa.Config config)
    {
        _ttsConfigHistory.Add(config);
    }

    public virtual void TextToSpeech(string mediaPlayer, string message)
    {
        _ttsHistory.Add((mediaPlayer, message));
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

    List<MediaPlayerEntity> IAlexa.MediaPlayersWithLabel(string label)
    {
        return _mediaPlayers.GetValueOrDefault(label, []);
    }

    List<string> IAlexa.MediaPlayerEntityIdsForLabel(string label)
    {
        return _mediaPlayers.GetValueOrDefault(label.ToLower(), []).Select(mp => mp.EntityId).ToList();
    }
}