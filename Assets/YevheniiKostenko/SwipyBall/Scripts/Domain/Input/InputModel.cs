using System;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Input;

namespace YevheniiKostenko.SwipyBall.Domain.Input
{
    internal class InputModel : IInputModel
    {
        public event Action<InputDirection> DirectionInputDown;
        public event Action<InputDirection> DirectionInputUp;

        public void RegisterInputProvider(IInputProvider provider)
        {
            if (provider == null)
                return;
            
            provider.DirectionInputDown += OnDirectionInputDown;
            provider.DirectionInputUp += OnDirectionInputUp;
        }

        public void ClearInputProvider(IInputProvider provider)
        {
            if (provider == null)
                return;
            
            provider.DirectionInputDown -= OnDirectionInputDown;
            provider.DirectionInputUp -= OnDirectionInputUp;
        }
        
        private void OnDirectionInputDown(InputDirection direction)
        {
            DirectionInputDown?.Invoke(direction);
        }
        
        private void OnDirectionInputUp(InputDirection direction)
        {
            DirectionInputUp?.Invoke(direction);
        }
    }
}