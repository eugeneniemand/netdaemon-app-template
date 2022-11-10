namespace Niemand;

public class KitchenConfiguration
{
    public string? TestConfig { get; set; }
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
    }
}