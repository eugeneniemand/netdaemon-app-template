using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization.Formatters;
using System.Text.Json;
using Moq;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NetDaemon.HassModel.Tests.Entities;

internal class HaContextMock : Mock<IHaContext>
{
    public HaContextMock()
    {
        Setup(m => m.StateAllChanges()).Returns(StateAllChangeSubject);
        Setup(h => h.CallService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ServiceTarget?>(), It.IsAny<object?>()))
            .Callback<string, string, ServiceTarget, object?>((domain, service, target, data) => { ServiceCalls.Add(new ServiceCall(domain, service, target, data)); });
    }

    public Subject<StateChange> StateAllChangeSubject { get; } = new();

    public List<ServiceCall> ServiceCalls { get; } = new();

    public record ServiceCall(string Domain, string Service, ServiceTarget? Target, object? Data);
}

internal static class Extensions
{
    public static IEnumerable<HaContextMock.ServiceCall> ForDomain(this IEnumerable<HaContextMock.ServiceCall> serviceCalls, string domain)
    {
        return serviceCalls.Where(sc => sc.Domain == domain);
    }

    public static IEnumerable<Dictionary<string, object>> Data(this IEnumerable<HaContextMock.ServiceCall> serviceCalls)
    {
        return serviceCalls.ToDictionary().Select(sc => sc["data"].ToDictionary());
    }

    public static Dictionary<string, object> Data(this HaContextMock.ServiceCall serviceCalls)
    {
        return serviceCalls.ToDictionary()["data"].ToDictionary();
    }

    public static IEnumerable<HaContextMock.ServiceCall> ForService(this IEnumerable<HaContextMock.ServiceCall> serviceCalls, string service)
    {
        return serviceCalls.Where(sc => sc.Service == service);
    }

    public static IReadOnlyCollection<string> EntityIds(this IEnumerable<HaContextMock.ServiceCall> serviceCalls)
    {
        return serviceCalls.SelectMany(sc => sc?.Target?.EntityIds ?? new List<string>()).ToList();
    }

    public static IReadOnlyCollection<string> AreaIds(this IEnumerable<HaContextMock.ServiceCall> serviceCalls)
    {
        return serviceCalls.SelectMany(sc => sc?.Target?.AreaIds ?? new List<string>()).ToList();
    }

    public static IReadOnlyCollection<string> DeviceIds(this IEnumerable<HaContextMock.ServiceCall> serviceCalls)
    {
        return serviceCalls.SelectMany(sc => sc?.Target?.DeviceIds ?? new List<string>()).ToList();
    }

    public static JsonElement AsJsonElement(this object value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        return JsonSerializer.Deserialize<JsonElement>(jsonString);
    }

    public static Mock<IObserver<T>> SubscribeMock<T>(this IObservable<T> observable)
    {
        var observerMock = new Mock<IObserver<T>>();

        observable.Subscribe(observerMock.Object);

        return observerMock;
    }
}

public static class ObjectToDictionaryHelper
{
    public static IEnumerable<Dictionary<string, object>> ToDictionary(this IEnumerable<object> sources)
    {
        return sources.Select(s => s.ToDictionary<object>());
    }

    public static Dictionary<string, object> ToDictionary(this object source)
    {
        return source.ToDictionary<object>();
    }

    public static Dictionary<string, T> ToDictionary<T>(this object source)
    {
        if (source == null)
            ThrowExceptionWhenSourceArgumentIsNull();

        var dictionary = new Dictionary<string, T>();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            AddPropertyToDictionary<T>(property, source, dictionary);

        return dictionary;
    }

    private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
    {
        var value = property.GetValue(source);
        if (IsOfType<T>(value))
            dictionary.Add(property.Name.ToLower(), (T)value);
    }

    private static bool IsOfType<T>(object value)
    {
        return value is T;
    }

    private static void ThrowExceptionWhenSourceArgumentIsNull()
    {
        throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
    }
}