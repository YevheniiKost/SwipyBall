using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class GameScreenPresenter : IGameScreenPresenter
    {
        private readonly IGameModel _gameModel;

        private IGameScreenView _gameScreenView;

        public GameScreenPresenter(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void AttachView(IGameScreenView view)
        {
            _gameScreenView = view;

            _gameScreenView.Create += OnCreate;
            _gameModel.LivesUpdated += OnLivesUpdated;
            _gameModel.GameStarted += OnLivesUpdated;
        }

        public void DetachView()
        {
            _gameScreenView.Create -= OnCreate;
            _gameScreenView = null;

            _gameModel.LivesUpdated -= OnLivesUpdated;
        }

        private void OnCreate()
        {
            _gameScreenView.UpdateLives(_gameModel.Lives, _gameModel.MaxLives);
        }

        private void OnLivesUpdated()
        {
            _gameScreenView?.UpdateLives(_gameModel.Lives, _gameModel.MaxLives);
        }
    }
}