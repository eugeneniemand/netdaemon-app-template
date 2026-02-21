using NetDaemon.HassModel.Entities;
using Niemand.Helpers.Notifications;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using YamlDotNet.Core.Tokens;

namespace Niemand.Helpers;

public class Alexa : IAlexa
{
    public enum NotificationType
    {
        Prompt,
        Announcement,
        Tts
    }

    private readonly IDictionary<string, AlexaDeviceConfig> _devices;
    private readonly IEntities _entities;
    private readonly IHaContext _ha;
    private readonly ILogger<Alexa> _logger;
    private readonly Subject<Config> _messages = new();

    private readonly Subject<PromptResponse> _promptResponses = new();
    private readonly IScheduler _scheduler;
    private readonly IServices _services;
    private readonly IVoiceProvider _voice;
    private readonly VolumeManager _volumeManager;
    private readonly MessageFormatter _messageFormatter;
    private readonly NotificationProcessor _notificationProcessor;
    private readonly PromptResponseHandler _responseHandler;

    public IObservable<PromptResponse> PromptResponses => _promptResponses;


    public Alexa(
        IHaContext ha,
        IEntities entities,
        IServices services,
        IScheduler scheduler,
        IVoiceProvider voice,
        IAppConfig<AlexaConfig> config,
        ILogger<Alexa> logger,
        Subject<PromptResponse> promptResponses,
        VolumeManager volumeManager,
        MessageFormatter messageFormatter,
        NotificationProcessor notificationProcessor,
        PromptResponseHandler responseHandler)
    {
        _ha = ha;
        _entities = entities;
        _services = services;
        _scheduler = scheduler;
        _voice = voice;
        _logger = logger;
        _devices = config.Value.Devices;
        People = (Dictionary<string, AlexaPeopleConfig>)config.Value.People;
        _promptResponses = promptResponses;
        _volumeManager = volumeManager;
        _messageFormatter = messageFormatter;
        _notificationProcessor = notificationProcessor;
        _responseHandler = responseHandler;

        // Set up Home Assistant event subscription (only happens once due to lock in SetupEventSubscription)
        // Pass the people config here since we have access to it in this scoped context
        _responseHandler.SetupEventSubscription(ha, People);

        _messages.Where(msg => msg.NotifyType is "tts" or "announce")
                 .Buffer(AlexaProcessingConfig.MessageBufferDelay, scheduler)
                 .Where(buffer => buffer.Any())
                 .SubscribeAsync(ProcessNotifications);

        _messages.Where(msg => msg.NotifyType is "prompt")
                 .Buffer(AlexaProcessingConfig.MessageBufferDelay, scheduler)
                 .Where(buffer => buffer.Any())
                 .SubscribeAsync(ProcessPrompts);
    }

    public List<MediaPlayerEntity> MediaPlayersWithLabel(string label) => [.. _entities.MediaPlayer.WithLabel(label)];
    public List<string> MediaPlayerEntityIdsForLabel(string label) => [.. _entities.MediaPlayer.WithLabel(label).Select(e => e.EntityId)];

    private MediaPlayerEntity? LastCalledMediaPlayerEntity => _ha
                                                              .GetAllEntities()
                                                              .Where(e => e != null && e.EntityId.StartsWith("media_player."))
                                                              .Select(e => new MediaPlayerEntity(_ha, e!.EntityId))
                                                              .FirstOrDefault(e => e.Attributes?.LastCalled != null && (bool)e.Attributes.LastCalled);

    public void Announce(Config config) =>
        QueueNotification(config, "announce");

    public void Announce(string mediaPlayer, string message) =>
        QueueNotification(new Config { Entity = mediaPlayer, Message = message }, "announce");

    public Dictionary<string, AlexaPeopleConfig> People { get; } = new();

    public void Prompt(string mediaPlayer, string message, string eventId) =>
        QueueNotification(new Config { Entity = mediaPlayer, Message = message, EventId = eventId }, "prompt");

    public void Prompt(Config config)
    {
        QueueNotification(config, "prompt");
    }



    public void TextToSpeech(Config config) =>
        QueueNotification(config, "tts");

    public void TextToSpeech(string mediaPlayer, string message) =>
        QueueNotification(new Config { Entity = mediaPlayer, Message = message }, "tts");

    public void PlaySound(MediaPlayerEntity mediaPlayer, string soundName) =>
        _services.MediaPlayer.PlayMedia(ServiceTarget.FromEntity(mediaPlayer.EntityId), new MediaPlayerPlayMediaParameters() { Media = new { media_content_type = MediaType.sound.ToString().ToLower(), media_content_id = soundName } });

    public void PlayMusic(MediaPlayerEntity mediaPlayer, string command) =>
        _services.MediaPlayer.PlayMedia(ServiceTarget.FromEntity(mediaPlayer.EntityId), new MediaPlayerPlayMediaParameters() { Media = new { media_content_type = MediaType.AMAZON_MUSIC.ToString(), media_content_id = command } });

    public void SendCommand(MediaPlayerEntity mediaPlayer, string command) =>
        _services.MediaPlayer.PlayMedia(ServiceTarget.FromEntity(mediaPlayer.EntityId), new MediaPlayerPlayMediaParameters() { Media = new { media_content_type = MediaType.custom.ToString().ToLower(), media_content_id = command } });

    private string FormatMessage(string message, string voice, bool whisper)
    {
        return _messageFormatter.FormatMessage(message, voice, whisper);
    }

    private async Task ProcessNotifications(IEnumerable<Config> cfgs)
    {
        // Group configs by the *set of entities* they target (order-independent)
        var groups = cfgs.GroupBy(cfg => MakeEntitySetKey(cfg.Entities));

        foreach (var group in groups)
        {
            await ProcessNotificationGroup(group);
        }
    }

    private static string MakeEntitySetKey(IEnumerable<string> entities)
    {
        // Normalise order + nulls/spaces so [a,b] == [b,a]
        return string.Join("|",
            (entities ?? Enumerable.Empty<string>())
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Select(e => e.Trim())
                .OrderBy(e => e, StringComparer.OrdinalIgnoreCase));
    }

    private async Task ProcessNotificationGroup(IEnumerable<Config> cfgs)
    {
        var voice = _voice.GetRandomVoice();
        var strategy = new AlexaMediaDeliveryStrategy(_services);
        await _notificationProcessor.ProcessAsync(cfgs, voice, strategy);
    }

    private async Task ProcessPrompts(IEnumerable<Config> cfgs)
    {
        var voice = _voice.GetRandomVoice();
        var strategy = new AlexaActionableNotificationDeliveryStrategy(_services);
        
        // For prompts, we need special handling: volume revert is triggered by a response event
        var entities = cfgs.First().Entities;
        var message = _messageFormatter.ConcatenateMessages(cfgs.Select(c => c.Message));
        var last = cfgs.Last();
        var eventId = last.EventId;

        var entitiesVolumeLevel = new Dictionary<string, double>();

        // Store volumes before processing
        foreach (var entity in entities)
        {
            _volumeManager.TryGetDeviceConfig(entity, out var deviceConfig);
            var (whisper, volume) = _volumeManager.GetVolumeDetailsForDevice(deviceConfig);
            _volumeManager.StoreCurrentVolume(entity, entitiesVolumeLevel);
        }

        // Send prompts
        await _notificationProcessor.ProcessAsync(cfgs, voice, strategy);

        // For prompts: wait for a response event, then revert volume
        _promptResponses
            .Where(r => r.EventId == eventId)
            .Take(1)
            .SelectMany(_ => Observable.FromAsync(() => _volumeManager.RestoreVolumesAsync(entitiesVolumeLevel)))
            .Subscribe(
                _ => { },
                e => _logger.LogError(e, "Error reverting volume after prompt")
            );
    }

    private void QueueNotification(Config cfg, string type)
    {
        cfg.NotifyType = type;
        _messages.OnNext(cfg);
    }

    public class Config : AlexaNotificationConfig
    {
        // Alias for backwards compatibility with existing code
    }

    public enum MediaType
    {
        sound,
        AMAZON_MUSIC,
        custom
    }
}
