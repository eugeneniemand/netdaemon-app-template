namespace daemonapp.apps.NotificationsManager;

public class BatteryNotificationsConfig(NumericSensorEntity batteryLevel, SensorEntity batteryState, SwitchEntity xboxBlock, string mobileApp)
{
    public SwitchEntity XboxBlock = xboxBlock;
    public NumericSensorEntity BatteryLevel { get; } = batteryLevel;
    public SensorEntity BatteryState { get; } = batteryState;
    public string MobileApp { get; } = mobileApp;
}