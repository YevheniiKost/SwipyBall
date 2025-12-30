using System;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Input;

namespace YevheniiKostenko.SwipyBall.Domain.Input
{
    public interface IInputModel
    {
        event Action<float> Swipe;
        event Action<InputDirection> DirectionInputDown;
        event Action<InputDirection> DirectionInputUp;

        void RegisterInputProvider(IInputProvider provider);
        void ClearInputProvider(IInputProvider provider);
    }
}