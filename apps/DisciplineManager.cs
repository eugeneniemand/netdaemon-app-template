using NetDaemon.Extensions.MqttEntityManager;
using Niemand.Helpers;

namespace Niemand;

//[Focus]
[NetDaemonApp]
public class DisciplineManager : IAsyncInitializable, IDisposable
{
    private readonly Entities _entities;
    private readonly IMqttEntityManager _entityManager;
    private readonly IScheduler _scheduler;
    private readonly IAlexa _alexa;
    private readonly IHaContext _haContext;
    private readonly ILogger<DisciplineManager> _logger;
    private readonly string _switchDisciplineManagerEnabled = "switch.discipline_manager_enabled";

    public SwitchEntity DisciplineManagerSwitch;


    public DisciplineManager(IHaContext haContext, IMqttEntityManager entityManager, IScheduler scheduler, IAlexa alexa, ILogger<DisciplineManager> logger)
    {
        _haContext = haContext;
        _entityManager = entityManager;
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
                     _entities.Switch.NiemandKids.TurnOff();
                 });

        _entities.Switch.DisciplineManagerEnabled.StateChanges()
                 .Where(s => s.New.IsOn() && s.Old.IsOff())
                 .Subscribe(_ =>
                 {                     
                     _entities.MediaPlayer.LoungeTv.TurnOff();
                     //_entities.Switch.NiemandKids.TurnOff();
                     _alexa.Announce(new Alexa.Config() { Entity = "media_player.downstairs_2", VolumeLevel = 0.5, Message = "Discipline manager turned on. Tele is off." });
                 });

        _entities.Switch.DisciplineManagerEnabled.StateChanges()
                 .Where(s => s.New.IsOff() && s.Old.IsOn())
                 .Subscribe(_ =>
                 {
                     //_entities.Switch.NiemandKids.TurnOn();
                     _alexa.Announce(new Alexa.Config() { Entity = "media_player.downstairs_2", VolumeLevel = 0.5, Message = "Discipline manager turned off. Tele is available again." });
                 });
    }

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