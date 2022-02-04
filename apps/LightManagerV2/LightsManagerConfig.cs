using System.Collections.Generic;
using Ha;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;

namespace LightManagerV2;

public class LightsManagerConfig
{
    private INetDaemonRxApp _app;

    public LightsManagerConfig()
    {
        //PresenceEntityIds     = new List<BinarySensorEntity>();
        //ControlEntityIds      = new List<LightEntity>();
        //KeepAliveEntityIds    = new List<BinarySensorEntity>();
        //NightControlEntityIds = new List<LightEntity>();
    }


    public int NightTimeout { get; set; }
    public int NightBrightness { get; set; }

    public int NightColour { get; set; }

    //public string RoomPresenceEntityId => $"sensor.light_manager_{Name.ToLower()}";
    //public string EnabledSwitchEntityId => $"switch.light_manager_{Name.ToLower()}";
    public int OverrideTimeout { get; set; } = 900;


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