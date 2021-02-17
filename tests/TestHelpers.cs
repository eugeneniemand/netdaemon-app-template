using System;
using System.Globalization;
using Microsoft.Reactive.Testing;

public static class TestHelpers
{


    public static DateTime DateTimeFromString(string testTime)
    {
        return DateTime.ParseExact(testTime, "HH:mm:ss", new DateTimeFormatInfo());
    }

    public static void AdvanceTo(this TestScheduler scheduler, string time)
    {
        scheduler.AdvanceTo(DateTimeFromString(time).Ticks);
    }
}