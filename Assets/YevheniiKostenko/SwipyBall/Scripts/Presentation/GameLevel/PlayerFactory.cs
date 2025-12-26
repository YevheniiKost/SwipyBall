using UnityEngine;
using YevheniiKostenko.SwipyBall.Data.config;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    internal sealed class PlayerFactory : IPlayerFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IInputModel _inputModel;
        
        public PlayerFactory(IConfigProvider configProvider, IInputModel inputModel)
        {
            _configProvider = configProvider;
            _inputModel = inputModel;
        }
        
        public IPlayerView Create(Vector3 position)
        {
            PlayerView playerView = Object.Instantiate(_configProvider.GetPlayerPrefab()).GetComponent<PlayerView>();
            PlayerModel playerModel = new PlayerModel(_configProvider.GetPlayerConfig());
            PlayerController playerController = new PlayerController(playerView, _inputModel, playerModel);
            playerView.Init(playerController);
            
            return playerView;
        }
    }
}