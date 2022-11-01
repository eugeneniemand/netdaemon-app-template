namespace Niemand.Energy;

public static class ExtMethods
{
    public static List<(DateTime startDate, double rate, int hours)> CheapestWindows(this SortedDictionary<DateTime, double> rates)
    {
        var cheapestHour       = rates.WindowedAverageLeft(2).MinWithKey();
        var cheapestTwoHours   = rates.WindowedAverageLeft(4).MinWithKey();
        var cheapestThreeHours = rates.WindowedAverageLeft(6).MinWithKey();

        var list = new List<(DateTime startDate, double rate, int hours)>
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
}