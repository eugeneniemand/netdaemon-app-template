namespace Niemand;

public class DishwasherConfiguration
{
    public InputSelectEntity? DishwasherCycleSelect { get; set; }
    public NumericSensorEntity? DishwasherPower { get; set; }
    public SwitchEntity? DishwasherSwitch { get; set; }
}

//[Focus]
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

    private readonly DishwasherConfiguration _config;

    private int _powerDecreaseCount;

    private int _powerIncreaseCount;

    public Dishwasher(IHaContext ha, IAppConfig<DishwasherConfiguration> configuration)
    {
        _config             = configuration.Value;
        _powerDecreaseCount = 0;
        _powerIncreaseCount = 0;
        _config.DishwasherCycleSelect!.SelectOption(DishwasherCycle.Dirty.ToString());

        ha.Events.Where(e => e.EventType == Shared.Events.Cheap3HourWindowStarted.ToString()).Subscribe(e => _config.DishwasherSwitch!.TurnOn());

        _config.DishwasherPower!.StateChanges().Subscribe(e =>
        {
            var time = e.New.LastChanged;
            if (_config.DishwasherCycleSelect.State is null || _config.DishwasherCycleSelect.State == DishwasherCycle.Clean.ToString() && e.New?.State == 0) SetDishwasherCycle(DishwasherCycle.Dirty);

            if (e.Old?.State < 150 && e.New?.State > 1500)
                _powerIncreaseCount++;

            if (e.Old?.State > 1500 && e.New?.State < 150)
                _powerDecreaseCount++;

            var state = _config.DishwasherCycleSelect.State;

            if (state == DishwasherCycle.Dirty.ToString() && _powerIncreaseCount == 1)
                SetDishwasherCycle(DishwasherCycle.Wash);

            if (state == DishwasherCycle.Wash.ToString() && _powerIncreaseCount == 3)
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