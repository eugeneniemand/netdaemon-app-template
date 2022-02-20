//using System.Collections.Generic;
//using NetDaemon.Common.Reactive;

//namespace LightManagerV2;

//public class LightsManagerConfig
//{
//    private INetDaemonRxApp _app;

//    public bool Debug { get; set; }
//    public bool SunriseBrightnessEnabled { get; set; }
//    public bool SunriseColourEnabled { get; set; }


//    public bool SunriseEnabled { get; set; }
//    public IEnumerable<string> RandomStates { get; set; }
//    public int GuardTimeout { get; set; } = 900;
//    public int NightBrightness { get; set; }

//    public int NightColour { get; set; }


//    public int NightTimeout { get; set; }

//    //public string RoomPresenceEntityId => $"sensor.light_manager_{Name.ToLower()}";
//    //public string EnabledSwitchEntityId => $"switch.light_manager_{Name.ToLower()}";
//    public int OverrideTimeout { get; set; } = 900;
//    public int SunriseEndBrightness { get; set; }
//    public int SunriseEndKelvin { get; set; }
//    public int SunriseStartBrightness { get; set; }
//    public int SunriseStartKelvin { get; set; }
//    public int SunriseUpdateInterval { get; set; }
//    public string ConditionEntityId { get; set; }
//    public string ConditionEntityState { get; set; }
//    public string NdUserId { get; set; }
//    public string RandomEntityId { get; set; }
//    public string SunriseEndTime { get; set; } = null!;
//    public string SunriseStartTime { get; set; } = null!;
//}