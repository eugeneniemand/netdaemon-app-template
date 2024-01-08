using MQTTnet.Protocol;
using NetDaemon.Extensions.MqttEntityManager;

namespace Niemand.Tests.Mocks;

public class MqttEntityManagerMock : IMqttEntityManager
{
    public MqttQualityOfServiceLevel QualityOfServiceLevel { get; set; }
    public Task CreateAsync(string entityId, EntityCreationOptions? options = null, object? additionalConfig = null) => throw new NotImplementedException();

    public Task RemoveAsync(string entityId) => throw new NotImplementedException();

    public Task SetAttributesAsync(string entityId, object attributes) => throw new NotImplementedException();

    public Task SetAvailabilityAsync(string entityId, string availability) => throw new NotImplementedException();

    public Task SetStateAsync(string entityId, string state) => throw new NotImplementedException();

    public Task<IObservable<string>> PrepareCommandSubscriptionAsync(string entityId) => throw new NotImplementedException();
}