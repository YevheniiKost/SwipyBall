using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using Logger = YeKostenko.CoreKit.Logging.Logger;

namespace YevheniiKostenko.SwipyBall.Domain.Input
{
    internal class InputModel : IInputModel
    {
        public event Action<float> Swipe;
        public event Action<InputDirection> DirectionInput;

        public void SwipeDetected(float angle)
        {
            Logger.Log($"Swipe detected with angle: {angle}");
            Swipe?.Invoke(angle);
        }

        public void DirectionInputDetected(InputDirection direction)
        {
            Logger.Log($"Direction input detected: {direction}");
            DirectionInput?.Invoke(direction);
        }

        public void Tick(float deltaTime)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            {
                DirectionInputDetected(InputDirection.Left);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.D))
            {
                DirectionInputDetected(InputDirection.Right);
            }
        }
    }
}