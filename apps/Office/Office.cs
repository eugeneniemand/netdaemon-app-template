using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.Helpers;
using Niemand.Helpers;

namespace Niemand;

[NetDaemonApp]
// [Focus]
public class Office : IAsyncInitializable, IAsyncDisposable
{
    private const    string             EugeneDesktopActive = "binary_sensor.eugene_desktop_active";
    private readonly IAlexa             _alexa;
    private readonly IEntities          _entities;
    private readonly IMqttEntityManager _entityManager;
    private readonly IHaContext         _haContext;
    private readonly ILogger<Office>    _logger;
    private readonly IScheduler         _scheduler;
    private readonly IServices          _services;
    private          IDisposable        _acSwitchOffDelaySchedule;
    private          IDisposable        _sleepDelaySchedule;

    public Office(IHaContext haContext, IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, IMqttEntityManager entityManager, ILogger<Office> logger)
    {
        _haContext     = haContext;
        _entities      = entities;
        _services      = services;
        _alexa         = alexa;
        _scheduler     = scheduler;
        _entityManager = entityManager;
        _logger        = logger;

        _logger.LogDebug("Office Started");
    }

    public async ValueTask DisposeAsync()
    {
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Office InitializeAsync");
        await SetupEugeneDesktopActiveSensor();

        // Computer Sensors
        _entities.Sensor.EugeneDesktopLastactive.StateChanges().Throttle(TimeSpan.FromMinutes(5), _scheduler)
                 .Subscribe(async s =>
                 {
                     _logger.LogDebug("EugeneDesktopLastactive Timeout");
                     await _entityManager.SetStateAsync(EugeneDesktopActive, "off");
                     SleepPcWhenUnoccupied();
                 });

        _entities.Sensor.EugeneDesktopLastactive.StateChanges().Sample(TimeSpan.FromMinutes(1), _scheduler)
                 .Subscribe(async s => { _logger.LogDebug("EugeneDesktopLastactive throttled log"); });

        _entities.Sensor.EugeneDesktopLastactive.StateChanges()
                 .Subscribe(async _ =>
                 {
                     await _entityManager.SetStateAsync(EugeneDesktopActive, "on");
                     _sleepDelaySchedule?.Dispose();
                 });

        // Motion Sensors
        _entities.BinarySensor.OfficeMotionOccupancy.StateChanges()
                 .WhenStateIsFor(s => s.IsOff(), TimeSpan.FromMinutes(5), _scheduler)
                 .Subscribe(s =>
                 {
                     SleepPcWhenUnoccupied();
                     TurnOffAcWhenDoorLeftOpen();
                 });

        _entities.BinarySensor.OfficeMotion.StateChanges().Where(s => s.New.IsOn()).Subscribe(_ =>
        {
            _sleepDelaySchedule?.Dispose();
            _acSwitchOffDelaySchedule?.Dispose();
        });

        _entities.BinarySensor.Study.StateChanges().Where(s => s.New.IsOn()).Subscribe(_ =>
        {
            _sleepDelaySchedule?.Dispose();
            _acSwitchOffDelaySchedule?.Dispose();
        });

        // Door Sensor
        _entities.BinarySensor.LumiLumiSensorMagnetAq2OnOff.StateChanges()
                 .WhenStateIsFor(s => s.IsOn(), TimeSpan.FromMinutes(2), _scheduler)
                 .Subscribe(_ => TurnOffAcWhenDoorLeftOpen());
        _entities.BinarySensor.LumiLumiSensorMagnetAq2OnOff.StateChanges()
                 .Where(s => s.New.IsOff())
                 .Subscribe(_ => _acSwitchOffDelaySchedule?.Dispose());
    }

    private async Task SetupEugeneDesktopActiveSensor()
    {
        if (_haContext.Entity(EugeneDesktopActive).State == null || string.Equals(_haContext.Entity(EugeneDesktopActive).State, "unavailable", StringComparison.InvariantCultureIgnoreCase))
            await _entityManager.CreateAsync(EugeneDesktopActive, new EntityCreationOptions(Name: "EugeneDesktopActive", DeviceClass: "connectivity", Persist: true, PayloadOn: "on", PayloadOff: "off"));


        await _entityManager.SetStateAsync(EugeneDesktopActive, "unknown");
    }

    private void SleepPcWhenUnoccupied()
    {
        if (!DateTime.TryParse(_entities.Sensor.EugeneDesktopLastactive.State, out var lastActive)) return;
        if ((DateTime.Now - lastActive).TotalMinutes <= 10) return;

        _alexa.Announce(_entities.MediaPlayer.Office.EntityId, "Your PC is about to sleep");
        _services.Notify.Eugene("Your PC will sleep in 30 seconds");
        _sleepDelaySchedule = _scheduler.Schedule(TimeSpan.FromSeconds(30), () => _entities.Button.EugeneDesktopSleep.Press());
    }

    private void TurnOffAcWhenDoorLeftOpen()
    {
        if (_entities.Climate.Office.IsUnavailable() || _entities.Climate.Office.IsOff()) return;

        _alexa.Announce(_entities.MediaPlayer.Downstairs.EntityId, "The Office AC will be turned off if the door is not closed");
        _acSwitchOffDelaySchedule = _scheduler.Schedule(TimeSpan.FromSeconds(60), () => _entities.Climate.Office.TurnOff());
    }
}