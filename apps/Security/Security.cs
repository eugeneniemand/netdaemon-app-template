using HomeAssistantGenerated;
using Microsoft.Win32;
using NetDaemon.Extensions.Observables;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Integration;
using NetDaemon.Helpers;
using Niemand.Helpers;
using Niemand.Helpers.Notifications;
using Reactive.Boolean;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Niemand.SecurityApps;

[NetDaemonApp]
[Focus]
public class Security(IHaContext ha, IHaRegistry registry, IEntities entities, IServices services, ILogger<Security> logger, IAlexa alexa, IScheduler scheduler, Common common, PushNotifier pushNotifier) : IAsyncInitializable
{
    private readonly List<BinarySensorEntity> DoorsOpened = new();
    record AlarmArmFailedData(string? message);

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        scheduler.ScheduleCron("0/5 19-23,0-6 * * *", () => ArmAlarm());
        AlarmFailure();
        AlarmTriggered();

        SimulateNightLights();
        DrivewayMotionAlarm();
        DoorBeep();
        DoorWatchdog();

        //var x = new DoorWatchdogService();
    }

    private void DoorWatchdog()
    {
        var doors = new[]
        {
            entities.BinarySensor.BackDoor,
            entities.BinarySensor.DiningDoor,
            entities.BinarySensor.GarageBackDoor,
            entities.BinarySensor.LoungeDoor
        };

        doors.StateChanges()
            .Where(e => e.New?.IsOn() ?? true)
            .Subscribe(e =>
            {
                if (!DoorsOpened.Contains(e.Entity))
                {
                    DoorsOpened.Add(e.Entity);
                    logger.LogInformation("Added open door: {door}", e.Entity.EntityId);
                }
            });

        void NotifyUncheckedDoors()
        {
            foreach (var door in DoorsOpened)
            {
                logger.LogInformation("Notifying door check: {door}", door.EntityId);
                services.Notify.Eugene(new NotifyEugeneParameters()
                {
                    Message = $"Is the {door.Attributes.FriendlyName} locked?",
                    Data = new
                    {
                        inline_keyboard = new List<string> {
                            $"🔒Locked:/locked {door.EntityId}, 🙈Ignore:/ignore {door.EntityId}" ,
                            $"⏳Defer 5 min:/defer {door.EntityId} 5, ⏳Defer 30 min:/defer {door.EntityId} 30",
                            $"⏳Defer 1 hour:/defer {door.EntityId} 60, ⏳Defer 6 hours:/defer {door.EntityId} 360"
                        }
                    }
                });
            }
        }

        doors.StateChanges()
            .WhenStateIsFor(e => e.IsOff(), TimeSpan.FromMinutes(5), scheduler)
            .Subscribe(e => NotifyUncheckedDoors());

        entities.AlarmControlPanel.Alarmo
                    .StateChanges()
                    .Where(e => e.Entity.IsArmed())
                    .Subscribe(e => NotifyUncheckedDoors());

        bool DoorInList(TelegramCallback e, out BinarySensorEntity? door)
        {
            door = DoorsOpened.FirstOrDefault(d => d.EntityId == e?.Args[0]);
            return door != null;

        }

        ha.Events.HandleTelegramCallback(services.TelegramBot, logger)
            .On("/locked", (e, handler) =>
            {
                if (!DoorInList(e, out var door)) return;

                DoorsOpened.Remove(door);

                handler.EditMessage(e, $"Acknowledged {door.Attributes?.FriendlyName} is locked");
            }).On("/defer", (e, handler) =>
            {
                if (!DoorInList(e, out var door) || !int.TryParse(e.Args[1], out int minutes)) return;

                handler.EditMessage(e, $"I'll remind you in {minutes} minutes to check {door.Attributes?.FriendlyName}");

                scheduler.Schedule(TimeSpan.FromMinutes(minutes), () => NotifyUncheckedDoors());
            }).On("/ignore", (e, handler) =>
            {
                if (!DoorInList(e, out var door)) return;

                DoorsOpened.Remove(door);

                handler.EditMessage(e, $"{door.Attributes?.FriendlyName} check ignored🤦‍");
            });
    }
    private void AlarmTriggered()
    {
        entities.AlarmControlPanel.Alarmo
                    .StateChanges()
                    .Where(e => e.Entity.IsTriggered())
                    .Subscribe(ha =>
                    {
                        logger.LogInformation("Alarm Triggered");
                        pushNotifier.Notify(PushNotifier.Recipient.All, "🚨 Alarm Triggered 🚨", "Alarm triggered", 1, true, "Anticipate.caf");
                    });
    }

    private void AlarmFailure()
    {
        ha.RegisterServiceCallBack<AlarmArmFailedData>("alarm_arm_failed", (data) =>
        {
            logger.LogDebug("Alarm Arm Failed: {data}", data.message);

            pushNotifier.Notify(PushNotifier.Recipient.All, "Alarm Failed", data.message ?? "Alarm failed to arm", 0.5, true, "shake.caf");            
            alexa.Announce(new Alexa.Config() { Entity = entities.MediaPlayer.Master.EntityId, Message = "Alarm failed to arm", NotifyType = "tts" });
        });
    }



    private void DoorBeep()
    {
        var doors = new[]
        {
            entities.BinarySensor.BackDoor,
            entities.BinarySensor.FrontDoor,
            entities.BinarySensor.DiningDoor,
            entities.BinarySensor.GarageBackDoor,
            entities.BinarySensor.LoungeDoor
        };

        doors.StateChanges()
            .Where(change => change.New.IsOn())
            .Subscribe(change =>
            {
                logger.LogDebug("Door Opened: {door}", change.Entity.EntityId);
                entities.Switch.AlarmBeepTwo.TurnOn();
            });
    }

    private void DrivewayMotionAlarm()
    {
        var cameras_person = new[]
                {
            entities.BinarySensor.NiemandFrontDoorMotion,
            entities.BinarySensor.NiemandDriveMotion,
        };

        var cameras_motion = new[]
        {
            entities.Sensor.NiemandFrontDoorLastMotion,
            entities.Sensor.NiemandDriveLastMotion,
        };

        cameras_motion.StateChanges()
             .SubscribeAsync(async change =>
             {
                 if (entities.AlarmControlPanel.Alarmo.IsDisarmed())
                     return;

                 logger.LogDebug("Motion Detected: {camera}", change.Entity.EntityId);
                 alexa.TextToSpeech(new Alexa.Config()
                 {
                     Entity = entities.MediaPlayer.EugeneS5thEchoDot.EntityId, // Garage
                     VolumeLevel = 1,
                     VolumeResetDelay = 30,
                     Message = "<audio src=\"soundbank://soundlibrary/scifi/amzn_sfx_scifi_alarm_03\"/>Warning, motion detected on Drive",
                     NotifyType = "tts",
                     Whisper = false
                 });
                 await scheduler.Sleep(TimeSpan.FromSeconds(7));
                 alexa.TextToSpeech(new Alexa.Config()
                 {
                     Entity = entities.MediaPlayer.EugeneS5thEchoDot.EntityId, // Garage
                     VolumeLevel = 1,
                     VolumeResetDelay = 30,
                     Message = "<audio src=\"soundbank://soundlibrary/scifi/amzn_sfx_scifi_alarm_03\"/>Warning, motion detected on Drive",
                     NotifyType = "tts",
                     Whisper = false
                 });
             });

        cameras_person.StateChanges()
             .Where(change => change.New.IsOn())
             .SubscribeAsync(async change =>
             {
                 if (entities.AlarmControlPanel.Alarmo.IsDisarmed())
                     return;

                 pushNotifier.Notify(PushNotifier.Recipient.All, "🚨 Person On Drive 🚨", "There is a person on the drive", 1, true, "Anticipate.caf");

                 logger.LogDebug("Person Detected: {camera}", change.Entity.EntityId);
                 alexa.TextToSpeech(new Alexa.Config()
                 {
                     Entity = entities.MediaPlayer.EugeneS5thEchoDot.EntityId, // Garage
                     VolumeLevel = 1,
                     VolumeResetDelay = 30,
                     Message = "<audio src=\"soundbank://soundlibrary/scifi/amzn_sfx_scifi_alarm_03\"/>Alert, Alert, person detected on Drive",
                     NotifyType = "tts",
                     Whisper = false
                 });
                 await scheduler.Sleep(TimeSpan.FromSeconds(7));
                 alexa.TextToSpeech(new Alexa.Config()
                 {
                     Entity = entities.MediaPlayer.EugeneS5thEchoDot.EntityId, // Garage
                     VolumeLevel = 1,
                     VolumeResetDelay = 30,
                     Message = "<audio src=\"soundbank://soundlibrary/scifi/amzn_sfx_scifi_alarm_03\"/>Alert, Alert, person detected on Drive",
                     NotifyType = "tts",
                     Whisper = false
                 });
                 await scheduler.Sleep(TimeSpan.FromSeconds(30));
                 entities.Light.OutsideGarage.TurnOn();
                 alexa.TextToSpeech(new Alexa.Config()
                 {
                     Entity = entities.MediaPlayer.Master.EntityId,
                     VolumeLevel = 0.2,
                     VolumeResetDelay = 30,
                     Message = "Alert, person detected on Drive",
                     NotifyType = "tts",
                     Whisper = false
                 });
                 await scheduler.Sleep(TimeSpan.FromSeconds(300));
                 entities.Light.OutsideGarage.TurnOff();
             });
    }

    private void SimulateNightLights()
    {
        var nightTime = entities.Sun.Sun
            .ToBooleanObservable(e => e.Attributes.Elevation <= 1);

        var armedAway = entities.AlarmControlPanel.Alarmo
            .ToBooleanObservable(s => s.IsArmedAway());

        nightTime.AndOp(armedAway)
            .SubscribeTrueFalse(
                () => entities.Switch.SimulateAllLights.TurnOn(),
                () => entities.Switch.SimulateAllLights.TurnOff()
        );
    }

    private void ArmAlarm()
    {
        if (entities.AlarmControlPanel.Alarmo.IsArmed())
            return;

        logger.LogDebug("MotionSensors.LastWasUpstairs: {bool}", common.MotionSensors.LastWasUpstairs);
        logger.LogDebug("MotionSensors.DownstairsClear: {bool}", common.MotionSensors.DownstairsClear);
        logger.LogDebug("MotionSensors.LastDownstairs.LastChangedOlderThan: {bool}", common.MotionSensors.LastDownstairs.LastChangedOlderThan(TimeSpan.FromMinutes(5)));
        logger.LogDebug("MotionSensors.EugeneDesktopLastactive.LastChangedOlderThan: {bool}", entities.Sensor.EugeneDesktopLastactive.LastChangedOlderThan(TimeSpan.FromMinutes(5)));
        logger.LogDebug("MotionSensors.LastDownstairs: {EntityId}", common.MotionSensors.LastDownstairs.EntityId);

        // If everyone is upstairs and the last motion and not in the office and desktop is inactive, arm the alarm
        if (common.MotionSensors.LastWasUpstairs
          && common.MotionSensors.DownstairsClear
          && ((common.MotionSensors.LastDownstairs.LastChangedOlderThan(TimeSpan.FromMinutes(5)) // Last downstairs motion was more than 5 minutes ago
          && entities.Sensor.EugeneDesktopLastactive.LastChangedOlderThan(TimeSpan.FromMinutes(5)) // Eugene Desktop is idle
          && entities.MediaPlayer.LoungeTv.IsOff() // Tv is off
          && LastDownstairsWasLanding(entities, common)) || (DateTime.Now.Hour > 1 && DateTime.Now.Hour < 6)))
        {
            entities.AlarmControlPanel.Alarmo.AlarmArmNight();

            foreach (var light in entities.Light.EnumerateAll().Where(e => e.Registration?.Labels?.Any(label => string.Equals(label.Id, "Downstairs", StringComparison.OrdinalIgnoreCase)) == true))
            {
                light.TurnOff();
            }
        }

        static bool LastDownstairsWasLanding(IEntities entities, Common common)
        {
            return new[]
                      {
            entities.BinarySensor.Landing.EntityId,
            entities.BinarySensor.LandingMotion.EntityId
          }.Contains(common.MotionSensors.LastDownstairs.EntityId);
        }
    }
}