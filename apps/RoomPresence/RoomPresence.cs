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

        public override void Initialize()
        {
            if (( Rooms ?? throw new ArgumentNullException(nameof(Rooms), "No Rooms Config Found") ).Any(r => r.Debug))
            {
                LogInformation($"Initialise (DEBUG) ");
                Rooms.Where(r => r.Debug).ToList().ForEach(InitRoom);
            }
            else
            {
                LogInformation($"Starting RoomPresence for {Rooms.Count()} rooms in config");
                Rooms.ToList().ForEach(InitRoom);
            }
        }

        private void InitRoom(RoomConfig room)
        {
            LogInformation($"Initialise for {room.Name}");
            var presenceConfig = new PresenceConfig(room) { NdUserId = NdUserId, GuardTimeout = GuardTimeout };
            var roomPresenceImplementation = new RoomPresenceImplementation(this, presenceConfig);
            roomPresenceImplementation.Initialize();
        }
    }
}