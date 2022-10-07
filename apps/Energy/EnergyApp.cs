namespace Niemand.Energy;

[NetDaemonApp]
public class App
{
    private readonly Alexa      _alexa;
    private readonly IEntities  _entities;
    private readonly IHaContext _haContext;
    private readonly IServices  _services;

    public App(IHaContext haContext, IScheduler scheduler, ILogger<App> logger, IEntities entities, IServices services, Alexa alexa)
    {
        _haContext = haContext;
        _alexa     = alexa;
        _entities  = entities;
        _services  = services;

        _haContext.Entity("input_button.get_energy_rates")
                  .StateChanges()
                  .Subscribe(_ => Debug());

        scheduler.ScheduleCron("0 6 * * *", () =>
        {
            var rates           = GetRates();
            var cheapestWindows = GetCheapestWindows(rates);
            _services.TelegramBot.SendMessage(GetRatesMessageFull(cheapestWindows), parseMode: "MarkdownV2");
        });
    }

    private void Debug()
    {
        var rates           = GetRates();
        var cheapestWindows = GetCheapestWindows(rates);
        _services.TelegramBot.SendMessage(GetRatesMessageFull(cheapestWindows), parseMode: "MarkdownV2");
        _alexa.SendNotification(new TextToSpeech(GetRatesMessageVoice(cheapestWindows), TimeSpan.Zero));
    }

    private static List<(DateTime, double, int)> GetCheapestWindows(SortedDictionary<DateTime, double> rates)
    {
        var cheapestHour       = rates.WindowLeft(2).MinWithKey();
        var cheapestTwoHours   = rates.WindowLeft(4).MinWithKey();
        var cheapestThreeHours = rates.WindowLeft(6).MinWithKey();

        var list = new List<(DateTime, double, int)>
        {
            ( cheapestHour.Key, cheapestHour.Value, 1 ),
            ( cheapestTwoHours.Key, cheapestTwoHours.Value, 2 ),
            ( cheapestThreeHours.Key, cheapestThreeHours.Value, 3 )
        };
        return list.OrderBy(kvp =>
        {
            var (date, _, _) = kvp;
            return date;
        }).ToList();
    }

    private SortedDictionary<DateTime, double> GetRates()
    {
        var rates = ( (Dictionary<string, object>)_haContext.Entity("octopusagile.all_rates").Attributes )
                    .Where(kvp => DateTime.Parse(kvp.Key) > DateTime.Now)
                    .ToDictionary(kvp => DateTime.Parse(kvp.Key), kvp => ( (JsonElement)kvp.Value ).GetDouble())
                    .ToSortedDictionary();
        return rates;
    }

    private static string GetRatesMessageFull(IEnumerable<(DateTime, double, int)> windows)
    {
        var enumerable = windows.Select(tuple =>
        {
            var (date, rate, hours) = tuple;
            var hoursPhrase = hours == 1 ? "hr" : "hrs";
            return $"{date:HH:mm} - {rate:N} p/kWh ({hours} {hoursPhrase})";
        });

        return "Cheapest Rates(AVG):\n" +
               "```\n" +
               string.Join("\n", enumerable) +
               "```";
    }

    private string GetRatesMessageVoice(IEnumerable<(DateTime, double, int)> windows)
    {
        var enumerable = windows.Select(tuple =>
        {
            var (date, rate, hours) = tuple;
            var hoursPhrase = hours == 1 ? "hour" : "hours";
            return $"{date:HH:mm tt}, for, {hours} {hoursPhrase})";
        });

        return "Cheapest energy rates are." + string.Join("\n", enumerable);
    }
}