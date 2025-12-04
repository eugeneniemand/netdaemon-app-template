using HomeAssistantGenerated;
using NetDaemon.Extensions.Observables;
using NetDaemon.HassModel.Entities;
using Niemand.Helpers;
using Niemand.Helpers.Notifications;
using Polly;
using Reactive.Boolean;
using System.Reactive.Subjects;

namespace Niemand;

public class KitchenConfiguration
{
    public LightEntity? CoffeeMachineLight { get; set; }
    public NumericSensorEntity? CoffeeMachinePower { get; set; }
    public SwitchEntity? CoffeeMachineAdaptiveLighting { get; set; }
}

//[Focus]
[NetDaemonApp]
public class Kitchen
{

    private readonly KitchenConfiguration _config;
    private readonly IScheduler _scheduler;
    private readonly ILogger<Kitchen> _logger;
    private readonly IEntities _entities;
    private readonly IAlexa _alexa;
    private readonly IServices _services;

    private bool DishwasherDoorOpen => _entities.BinarySensor.NeffDishwasherDoor.IsOn();
    private bool DishwasherScheduled => _entities.InputBoolean.DishwasherReminder.IsOn();

    public Kitchen(IHaContext ha, IScheduler scheduler, IAppConfig<KitchenConfiguration> config, ILogger<Kitchen> logger, IServices services, IEntities entities, IAlexa alexa, PushNotifier pushNotifier)
    {
        _scheduler = scheduler;
        _logger = logger;
        _config = config.Value;
        _entities = entities;
        _alexa = alexa;
        _services = services;
        var lastNotification = DateTime.MinValue;

        var retryPolicy = Policy
            .HandleResult<bool>(result => !result)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
            {
                _logger.LogWarning($"Retry {retryCount} for turning on DishwasherProgramEco50");
            });

        var cheapEnergyActive = entities.BinarySensor.OctopusEnergyTargetThreeHour.ToBooleanObservable()
            .Or(entities.BinarySensor.OctopusEnergyTargetThreeHourDay.ToBooleanObservable());

        cheapEnergyActive.SubscribeTrue(
            async () =>
            {
                await RunDishwasherAsync(services, entities, alexa, pushNotifier, retryPolicy);
            });

        entities.InputBoolean.DishwasherReminder.ToChangesOnlyBooleanObservable().SubscribeOn(() =>
        {
            if (DishwasherDoorOpen)
                DishwasherNotification("Dishwasher cant be scheduled, door is open");
            else
                DishwasherNotification("Dishwasher scheduled and ready");
        });

        entities.BinarySensor.NeffDishwasherDoor.ToChangesOnlyBooleanObservable().SubscribeOpenClosed (
            () => { if (DishwasherScheduled) DishwasherNotification("Dishwasher scheduled. Please close the door"); },
            () => { if (DishwasherScheduled) DishwasherNotification("Dishwasher scheduled and ready"); }
        );

        if (_config.CoffeeMachinePower != null)
        {
            var coffeePower = _config.CoffeeMachinePower;
            coffeePower.StateChanges().Subscribe(e =>
            {
                try
                {
                    if (e.New == null) return;
                    if (e.New.State <= 6) return;
                    if (_config.CoffeeMachineLight == null)
                    {
                        _logger.LogWarning("CoffeeMachineLight not configured; skipping light turn-on.");
                        return;
                    }
                    if (_config.CoffeeMachineLight.IsOff()) return;

                    _logger.LogInformation("Coffee machine turned on");
                    _config.CoffeeMachineLight.TurnOn(new LightTurnOnParameters { BrightnessPct = 100 });
                    //entities.Light.Utilitycupboard.TurnOn();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling CoffeeMachinePower state change");
                }
            });

            coffeePower.StateChanges().Where(s => s.Old != null && s.New != null && s.Old.State >= 1200 && s.New.State < 1200).Subscribe(e =>
            {
                try
                {
                    if (lastNotification != DateTime.MinValue && (DateTime.Now - lastNotification).TotalMinutes < 15) return;
                    try
                    {
                        services.Notify.Twinstead("Coffee machine is ready");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Notify service call failed in CoffeeMachine ready handler");
                    }
                    alexa.Announce(new Alexa.Config { Entity = "media_player.kitchen", Message = "The coffee machine is ready" });
                    lastNotification = DateTime.Now;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling CoffeeMachine ready state change");
                }
            });
        }
    }

    private void DishwasherNotification(string message)
    {
        _alexa.TextToSpeech(new Alexa.Config() { Entity = "media_player.dining", Message = message, Whisper = false, VolumeLevel = 0.3 });
    }

    private async Task<bool> RunDishwasherAsync(IServices services, IEntities entities, IAlexa alexa, PushNotifier pushNotifier, Polly.Retry.AsyncRetryPolicy<bool> retryPolicy)
    {
        try
        {
            
            if (entities.InputBoolean?.DishwasherReminder?.IsOff() ?? true) return false;

            _logger.LogInformation("Dishwasher starting");
            entities.Switch.NeffDishwasherPower.TurnOn();
            await _scheduler.Sleep(TimeSpan.FromMinutes(1));

            if (entities.BinarySensor.NeffDishwasherDoor.IsOn())
            {
                var message = "The dishwasher cant start door is open";
                alexa.Announce(new Alexa.Config { Entity = "media_player.kitchen", Message = message });
                alexa.Announce(new Alexa.Config { Entity = "media_player.master", Message = message });
                services.Notify.Twinstead(message);
                pushNotifier.Notify(PushNotifier.Recipient.All, "Dishwasher Failure", message, 0.5, true, "shake.caf");
                _logger.LogError("Failed to start DishwasherProgramEco50 after retries");
                return false;
            }

            entities.InputBoolean.DishwasherReminder.TurnOff();
            _logger.LogInformation("Dishwasher waiting for power on");
            await _scheduler.Sleep(TimeSpan.FromMinutes(1));
            var success = await retryPolicy.ExecuteAsync(() => TurnOnDishwasherProgramEco50Async());
            if (!success)           
            {
                var message = "The dishwasher failed to start";
                alexa.Announce(new Alexa.Config { Entity = "media_player.kitchen", Message = message });
                alexa.Announce(new Alexa.Config { Entity = "media_player.master", Message = message });
                services.Notify.Twinstead(message);
                pushNotifier.Notify(PushNotifier.Recipient.All, "Dishwasher Failure", message, 0.5, true, "shake.caf");
                _logger.LogError("Failed to start DishwasherProgramEco50 after retries");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CheapEnergyActive handler");
        }

        return true;
    }

    private async Task<bool> TurnOnDishwasherProgramEco50Async()
    {
        _logger.LogInformation("Dishwasher waiting for program to start");
        await _scheduler.Sleep(TimeSpan.FromSeconds(10)); // Wait for a short period to allow the state to change
        _entities.Select.NeffDishwasherSelectedProgram.SelectOption(new SelectSelectOptionParameters() { Option = "dishcare_dishwasher_program_eco_50" });
        await _scheduler.Sleep(TimeSpan.FromSeconds(10)); // Wait for a short period to allow the state to change
        _entities.Button.NeffDishwasherStart.Press();
        await _scheduler.Sleep(TimeSpan.FromSeconds(10)); // Wait for a short period to allow the state to change
        return _entities.Sensor.NeffDishwasherOperationState.State?.Equals("run", StringComparison.OrdinalIgnoreCase) ?? false;
    }
}
