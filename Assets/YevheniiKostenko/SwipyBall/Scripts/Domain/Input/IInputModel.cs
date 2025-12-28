using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Input
{
    public interface IInputModel
    {
        event Action<float> Swipe;
        event Action<InputDirection> DirectionInput;
            
        void SwipeDetected(float angle);
        void DirectionInputDetected(InputDirection direction);
        void Tick(float deltaTime);
    }
}