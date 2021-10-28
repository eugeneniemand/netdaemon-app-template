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
        public string? NdUserId { get; set; }
        public int GuardTimeout { get; set; } = 900;
        public int MinDuration { get; set; }
        public int MaxDuration { get; set; }
        public const string UNKNOWN = "Unknown";

        public override void Initialize()
        {
            //if (!( Rooms ?? throw new ArgumentNullException(nameof(Rooms), "No Rooms Config Found") ).Any(r => r.Debug)) return;

            LogInformation($"Initialise (DEBUG) ");
            foreach (var lightsManagerConfig in Rooms.Where(r => r.Debug).ToList())
            {
                var configurator = new Configurator(lightsManagerConfig);
                configurator.Configure(this);
                var manager = new Manager(this, configurator);
            }
        }
    }
}