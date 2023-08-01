namespace Niemand;

[NetDaemonApp]
//[Focus]
public class Office
{
    private readonly IAlexa      _alexa;
    private readonly IEntities   _entities;
    private readonly IScheduler  _scheduler;
    private readonly IServices   _services;
    private          IDisposable _sleepDelaySchedule;

    public Office(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler)
    {
        _entities  = entities;
        _services  = services;
        _alexa     = alexa;
        _scheduler = scheduler;

        entities.BinarySensor.OfficeMotionOccupancy.StateChanges()
                .WhenStateIsFor(s => s.IsOff(), TimeSpan.FromMinutes(5), scheduler)
                .Subscribe(s => Setup());

        entities.BinarySensor.OfficeMotion.StateChanges().Where(s => s.New.IsOn()).Subscribe(_ => _sleepDelaySchedule?.Dispose());
        entities.BinarySensor.Study.StateChanges().Where(s => s.New.IsOn()).Subscribe(_ => _sleepDelaySchedule?.Dispose());
    }

    private void Setup()
    {
        if (_entities.Sensor.TemplateLastMotion.State == "Office Motion" ||
            _entities.BinarySensor.EugenesMacbookActive.IsOn() ||
            _entities.BinarySensor.HaileysMacbookAirActive.IsOn()) return;

        _alexa.Announce(_entities.MediaPlayer.Office.EntityId, "Your PC is about to sleep");
        _services.Notify.Eugene("Your PC will sleep in 30 seconds");
        _sleepDelaySchedule = _scheduler.Schedule(TimeSpan.FromSeconds(30), () => _entities.Button.EugeneDesktopSleep.Press());
    }
}