using System.IO;
using NetDaemon.Client;

namespace Niemand.HistoryReader;

[NetDaemonApp]
//[Focus]
public class HistoryReader(IHomeAssistantApiManager apiManager) :IAsyncInitializable
{
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var history = await apiManager.GetApiCallAsync<List<List<History>>>("history/period?filter_entity_id=light.kitchen", CancellationToken.None);
        foreach (var histEvent in history.First())
        {
            //await File.AppendAllTextAsync("history.txt", $"{histEvent.last_changed} - {histEvent.state} - {histEvent.attributes.friendly_name}\n");
        }
    }
}

public class Attributes
{
    public object color_mode { get; set; }
    public object brightness { get; set; }
    public bool off_with_transition { get; set; }
    public int? off_brightness { get; set; }
    public string friendly_name { get; set; }
}

public class Context
{
    public string id { get; set; }
    public object parent_id { get; set; }
    public object user_id { get; set; }
}

public class History
{
    public string entity_id { get; set; }
    public string state { get; set; }
    public Attributes attributes { get; set; }
    public DateTime last_changed { get; set; }
    public DateTime last_updated { get; set; }
    public Context context { get; set; }
}