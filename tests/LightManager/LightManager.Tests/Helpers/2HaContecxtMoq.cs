// Decompiled with JetBrains decompiler
// Type: LightManager.Tests.TestExtensions
// Assembly: LightManager.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2F39857-CC8B-442E-A899-B01585AEA5F9
// Assembly location: C:\eugene\LightManager.Tests\bin\Debug\net6.0\LightManager.Tests.dll

using NetDaemon.HassModel.Entities;
using System.Reflection;
using System.Text.Json;


#nullable enable
namespace LightManager.Tests
{
  public static class TestExtensions
  {
    public static JsonElement AsJsonElement(this object value) => JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize<object>(value));

    public static EntityState WithAttributes(
      this EntityState entityState,
      object attributes)
    {
      EntityState entityState1 = entityState.\u003CClone\u003E\u0024();
      entityState.GetType().GetProperty("AttributesJson", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) entityState1, (object) attributes.AsJsonElement());
      return entityState1;
    }
  }
}
