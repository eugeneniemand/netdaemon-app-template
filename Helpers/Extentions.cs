using System.Diagnostics.CodeAnalysis;
using System.Text;
using HomeAssistantGenerated;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Niemand;
using Niemand.Helpers;
using Niemand.Helpers.Notifications;

namespace NetDaemon.Helpers;

public static class Extentions
{
    private const int MustBeLessThan = 100000000; // 8 decimal digits

    public static bool IsUnavailable(this Entity? entity) => entity?.EntityState.IsUnavailable() ?? true;
    public static bool IsUnavailable(this EntityState? entityState) => string.Equals(entityState?.State, "unavailable", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Armed away or Armed Night
    /// </summary>
    /// <param name="alarmControlPanel"></param>
    /// <returns></returns>
    public static bool IsArmed(this AlarmControlPanelEntity alarmControlPanel) => alarmControlPanel.IsArmedAway() || alarmControlPanel.IsArmedNight() ;
    public static bool IsArmedNight(this AlarmControlPanelEntity alarmControlPanel) => string.Equals(alarmControlPanel?.State, "armed_night", StringComparison.OrdinalIgnoreCase);
    public static bool IsArmedAway(this AlarmControlPanelEntity alarmControlPanel) => string.Equals(alarmControlPanel?.State, "armed_away", StringComparison.OrdinalIgnoreCase);
    public static bool IsDisarmed(this AlarmControlPanelEntity alarmControlPanel) => string.Equals(alarmControlPanel?.State, "disarmed", StringComparison.OrdinalIgnoreCase);
    public static bool IsTriggered(this AlarmControlPanelEntity alarmControlPanel) => string.Equals(alarmControlPanel?.State, "triggered", StringComparison.OrdinalIgnoreCase);

    public static bool IsArmed(this EntityState<AlarmControlPanelAttributes> alarmControlPanel) => alarmControlPanel.IsArmedAway() || alarmControlPanel.IsArmedNight();
    public static bool IsArmedNight(this EntityState<AlarmControlPanelAttributes> alarmControlPanel) => string.Equals(alarmControlPanel?.State, "armed_night", StringComparison.OrdinalIgnoreCase);
    public static bool IsArmedAway(this EntityState<AlarmControlPanelAttributes> alarmControlPanel) => string.Equals(alarmControlPanel?.State, "armed_away", StringComparison.OrdinalIgnoreCase);
    public static bool IsDisarmed(this EntityState<AlarmControlPanelAttributes> alarmControlPanel) => string.Equals(alarmControlPanel?.State, "disarmed", StringComparison.OrdinalIgnoreCase);
    public static bool IsTriggered(this EntityState<AlarmControlPanelAttributes> alarmControlPanel) => string.Equals(alarmControlPanel?.State, "triggered", StringComparison.OrdinalIgnoreCase);

    public static bool IsAboveHorizon(this SunEntity sun) => string.Equals(sun.State, "above_horizon", StringComparison.OrdinalIgnoreCase);
    public static bool IsBelowHorizon(this SunEntity sun) => string.Equals(sun.State, "below_horizon", StringComparison.OrdinalIgnoreCase);
    
    
    
    public static IServiceCollection SetupDependencies(this IServiceCollection serviceCollection)
        => serviceCollection
           .AddTransient<IEntities, Entities>()
           .AddTransient<IServices, Services>()
           .AddTransient<IAlexa, Alexa>()
           .AddSingleton<IVoiceProvider, VoiceProvider>()
           .AddTransient<Common>()
           .AddScoped<INotificationConfigFactory, NotificationConfigFactory>()
           .AddScoped<IApplianceFactory, ApplianceFactory>()
           .AddScoped<People>()
           .AddScoped<PushNotifier>()
           .AddSingleton<IServiceProvider>(sp => sp);


    public static string GetFixedHash(this string s)
    {
        uint hash = 0;
        // if you care this can be done much faster with unsafe 
        // using fixed char* reinterpreted as a byte*
        foreach (var b in Encoding.Unicode.GetBytes(s))
        {
            hash += b;
            hash += hash << 10;
            hash ^= hash >> 6;
        }

        // final avalanche
        hash += hash << 3;
        hash ^= hash >> 11;
        hash += hash << 15;
        // helpfully we only want positive integer < MUST_BE_LESS_THAN
        // so simple truncate cast is ok if not perfect
        return ((int)(hash % MustBeLessThan)).ToString();
    }


    /// <summary>
    ///     Returns the key and value of the item in the collection with the minimum value
    /// </summary>
    /// <param name="existing"></param>
    /// <returns></returns>
    public static KeyValuePair<DateTime, double> MinWithKey(this SortedDictionary<DateTime, double> existing)
    {
        var dates = existing.Keys.ToList();
        var rates = existing.Values.ToList();
        var min = rates.Min();
        var key = dates[rates.IndexOf(min)];
        return new KeyValuePair<DateTime, double>(key, min);
    }

    public static Dictionary<string, object>? ToDictionary(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        return dictionary;
    }

    public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this Dictionary<TKey, TValue> existing) where TKey : notnull => new(existing);


    /// <summary>
    ///     Calculates a sliding the average from the left of collection over the window of items specified by size
    /// </summary>
    /// <param name="existing"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static SortedDictionary<DateTime, double> WindowedAverageLeft(this SortedDictionary<DateTime, double> existing, int size)
    {
        var dict = new SortedDictionary<DateTime, double>();
        var keys = existing.Keys.ToList();
        var values = existing.Values.ToList();
        for (var i = 0; i < existing.Count - size + 1; i++) dict.Add(keys[i], values.Skip(i).Take(size).Average());

        return dict;
    }


    public static bool LastChangedOlderThan(this Entity entityState, TimeSpan timeSpan) => DateTime.Now - (entityState.EntityState?.LastChanged ?? DateTime.Today) > timeSpan;
}