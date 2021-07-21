using System;
using System.Collections.Generic;
using System.Linq;
using NetDaemon.Common.Reactive;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace Presence
{
    public class RoomPresence : NetDaemonRxApp
    {
        public IEnumerable<RoomConfig>? Rooms { get; set; }
        public string? NdUserId { get; set; }
        public int GuardTimeout { get; set; } = 900;
        public int MinDuration { get; set; }
        public int MaxDuration { get; set; }

        public override void Initialize()
        {
            var randomGenerator = new RandomController(MinDuration, MaxDuration);
            if (( Rooms ?? throw new ArgumentNullException(nameof(Rooms), "No Rooms Config Found") ).Any(r => r.Debug))
            {
                LogInformation($"Initialise (DEBUG) ");
                Rooms.Where(r => r.Debug).ToList().ForEach(room => InitRoom(room, randomGenerator));
            }
            else
            {
                LogInformation($"Starting RoomPresence for {Rooms.Count()} rooms in config");
                Rooms.ToList().ForEach(room => InitRoom(room, randomGenerator));
            }
        }

        private void InitRoom(RoomConfig room, IRandomController randomController)
        {
            LogInformation($"Initialise for {room.Name}");
            var presenceConfig             = new PresenceConfig(room) { NdUserId = NdUserId, GuardTimeout = GuardTimeout };
            var roomPresenceImplementation = new RoomPresenceImplementation(this, presenceConfig, randomController);
            roomPresenceImplementation.Initialize();
        }
    }
}