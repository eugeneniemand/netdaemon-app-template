public class KitchenConfiguration
{
    public LightEntity? CoffeeMachineLight { get; set; }
    public NumericSensorEntity? CoffeeMachinePower { get; set; }
}

[Focus]
[NetDaemonApp]
public class Kitchen
{
    private readonly KitchenConfiguration _config;
    private readonly ILogger<Kitchen>     _logger;

    public Kitchen(IHaContext ha, IAppConfig<KitchenConfiguration> config, ILogger<Kitchen> logger)
    {
        _logger = logger;
        _config = config.Value;

        _config.CoffeeMachinePower!.StateChanges().Subscribe(e =>
        {
            if (e.New.State < 4 || _config.CoffeeMachineLight.IsOff()) return;
            _logger.LogInformation("Coffee machine turned on");
            _config.CoffeeMachineLight.TurnOn(new LightTurnOnParameters { BrightnessPct = 100 });
        });
    }
}