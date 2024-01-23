using NetDaemon;

namespace Niemand.NotificationManager;

public interface IApplianceNotificationConfig
{
    Dictionary<string, CycleState> CycleStates { get; }
    InputBooleanEntity Acknowledge { get; }
    SensorEntity RemainingTime { get; }
    SensorEntity Status { get; }
    string Name { get; }
}