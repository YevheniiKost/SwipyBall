using YevheniiKostenko.SwipyBall.Core.Entities;
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

            _view.Create += OnCreate;
        }
        
        public void DetachView()
        {
            _view.Create -= OnCreate;
            
            _view = null;
        }
        
        private void OnCreate(GameResult result)
        {
            _view.SetGameResult(result.IsPlayerWon);
            _view.SetNextLevelButtonActive(result.IsPlayerWon);
        }
    }
}