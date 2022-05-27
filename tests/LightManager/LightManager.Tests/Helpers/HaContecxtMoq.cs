// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.HaContextMockImpl
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;


#nullable enable
namespace LightManager.Tests
{
  public class HaContextMockImpl : IHaContext
  {
    public Dictionary<string, EntityState> _entityStates { get; } = new Dictionary<string, EntityState>();

    public Subject<Event> EventsSubject { get; } = new Subject<Event>();

    public Subject<StateChange> StateAllChangeSubject { get; } = new Subject<StateChange>();

    public virtual void CallService(
      string domain,
      string service,
      ServiceTarget? target = null,
      object? data = null)
    {
    }

    public IObservable<Event> Events => (IObservable<Event>) this.EventsSubject;

    public IReadOnlyList<Entity> GetAllEntities() => (IReadOnlyList<Entity>) this._entityStates.Keys.Select<string, Entity>((Func<string, Entity>) (s => new Entity((IHaContext) this, s))).ToList<Entity>();

    public Area? GetAreaFromEntityId(string entityId) => (Area) null;

    public EntityState? GetState(string entityId)
    {
      EntityState entityState;
      return !this._entityStates.TryGetValue(entityId, out entityState) ? (EntityState) null : entityState;
    }

    public virtual void SendEvent(string eventType, object? data = null)
    {
    }

    public IObservable<StateChange> StateAllChanges() => (IObservable<StateChange>) this.StateAllChangeSubject;
  }
}
