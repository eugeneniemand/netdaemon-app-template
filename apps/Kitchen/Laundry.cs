using HomeAssistantGenerated;
using CodeCasa.NetDaemon.Extensions.Observables;
using NetDaemon.HassModel.Entities;
using Niemand.Helpers;
using Niemand.Helpers.Notifications;
using Polly;
using Polly.Retry;
using Reactive.Boolean;
using System.Reactive.Subjects;

namespace Niemand;

public class LaundryConfiguration
{
    public LightEntity? CoffeeMachineLight { get; set; }
    public NumericSensorEntity? CoffeeMachinePower { get; set; }
    public SwitchEntity? CoffeeMachineAdaptiveLighting { get; set; }
}

//[Focus]
[NetDaemonApp]
public class Laundry
{
    private readonly LaundryConfiguration _config;
    private readonly IScheduler _scheduler;
    private readonly ILogger<Laundry> _logger;
    private readonly IEntities _entities;
    private readonly IAlexa _alexa;
    private readonly PushNotifier _pushNotifier;
    private readonly IServices _services;
    private ApplianceHandler _washingMachine = null!;
    private ApplianceHandler _dryer = null!;

    private class ApplianceHandler
    {
        private bool _finished;
        private CancellationTokenSource _cancellationTokenSource = new();
        private readonly BinarySensorEntity _remoteControlEntity;
        private readonly BinarySensorEntity _machineRunningEntity;
        private readonly ButtonEntity _machineStartButtonEntity;
        private readonly string _applianceName;
        private readonly IScheduler _scheduler;
        private readonly ILogger _logger;
        private readonly IAlexa _alexa;
        private readonly IServices _services;
        private readonly PushNotifier _pushNotifier;
        private readonly AsyncRetryPolicy<bool> _retryPolicy;

        public bool IsFinished => _finished;
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplianceHandler"/> class for a single appliance (for example washer or dryer).
        /// </summary>
        /// <param name="jobStateEntity">
        /// Home Assistant sensor that reports the current job/program lifecycle.
        /// Expected to publish values such as <c>finished</c> / <c>finish</c>; those values are used to mark the appliance run as completed.
        /// </param>
        /// <param name="smartControlEntity">
        /// Home Assistant binary sensor that indicates whether remote start/control is enabled for the appliance.
        /// When this sensor turns on, a run is considered scheduled; when it turns off before completion, the pending run is cancelled.
        /// </param>
        /// <param name="machineRunningEntity">
        /// Home Assistant binary sensor that reflects the appliance execution state after commands are sent.
        /// This handler checks it for <c>on</c> to verify that a start command succeeded.
        /// </param>
        /// <param name="machineStartButtonEntity">
        /// Home Assistant button entity used to send control commands to the appliance.
        /// This handler issues <c>start</c> via <c>button.press</c> to start the appliance remotely.
        /// </param>
        /// <param name="applianceName">
        /// Friendly appliance label used in logs and spoken/push notifications (for example <c>Washing machine</c>).
        /// </param>
        /// <param name="logger">Logger used for operational and failure diagnostics.</param>
        /// <param name="scheduler">Scheduler used for non-blocking waits between command and state verification.</param>
        /// <param name="alexa">Alexa integration used for voice announcements and text-to-speech notifications.</param>
        /// <param name="services">Home Assistant services wrapper used to dispatch notification services.</param>
        /// <param name="pushNotifier">Push notification helper used for mobile alerts on start failures.</param>
        public ApplianceHandler(
            SensorEntity jobStateEntity,
            BinarySensorEntity smartControlEntity,
            BinarySensorEntity machineRunningEntity,
            ButtonEntity machineStartButtonEntity,
            string applianceName,            
            ILogger logger,
            IScheduler scheduler,
            IAlexa alexa,
            IServices services,
            PushNotifier pushNotifier)
        {
            _remoteControlEntity = smartControlEntity;
            _machineRunningEntity = machineRunningEntity;
            _machineStartButtonEntity = machineStartButtonEntity;
            _applianceName = applianceName;
            _scheduler = scheduler;
            _logger = logger;
            _alexa = alexa;
            _services = services;
            _pushNotifier = pushNotifier;

            SubscribeToStateChanges(jobStateEntity);
            SubscribeToRemoteControl(smartControlEntity, applianceName);
        }

        public void ResetCancellationToken()
        {
            _cancellationTokenSource = new();
            _finished = false;
            _logger.LogDebug($"{_applianceName} reset");
        }

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            _logger.LogInformation($"{_applianceName} cancelled");
        }

        public void SetFinished()
        {
            _finished = true;
            _logger.LogInformation($"{_applianceName} finished");
        }

        public async void Run()
        {
            try
            {
                var retryPolicy = Policy
                   .HandleResult<bool>(result => !result)
                   .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMinutes(2 * retryAttempt), (result, timeSpan, retryCount, context) =>
                   {
                       _logger.LogWarning($"Retry {retryCount} for turning on appliance {context["ApplianceName"]}");
                   });

                if (!_remoteControlEntity.IsOn() ) {
                    _logger.LogInformation($"{_applianceName} remote control is off");
                    return;
                }

                if (_machineRunningEntity.IsOn()) {
                    _logger.LogInformation($"{_applianceName} is already running");
                    return;
                }

                

                var context = new Polly.Context
                {
                    { "ApplianceName", _applianceName }
                };

                var success = await retryPolicy.ExecuteAsync(async (context, cancellationToken) =>
                {
                    _logger.LogInformation($"{_applianceName} starting");
                    _machineStartButtonEntity.Press();
                    await _scheduler.Sleep(TimeSpan.FromMinutes(1));                    
                    return await Task.FromResult(_machineRunningEntity.IsOn());
                }, context, CancellationToken);

                if (!success || CancellationToken.IsCancellationRequested)
                {
                    var message = $"{_applianceName} failed to start";
                    _alexa.Announce(new Alexa.Config { Entity = "media_player.kitchen", Message = message });
                    _alexa.Announce(new Alexa.Config { Entity = "media_player.master", Message = message });
                    _services.Notify.Twinstead(message);
                    _pushNotifier.Notify(PushNotifier.Recipient.All, $"{_applianceName} Start Failed", message, 0.5, true, "shake.caf");
                    _logger.LogError("Failed to start {ApplianceName}", _applianceName);
                }
                else
                {
                    _logger.LogInformation($"{_applianceName} started successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running {ApplianceName}", _applianceName);
            }
        }

        private void SubscribeToStateChanges(SensorEntity jobStateEntity)
        {
            jobStateEntity.StateChanges().Subscribe(s =>
            {
                if ((s.New?.State?.Equals("finished", StringComparison.OrdinalIgnoreCase) ?? false) || (s.New?.State?.Equals("finish", StringComparison.OrdinalIgnoreCase) ?? false))
                    SetFinished();
            });
        }

        private void SubscribeToRemoteControl(BinarySensorEntity remoteControlEntity, string applianceName)
        {
            remoteControlEntity.ToChangesOnlyBooleanObservable().SubscribeOnOff(
                () =>
                {
                    ResetCancellationToken();
                    ApplianceNotification($"{applianceName} scheduled");
                },
                () =>
                {
                    if (IsFinished) return;
                    Cancel();
                    ApplianceNotification($"{applianceName} schedule cancelled");
                });
        }

        private void ApplianceNotification(string message)
        {
            _alexa.TextToSpeech(new Alexa.Config() { Entity = "media_player.kitchen", Message = message, Whisper = false, VolumeLevel = 0.3 });
        }
    }

    public Laundry(IHaContext ha, IScheduler scheduler, IAppConfig<LaundryConfiguration> config, ILogger<Laundry> logger, IServices services, IEntities entities, IAlexa alexa, PushNotifier pushNotifier)
    {
        _scheduler = scheduler;
        _logger = logger;
        _config = config.Value;
        _entities = entities;
        _alexa = alexa;
        _pushNotifier = pushNotifier;
        _services = services;

        _washingMachine = new ApplianceHandler(
            entities.Sensor.SamsungWasherProgress,
            entities.BinarySensor.SamsungWasherSmartControl,
            entities.BinarySensor.SamsungWasherRunning,
            entities.Button.SamsungWasherStart,
            "Washing machine",
            _logger,
            _scheduler,
            _alexa,
            _services,
            _pushNotifier
         );

        _dryer = new ApplianceHandler(
            entities.Sensor.SamsungDryerProgress,
            entities.BinarySensor.SamsungDryerSmartControl,
            entities.BinarySensor.SamsungDryerRunning,
            entities.Button.SamsungDryerStart,
            "Tumble dryer",
            _logger,
            _scheduler,
            _alexa,
            _services,
            _pushNotifier
           );

        var cheapEnergyActive = entities.BinarySensor.OctopusEnergyTargetThreeHour.ToBooleanObservable()
            .Or(entities.BinarySensor.OctopusEnergyTargetThreeHourDay.ToBooleanObservable());

        cheapEnergyActive.SubscribeTrue(
            () =>
            {
                _washingMachine.Run();
                _dryer.Run();
            });
    }



}
