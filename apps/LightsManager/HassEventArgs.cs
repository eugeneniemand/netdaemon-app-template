using System;
using NetDaemon.Common;

namespace LightsManager
{
    public class HassEventArgs : EventArgs
    {
        public string CorrelationId;
        public string RoomName { get; set; }
        public EventType EventType { get; }
        public (EntityState Old, EntityState New) State { get; }
        public string EntityId { get; set; }

        public HassEventArgs(string roomConfigName, EventType eventType, (EntityState Old, EntityState New) state = default)
        {
            CorrelationId = Guid.NewGuid().ToString();
            RoomName      = roomConfigName;
            EventType     = eventType;
            State         = state;
        }
    }

    public class ManagerStateEventArgs : HassEventArgs
    {
        public ManagerState State { get; set; }

        public ManagerStateEventArgs(string roomConfigName, ManagerState state) : base(roomConfigName, EventType.ManagerStateChanged)
        {
            CorrelationId = Guid.NewGuid().ToString();
            RoomName      = roomConfigName;
            State         = state;
        }
    }

    public class EntityOverrideEventArgs : HassEventArgs
    {
        public string? NewState { get; set; }

        public EntityOverrideEventArgs(string roomConfigName, string? newState) : base(roomConfigName, EventType.ManualOverride)
        {
            CorrelationId = Guid.NewGuid().ToString();
            RoomName      = roomConfigName;
            NewState      = newState;
        }
    }

    public class ManagerResetTimerEventArgs : HassEventArgs
    {
        public ManagerResetTimerEventArgs(string roomConfigName, string correlationId) : base(roomConfigName, EventType.ManagerResetTimer)
        {
            CorrelationId = correlationId;
            RoomName      = roomConfigName;
        }
    }

    public class ManagerSetTimerEventArgs : HassEventArgs
    {
        public TimeSpan TimeoutSeconds { get; }

        public ManagerSetTimerEventArgs(string roomConfigName, string correlationId, TimeSpan timeoutSeconds) : base(roomConfigName, EventType.ManagerResetTimer)
        {
            TimeoutSeconds = timeoutSeconds;
            CorrelationId  = correlationId;
            RoomName       = roomConfigName;
        }
    }
}