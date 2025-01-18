using Niemand.Helpers;
using Polly;

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

    public Kitchen(IHaContext ha, IScheduler scheduler, IAppConfig<KitchenConfiguration> config, ILogger<Kitchen> logger, IServices services, IEntities entities, IAlexa alexa)
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

        entities.BinarySensor
                .OctopusEnergyTargetThreeHour
                .StateChanges()
                .Where(e => e.Old.IsOff() && e.New.IsOn())
                .SubscribeAsync(async e =>
                {
                    if (entities.BinarySensor.DishwasherDoor.IsOn() || entities.InputBoolean.DishwasherReminder.IsOff()) return;
                    _logger.LogInformation("Dishwasher starting");
                    entities.Switch.DishwasherPower.TurnOn();
                    entities.InputBoolean.DishwasherReminder.TurnOff();
                    _logger.LogInformation("Dishwasher waiting for power on");
                    await _scheduler.Sleep(TimeSpan.FromMinutes(1));
                    var success = await retryPolicy.ExecuteAsync(() => TurnOnDishwasherProgramEco50Async());
                    if (success)
                    {
                        alexa.Announce(new Alexa.Config { Entity = "media_player.kitchen", Message = "The dishwasher started" });
                        services.Notify.Twinstead("The dishwasher has started");
                        _logger.LogInformation("DishwasherProgramEco50 Started");
                    }
                    else
                    {
                        alexa.Announce(new Alexa.Config { Entity = "media_player.kitchen", Message = "The dishwasher failed to start" });
                        alexa.Announce(new Alexa.Config { Entity = "media_player.master", Message = "The dishwasher failed to start" });
                        services.Notify.Twinstead("The dishwasher failed to start");
                        _logger.LogError("Failed to start DishwasherProgramEco50 after retries");
                    }
                });

        _config.CoffeeMachinePower!.StateChanges().Subscribe(e =>
        {
            if (e.New.State <= 6 || _config.CoffeeMachineLight.IsOff()) return;

            _logger.LogInformation("Coffee machine turned on");
            _config.CoffeeMachineLight.TurnOn(new LightTurnOnParameters { BrightnessPct = 100 });
            entities.Light.Utilitycupboard.TurnOn();
        });

        _config.CoffeeMachinePower!.StateChanges().Where(s => s.Old.State >= 1200 && s.New.State < 1200).Subscribe(e =>
        {
            if (lastNotification != DateTime.MinValue && (DateTime.Now - lastNotification).TotalMinutes < 15) return;
            services.Notify.Twinstead("Coffee machine is ready");
            alexa.Announce(new Alexa.Config { Entity = "media_player.kitchen", Message = "The coffee machine is ready" });
            lastNotification = DateTime.Now;
        });
    }

    private async Task<bool> TurnOnDishwasherProgramEco50Async()
    {
        _logger.LogInformation("Dishwasher waiting for program to start");
        _entities.Switch.DishwasherProgramEco50.TurnOn();
        await _scheduler.Sleep(TimeSpan.FromSeconds(30)); // Wait for a short period to allow the state to change
        return _entities.Sensor.DishwasherOperationState.State?.Equals("run", StringComparison.OrdinalIgnoreCase) ?? false;
    }
}
