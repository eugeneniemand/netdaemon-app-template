public class DishwasherConfiguration
{
    public InputSelectEntity? DishwasherCycleSelect { get; set; }
    public NumericSensorEntity? DishwasherPower { get; set; }
}

[Focus]
[NetDaemonApp]
public class Dishwasher
{
    public enum DishwasherCycle
    {
        Wash,
        Rinse,
        Clean,
        Dirty
    }

    private int _powerDecreaseCount;

    private int _powerIncreaseCount;

    public Dishwasher(IHaContext ha, IAppConfig<DishwasherConfiguration> configuration)
    {
        var config = configuration.Value;
        _powerDecreaseCount = 0;
        _powerIncreaseCount = 0;
        config.DishwasherCycleSelect!.SelectOption(DishwasherCycle.Dirty.ToString());

        config.DishwasherPower!.StateChanges().Subscribe(e =>
        {
            if (config.DishwasherCycleSelect.State is null || config.DishwasherCycleSelect.State == DishwasherCycle.Clean.ToString() && e.New?.State < 1)
            {
                _powerDecreaseCount = 0;
                _powerIncreaseCount = 0;
                config.DishwasherCycleSelect!.SelectOption(DishwasherCycle.Dirty.ToString());
            }

            if (e.Old?.State is null or < 100 && e.New?.State > 1500)
                _powerIncreaseCount++;

            if (e.Old?.State is null or > 1500 && e.New?.State < 100)
                _powerDecreaseCount++;

            var state = config.DishwasherCycleSelect.State;

            if (state == DishwasherCycle.Dirty.ToString() && _powerIncreaseCount == 1)
                config.DishwasherCycleSelect.SelectOption(DishwasherCycle.Wash.ToString());

            if (state == DishwasherCycle.Wash.ToString() && _powerIncreaseCount == 4)
                config.DishwasherCycleSelect.SelectOption(DishwasherCycle.Rinse.ToString());

            if (state == DishwasherCycle.Rinse.ToString() && _powerDecreaseCount == 4)
                config.DishwasherCycleSelect.SelectOption(DishwasherCycle.Clean.ToString());
        });
    }
}