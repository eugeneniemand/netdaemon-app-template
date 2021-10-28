using System;

namespace LightsManager
{
    public class HassEventArgs : EventArgs
    {
        public string EntityId { get; set; }
        //public string OldState { get; set; }
        //public string NewState { get; set; }

        public string CorrelationId;

        public string RoomName { get; set; }
        public EventType EventType { get; }

        public HassEventArgs(string roomConfigName, EventType eventType)
        {
            CorrelationId = Guid.NewGuid().ToString();
            RoomName      = roomConfigName;
            EventType     = eventType;
        }
    }
}