using System.Web;
using NetDaemon.Client;
using NetDaemon.Extensions.MqttEntityManager;

namespace Niemand;

public class VacationAppConfiguration
{
    public IEnumerable<string> Lights { get; set; }
}

//[Focus]
[NetDaemonApp]
public class VacationApp : IAsyncInitializable
{
    private const    string                   SensorVacationNextState     = "sensor.vacation_next_state";
    private const    string                   SwitchVacationModeEnabledId = "switch.vacation_mode_enabled";
    private readonly IHomeAssistantApiManager _api;
    private readonly VacationAppConfiguration _config;
    private readonly IEntities                _entities;
    private readonly IMqttEntityManager       _entityManager;
    private readonly IHaContext               _haContext;
    private readonly ILogger<VacationApp>     _logger;
    private readonly IScheduler               _scheduler;
    private readonly IServices                _services;
    private          List<IDisposable>?       _schedules;
    private          IDisposable              _setupScheduler;
    private          List<LampEntity>         _statesRemainingForToday;

    public VacationApp(IHaContext ha, ILogger<VacationApp> logger, IAppConfig<VacationAppConfiguration> config, IEntities entities, IServices services, IHomeAssistantApiManager api, IMqttEntityManager entityManager, IScheduler scheduler)
    {
        _haContext     = ha;
        _logger        = logger;
        _entities      = entities;
        _services      = services;
        _api           = api;
        _entityManager = entityManager;
        _scheduler     = scheduler;
        _config        = config.Value;
        var lastNotification = DateTime.MinValue;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_haContext.Entity(SwitchVacationModeEnabledId).State == null || string.Equals(_haContext.Entity(SwitchVacationModeEnabledId).State, "unavailable", StringComparison.InvariantCultureIgnoreCase))
            await _entityManager.CreateAsync(SwitchVacationModeEnabledId, new EntityCreationOptions(Name: "Vacation Mode", DeviceClass: "switch", Persist: true));

        if (_haContext.Entity(SensorVacationNextState).State == null || string.Equals(_haContext.Entity(SensorVacationNextState).State, "unavailable", StringComparison.InvariantCultureIgnoreCase))
            await _entityManager.CreateAsync(SensorVacationNextState, new EntityCreationOptions(Name: "Vacation Next State", Persist: true));

        ( await _entityManager.PrepareCommandSubscriptionAsync(SwitchVacationModeEnabledId) )
            .Subscribe(async s =>
            {
                if (s.ToLower() == "on")
                    await EnableVacationMode();
                else
                    DisableVacationMode();

                await _entityManager.SetStateAsync(SwitchVacationModeEnabledId, s);
            });
    }

    public void DisableVacationMode()
    {
        _logger.LogInformation("Vacation Mode Disabling...");
        _haContext.GetAllEntities().Where(e => e.EntityId.Contains("adaptive")).ToList().ForEach(e => { _services.Switch.TurnOn(ServiceTarget.FromEntity(e.EntityId)); });
        _schedules?.ForEach(s => s?.Dispose());
        _setupScheduler?.Dispose();
        _logger.LogInformation("Vacation Mode Disabled");
    }

    public async Task EnableVacationMode()
    {
        _logger.LogInformation("Vacation Mode Enabling...");
        _haContext.GetAllEntities().Where(e => e.EntityId.Contains("adaptive")).ToList().ForEach(e => { _services.Switch.TurnOff(ServiceTarget.FromEntity(e.EntityId)); });
        _schedules?.ForEach(s => s?.Dispose());
        await SetupDay();
        _setupScheduler = _scheduler.ScheduleCron("1 0 * * *", async () => await SetupDay());
        _logger.LogInformation("Vacation Mode Enabled");
    }

    private async Task<List<LampEntity>> GetHistory(DateTime dateTime, string lightEntityId)
    {
        _logger.LogInformation("Getting History on {date:d} for {light}...", dateTime, lightEntityId);
        var apiUrl               = $"history/period/{dateTime.Date:s}?filter_entity_id={HttpUtility.UrlEncode(lightEntityId)}&no_attributes";
        var lampEntityCollection = await _api.GetApiCallAsync<LampEntityCollection>(apiUrl, default);
        var lightStatesRaw       = lampEntityCollection?.First() ?? new List<LampEntity>();
        // API is returning previous day's last record
        lightStatesRaw.RemoveAll(sc => sc.last_changed.Date != dateTime.Date); // lightStatesRaw.Where(s => s.last_changed.Date == dateTime.Date).ToList();
        _logger.LogInformation("Got {count} events on {date:d} for {light}", lightStatesRaw.Count, dateTime, lightEntityId);
        return lightStatesRaw;
    }

    private async Task SetupDay()
    {
        var history = new List<LampEntity>();
        foreach (var entityId in _config.Lights) history.AddRange(await GetHistory(_scheduler.Now.AddDays(-7).ToLocalTime().Date, entityId));

        history.ForEach(sc => sc.last_changed = sc.last_changed.AddDays(7));
        _statesRemainingForToday = history.Where(s => s.last_changed >= _scheduler.Now.ToLocalTime()).OrderBy(s => s.last_changed).ToList();

        var msg = _statesRemainingForToday.Aggregate("", (current, lampEntity) => current + $"{lampEntity.last_changed} {lampEntity.entity_id} {lampEntity.state.ToUpper()}\n");
        _logger.LogInformation(msg);
        await _entityManager.SetStateAsync(SensorVacationNextState, $"Waiting for {_statesRemainingForToday.FirstOrDefault()?.last_changed}");

        _schedules = _statesRemainingForToday.Select(lampEntity => _scheduler.Schedule(lampEntity.last_changed, async () =>
        {
            await _entityManager.SetStateAsync(SensorVacationNextState, $"{lampEntity.entity_id} turned {lampEntity.state}");
            if (lampEntity.state.ToLower() == "on")
            {
                _services.Light.TurnOn(ServiceTarget.FromEntity(lampEntity.entity_id), brightnessPct: 100);
                _logger.LogInformation("{light} turned on", lampEntity.entity_id);
            }
            else
            {
                _services.Light.TurnOff(ServiceTarget.FromEntity(lampEntity.entity_id));
                _logger.LogInformation("{light} turned off", lampEntity.entity_id);
            }
        })).ToList();
    }
}

public class LampEntity
{
    public DateTime last_changed { get; set; }

    public DateTime last_updated { get; set; }

    //public LampAttributes attributes { get; set; }
    public string entity_id { get; set; }
    public string state { get; set; }
}

public class LampAttributes
{
    public bool? off_with_transition { get; set; }
    public int? off_brightness { get; set; }
    public string color_mode { get; set; }
    public string friendly_name { get; set; }
}

public class LampEntityCollection : List<List<LampEntity>>
{
}