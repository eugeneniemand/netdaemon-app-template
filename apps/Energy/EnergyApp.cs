using NetDaemon.Helpers;

namespace Niemand.Energy;

[NetDaemonApp]
// [Focus]
public class EnergyApp
{
    private readonly IEntities _entities;
    //private readonly Alexa      _alexa;

    private readonly IHaContext                                         _haContext;
    private readonly ILogger<EnergyApp>                                 _logger;
    private readonly IScheduler                                         _scheduler;
    private readonly IServices                                          _services;
    private          List<(DateTime startDate, double rate, int hours)> _cheapestWindows;

    public EnergyApp(IHaContext haContext, IScheduler scheduler, ILogger<EnergyApp> logger, IServices services, IEntities entities) //, Alexa alexa)
    {
        _haContext = haContext;
        _scheduler = scheduler;
        _logger    = logger;
        //_alexa     = alexa;

        _services = services;
        _entities = entities;
        CacheCheapestWindows();

        _haContext.Entity("input_button.get_energy_rates")
                  .StateChanges()
                  .Subscribe(_ => { NotifyRates(_cheapestWindows); });

        _haContext.Entity("sensor.all_rates_new")
                  .StateAllChanges()
                  .Subscribe(_ => CacheCheapestWindows());

        scheduler.ScheduleCron("0 6 * * *", () => NotifyRates(_cheapestWindows));
        scheduler.ScheduleCron("0 18 * * *", () => NotifyRates(_cheapestWindows));
        scheduler.ScheduleCron("0,30 * * * *", () =>
        {
            CacheCheapestWindows();
            NotifyWindowsStarted(_cheapestWindows);
        });
    }

    public SortedDictionary<DateTime, double> Rates
    {
        get
        {
            var rates = ( (Dictionary<string, object>)_haContext.Entity("sensor.all_rates_new").Attributes ?? new Dictionary<string, object>() )
                        .Where(kvp => kvp.Key != "friendly_name")
                        .Where(kvp => DateTime.Parse(kvp.Key) >= _scheduler.Now.DateTime)
                        .ToDictionary(kvp => DateTime.Parse(kvp.Key), kvp => ( (JsonElement)kvp.Value ).GetDouble())
                        .ToSortedDictionary();
            return rates;
        }
    }

    public static string GetRatesMessageText(IEnumerable<(DateTime, double, int)> windows)
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

    private void CacheCheapestWindows()
    {
        _logger.LogDebug("Caching rates");
        if (Rates.Count == 0)
        {
            _logger.LogWarning("No Rates Available");
            return;
        }

        _cheapestWindows = Rates.CheapestWindows();

        _entities.InputDatetime.Energy1HourWindow.SetDatetime(time: $"{_cheapestWindows.Single(r => r.hours == 1).startDate:T}");
        _entities.InputDatetime.Energy2HourWindow.SetDatetime(time: $"{_cheapestWindows.Single(r => r.hours == 2).startDate:T}");
        _entities.InputDatetime.Energy3HourWindow.SetDatetime(time: $"{_cheapestWindows.Single(r => r.hours == 3).startDate:T}");

        _logger.LogInformation(GetRatesMessageText(_cheapestWindows));
    }

    private void Debug()
    {
        var rates           = Rates;
        var cheapestWindows = Rates.CheapestWindows();
        _services.TelegramBot.SendMessage(GetRatesMessageText(cheapestWindows), parseMode: "MarkdownV2");
        //_alexa.SendNotification(new TextToSpeech(GetRatesMessageVoice(cheapestWindows), TimeSpan.Zero));
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

    private void NotifyRates(List<(DateTime, double, int)>? cheapestWindows)
    {
        _logger.LogDebug("NotifyRates");
        if (cheapestWindows == null)
            CacheCheapestWindows();

        if (_cheapestWindows == null)
        {
            _logger.LogError("No Rates Found");
            return;
        }

        _services.TelegramBot.SendMessage(GetRatesMessageText(_cheapestWindows), parseMode: "MarkdownV2");
    }

    private void NotifyWindowsStarted(List<(DateTime startDateTime, double rate, int hourWindow)>? cheapestWindows)
    {
        _logger.LogDebug("NotifyWindowsStarted");
        if (cheapestWindows == null)
            CacheCheapestWindows();

        if (cheapestWindows.Single(t => t.hourWindow == 3).startDateTime.ToShortTimeString() != _scheduler.Now.DateTime.ToShortTimeString()) return;

        _logger.LogDebug("NotifyWindowsStarted {window}", Shared.Events.Cheap3HourWindowStarted);
        _haContext.SendEvent(Shared.Events.Cheap3HourWindowStarted.ToString());
    }
}