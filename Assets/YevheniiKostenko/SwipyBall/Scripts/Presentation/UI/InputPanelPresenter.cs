using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Scripts.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class InputPanelPresenter : IInputPanelPresenter
    {
        private readonly IInputModel _inputModel;
        
        public InputPanelPresenter(IInputModel inputModel)
        {
            _inputModel = inputModel;
        }
        
        public void AttachView(IInputPanelView view)
        {
            view.OnSwipe += OnSwipe;
        }

        private void OnSwipe(float angle)
        {
            _inputModel.SwipeDetected(angle);
        }

        public void DetachView()
        {
            
        }
    }
}