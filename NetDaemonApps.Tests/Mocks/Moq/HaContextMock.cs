using System.Linq;
using System.Reactive.Subjects;
using HomeAssistantGenerated;
using Moq;
using NetDaemon.HassModel.Entities;

namespace NetDaemon.HassModel.Mocks.Moq;

public class HaContextMock : Mock<HaContextMockBase>, IHaContextMock
{
    public HaContextMock()
    {
        void Callback(string domain, string service, ServiceTarget target, object? data)
        {
            if (data == null)
                TriggerStateChange(target.EntityIds.First(), new EntityState { State = "" });
            else
                TriggerStateChange(target.EntityIds.First(),
                    data is InputSelectSelectOptionParameters selectOption
                        ? new EntityState { State = selectOption.Option }
                        : new EntityState { State = data.ToString() });
        }

        Setup(m => m.CallService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ServiceTarget>(), It.IsAny<object>()))
            .Callback(Callback)
            .CallBase();
        HaContext = Object;
    }

    public IHaContext HaContext { get; }

    public Subject<StateChange> StateChangeSubject { get; } = new();

    public void TriggerStateChange(Entity entity, string newStatevalue, object? attributes = null)
    {
        Object.TriggerStateChange(entity, newStatevalue, attributes);
    }

    public void TriggerStateChange(string entityId, EntityState newState)
    {
        Object.TriggerStateChange(entityId, newState);
    }

    public void VerifyServiceCalled(Entity entity, string domain, string service)
    {
        Verify(m => m.CallService(domain, service,
            It.Is<ServiceTarget?>(s => s!.EntityIds!.SingleOrDefault() == entity.EntityId),
            null));
    }

    public void TriggerStateChange(string entityId, EntityState oldState, EntityState newState)
    {
        Object.TriggerStateChange(entityId, oldState, newState);
    }
}