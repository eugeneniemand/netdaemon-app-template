using System.Reactive.Concurrency;

namespace Presence
{
    public class PresenceConfig
    {
        public PresenceConfig(RoomConfig roomConfig)
        {
            RoomConfig = roomConfig;
        }

        public RoomConfig RoomConfig { get; set; }
        public IScheduler? Scheduler { get; set; } = null!;
        public string? NdUserId { get; set; } = null!;
        public int GuardTimeout { get; set; } = 900;
    }
}