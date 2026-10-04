namespace LightManagerV2;

public class ManagerConfig
{
    public List<Manager> Rooms { get; set; } = new List<Manager>();
    public int GuardTimeout { get; set; } = 900;
    public string MaxDuration { get; set; }
    public string MinDuration { get; set; }
    public string NdUserId { get; set; }
    public SwitchEntity RandomSwitchEntity { get; set; }
    public bool RandomizerEnabled { get; set; } = true;
    public int RandomizerMinActiveRooms { get; set; } = 2;
    public int RandomizerMaxActiveRooms { get; set; } = 3;
    public string RandomizerMinOnDuration { get; set; } = "00:20:00";
    public string RandomizerMaxOnDuration { get; set; } = "00:45:00";
    public string RandomizerMinShuffleInterval { get; set; } = "00:10:00";
    public string RandomizerMaxShuffleInterval { get; set; } = "00:25:00";
}
