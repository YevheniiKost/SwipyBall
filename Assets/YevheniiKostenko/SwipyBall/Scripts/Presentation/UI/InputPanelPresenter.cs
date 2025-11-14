using YevheniiKostenko.SwipyBall.Scripts.Domain;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.UI
{
    public class InputPanelPresenter : IInputPanelPresenter
    {
        private readonly IGameModel _gameModel;
        
        public InputPanelPresenter(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }
        
        public void AttachView(IInputPanelView view)
        {
            view.OnSwipe += OnSwipe;
        }

        private void OnSwipe(float angle)
        {
            _gameModel.SwipeDetected(angle);
        }

        public void DetachView()
        {
            
        }
    }
}