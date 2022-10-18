using Microsoft.Extensions.Configuration;

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
    DishwasherConfiguration _config;

    public Dishwasher(IHaContext ha, IAppConfig<DishwasherConfiguration> configuration)
    {
        _config = configuration.Value;
        _powerDecreaseCount = 0;
        _powerIncreaseCount = 0;
        _config.DishwasherCycleSelect!.SelectOption(DishwasherCycle.Dirty.ToString());

        _config.DishwasherPower!.StateChanges().Subscribe(e =>
        {
            var time = e.New.LastChanged;
            if (_config.DishwasherCycleSelect.State is null || _config.DishwasherCycleSelect.State == DishwasherCycle.Clean.ToString() && e.New?.State == 0)
            {                
                SetDishwasherCycle(DishwasherCycle.Dirty);
            }

            if (e.Old?.State < 100 && e.New?.State > 1500)
                _powerIncreaseCount++;

            if (e.Old?.State > 1500 && e.New?.State < 100)
                _powerDecreaseCount++;

            var state = _config.DishwasherCycleSelect.State;

            if (state == DishwasherCycle.Dirty.ToString() && _powerIncreaseCount == 1)
                SetDishwasherCycle(DishwasherCycle.Wash);

            if (state == DishwasherCycle.Wash.ToString() && _powerIncreaseCount == 2)
                SetDishwasherCycle(DishwasherCycle.Rinse);

            if (state == DishwasherCycle.Rinse.ToString() && _powerDecreaseCount == 1)
                SetDishwasherCycle(DishwasherCycle.Clean);
        });
    }

    private void SetDishwasherCycle(DishwasherCycle cycle)
    {
        _config.DishwasherCycleSelect!.SelectOption(cycle.ToString());
        _powerDecreaseCount = 0;
        _powerIncreaseCount = 0;        
    }
}