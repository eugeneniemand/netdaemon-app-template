using System;
using NetDaemon.Common;

namespace LightsManager
{
    public class HassEventArgs : EventArgs
    {
        public string CorrelationId;
        public EventType EventType { get; }
        public (EntityState Old, EntityState New) EntityStates { get; }
        public string EntityId { get; set; } = "UNKNOWN";

        public HassEventArgs(EventType eventType, (EntityState Old, EntityState New) entityStates = default)
        {
            CorrelationId = Guid.NewGuid().ToString();
            EventType     = eventType;
            EntityStates  = entityStates;
        }
    }

    public class ManagerStateEventArgs : HassEventArgs
    {
        public ManagerState State { get; set; }

        public ManagerStateEventArgs(string correlationId, ManagerState state) : base(EventType.ManagerStateChanged)
        {
            CorrelationId = correlationId;
            State         = state;
        }
    }

    public class EntityOverrideEventArgs : HassEventArgs
    {
        public string NewState { get; set; }

        public EntityOverrideEventArgs(string correlationId, string newState) : base(EventType.ManualOverride)
        {
            CorrelationId = correlationId;
            NewState      = newState;
        }
    }

    public class ManagerTimerResetEventArgs : HassEventArgs
    {
        public ManagerTimerResetEventArgs(string correlationId) : base(EventType.ManagerTimerReset)
        {
            CorrelationId = correlationId;
        }
    }

    public class ManagerTimerSetEventArgs : HassEventArgs
    {
        public TimeSpan TimeoutSeconds { get; }

        public ManagerTimerSetEventArgs(string correlationId, TimeSpan timeoutSeconds) : base(EventType.ManagerTimerSet)
        {
            TimeoutSeconds = timeoutSeconds;
            CorrelationId  = correlationId;
        }
    }

    public class ManagerGuardDogArgs : HassEventArgs
    {
        public ManagerGuardDogArgs(string correlationId) : base(EventType.GuardDogPatrolling)
        {
            CorrelationId = correlationId;
        }
    }
}