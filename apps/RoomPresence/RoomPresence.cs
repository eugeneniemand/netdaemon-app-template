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
        private readonly List<RoomPresenceImplementation> _roomServices = new();
        public IEnumerable<RoomConfig>? Rooms { get; set; }
        public string? NdUserId { get; set; }

        public override void Initialize()
        {
            if (( Rooms ?? throw new ArgumentNullException(nameof(Rooms), "No Rooms Config Found") ).Any(r => r.Debug))
            {
                Rooms.Where(r => r.Debug).ToList().ForEach(r =>
                {
                    LogInformation($"Initialise (DEBUG) for {r.Name}");
                    InitRoom(r, NdUserId);
                });
            }
            else
            {
                LogInformation($"Starting RoomPresence for {Rooms.Count()} rooms in config");
                Rooms.ToList().ForEach(r => InitRoom(r, NdUserId));
            }
        }

        private void InitRoom(RoomConfig room, string? ndUserId)
        {
            LogInformation($"Initialise for {room.Name}");
            var roomPresenceImplementation = new RoomPresenceImplementation(this, room, ndUserId: ndUserId);
            _roomServices.Add(roomPresenceImplementation);
            roomPresenceImplementation.Initialize();
        }
    }
}