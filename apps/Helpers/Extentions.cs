using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Niemand.Helpers;

public static class Extentions
{
    private const int MustBeLessThan = 100000000; // 8 decimal digits

    public static IServiceCollection AddGeneratedCode(this IServiceCollection serviceCollection)
        => serviceCollection
           .AddTransient<IEntities, Entities>()
           .AddTransient<IServices, Services>();
    //.AddTransient<Alexa>();

    public static bool Equals(this string enumString, Dishwasher.DishwasherCycle value)
    {
        if (Enum.TryParse<Dishwasher.DishwasherCycle>(enumString, out var v)) return value == v;

        return false;
    }

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
        return ( (int)( hash % MustBeLessThan ) ).ToString();
    }

    public static KeyValuePair<DateTime, double> MinWithKey(this SortedDictionary<DateTime, double> existing)
    {
        var dates = existing.Keys.ToList();
        var rates = existing.Values.ToList();
        var min   = rates.Min();
        var key   = dates[rates.IndexOf(min)];
        return new KeyValuePair<DateTime, double>(key, min);
    }

    public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this Dictionary<TKey, TValue> existing) where TKey : notnull => new(existing);


    public static SortedDictionary<DateTime, double> WindowLeft(this SortedDictionary<DateTime, double> existing, int size)
    {
        var dict   = new SortedDictionary<DateTime, double>();
        var keys   = existing.Keys.ToList();
        var values = existing.Values.ToList();
        for (var i = 0; i < existing.Count - size - 1; i++) dict.Add(keys[i], values.Skip(i).Take(size).Average());

        return dict;
    }


    ///// <summary>
    /////     Convert N3 to int 0.3 or W10 to in 1.0
    ///// </summary>
    ///// <param name="volume"></param>
    ///// <returns></returns>
    //public static double ToVolumeLevel(this Volume volume)
    //{
    //    var intVol = int.Parse(Enum.GetName(volume)![1..]);
    //    return intVol / 10d;
    //}
}