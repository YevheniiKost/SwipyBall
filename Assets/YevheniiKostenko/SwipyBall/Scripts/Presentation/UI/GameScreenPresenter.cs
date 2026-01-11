using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class GameScreenPresenter : IGameScreenPresenter
    {
        private readonly IGameModel _gameModel;

        private IGameScreenView _view;
        private GameScreenUIContext _gameScreenUIContext;

        public GameScreenPresenter(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void AttachView(IGameScreenView view)
        {
            if (_view != null)
            {
                throw new System.InvalidOperationException("View is already attached.");
            }
            
            _view = view ?? throw new System.ArgumentNullException(nameof(view), "View cannot be null.");

            _view.Create += OnCreate;
            _view.PauseClick += OnPauseClick;
            
            _gameModel.LivesUpdated += OnLivesUpdated;
            _gameModel.GameStarted += OnLivesUpdated;
        }

        public void DetachView()
        {
            if (_view == null)
            {
                return;
            }
            
            _view.Create -= OnCreate;
            _view.PauseClick -= OnPauseClick;
            
            _view = null;
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
            _view.UpdateLives(_gameModel.Lives, _gameModel.MaxLives);
        }

        private void OnLivesUpdated()
        {
            _view?.UpdateLives(_gameModel.Lives, _gameModel.MaxLives);
        }
    }
}