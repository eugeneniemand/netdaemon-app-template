using NetDaemon.Helpers;
using Niemand.Helpers;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class AlarmClock
{
    private readonly IAlexa       _alexa;
    private readonly IEntities    _entities;
    private readonly IScheduler   _scheduler;
    private          IDisposable? _alarmScheduler;

    public AlarmClock(IEntities entities, IAlexa alexa, IScheduler scheduler)
    {
        _entities  = entities;
        _alexa     = alexa;
        _scheduler = scheduler;

        entities.InputBoolean.AlarmClockEnabled.StateChanges().Subscribe(s => SetAlarmClock());
        entities.InputDatetime.AlarmClockTime.StateChanges().Subscribe(s => SetAlarmClock());
        //TriggerAlarm();
    }

    public DateTimeOffset AlarmDue => DateTimeOffset.Parse(_entities.InputDatetime.AlarmClockTime.State!);

    private void SetAlarmClock()
    {
        if (_entities.InputBoolean.AlarmClockEnabled.IsOn())
        {
            var minute = AlarmDue.Minute;
            var hour   = AlarmDue.Hour;
            _alarmScheduler = _scheduler.ScheduleCron($"{minute}/2 {hour} * * 1-5", TriggerAlarm);
        }

        if (_entities.InputBoolean.AlarmClockEnabled.IsOff())
            _alarmScheduler?.Dispose();
    }

    private void TriggerAlarm()
    {
        if (_entities.BinarySensor.MasterMotion.EntityState!.LastChanged!.Value.ToUniversalTime().TimeOfDay < AlarmDue.ToUniversalTime().TimeOfDay) _alexa.Announce(new Alexa.Config { Entities = _entities.MediaPlayer.AlarmClockDevices.Attributes.EntityId.ToList(), Message = $"Wakey Wakey, its time to get up. It is {DateTime.Now.ToLocalTime():dddd, HH mm}", VolumeLevel = 0.3, VolumeResetDelay = 9, Whisper = false });
    }
}