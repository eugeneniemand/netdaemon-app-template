using System.Collections.Generic;
using HomeAssistantGenerated;

namespace LightManagerV2;

public class ManagerConfig
{
    public IEnumerable<Manager>? Rooms { get; set; }
    public int GuardTimeout { get; set; } = 900;
    public string MaxDuration { get; set; }
    public string MinDuration { get; set; }
    public string NdUserId { get; set; }
    public SwitchEntity RandomSwitchEntity { get; set; }
}