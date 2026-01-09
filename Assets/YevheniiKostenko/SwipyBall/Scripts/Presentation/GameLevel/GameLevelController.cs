using YeKostenko.CoreKit.Logging;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class GameLevelController : IGameLevelController
    {
        private readonly IGameModel _gameModel;
        private readonly IGameLevelView _levelView;

        public GameLevelController(IGameModel gameModel, IGameLevelView levelView)
        {
            _gameModel = gameModel;
            _levelView = levelView;
        }

        public void Initialize()
        {
            _gameModel.GameStarted += OnGameStarted;
            _gameModel.GameEnded += OnGameEnded;

            if (_gameModel.IsGameStarted)
            {
                OnGameStarted();
            }
        }

        public void RegisterCollectableInteraction(ICollectable collectable)
        {
            if (_gameModel.IsGameStarted)
            {
                if(collectable.Type == CollectableType.Coin)
                    _gameModel.AddScore(collectable.Value);
                else if(collectable.Type == CollectableType.Hp)
                    _gameModel.AddHealth(collectable.Value);
                
                _levelView.DeactivateCollectable(collectable);
            }
        }

        public void RegisterPlayerReachedFinish()
        {
            try
            {
                if (_gameModel.IsGameStarted)
                {
                    _gameModel.PlayerReachGoal();
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"Error while processing player entering portal: {ex}");
            }
        }

        public void Dispose()
        {
            _gameModel.GameStarted -= OnGameStarted;
            _gameModel.GameEnded -= OnGameEnded;
        }

        private void OnGameStarted()
        {
            _levelView.SpawnPlayer();
            _levelView.ActivateViews();
        }

        private void OnGameEnded(bool isPlayerWon)
        {
            _levelView.DestroyPlayer();
        }
    }
}