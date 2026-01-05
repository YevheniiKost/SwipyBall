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

            _gameScreen.Create += OnCreate;
            _gameModel.LivesUpdated += OnLivesUpdated;
            _gameModel.GameStarted += OnLivesUpdated;
        }

        public void DetachView()
        {
            _gameScreen.Create -= OnCreate;
            _gameScreen = null;

            _gameModel.LivesUpdated -= OnLivesUpdated;
        }

        private void OnCreate()
        {
            _gameScreen.UpdateLives(_gameModel.Lives, _gameModel.MaxLives);
        }

        private void OnLivesUpdated()
        {
            _gameScreen?.UpdateLives(_gameModel.Lives, _gameModel.MaxLives);
        }
    }
}