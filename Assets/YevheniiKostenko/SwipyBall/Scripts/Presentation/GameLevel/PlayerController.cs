using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Scripts.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    internal class PlayerController : IPlayerController
    {
        private readonly PlayerView _playerView;
        private readonly IInputModel _inputModel;
        private readonly IPlayerModel _playerModel;
        
        public PlayerController(PlayerView playerView, IInputModel inputModel, IPlayerModel playerModel)
        {
            _playerView = playerView;
            _inputModel = inputModel;
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            _inputModel.Swipe += OnSwipe;
            
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
            _inputModel.Swipe -= OnSwipe;
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