using System;

namespace YevheniiKostenko.SwipyBall.Scripts.Domain.Input
{
    public interface IInputModel
    {
        event Action<float> Swipe;
        void SwipeDetected(float angle);
    }
}