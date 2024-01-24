using LightManagerV2;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Niemand.Tests.Mocks;

public class RandomManagerMock : IRandomManager
{
    public void Init(LightEntity entity, IEnumerable<string> randomStates)
    {
        throw new NotImplementedException();
    }

    public void Init(IEnumerable<LightEntity> entities, IEnumerable<string> randomStates)
    {
        throw new NotImplementedException();
    }

    public TimeSpan RandomDelay { get; set; }
    public SwitchEntity RandomSwitchEntity { get; } 

    public void StartQueue()
    {
        throw new NotImplementedException();
    }

    public void StopQueue()
    {
        throw new NotImplementedException();
    }
}