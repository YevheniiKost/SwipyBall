using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Data.Input
{
    public interface IInputProvider : IDisposable
    {
        event Action<InputDirection> DirectionInputDown;
        event Action<InputDirection> DirectionInputUp;
    }
}