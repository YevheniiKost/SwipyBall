using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class GameScreenPresenter : IGameScreenPresenter
    {
        private readonly IGameModel _gameModel;

        private IGameScreenView _gameScreenView;
        private GameScreenUIContext _gameScreenUIContext;

        public GameScreenPresenter(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void AttachView(IGameScreenView view)
        {
            _gameScreenView = view;

            _gameScreenView.Create += OnCreate;
            _gameScreenView.PauseClick += OnPauseClick;
            
            _gameModel.LivesUpdated += OnLivesUpdated;
            _gameModel.GameStarted += OnLivesUpdated;
        }

        public void DetachView()
        {
            _gameScreenView.Create -= OnCreate;
            _gameScreenView.PauseClick -= OnPauseClick;
            
            _gameScreenView = null;
            _gameModel.LivesUpdated -= OnLivesUpdated;
            _gameModel.GameStarted -= OnLivesUpdated;
        }

        private void OnPauseClick()
        {
            _gameScreenUIContext?.PauseButtonClicked?.Invoke();
        }

        private void OnCreate(GameScreenUIContext gameScreenUIContext)
        {
            _gameScreenUIContext = gameScreenUIContext;
            _gameScreenView.UpdateLives(_gameModel.Lives, _gameModel.MaxLives);
        }

        private void OnLivesUpdated()
        {
            _gameScreenView?.UpdateLives(_gameModel.Lives, _gameModel.MaxLives);
        }
    }
}