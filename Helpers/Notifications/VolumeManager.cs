using NetDaemon.HassModel.Entities;

namespace Niemand.Helpers.Notifications;

/// <summary>
/// Manages volume operations for Alexa media players.
/// Handles retrieving, storing, setting, and reverting volume levels.
/// </summary>
public class VolumeManager
{
    private readonly IHaContext _ha;
    private readonly IServices _services;
    private readonly IEntities _entities;
    private readonly IDictionary<string, AlexaDeviceConfig> _devices;

    public VolumeManager(
        IHaContext ha,
        IServices services,
        IEntities entities,
        IDictionary<string, AlexaDeviceConfig> devices)
    {
        _ha = ha;
        _services = services;
        _entities = entities;
        _devices = devices;
    }

    /// <summary>
    /// Gets the current volume level of a media player entity.
    /// </summary>
    /// <returns>Volume level between 0 and 1, or -1 if not available.</returns>
    public double GetCurrentVolume(string entityId)
    {
        object? vol = null;
        _ha.Entity(entityId).Attributes?.ToDictionary()?.TryGetValue("volume_level", out vol);
        return double.Parse(vol?.ToString() ?? "-1");
    }

    /// <summary>
    /// Gets the appropriate volume level and whisper setting based on device config and house mode.
    /// </summary>
    public (bool whisper, double volume) GetVolumeDetailsForDevice(AlexaDeviceConfig? deviceConfig)
    {
        var whisper = false;
        var volume = 0d;

        switch (_entities.InputSelect.HouseMode.State)
        {
            case "night":
                whisper = deviceConfig?.NightWhisper ?? true;
                volume = deviceConfig?.NightVolume ?? 0.2d;
                break;
            case "day":
                whisper = false;
                volume = deviceConfig?.DayVolume ?? 0.4d;
                break;
        }

        return (whisper, volume);
    }

    /// <summary>
    /// Sets the volume for a media player entity.
    /// </summary>
    public void SetVolume(string entityId, double volumeLevel)
    {
        _services.MediaPlayer.VolumeSet(
            ServiceTarget.FromEntity(entityId),
            new MediaPlayerVolumeSetParameters { VolumeLevel = volumeLevel });
    }

    /// <summary>
    /// Stores the current volume of an entity for later restoration.
    /// </summary>
    public void StoreCurrentVolume(string entityId, IDictionary<string, double> volumeDictionary)
    {
        volumeDictionary.Add(entityId, GetCurrentVolume(entityId));
    }

    /// <summary>
    /// Restores previously saved volume levels for all entities.
    /// </summary>
    public async Task RestoreVolumesAsync(Dictionary<string, double> savedVolumes)
    {
        foreach (var (entity, volume) in savedVolumes)
        {
            if (volume < 0) continue; // Skip if volume was not available
            SetVolume(entity, volume);
        }
    }

    /// <summary>
    /// Gets the device configuration for an entity, if it exists.
    /// </summary>
    public bool TryGetDeviceConfig(string entityId, out AlexaDeviceConfig? config)
    {
        return _devices.TryGetValue(entityId, out config);
    }
}
