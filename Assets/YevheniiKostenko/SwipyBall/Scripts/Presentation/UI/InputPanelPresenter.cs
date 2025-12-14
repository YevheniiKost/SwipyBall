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
        }

        private void OnSwipe(float angle)
        {
            _inputModel.SwipeDetected(angle);
        }

        public void DetachView()
        {
            if (_view != null)
            {
                _view.OnSwipe -= OnSwipe;
                _view = null;
            }
        }
    }
}