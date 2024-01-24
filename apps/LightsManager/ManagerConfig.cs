namespace LightManagerV2;

public class ManagerConfig
{
    public List<Manager> Rooms { get; set; } = new List<Manager>();
    public int GuardTimeout { get; set; } = 900;
    public string MaxDuration { get; set; }
    public string MinDuration { get; set; }
    public string NdUserId { get; set; }
    public SwitchEntity RandomSwitchEntity { get; set; }
}