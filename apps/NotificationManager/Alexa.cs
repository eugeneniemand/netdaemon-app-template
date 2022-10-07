using System.Diagnostics.CodeAnalysis;

namespace Niemand;

[SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
public class Alexa
{
    private readonly IHaContext _haContext;
    private readonly ILogger    _logger;
    private readonly IScheduler _scheduler;
    private readonly Services   _services;

    public Alexa(IHaContext haContext, IScheduler scheduler, ILogger<Alexa> logger)
    {
        _haContext = haContext;
        _scheduler = scheduler;
        _logger    = logger;
        _services  = new Services(haContext);

        SetupResponseHandler(haContext);
    }

    private MediaPlayerEntity? LastCalledMediaPlayerEntity => _haContext
                                                              .GetAllEntities()
                                                              .Where(e => e != null && e.EntityId.StartsWith("media_player."))
                                                              .Select(e => new MediaPlayerEntity(_haContext, e!.EntityId))
                                                              .FirstOrDefault(e => e.Attributes?.LastCalled != null && (bool)e.Attributes.LastCalled);

    public event EventHandler<NotificationResponseEvent> ResponseReceived;

    public void SendNotification(Prompt notification)
    {
        SetVolumeLevel(notification);
        _services.Script.ActivateAlexaActionableNotification(GetAnnouncementSSML(notification), notification.EventId, TargetMediaPlayerEntity(notification).EntityId);
    }

    public void SendNotification(Announcement notification)
    {
        SetVolumeLevel(notification);
        _services.Notify.AlexaMedia(GetAnnouncementSSML(notification), null, TargetMediaPlayerEntity(notification).EntityId, new { type = "announce" });
    }

    public void SendNotification(TextToSpeech notification)
    {
        SetVolumeLevel(notification);
        _services.Notify.AlexaMedia(GetAnnouncementSSML(notification), null, TargetMediaPlayerEntity(notification).EntityId, new { type = "tts" });
    }

    public MediaPlayerEntity TargetMediaPlayerEntity(Notification notification)
    {
        var lastUpdateSeconds = ( DateTime.Now - DateTime.UnixEpoch.AddMilliseconds((double)LastCalledMediaPlayerEntity.Attributes.LastCalledTimestamp) ).Seconds;
        if (notification.Target == null || lastUpdateSeconds > 10) _services.AlexaMedia.UpdateLastCalled();
        return notification.Target ?? LastCalledMediaPlayerEntity;
    }

    private static string GetAnnouncementSSML(Notification notification)
    {
        var announcement = notification.Announcement
                                       .Replace(",", "<break time='250ms' />")
                                       .Replace(".", "<break time='500ms' />");

        return notification.Whisper
            ? $"<amazon:effect name='whispered'>{announcement}</amazon:effect>"
            : $"<voice name='Emma'>{announcement}</voice>";
    }

    private void SetupResponseHandler(IHaContext haContext)
    {
        haContext.Events.Filter<NotificationResponseEvent>("alexa_actionable_notification").Subscribe(responseEvent =>
        {
            _logger.LogInformation("Event(alexa_actionable_notification): {EventId} - {Response} - {ResponseType}", responseEvent.Data?.EventId, responseEvent.Data?.Response, responseEvent.Data?.ResponseType);
            ResponseReceived?.Invoke(this, responseEvent.Data);
        });
    }

    private void SetVolumeLevel(Notification notification)
    {
        if (TargetMediaPlayerEntity(notification).Attributes.VolumeLevel != notification.VolumeLevel)
            _services.MediaPlayer.VolumeSet(ServiceTarget.FromEntity(TargetMediaPlayerEntity(notification).EntityId), notification.VolumeLevel);
    }
}