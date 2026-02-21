using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using NetDaemon.AppModel;
using NetDaemon.Extensions.Observables;
using NetDaemon.HassModel;

[NetDaemonApp]
//[Focus]
public class TvIdentityHandler
{
    private readonly IHaContext _ha;
    private readonly IEntities _entities;
    private readonly IServices _services;
    private readonly ILogger<TvIdentityHandler> _logger;
    private string viewer = UNKNOWN_VIEWER;
    private const string UNKNOWN_VIEWER = "Unknown";

    public TvIdentityHandler(IHaContext ha, IEntities entities, IServices services, ILogger<TvIdentityHandler> logger)
    {
        _ha = ha;
        _entities = entities;
        _services = services;
        _logger = logger;
        
        entities.InputSelect.TvViewer.StateChanges()
            .Subscribe(e => {
                _logger.LogInformation("TvViewer changed to {viewer}", e.New?.State);
                viewer = e.New?.State ?? UNKNOWN_VIEWER;
            });


        entities.MediaPlayer.LoungeTv.StateAllChanges()
            .Where(e => e.New?.Attributes?.Source != e.Old?.Attributes?.Source)
            .Subscribe(_ => OnSourceChanged());


        entities.MediaPlayer.LoungeTv.SubscribeOnOff(() => OpenIdentityPage(), () => ResetIdentitySelector());
    }

    private void ResetIdentitySelector()
    {
        _logger.LogInformation("ResetIdentitySelector");
        _entities.InputSelect.TvViewer.SelectOption(UNKNOWN_VIEWER);
    }

    private void OpenIdentityPage()
    {
        _logger.LogInformation("OpenIdentityPage");
        _services.Webostv.Command(new()
        {
            EntityId = _entities.MediaPlayer.LoungeTv.EntityId,
            Command = "system.launcher/open",
            Payload = new
            {
                target = "http://tvtaskboard.niemand/tv_identity"
            }
        });
    }

    private void OnSourceChanged()
    {       
        if (viewer == UNKNOWN_VIEWER || viewer == null)
        {
            _logger.LogInformation("Viewer not selected → forcing browser identity gate.");
            OpenIdentityPage();
        }
    }

    public record TvIdentityPayload(string viewer);
}
