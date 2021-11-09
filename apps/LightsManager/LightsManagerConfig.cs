using System.Collections.Generic;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;

namespace LightsManager
{
    public class LightsManagerConfig
    {
        private          INetDaemonRxApp _app;
        private readonly Configurator    _configurator;

        public LightsManagerConfig()
        {
            PresenceEntityIds     = new List<string>();
            ControlEntityIds      = new List<string>();
            KeepAliveEntityIds    = new List<string>();
            NightControlEntityIds = new List<string>();
            _configurator         = new Configurator(this);
        }


        public string Name { get; set; } = null!;
        public int? LuxLimit { get; set; }
        public string? LuxEntityId { get; set; }
        public string? LuxLimitEntityId { get; set; }

        public int Timeout { get; set; }

        public int OverrideTimeout { get; set; } = 900;
        public IEnumerable<string> PresenceEntityIds { get; set; }
        public IEnumerable<string> ControlEntityIds { get; set; }
        public IEnumerable<string> KeepAliveEntityIds { get; set; }
        public IEnumerable<string> NightControlEntityIds { get; set; }
        public IEnumerable<string> NightTimeEntityStates { get; set; } = null!;
        public string? NightTimeEntityId { get; set; }

        public int NightTimeout { get; set; }
        public int NightBrightness { get; set; }
        public int NightColour { get; set; }
        public string RoomPresenceEntityId => $"sensor.light_manager_{Name.ToLower()}";
        public string EnabledSwitchEntityId => $"switch.light_manager_{Name.ToLower()}";
        public string? CircadianSwitchEntityId { get; set; }


        public bool SunriseEnabled { get; set; }
        public bool SunriseColourEnabled { get; set; }
        public bool SunriseBrightnessEnabled { get; set; }
        public int SunriseStartBrightness { get; set; }
        public int SunriseStartKelvin { get; set; }
        public string SunriseStartTime { get; set; } = null!;
        public string SunriseEndTime { get; set; } = null!;
        public int SunriseEndBrightness { get; set; }
        public int SunriseEndKelvin { get; set; }
        public int SunriseUpdateInterval { get; set; }
        public bool Debug { get; set; }
        public string ConditionEntityId { get; set; }
        public string ConditionEntityState { get; set; }
        public string RandomEntityId { get; set; }
        public IEnumerable<string> RandomStates { get; set; }
        public string NdUserId { get; set; }
        public int GuardTimeout { get; set; } = 900;
    }
}