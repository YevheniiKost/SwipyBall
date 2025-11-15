using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class GameScreenPresenter : IGameScreenPresenter
    {
        private readonly IGameModel _gameModel;
        
        private IGameScreen _gameScreen;
        
        public GameScreenPresenter(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }
        
        public void AttachView(IGameScreen view)
        {
            _gameScreen = view;
            
            _gameModel.ScoreUpdated += OnScoreUpdated;
            _gameModel.LivesUpdated += OnLivesUpdated;
        }

        public void DetachView()
        {
            _gameScreen = null;
            
            _gameModel.ScoreUpdated -= OnScoreUpdated;
            _gameModel.LivesUpdated -= OnLivesUpdated;
        }
        
        private void OnLivesUpdated()
        {
            _gameScreen?.UpdateLives(_gameModel.Lives);
        }

        private void OnScoreUpdated()
        {
            _gameScreen?.UpdateScore(_gameModel.GameScore);
        }
    }
}