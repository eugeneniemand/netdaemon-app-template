namespace Niemand;

public class KitchenConfiguration
{
    public LightEntity? CoffeeMachineLight { get; set; }
    public NumericSensorEntity? CoffeeMachinePower { get; set; }
}

//[Focus]
[NetDaemonApp]
public class Kitchen
{
    private readonly KitchenConfiguration _config;
    private readonly ILogger<Kitchen>     _logger;

    public Kitchen(IHaContext ha, IAppConfig<KitchenConfiguration> config, ILogger<Kitchen> logger, IServices services, IAlexa alexa)
    {
        _logger = logger;
        _config = config.Value;
        var lastNotification = DateTime.MinValue;

        _config.CoffeeMachinePower!.StateChanges().Subscribe(e =>
        {
            if (e.New.State <= 6 || _config.CoffeeMachineLight.IsOff()) return;

            _logger.LogInformation("Coffee machine turned on");
            _config.CoffeeMachineLight.TurnOn(new LightTurnOnParameters { BrightnessPct = 100 });
        });

        _config.CoffeeMachinePower!.StateChanges().Where(s => s.Old.State >= 1200 && s.New.State < 1200).Subscribe(e =>
        {
            if (lastNotification != DateTime.MinValue && ( DateTime.Now - lastNotification ).TotalMinutes < 15) return;
            services.Notify.Twinstead("Coffee machine is ready");
            alexa.Announce(new Alexa.Config { Entity = "media_player.kitchen", Message = "The coffee machine is ready" });
            lastNotification = DateTime.Now;
        });
    }
}