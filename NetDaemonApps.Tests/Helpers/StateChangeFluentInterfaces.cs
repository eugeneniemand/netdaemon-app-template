using NetDaemon.HassModel.Entities;

namespace NetDaemonApps.Tests.Helpers;

public interface IWithState : IFromState
{
    IFromState ChangeStateFor(string entityId);
    IWithState WithEntityState<T>(string entityId, T state);
    IWithState WithEntityState(string entityId, EntityState state);
}

public interface IFromState
{
    IToState FromHassState(EntityState hassState);
    IToState FromState<T>(T state);
}

public interface IToState
{
    void ToHassState(EntityState hassState);
    void ToState<T>(T state);
}