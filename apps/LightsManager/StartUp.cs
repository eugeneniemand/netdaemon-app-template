using System;
using System.Collections.Generic;
using System.Linq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;


namespace LightsManager
{
    [Focus]
    public class StartUp : NetDaemonRxApp
    {
        public IEnumerable<LightsManagerConfig>? Rooms { get; set; }
        public string NdUserId { get; set; }
        public int GuardTimeout { get; set; } = 900;
        public int MinDuration { get; set; }
        public int MaxDuration { get; set; }
        public const string UNKNOWN = "Unknown";

        public override void Initialize()
        {
            var configs = Rooms.Any(r => r.Debug) ? Rooms.Where(r => r.Debug).ToList() : Rooms.ToList();
            foreach (var config in configs)
            {
                config.NdUserId = NdUserId;
                var manager = new Manager(this, config);
            }
        }
    }
}