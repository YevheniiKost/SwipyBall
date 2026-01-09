using System;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Core.Time;

namespace YevheniiKostenko.SwipyBall.Data.Input
{
    public class PCInputProvider : IInputProvider, ITimeListener, IDisposable
    {
        private readonly ITimeProvider _timeProvider;

        public PCInputProvider(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _timeProvider.RegisterTimeListener(this);
        }
        
        public event Action<InputDirection> DirectionInputDown;
        public event Action<InputDirection> DirectionInputUp;

        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftArrow) ||
                UnityEngine.Input.GetKey(UnityEngine.KeyCode.A))
            {
                DirectionInputDown?.Invoke(InputDirection.Left);
            }

            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.LeftArrow) ||
                UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.A))
            {
                DirectionInputUp?.Invoke(InputDirection.Left);
            }

            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightArrow) ||
                UnityEngine.Input.GetKey(UnityEngine.KeyCode.D))
            {
                DirectionInputDown?.Invoke(InputDirection.Right);
            }

            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.RightArrow) ||
                UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.D))
            {
                DirectionInputUp?.Invoke(InputDirection.Right);
            }
        }

        public void Dispose()
        {
            _timeProvider.ClearTimeListener(this);
        }
    }
}