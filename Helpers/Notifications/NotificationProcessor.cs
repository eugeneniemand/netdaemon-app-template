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
        _services.Script.ActivateAlexaActionableNotification(formattedMessage, eventId, entity);
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

    public NotificationProcessor(
        IEntities entities,
        IScheduler scheduler,
        VolumeManager volumeManager,
        MessageFormatter messageFormatter,
        IDictionary<string, AlexaDeviceConfig> devices)
    {
        _entities = entities;
        _scheduler = scheduler;
        _volumeManager = volumeManager;
        _messageFormatter = messageFormatter;
        _devices = devices;
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

        var entitiesVolumeLevel = new Dictionary<string, double>();

        // Phase 1: Store current volumes and set delivery volumes
        foreach (var entity in entities)
        {
            _volumeManager.TryGetDeviceConfig(entity, out var deviceConfig);
            var (whisper, volume) = _volumeManager.GetVolumeDetailsForDevice(deviceConfig);
            _volumeManager.StoreCurrentVolume(entity, entitiesVolumeLevel);
            _volumeManager.SetVolume(entity, volumeOverride ?? volume);
        }

        // Phase 2: Wait for volume stabilization
        await _scheduler.Sleep(AlexaProcessingConfig.VolumeSetupDelay);

        // Phase 3: Deliver notifications to all entities
        foreach (var entity in entities)
        {
            _volumeManager.TryGetDeviceConfig(entity, out var deviceConfig);
            var (whisper, volume) = _volumeManager.GetVolumeDetailsForDevice(deviceConfig);
            var formatMessage = _messageFormatter.FormatMessage(message, voice, whisperOverride ?? whisper);
            strategy.Deliver(formatMessage, entity, notificationType, eventId);
        }

        // Phase 4: Wait for message to finish and revert volume
        var words = _messageFormatter.GetWordCount(message);
        await _scheduler.Sleep(TimeSpan.FromSeconds(delayOverride ?? words * AlexaProcessingConfig.WordDelay));
        await _volumeManager.RestoreVolumesAsync(entitiesVolumeLevel);
    }
}
