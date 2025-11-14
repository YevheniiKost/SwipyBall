using UnityEngine;
using YevheniiKostenko.SwipyBall.Scripts.Data.config;
using YevheniiKostenko.SwipyBall.Scripts.Domain;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel
{
    internal sealed class PlayerFactory : IPlayerFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IGameModel _gameModel;
        
        public PlayerFactory(IConfigProvider configProvider, IGameModel gameModel)
        {
            _configProvider = configProvider;
            _gameModel = gameModel;
        }
        
        public PlayerView Create(Vector3 position)
        {
            PlayerView playerView = Object.Instantiate(_configProvider.GetPlayerPrefab()).GetComponent<PlayerView>();
            PlayerModel playerModel = new PlayerModel(_configProvider.GetPlayerConfig());
            PlayerController playerController = new PlayerController(playerView, _gameModel, playerModel);
            playerView.Init(playerController);
            
            return playerView;
        }
    }
}