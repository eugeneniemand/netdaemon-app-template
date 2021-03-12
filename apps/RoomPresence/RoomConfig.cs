using System.Collections.Generic;

namespace Presence
{
    public class RoomConfig
    {
        public RoomConfig()
        {
            PresenceEntityIds = new List<string>();
            ControlEntityIds = new List<string>();
            KeepAliveEntityIds = new List<string>();
            NightControlEntityIds = new List<string>();
        }
        public string Name { get; set; }
        public int? LuxLimit { get; set; }
        public string? LuxEntityId { get; set; }
        public string? LuxLimitEntityId { get; set; }
        public int Timeout { get; set; }
        public int OverrideTimeout { get; set; }
        public IEnumerable<string> PresenceEntityIds { get; set; }
        public IEnumerable<string> ControlEntityIds { get; set; }
        public IEnumerable<string> KeepAliveEntityIds { get; set; }
        public IEnumerable<string> NightControlEntityIds { get; set; }
        public IEnumerable<string> NightTimeEntityStates { get; set; }
        public string? NightTimeEntityId { get; set; }        
        public int NightTimeout { get; set; }
        public int NightBrightness { get; set; }
        public int NightColour { get; set; }
        public string RoomPresenceEntityId => $"sensor.room_presence_{Name.ToLower()}";
        public string EnabledSwitchEntityId => $"switch.room_presence_enabled_{Name.ToLower()}";
        public string? CircadianSwitchEntityId { get; set; }


        public bool SunriseEnabled { get; set; } 
        public bool SunriseColourEnabled { get; set; } 
        public bool SunriseBrightnessEnabled { get; set; } 
        public int SunriseStartBrightness { get; set; } 
        public int SunriseStartKelvin { get; set; } 
        public string SunriseStartTime { get; set; } 
        public string SunriseEndTime { get; set; } 
        public int SunriseEndBrightness { get; set; } 
        public int SunriseEndKelvin { get; set; }
        public int SunriseUpdateInterval { get; set; }
    }
}