using YevheniiKostenko.SwipyBall.Scripts.Domain;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel
{
    internal class PlayerController : IPlayerController
    {
        private readonly PlayerView _playerView;
        private readonly IGameModel _gameModel;
        private readonly IPlayerModel _playerModel;
        
        public PlayerController(PlayerView playerView, IGameModel gameModel, IPlayerModel playerModel)
        {
            _playerView = playerView;
            _gameModel = gameModel;
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            _gameModel.Swipe += OnSwipe;
            
            _playerModel.Jumped += OnJumped;
            _playerModel.Pushed += OnPushed;
        }

        public void Tick(float deltaTime)
        {
            bool isGrounded = _playerView.IsGrounded(_playerModel.Config.GroundCheckDistance);
            _playerModel.SetGroundedState(isGrounded);
        }

        public void Dispose()
        {
            _gameModel.Swipe -= OnSwipe;
        }
        
        private void OnSwipe(float angle)
        {
            _playerModel.Swipe(angle);
        }
        
        private void OnJumped(PlayerJumpHandler handler)
        {
            _playerView.Jump(handler.JumpForce);
        }

        private void OnPushed(PlayerJumpHandler handler)
        {
            _playerView.Push(handler.JumpForce);
        }
    }
}