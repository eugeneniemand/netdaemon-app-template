using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using System.Text.Json;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;

namespace AwesomeNetdaemon.Test.TestUtils;

public class HaContextMockImpl : IHaContext
{

    public Dictionary<string, EntityState> EntityStates { get; } = new();
    public Subject<StateChange> StateAllChangeSubject { get; } = new();
    public Subject<Event> EventsSubject { get; } = new();

    private readonly List<TestServiceCall> _serviceCalls = new();
    public ReadOnlyCollection<TestServiceCall> ServiceCalls => new(_serviceCalls);

    public IObservable<StateChange> StateAllChanges() => StateAllChangeSubject;

    public EntityState? GetState(string entityId) => EntityStates.TryGetValue(entityId, out var result) ? result : null;

    public IReadOnlyList<Entity> GetAllEntities() => EntityStates.Keys.Select(s => new Entity(this, s)).ToList();

    public void CallService(string domain, string service, ServiceTarget? target = null, object? data = null)
    {
        _serviceCalls.Add(new TestServiceCall(domain, service, target, data));
    }

    public Task<JsonElement?> CallServiceWithResponseAsync(string domain, string service, ServiceTarget? target = null, object? data = null)
    {
        throw new NotImplementedException();
    }

    public Area? GetAreaFromEntityId(string entityId) => null;

    public virtual void SendEvent(string eventType, object? data = null)
    { }

    public IObservable<Event> Events => EventsSubject;

    public void TriggerStateChange(Entity entity, string newStatevalue, object? attributes = null)
    {
        var newState = new EntityState { State = newStatevalue };
        if (attributes != null)
        {
            newState = newState.WithAttributes(attributes);
        }

        TriggerStateChange(entity.EntityId, newState);
    }

    public void TriggerStateChange(string entityId, EntityState newState)
    {
        var oldState = EntityStates.TryGetValue(entityId, out var current) ? current : null;
        EntityStates[entityId] = newState;
        StateAllChangeSubject.OnNext(new StateChange(new Entity(this, entityId), oldState, newState));
    }
}

public record TestServiceCall(string Domain, string Service, ServiceTarget? Target = null, object? Data = null);