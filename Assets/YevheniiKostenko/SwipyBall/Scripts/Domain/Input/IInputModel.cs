using System;

namespace YevheniiKostenko.SwipyBall.Domain.Input
{
    public interface IInputModel
    {
        event Action<float> Swipe;
        void SwipeDetected(float angle);
    }
}