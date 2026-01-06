using UnityEngine;
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
        
        public PlayerFactory(IConfigProvider configProvider, IInputModel inputModel, IGameModel gameModel)
        {
            _configProvider = configProvider;
            _inputModel = inputModel;
            _gameModel = gameModel;
        }
        
        public IPlayerView Create(Vector3 position)
        {
            PlayerView playerView = Object.Instantiate(_configProvider.GetPlayerPrefab()).GetComponent<PlayerView>();
            PlayerModel playerModel = new PlayerModel(_configProvider.GetPlayerConfig());
            PlayerController playerController = new PlayerController(playerView, _inputModel, playerModel, _gameModel);
            playerView.Init(playerController);
            playerView.Transform.position = position;
            
            return playerView;
        }
    }
}