using MQTTnet.Protocol;
using NetDaemon.Extensions.MqttEntityManager;

namespace Niemand.Tests.Mocks;

public class MqttEntityManagerMock : IMqttEntityManager
{
    public Task CreateAsync(string entityId, EntityCreationOptions? options = null, object? additionalConfig = null) => Task.CompletedTask;

    public Task<IObservable<string>> PrepareCommandSubscriptionAsync(string entityId) => (Task<IObservable<string>>)Task.CompletedTask;
    public MqttQualityOfServiceLevel QualityOfServiceLevel { get; set; }

    public Task RemoveAsync(string entityId) => Task.CompletedTask;

    public Task SetAttributesAsync(string entityId, object attributes) => Task.CompletedTask;

    public Task SetAvailabilityAsync(string entityId, string availability) => Task.CompletedTask;

    public Task SetStateAsync(string entityId, string state) => Task.CompletedTask;
}