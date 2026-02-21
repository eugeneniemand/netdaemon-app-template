using Humanizer;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel.Integration;
using Niemand.Helpers;
using Niemand.Helpers.Notifications;
using Stateless.Graph;
using System;
using System.Text.Json;

namespace Niemand;

//[Focus]
[NetDaemonApp]
public class DisciplineManager : IAsyncInitializable, IDisposable
{
    private readonly Entities _entities;
    private readonly IMqttEntityManager _entityManager;
    private readonly TimerManager _timerManager;
    private readonly IScheduler _scheduler;
    private readonly IAlexa _alexa;
    private readonly IHaContext _haContext;
    private readonly ILogger<DisciplineManager> _logger;
    private readonly string _switchDisciplineManagerEnabled = "switch.discipline_manager_enabled";

    public SwitchEntity DisciplineManagerSwitch;

    public DisciplineManager(IHaContext haContext, IMqttEntityManager entityManager, TimerManager timerManager, IScheduler scheduler, IAlexa alexa, ILogger<DisciplineManager> logger)
    {
        _haContext = haContext;
        _entityManager = entityManager;
        _timerManager = timerManager;
        _scheduler = scheduler;
        _alexa = alexa;
        _logger = logger;
        _entities = new Entities(haContext);
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {        


        await CreateSwitch();
         
        _scheduler.SchedulePeriodic(TimeSpan.FromSeconds(30), () =>
        {
            if (_entities.Switch.DisciplineManagerEnabled.IsOff()) return;
            if (_entities.MediaPlayer.LoungeTv.IsOn())
                _entities.MediaPlayer.LoungeTv.TurnOff();
        });

        _entities.MediaPlayer.LoungeTv.StateChanges()
                 .Where(s => s.New.IsOn())
                 .Subscribe(_ =>
                 {
                     if (_entities.Switch.DisciplineManagerEnabled.IsOff()) return;
                     if (DisciplineManagerSwitch.IsOff()) return;
                     _entities.MediaPlayer.LoungeTv.TurnOff();
                     _alexa.Announce(new Alexa.Config() { Entity = "media_player.downstairs_2", VolumeLevel = 0.5, Message = "Nice try! The discipline manager is on and the Tele will continue turning off." });
                 });
        
        _entities.MediaPlayer.MasterTv2.StateChanges()
                 .Where(s => s.New.IsOn())
                 .Subscribe(_ =>
                 {
                     if (_entities.Switch.DisciplineManagerEnabled.IsOff()) return;
                     if (DisciplineManagerSwitch.IsOff()) return;
                     _entities.MediaPlayer.MasterTv2.TurnOff();
                     _alexa.Announce(new Alexa.Config() { Entity = "media_player.downstairs_2", VolumeLevel = 0.5, Message = "Nice try! The discipline manager is on and the Tele will continue turning off." });
                 });

        _entities.Switch.DisciplineManagerEnabled.StateChanges()
                 .Where(s => s.New.IsOn() && s.Old.IsOff())
                 .Subscribe(_ =>
                 {                     
                     _entities.MediaPlayer.LoungeTv.TurnOff();
                     _entities.MediaPlayer.MasterTv2.TurnOff();
                     _entities.Switch.NiemandKids.TurnOff();
                     _alexa.Announce(new Alexa.Config() { Entity = "media_player.downstairs_2", VolumeLevel = 0.5, Message = "Discipline manager turned on. Tele and Internet is off." });
                 });

        _entities.Switch.DisciplineManagerEnabled.StateChanges()
                 .Where(s => s.New.IsOff() && s.Old.IsOn())
                 .Subscribe(_ =>
                 {
                     _entities.Switch.NiemandKids.TurnOn();
                     _alexa.Announce(new Alexa.Config() { Entity = "media_player.downstairs_2", VolumeLevel = 0.5, Message = "Discipline manager turned off. Tele and Internet is available." });
                 });

        _entities.Timer.JaydenDiscipline.StateAllChanges()
                 .Where(s => s.Old.Attributes.Duration != s.New.Attributes.Duration)
                 .Throttle(TimeSpan.FromSeconds(5))
                 .Subscribe(NotifyDisciplineStateChanged);

        _entities.Timer.AaronDiscipline.StateAllChanges()
                 .Where(s => s.Old.Attributes.Duration != s.New.Attributes.Duration)
                 .Throttle(TimeSpan.FromSeconds(5))
                 .Subscribe(NotifyDisciplineStateChanged);
        
        _entities.Timer.GabrielDiscipline.StateAllChanges()
                 .Where(s => s.Old.Attributes.Duration != s.New.Attributes.Duration)
                 .Throttle(TimeSpan.FromSeconds(5))
                 .Subscribe(NotifyDisciplineStateChanged);       

        _haContext.RegisterServiceCallBack<TimerData>("increment_timer", async e =>
            { 
                if (e.entityId == null || e.value == null)
                {
                    _logger.LogWarning("Service called without timer name");
                    return;
                }
                _logger.LogInformation("Service called action: {Action} value: {value}", e.entityId, e.value);
                if (int.TryParse(e.value.ToString(), out var minutes))
                    await _timerManager.AddAsync(e.entityId, TimeSpan.FromMinutes(minutes));
                else
                {
                    _logger.LogError("Service called action: {Action} value: {value}", e.entityId, e.value);
                }
            }
        );  
    }
   

    private void NotifyDisciplineStateChanged(StateChange<TimerEntity, EntityState<TimerAttributes>> s)
    {
        var timeSpan = TimeSpan.Parse(s.New.Attributes.Duration.ToString() ?? "00:00:00");
        var childName = s.Entity.Attributes.FriendlyName.Replace("Discipline", "");
        var friendlyTime = timeSpan.Humanize(minUnit: TimeUnit.Minute);
        
        
        _logger.LogInformation("{childName}, your discipline timer is now {friendlyTime}", childName, friendlyTime);

        if (_entities.InputBoolean.Announcements.IsOff())
            return;

        var downstairs = _alexa.MediaPlayerEntityIdsForLabel("Downstairs");
        var upstairs = _alexa.MediaPlayerEntityIdsForLabel("Upstairs");
        var sfx = "";
        var message = "";

        if (timeSpan.TotalSeconds <= 0)
        {
            sfx = "<audio src=\"soundbank://soundlibrary/human/amzn_sfx_crowd_excited_cheer_01\"/>";
            message = $"{childName}, your discipline timer has finished";            
        }
        else
        {
            sfx = "<audio src=\"soundbank://soundlibrary/human/amzn_sfx_crowd_boo_01\"/>";
            message = $"{childName}, your discipline timer is now {friendlyTime}";
        }

        _alexa.TextToSpeech(new Alexa.Config() { Entities = downstairs, VolumeLevel = 0.5, Message = sfx, Whisper = false });
        _alexa.TextToSpeech(new Alexa.Config() { Entities = downstairs, VolumeLevel = 0.5, Message = message, Whisper = false });
        
        _alexa.TextToSpeech(new Alexa.Config() { Entities = upstairs, VolumeLevel = 0.5, Message = sfx, Whisper = false });
        _alexa.TextToSpeech(new Alexa.Config() { Entities = upstairs, VolumeLevel = 0.5, Message = message, Whisper = false });
    }

    record TimerData(string? entityId, object? value);

    public async void Dispose()
    {
        await _entityManager.RemoveAsync(_switchDisciplineManagerEnabled);
    }

    private async Task CreateSwitch()
    {
        const string payloadOn = "on";
        const string payloadOff = "off";
        await _entityManager.CreateAsync(_switchDisciplineManagerEnabled, new EntityCreationOptions(Name: "Discipline Manager", DeviceClass: "switch", PayloadOn: payloadOn, PayloadOff: payloadOff));

        (await _entityManager.PrepareCommandSubscriptionAsync(_switchDisciplineManagerEnabled)).Subscribe(async s =>
        {
            _logger.LogInformation("setting state {switch}", _switchDisciplineManagerEnabled);
            await _entityManager.SetStateAsync(_switchDisciplineManagerEnabled, s);
        });

        DisciplineManagerSwitch = new SwitchEntity(_haContext, _switchDisciplineManagerEnabled);
    }   
}