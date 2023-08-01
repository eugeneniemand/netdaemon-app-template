using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using NetDaemon.Extensions.MqttEntityManager;

namespace Niemand.Energy;

[NetDaemonApp]
//[Focus]
public class AgileRatesApp : IAsyncInitializable, IDisposable
{
    private readonly IMqttEntityManager _entityManager;
    //private readonly Alexa      _alexa;

    private readonly IHaContext                                         _haContext;
    private readonly ILogger<AgileRatesApp>                             _logger;
    private readonly IScheduler                                         _scheduler;
    private readonly IServices                                          _services;
    private          List<(DateTime startDate, double rate, int hours)> _cheapestWindows;

    public AgileRatesApp(IHaContext haContext, IScheduler scheduler, ILogger<AgileRatesApp> logger, IServices services, IMqttEntityManager entityManager) //, Alexa alexa)
    {
        _haContext     = haContext;
        _scheduler     = scheduler;
        _logger        = logger;
        _entityManager = entityManager;
        //_alexa     = alexa;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating Entity {entity}", "octopusagile.all_rates_new");
        await _entityManager.CreateAsync("sensor.all_rates_new", new EntityCreationOptions(Name: "", Persist: true));
        _logger.LogInformation("Created Entity {entity}", "octopusagile.all_rates_new");
        await SetRates();
        _scheduler.ScheduleCron("0,30 * * * *", async () => await SetRates());
    }

    public void Dispose()
    {
    }

    private static async Task<Dictionary<string, double>> GetRatesForToday()
    {
        var client  = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.octopus.energy/v1/products/AGILE-VAR-22-10-19/electricity-tariffs/E-1R-AGILE-VAR-22-10-19-A/standard-unit-rates/");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "sk_live_am45F158HgvTnJzEtj02EeQx:");
        var response = client.SendAsync(request);
        if (response.Result.StatusCode != HttpStatusCode.OK) throw new Exception("Couldn't Get Rates");

        var ratesJson     = await response.Result.Content.ReadAsStringAsync();
        var rates         = JsonSerializer.Deserialize<Rates>(ratesJson);
        var ratesForToday = rates.Results.OrderByDescending(r => r.ValidFrom).Where(r => r.ValidTo >= DateTime.Now.ToUniversalTime()).ToDictionary(r => $"{r.ValidFrom:s}Z", r => r.Value);
        return ratesForToday;
    }

    private async Task SetRates()
    {
        _logger.LogInformation("Setting Entity Attributes for {entity}", "sensor.all_rates_new");
        try
        {
            await _entityManager.SetStateAsync("sensor.all_rates_new", $"{DateTime.Now:s}").WaitAsync(TimeSpan.FromSeconds(1));
            await _entityManager.SetAttributesAsync("sensor.all_rates_new", await GetRatesForToday()).WaitAsync(TimeSpan.FromSeconds(1));
            _logger.LogInformation("Set Entity Attributes for {entity}", "sensor.all_rates_new");
        }
        catch (TimeoutException e)
        {
            _logger.LogInformation("Set Entity Attributes timed out for {entity}\n{error}", "sensor.all_rates_new", e.Message);
        }
    }

    public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => DateTime.Parse(reader.GetString() ?? string.Empty).ToLocalTime();

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString());
        }
    }

    private class Rates
    {
        [JsonPropertyName("count")] public int Count { get; set; }
        [JsonPropertyName("results")] public List<Rate> Results { get; set; }
        [JsonPropertyName("next")] public string Next { get; set; }
        [JsonPropertyName("previous")] public string Previous { get; set; }
    }

    private class Rate
    {
        [JsonPropertyName("valid_from")] public DateTime ValidFrom { get; set; }
        [JsonPropertyName("valid_to")] public DateTime ValidTo { get; set; }
        [JsonPropertyName("value_inc_vat")] public double Value { get; set; }
    }
}