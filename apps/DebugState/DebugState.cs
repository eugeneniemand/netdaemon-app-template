using System.IO;

public class DebugStateConfiguration
{
    public bool DebugStateEnabled { get; set; }
    public List<Entity>? DebugStateEntities { get; set; }
}

[Focus]
[NetDaemonApp]
public class DebugState
{
    public DebugState(IHaContext ha, IAppConfig<DebugStateConfiguration> configuration)
    {
        if (!configuration.Value.DebugStateEnabled) return;
        foreach (var entity in configuration.Value.DebugStateEntities)
            ha.Entity(entity.EntityId).StateChanges().Subscribe(s =>
            {
                var json = JsonSerializer.Serialize(s.New);
                File.AppendAllText($"data_dump_{DateTime.Now:yyyyMMdd}.jsonl", json + '\n');
            });
    }
}