using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class FinishGameWindowPresenter : IFinishGameWindowPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly IUINavigation _uiNavigation;
        
        private IFinishGameWindowView _view;

        public FinishGameWindowPresenter(IGameModel gameModel, IUINavigation uiNavigation)
        {
            _gameModel = gameModel;
            _uiNavigation = uiNavigation;
        }

        public void AttachView(IFinishGameWindowView view)
        {
            _view = view;
            
            // _view.RestartButtonClick += OnRestartButtonClick;
        }
        
        public void DetachView()
        {
            // _view.RestartButtonClick -= OnRestartButtonClick;
            _view = null;
        }

        private void OnRestartButtonClick()
        {
            _uiNavigation.CloseTopWindow();
            _gameModel.StartGame();
        }
    }
}