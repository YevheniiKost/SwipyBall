using System;
using YeKostenko.CoreKit.Logging;

namespace YevheniiKostenko.SwipyBall.Domain.Input
{
    internal class InputModel : IInputModel
    {
        public event Action<float> Swipe;
        
        public void SwipeDetected(float angle)
        {
            Logger.Log($"Swipe detected with angle: {angle}");
            Swipe?.Invoke(angle);
        }
    }
}