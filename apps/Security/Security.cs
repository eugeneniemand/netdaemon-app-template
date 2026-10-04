using Microsoft.Reactive.Testing;
using CodeCasa.NetDaemon.Extensions.Observables;
using NetDaemon.HassModel.Integration;
using Niemand.Helpers;
using Niemand.Helpers.Notifications;
using Reactive.Boolean;
using System.Reactive.Linq;

namespace Niemand.SecurityApps;

[NetDaemonApp]
//[Focus]
public class Security(IHaContext ha, IEntities entities, IServices services, ILogger<Security> logger, IAlexa alexa, IScheduler scheduler, Common common, PushNotifier pushNotifier, TelegramBotServices bot) : IAsyncInitializable
{
    private readonly List<BinarySensorEntity> DoorsOpened = new();
    private readonly Dictionary<string, TelegramChatMessage> DoorsMessagesSent = new();
    record AlarmArmFailedData(string? message);

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        //scheduler.ScheduleCron("0/5 19-23,0-6 * * *", () => ArmAlarm());
        ArmAlarm();
        AlarmStateChanged();
        AlarmFailure();
        AlarmTriggered();

        //SimulateNightLights();
        DrivewayMotionAlarm();
        DoorBeep();
        // LastMotionSensorWatchdog removed - Common.MotionEntities now tracks last sensors reactively
        //DoorWatchdog();
        //await DebugMethod();
    }

    public void Beep(int beeps = 1, int delay = 100)
    {
        BeepAsync(beeps, delay).GetAwaiter().GetResult();
    }


    private async Task BeepAsync(int beeps = 1, int delay = 100)
    {
        for (int i = 0; i < beeps; i++)
        {
            entities.Switch.KonnectedMainSiren.TurnOn();
            await Task.Delay(delay);
            entities.Switch.KonnectedMainSiren.TurnOff();
            await Task.Delay(delay);
        }
    }

    private async Task DebugMethod()
    {
        var door = new
        {
            Attributes = new { FriendlyName = "DebugDoor" },
            EntityId = "binary_sensor.debug_door"
        };

        var serviceData = new
        {
            target = 1431752361, // Replace with your Telegram chat ID
            message = $"Is the {door.Attributes.FriendlyName} locked?",
            inline_keyboard = new List<string> {
                        $"🔒Locked:/locked {door.EntityId}, 🙈Ignore:/ignore {door.EntityId}" ,
                        $"⏳Defer 5 min:/defer {door.EntityId} 5, ⏳Defer 30 min:/defer {door.EntityId} 30",
                        $"⏳Defer 1 hour:/defer {door.EntityId} 60, ⏳Defer 6 hours:/defer {door.EntityId} 360"
                    }

        };

        var jsonResult = await ha.CallServiceWithResponseAsync("telegram_bot", "send_message", null, serviceData);
        var chatMessages = JsonSerializer.Deserialize<TelegramChats>(jsonResult.ToString());

        DoorsMessagesSent.Add(door.EntityId, chatMessages.Chats[0]);

        if (DoorsMessagesSent.ContainsKey(door.EntityId))
        {
            var chat = DoorsMessagesSent[door.EntityId];
            bot.DeleteMessage(new TelegramBotDeleteMessageParameters() {  MessageId = chat.MessageId.ToString() });

            await ha.CallServiceWithResponseAsync("telegram_bot", "send_message", null, serviceData);
        }
    }

    private void DoorWatchdog()
    {
        var doors = new[]
        {
            entities.BinarySensor.BackDoor,
            entities.BinarySensor.KonnectedFrontDoor,
            entities.BinarySensor.KonnectedUtilityDoor,
            entities.BinarySensor.BackOfficeDoor
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

                NotifyEugeneParameters data = new NotifyEugeneParameters()
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
                };
                services.Notify.Eugene(data);
                var result = ha.CallServiceWithResponseAsync("notify", "eugene", null, data);
            }
        }

        doors.StateChanges()
            .WhenStateIsFor(e => e.IsOff(), TimeSpan.FromMinutes(5), scheduler)
            .Subscribe(e => NotifyUncheckedDoors());

        entities.AlarmControlPanel.Alarmo
                    .StateChanges()
                    .Where(e => e.Entity.IsArmed())
                    .Subscribe(e => NotifyUncheckedDoors());

        bool DoorInList(TelegramCallback e, out BinarySensorEntity? door, out string entityId)
        {
            entityId = e?.Args[0] ?? "UNKNOWN";
            door = DoorsOpened.FirstOrDefault(d => d.EntityId == e?.Args[0]);
            return door != null;

        }

        ha.Events.HandleTelegramCallback(services.TelegramBot, logger)
            .On("/locked", (e, handler) =>
            {
                if (!DoorInList(e, out var door, out var entityId))
                {
                    handler.EditMessage(e, $"{entityId} has expired🤦‍");
                    return;
                }

                DoorsOpened.Remove(door);

                handler.EditMessage(e, $"Acknowledged {door.Attributes?.FriendlyName} is locked");
            }).On("/defer", (e, handler) =>
            {
                if (!DoorInList(e, out var door, out var entityId) || !int.TryParse(e.Args[1], out int minutes))
                {
                    handler.EditMessage(e, $"{entityId} has expired🤦‍");
                    return;
                }

                handler.EditMessage(e, $"I'll remind you in {minutes} minutes to check {door.Attributes?.FriendlyName}");

                scheduler.Schedule(TimeSpan.FromMinutes(minutes), () => NotifyUncheckedDoors());
            }).On("/ignore", (e, handler) =>
            {
                if (!DoorInList(e, out var door, out var entityId))
                {
                    handler.EditMessage(e, $"{entityId} has expired🤦‍");
                    return;
                }

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

    private void AlarmStateChanged()
    {
        entities.AlarmControlPanel.Alarmo
                    .StateChanges()
                    .Subscribe(ha =>
                    {
                        if (ha.New.IsArmed())
                            NotifyArmed();
                        if (ha.New.IsDisarmed())
                            NotifyDisarmed();
                    });


    }

    private void NotifyDisarmed()
    {
        if (entities.InputSelect.HouseMode.State == "night")
            alexa.TextToSpeech(new Alexa.Config() { Entity = entities.MediaPlayer.Playroom.EntityId, Message = "Alarm disarmed", VolumeLevel = 0.3, Whisper = false });
        else
            Beep(3);
    }

    private void NotifyArmed()
    {
        if (entities.InputSelect.HouseMode.State == "night")
            alexa.TextToSpeech(new Alexa.Config() { Entity = entities.MediaPlayer.Playroom.EntityId, Message = "Alarm armed", VolumeLevel = 0.3, Whisper = false });
        else
            Beep(2);
    }

    private void AlarmFailure()
    {
        ha.RegisterServiceCallBack<AlarmArmFailedData>("alarm_arm_failed", (data) =>
        {
            logger.LogDebug("Alarm Arm Failed: {data}", data.message);
            Beep(3, 300);
            pushNotifier.Notify(PushNotifier.Recipient.All, "Alarm Failed", data.message ?? "Alarm failed to arm", 0.5, true, "shake.caf");
            alexa.Announce(new Alexa.Config() { Entity = entities.MediaPlayer.Master.EntityId, Message = "Alarm failed to arm", NotifyType = "tts" });
        });
    }



    private void DoorBeep()
    {
        var doors = new[]
        {
            entities.BinarySensor.BackDoor,
            entities.BinarySensor.KonnectedFrontDoor,
            entities.BinarySensor.KonnectedUtilityDoor,
            entities.BinarySensor.LoungeDoor
        };

        doors.StateChanges()
            .Where(change => change.New.IsOn())
            .Subscribe(change =>
            {
                logger.LogDebug("Door Opened: {door}", change.Entity.EntityId);
                Beep(1);
            });
    }

    private void DrivewayMotionAlarm()
    {
        var cameras_person = new[]
                {            
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

        //cameras_person.StateChanges()
        //     .Where(change => change.New.IsOn())
        //     .SubscribeAsync(async change =>
        //     {
        //         if (entities.AlarmControlPanel.Alarmo.IsDisarmed() || DateTime.Now.Hour is >= 7 and < 19)
        //             return;

        //         pushNotifier.Notify(PushNotifier.Recipient.All, "🚨 Person On Drive 🚨", "There is a person on the drive", 1, true, "Anticipate.caf");

        //         logger.LogDebug("Person Detected: {camera}", change.Entity.EntityId);
        //         alexa.TextToSpeech(new Alexa.Config()
        //         {
        //             Entity = entities.MediaPlayer.EugeneS5thEchoDot.EntityId, // Garage
        //             VolumeLevel = 1,
        //             VolumeResetDelay = 30,
        //             Message = "<audio src=\"soundbank://soundlibrary/scifi/amzn_sfx_scifi_alarm_03\"/>Alert, Alert, person detected on Drive",
        //             NotifyType = "tts",
        //             Whisper = false
        //         });
        //         await scheduler.Sleep(TimeSpan.FromSeconds(7));
        //         alexa.TextToSpeech(new Alexa.Config()
        //         {
        //             Entity = entities.MediaPlayer.EugeneS5thEchoDot.EntityId, // Garage
        //             VolumeLevel = 1,
        //             VolumeResetDelay = 30,
        //             Message = "<audio src=\"soundbank://soundlibrary/scifi/amzn_sfx_scifi_alarm_03\"/>Alert, Alert, person detected on Drive",
        //             NotifyType = "tts",
        //             Whisper = false
        //         });
        //         await scheduler.Sleep(TimeSpan.FromSeconds(30));
        //         entities.Light.Sofit.TurnOn();
        //         alexa.TextToSpeech(new Alexa.Config()
        //         {
        //             Entity = entities.MediaPlayer.Master.EntityId,
        //             VolumeLevel = 0.2,
        //             VolumeResetDelay = 30,
        //             Message = "Alert, person detected on Drive",
        //             NotifyType = "tts",
        //             Whisper = false
        //         });
        //         await scheduler.Sleep(TimeSpan.FromSeconds(300));
        //         entities.Light.Sofit.TurnOff();
        //     });
    }

    private void SimulateNightLights()
    {
        var nightTime = entities.Sun.Sun
            .ToBooleanObservable(e => e.Attributes.Elevation <= 1);

        var armedAway = entities.AlarmControlPanel.Alarmo
            .ToBooleanObservable(s => s.IsArmedAway());

        var armedNight = entities.AlarmControlPanel.Alarmo
            .ToBooleanObservable(s => s.IsArmedNight());

        var disarmed = entities.AlarmControlPanel.Alarmo
            .ToBooleanObservable(s => s.IsDisarmed());

        nightTime.AndOp(armedAway)
            .SubscribeTrue(
                () => entities.Switch.SimulateAllLights.TurnOn()
        );

        nightTime.AndOp(armedNight)
            .SubscribeTrue(
                () => entities.Switch.SimulateDownstairsLights.TurnOn()
        );

        disarmed.SubscribeTrue(() =>
        {
            entities.Switch.SimulateAllLights.TurnOff();
            entities.Switch.SimulateDownstairsLights.TurnOff();
        });
    }

    private void ArmAlarm()
    {

        // ---- Select the state observable from each sensor then merge and select the string "downstairs" for each event ----
        var downstairsMotion = common.MotionSensors.Downstairs
            .Select(sensor => sensor.StateChanges().Where(e => e.New?.State == "on"))
            .Merge()
            .Select(_ => "downstairs");

        // ---- Select the state observable from each sensor then merge and select the string "downstairs" for each event ----
        var lastDownstairsMotionWasHallway = common.MotionSensors.Downstairs
            .Select(sensor => sensor.StateChanges().Where(e => e.New?.State == "on"))
            .Merge()
            .Select(sensor => string.Equals(sensor.Entity.EntityId, entities.BinarySensor.KonnectedHallway.EntityId, StringComparison.OrdinalIgnoreCase))
            .StartWith(false);

        // ---- Select the state observable from each sensor then merge and select the string "upstairs" for each event ----
        var upstairsMotion = common.MotionSensors.Upstairs
            .Select(sensor => sensor.StateChanges().Where(e => e.New?.State == "on"))
            .Merge()
            .Select(_ => "upstairs");

        // ---- Track any motion ----
        var anyMotion = upstairsMotion.Merge(downstairsMotion);

        // ---- Track LAST motion zone ----
        var lastMotionZone =
            anyMotion
                .StartWith("unknown")
                .Replay(1)
                .RefCount();

        // ---- "No motion for 5 minutes" signal ----
        var noMotionForFiveMin =
            anyMotion
                .Select(_ =>
                    // Inner observable: immediately not idle, then idle after 5 minutes
                    Observable.Return(false) // false = NOT idle right now, we just saw motion
                        .Concat(
                            Observable
                                .Timer(TimeSpan.FromMinutes(5), scheduler)
                                .Select(__ => true) // true = idle after 5 minutes with no motion
                        )
                )
                .Switch()                 // cancel previous timer when new motion happens
                .StartWith(false)         // start as not idle
                .DistinctUntilChanged();  // only fire on changes

        // ---- PC idle observable (replace with your entity) ----
        var pcIdle =
            entities.Sensor.EugeneDesktopLastactive
                .StateAllChanges()
                .Select(_ =>
                    // Inner observable: immediately "active" then "idle" after timeout
                    Observable.Return(false) // false = active now
                        .Concat(
                            Observable
                                .Timer(TimeSpan.FromMinutes(1), scheduler)
                                .Select(__ => true) // true = idle after 1 minute
                        )
                )
                .Switch() // cancel previous timer when new activity happens
                .StartWith(true) // or false, depending how you want to start
                .DistinctUntilChanged() // only fire on actual changes                
                .Replay(1)
                .RefCount();

        // ---- TV off ----
        var tvOff =
            entities.MediaPlayer.LoungeTv
                .StateChanges()
                .Select(e => e.New.IsOff() || e.New.IsUnavailable())
                .StartWith(entities.MediaPlayer.LoungeTv.IsOff());

        // ---- Alarm Armed ----
        var alarmNotArmed =
            entities.AlarmControlPanel.Alarmo
                .StateChanges()
                .Select(e => !e.New.IsArmedNight())
                .StartWith(!entities.AlarmControlPanel.Alarmo.IsArmedNight());

        var state =
            Observable.CombineLatest(
                lastMotionZone,
                pcIdle,
                tvOff,
                alarmNotArmed, lastDownstairsMotionWasHallway,
                (lastZone, isPcIdle, isTvOff, isAlarmNotArmed, isLastDownstairsMotionHallway) =>
                    new { lastZone, isPcIdle, isTvOff, isAlarmNotArmed, isLastDownstairsMotionHallway }
            );

        // ---- Combine everything ----
        var subscription =
            noMotionForFiveMin
                .WithLatestFrom(state, (noMotion, s) => s) // we don't care about noMotion's value, only the timing
                .Do(observer => logger.LogDebug("Evaluating alarm arm conditions {state}", observer))
                .Where(x => x.lastZone == "upstairs")
                .Where(x => x.isPcIdle)
                .Where(x => x.isTvOff)
                .Where(x => x.isAlarmNotArmed)
                .Where(x => x.isLastDownstairsMotionHallway)
                .Subscribe(_ =>
                {
                    entities.AlarmControlPanel.Alarmo.AlarmArmNight();

                    foreach (var light in entities.Light.EnumerateAll().Where(e => e.Registration?.Labels?.Any(label => string.Equals(label.Id, "Downstairs", StringComparison.OrdinalIgnoreCase)) == true))
                    {
                        light.TurnOff();
                    }
                });
    }
}