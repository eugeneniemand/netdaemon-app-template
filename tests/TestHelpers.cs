using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using Microsoft.Reactive.Testing;
using Newtonsoft.Json;

public static class TestHelpers
{

    /// <summary>
    /// Time in "HH:mm:ss" format
    /// </summary>
    /// <param name="testTime"></param>
    /// <returns></returns>
    public static DateTime DateTimeFromString(string testTime)
    {
        return DateTime.ParseExact(testTime, "HH:mm:ss", new DateTimeFormatInfo());
    }

    public static void AdvanceTo(this TestScheduler scheduler, string time)
    {
        scheduler.AdvanceTo(DateTimeFromString(time).Ticks);
    }

    public static bool AreEqualObjects(object expected, object actual)
    {
        var expectedJson = JsonConvert.SerializeObject(expected);
        var actualJson = JsonConvert.SerializeObject(actual);

        return expectedJson == actualJson;
    }



    public static dynamic ToDynamic(this object value)
    {
        IDictionary<string, object> expando = new ExpandoObject();

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            expando.Add(property.Name, property.GetValue(value));

        return expando as ExpandoObject;
    }
}
