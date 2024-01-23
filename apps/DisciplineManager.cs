using NetDaemon.Extensions.MqttEntityManager;

namespace Niemand;

//[Focus]
[NetDaemonApp]
public class DisciplineManager : IAsyncInitializable, IDisposable
{
    private readonly Entities                   _entities;
    private readonly IMqttEntityManager         _entityManager;
    private readonly IHaContext                 _haContext;
    private readonly ILogger<DisciplineManager> _logger;
    private readonly string                     _switchDisciplineManagerEnabled = "switch.discipline_manager_enabled";

    public SwitchEntity DisciplineManagerSwitch;


    public DisciplineManager(IHaContext haContext, IMqttEntityManager entityManager, ILogger<DisciplineManager> logger)
    {
        _haContext     = haContext;
        _entityManager = entityManager;
        _logger        = logger;
        _entities      = new Entities(haContext);
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await CreateSwitch();

        DisciplineManagerSwitch.StateAllChanges()
                               .Where(s => s.New.IsOn())
                               .Subscribe(_ =>
                               {
                                   _entities.Switch.JaydenRaspberrypi.TurnOff();
                                   _entities.Switch.JaydenAppletv.TurnOff();
                                   _entities.Switch.JaydenIpad.TurnOff();
                               });

        DisciplineManagerSwitch.StateAllChanges()
                               .Where(s => s.New.IsOff())
                               .Subscribe(_ =>
                               {
                                   _entities.Switch.JaydenRaspberrypi.TurnOn();
                                   _entities.Switch.JaydenAppletv.TurnOn();
                                   _entities.Switch.JaydenIpad.TurnOn();
                               });

        _entities.MediaPlayer.LoungeTv.StateChanges()
                 .Where(s => s.New.IsOn())
                 .Subscribe(_ =>
                 {
                     if (DisciplineManagerSwitch.IsOff()) return;
                     _entities.MediaPlayer.LoungeTv.TurnOff();
                 });
    }

    public async void Dispose()
    {
        await _entityManager.RemoveAsync(_switchDisciplineManagerEnabled);
    }

    private async Task CreateSwitch()
    {
        const string payloadOn  = "on";
        const string payloadOff = "off";
        await _entityManager.CreateAsync(_switchDisciplineManagerEnabled, new EntityCreationOptions(Name: "Discipline Manager", DeviceClass: "switch", PayloadOn: payloadOn, PayloadOff: payloadOff));

        ( await _entityManager.PrepareCommandSubscriptionAsync(_switchDisciplineManagerEnabled) ).Subscribe(async s =>
        {
            _logger.LogInformation("setting state {switch}", _switchDisciplineManagerEnabled);
            await _entityManager.SetStateAsync(_switchDisciplineManagerEnabled, s);
        });

        DisciplineManagerSwitch = new SwitchEntity(_haContext, _switchDisciplineManagerEnabled);
    }
}