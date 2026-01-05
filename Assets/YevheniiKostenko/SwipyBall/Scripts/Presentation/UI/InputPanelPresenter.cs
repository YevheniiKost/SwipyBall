using System;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Input;
using YevheniiKostenko.SwipyBall.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class InputPanelPresenter : IInputPanelPresenter, IInputProvider
    {
        private readonly IInputModel _inputModel;
        
        private IInputPanelView _view;
        
        public InputPanelPresenter(IInputModel inputModel)
        {
            _inputModel = inputModel;
        }
        
        public event Action<InputDirection> DirectionInputDown;
        public event Action<InputDirection> DirectionInputUp;
        
        public void AttachView(IInputPanelView view)
        {
            _view = view;
            
            _inputModel.RegisterInputProvider(this);
            view.OnSwipe += OnSwipe;
            view.LeftButtonUp += OnLeftButtonUp;
            view.RightButtonUp += OnRightButtonUp;
            view.LeftButtonUp += OnLeftButtonDown;
            view.RightButtonDown += OnRightButtonDown;
        }

        public void DetachView()
        {
            if (_view == null) return;
            
            _inputModel.ClearInputProvider(this);
            
            _view.OnSwipe -= OnSwipe;
            _view.LeftButtonUp -= OnLeftButtonUp;
            _view.RightButtonUp -= OnRightButtonUp;
            _view.LeftButtonUp -= OnLeftButtonDown;
            _view.RightButtonDown -= OnRightButtonDown;
            
            _view = null;
        }

        private void OnSwipe(float angle)
        {
        }

        private void OnLeftButtonUp() => DirectionInputUp?.Invoke(InputDirection.Left);

        private void OnRightButtonUp() =>  DirectionInputUp?.Invoke(InputDirection.Right);
        
        private void OnRightButtonDown() => DirectionInputDown?.Invoke(InputDirection.Right);

        private void OnLeftButtonDown() => DirectionInputDown?.Invoke(InputDirection.Left);
    }
}