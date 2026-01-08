using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Time;
using YevheniiKostenko.SwipyBall.Data.Config;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    internal sealed class PlayerFactory : IPlayerFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IInputModel _inputModel;
        private readonly IGameModel _gameModel;
        private readonly ITimeProvider _timeProvider;

        public PlayerFactory(IConfigProvider configProvider, IInputModel inputModel, IGameModel gameModel,
            ITimeProvider timeProvider)
        {
            _configProvider = configProvider;
            _inputModel = inputModel;
            _gameModel = gameModel;
            _timeProvider = timeProvider;
        }

        public IPlayerView Create(Vector3 position)
        {
            PlayerView playerView = Object.Instantiate(_configProvider.GetPlayerPrefab()).GetComponent<PlayerView>();
            PlayerModel playerModel = new PlayerModel(_configProvider.GetPlayerConfig(), _timeProvider);
            PlayerController playerController = new PlayerController(playerView, _inputModel, playerModel, _gameModel,
                _timeProvider);
            playerView.Init(playerController);
            playerView.Transform.position = position;

            return playerView;
        }
    }
}