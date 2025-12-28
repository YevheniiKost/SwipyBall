using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class InputPanelPresenter : IInputPanelPresenter
    {
        private readonly IInputModel _inputModel;
        
        private IInputPanelView _view;
        
        public InputPanelPresenter(IInputModel inputModel)
        {
            _inputModel = inputModel;
        }
        
        public void AttachView(IInputPanelView view)
        {
            _view = view;
            view.OnSwipe += OnSwipe;
            view.LeftButtonPressed += OnLeftButtonPressed;
            view.RightButtonPressed += OnRightButtonPressed;
        }
        
        public void DetachView()
        {
            if (_view != null)
            {
                _view.OnSwipe -= OnSwipe;
                _view = null;
            }
        }

        private void OnSwipe(float angle) => _inputModel.SwipeDetected(angle);

        private void OnLeftButtonPressed() => _inputModel.DirectionInputDetected(InputDirection.Left);

        private void OnRightButtonPressed() => _inputModel.DirectionInputDetected(InputDirection.Right);
    }
}