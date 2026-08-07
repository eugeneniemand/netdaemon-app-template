namespace Niemand.Helpers.Notifications;

/// <summary>
/// Strategy for delivering a formatted message to entities.
/// Different notification types (announcements, TTS, prompts) use different delivery mechanisms.
/// </summary>
public interface INotificationDeliveryStrategy
{
    /// <summary>
    /// Delivers the formatted message to the specified entity.
    /// </summary>
    void Deliver(string formattedMessage, string entity, string notificationType, string eventId = "");
}

/// <summary>
/// Delivers notifications via the Alexa Media service (for announcements and TTS).
/// </summary>
public class AlexaMediaDeliveryStrategy : INotificationDeliveryStrategy
{
    private readonly IServices _services;

    public AlexaMediaDeliveryStrategy(IServices services)
    {
        _services = services;
    }

    public void Deliver(string formattedMessage, string entity, string notificationType, string eventId = "")
    {
        _services.Notify.AlexaMedia(formattedMessage, target: entity, data: new { type = notificationType });
    }
}

/// <summary>
/// Delivers notifications via the Alexa Actionable Notification script (for prompts).
/// </summary>
public class AlexaActionableNotificationDeliveryStrategy : INotificationDeliveryStrategy
{
    private readonly IServices _services;

    public AlexaActionableNotificationDeliveryStrategy(IServices services)
    {
        _services = services;
    }

    public void Deliver(string formattedMessage, string entity, string notificationType, string eventId = "")
    {
        _services.Mqtt.Publish("alexa_actionable_notification", $"{{\"text\": \"{formattedMessage}\", \"event\": \"{eventId}\"}}");
        _services.MediaPlayer.PlayMedia(ServiceTarget.FromEntity(entity), new MediaPlayerPlayMediaParameters() { Media = new { media_content_type = "skill", media_content_id = "amzn1.ask.skill.9fd6a51d-54f1-43ec-9a74-cd3fbecd1664" } });
    }
}

/// <summary>
/// Consolidates the common notification processing pipeline.
/// Handles message preparation, volume management, delivery, and timing.
/// </summary>
public class NotificationProcessor
{
    private readonly IEntities _entities;
    private readonly IScheduler _scheduler;
    private readonly VolumeManager _volumeManager;
    private readonly MessageFormatter _messageFormatter;
    private readonly IDictionary<string, AlexaDeviceConfig> _devices;
    private readonly ILogger<NotificationProcessor> _logger;

    public NotificationProcessor(
        IEntities entities,
        IScheduler scheduler,
        VolumeManager volumeManager,
        MessageFormatter messageFormatter,
        IDictionary<string, AlexaDeviceConfig> devices,
        ILogger<NotificationProcessor> logger)
    {
        _entities = entities;
        _scheduler = scheduler;
        _volumeManager = volumeManager;
        _messageFormatter = messageFormatter;
        _devices = devices;
        _logger = logger;
    }

    /// <summary>
    /// Processes a batch of notification configs targeting the same entities.
    /// </summary>
    /// <param name="cfgs">Notification configurations in arrival order</param>
    /// <param name="voice">The voice to use for message formatting</param>
    /// <param name="strategy">The delivery strategy (Alexa Media or Actionable Notification)</param>
    public async Task ProcessAsync(
        IEnumerable<Alexa.Config> cfgs,
        string voice,
        INotificationDeliveryStrategy strategy)
    {
        var entities = cfgs.First().Entities;
        var message = _messageFormatter.ConcatenateMessages(cfgs.Select(c => c.Message));

        // Extract merge policy for override fields (take last value in batch)
        var last = cfgs.Last();
        var notificationType = last.NotifyType;
        var eventId = last.EventId;

        var volumeOverride = cfgs.Select(c => c.VolumeLevel).LastOrDefault(v => v is not null);
        var delayOverride = cfgs.Select(c => c.VolumeResetDelay).LastOrDefault(v => v is not null);
        var whisperOverride = cfgs.Select(c => c.Whisper).LastOrDefault(v => v is not null);
        var useDefaultVoice = cfgs.Select(c => c.UseDefaultVoice).LastOrDefault();

        var entitiesVolumeLevel = new Dictionary<string, double>();                
        // Phase 1: Store current volumes and set delivery volumes
        foreach (var entity in entities)
        {
            _logger.LogDebug($"Store and set volumes for entity: {entity} with override volume: {volumeOverride}, delay: {delayOverride}, whisper: {whisperOverride}");
            _volumeManager.TryGetDeviceConfig(entity, out var deviceConfig);
            var (whisper, volume) = _volumeManager.GetVolumeDetailsForDevice(deviceConfig);
            _volumeManager.StoreCurrentVolume(entity, entitiesVolumeLevel);
            _volumeManager.SetVolume(entity, volumeOverride ?? volume);
        }
        _logger.LogDebug($"Completed volume setup for entities: {string.Join(", ", entities)}. Waiting for stabilization...");
        // Phase 2: Wait for volume stabilization
        await _scheduler.Sleep(AlexaProcessingConfig.VolumeSetupDelay);
        _logger.LogDebug($"Volume stabilized");

        // Phase 3: Deliver notifications to all entities
        foreach (var entity in entities)
        {
            _logger.LogDebug($"Delivering notification to entity: {entity}");
            _volumeManager.TryGetDeviceConfig(entity, out var deviceConfig);
            var (whisper, volume) = _volumeManager.GetVolumeDetailsForDevice(deviceConfig);
            var formatMessage = _messageFormatter.FormatMessage(message, voice, whisperOverride ?? whisper, useDefaultVoice);
            strategy.Deliver(formatMessage, entity, notificationType, eventId);
            _logger.LogDebug($"Delivered notification to entity: {entity} with message: {formatMessage}");
        }

        // Phase 4: Wait for message to finish and revert volume
        var words = _messageFormatter.GetWordCount(message);
        _logger.LogDebug($"Waiting for message to finish. Word count: {words}, calculated delay: {TimeSpan.FromSeconds(words * AlexaProcessingConfig.WordDelay)}, override delay: {delayOverride}");
        await _scheduler.Sleep(TimeSpan.FromSeconds(delayOverride ?? words * AlexaProcessingConfig.WordDelay));
        _logger.LogDebug($"Reverting volumes for entities: {string.Join(", ", entities)}");
        await _volumeManager.RestoreVolumesAsync(entitiesVolumeLevel);
        _logger.LogDebug($"Completed volume restoration for entities: {string.Join(", ", entities)}");
    }
}
