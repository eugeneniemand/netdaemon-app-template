// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.HaContextMock
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using Moq;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


#nullable enable
namespace LightManager.Tests
{
  public class HaContextMock : Mock<HaContextMockImpl>
  {
    public T GetEntity<T>(string entityId, string state) where T : Entity
    {
      T instance = Activator.CreateInstance(typeof (T), (object) this.Object, (object) entityId) as T;
      this.TriggerStateChange((Entity) instance, state);
      return instance;
    }

    public void TriggerEvent(Event @event) => this.Object.EventsSubject.OnNext(@event);

    public void TriggerStateChange(Entity entity, string newStatevalue, object? attributes = null)
    {
      EntityState newState = new EntityState()
      {
        State = newStatevalue
      };
      this.TriggerStateChange(entity.EntityId, newState, attributes);
    }

    public void TriggerStateChange(string entityId, EntityState newState, object? attributes = null)
    {
      if (attributes != null)
        newState = newState.WithAttributes(attributes);
      EntityState entityState;
      EntityState old = this.Object._entityStates.TryGetValue(entityId, out entityState) ? entityState : (EntityState) null;
      this.Object._entityStates[entityId] = newState;
      this.Object.StateAllChangeSubject.OnNext(new StateChange(new Entity((IHaContext) this.Object, entityId), old, newState));
    }

    public void TriggerStateChange(Entity entity, EntityState newState, object? attributes = null) => this.TriggerStateChange(entity.EntityId, newState, attributes);

    public void VerifyCallService(string domain, string service, Entity entity, Times? times = null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      HaContextMock.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new HaContextMock.\u003C\u003Ec__DisplayClass5_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass50.domain = domain;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass50.service = service;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass50.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass50.entity = entity;
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      this.Verify(Expression.Lambda<Action<HaContextMockImpl>>((Expression) Expression.Call(m, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (HaContextMockImpl.CallService)), new Expression[4]
      {
        cDisplayClass50.domain,
        cDisplayClass50.service,
        It.Is<ServiceTarget>((Expression<Func<ServiceTarget, bool>>) (s => this.ServiceTargetEquals(s, cDisplayClass50.entity.EntityId))),
        (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (It.IsAny)), Array.Empty<Expression>())
      }), parameterExpression), times ?? Times.Once());
    }

    public void VerifyEntityTurnedOff(Entity entity, Times? times = null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      HaContextMock.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new HaContextMock.\u003C\u003Ec__DisplayClass6_0()
      {
        \u003C\u003E4__this = this,
        entity = entity
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.domain = cDisplayClass60.entity.EntityId.Substring(0, cDisplayClass60.entity.EntityId.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) - 0);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      this.Verify(Expression.Lambda<Action<HaContextMockImpl>>((Expression) Expression.Call(m, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (HaContextMockImpl.CallService)), new Expression[4]
      {
        cDisplayClass60.domain,
        "turn_off",
        It.Is<ServiceTarget>((Expression<Func<ServiceTarget, bool>>) (s => this.ServiceTargetEquals(s, cDisplayClass60.entity.EntityId))),
        (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (It.IsAny)), Array.Empty<Expression>())
      }), parameterExpression), times ?? Times.Once());
    }

    public void VerifyEntityTurnedOn(Entity entity, Times? times = null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      HaContextMock.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new HaContextMock.\u003C\u003Ec__DisplayClass7_0()
      {
        \u003C\u003E4__this = this,
        entity = entity
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.domain = cDisplayClass70.entity.EntityId.Substring(0, cDisplayClass70.entity.EntityId.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) - 0);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      this.Verify(Expression.Lambda<Action<HaContextMockImpl>>((Expression) Expression.Call(m, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (HaContextMockImpl.CallService)), new Expression[4]
      {
        cDisplayClass70.domain,
        "turn_on",
        It.Is<ServiceTarget>((Expression<Func<ServiceTarget, bool>>) (s => this.ServiceTargetEquals(s, cDisplayClass70.entity.EntityId))),
        (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (It.IsAny)), Array.Empty<Expression>())
      }), parameterExpression), times ?? Times.Once());
    }

    public void VerifyServiceCalled(Entity entity, string domain, string service)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      HaContextMock.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new HaContextMock.\u003C\u003Ec__DisplayClass8_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.domain = domain;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.service = service;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.entity = entity;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this.Verify((Expression<Action<HaContextMockImpl>>) (m => m.CallService(cDisplayClass80.domain, cDisplayClass80.service, It.Is<ServiceTarget>((Expression<Func<ServiceTarget, bool>>) (s => s.EntityIds.SingleOrDefault<string>() == cDisplayClass80.entity.EntityId)), default (object))));
    }

    private bool ServiceTargetEquals(ServiceTarget? serviceTarget, string entity) => serviceTarget.EntityIds.Contains<string>(entity);
  }
}
