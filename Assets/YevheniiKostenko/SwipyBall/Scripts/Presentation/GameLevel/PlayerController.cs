using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Core.Time;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    internal class PlayerController : IPlayerController, ITimeListener
    {
        private readonly IPlayerView _playerView;
        private readonly IInputModel _inputModel;
        private readonly IPlayerModel _playerModel;
        private readonly IGameModel _gameModel;
        private readonly ITimeProvider _timeProvider;
        
        public PlayerController(IPlayerView playerView, IInputModel inputModel, IPlayerModel playerModel,
            IGameModel gameModel, ITimeProvider timeProvider)
        {
            _playerView = playerView;
            _inputModel = inputModel;
            _playerModel = playerModel;
            _gameModel = gameModel;
            _timeProvider = timeProvider;
        }

        public void Initialize()
        {
            _inputModel.DirectionInputDown += OnDirectionInputDown;
            
            _playerModel.Jumped += OnJumped;
            _playerModel.Pushed += OnPushed;
            _playerModel.Landed += OnLanded;
            _playerModel.Moved += OnMoved;
            
            _timeProvider.RegisterTimeListener(this);
            _playerModel.Initialize();
        }
        
        public void Update(float deltaTime)
        {
            bool isGrounded = _playerView.IsGrounded(_playerModel.Config.GroundCheckDistance);
            _playerModel.SetGroundedState(isGrounded);
        }

        public void InteractWithCollectable(ICollectable collectable)
        {
            collectable.Collect();
        }

        public void RegisterHit(int damage, Vector2 hitDirection)
        {
            if (_playerModel.CanBeHit())
            {
                _gameModel.HitPlayer(damage);
                _playerModel.RegisterHit(damage, hitDirection);
                _playerView?.ShowDamageEffect();
            }
        }

        public void Dispose()
        {
            _inputModel.DirectionInputDown -= OnDirectionInputDown;
            
            _playerModel.Jumped -= OnJumped;
            _playerModel.Pushed -= OnPushed;
            _playerModel.Landed -= OnLanded;
            _playerModel.Moved -= OnMoved;
            
            _timeProvider.ClearTimeListener(this);
            _playerModel.Dispose();
        }

        private void OnLanded()
        {
            _playerView.ShowLandedEffect();
        }
        
        private void OnDirectionInputDown(InputDirection direction)
        {
            _playerModel.Move(direction);
        }
        
        private void OnJumped(PlayerForceMoveHandler handler)
        {
            _playerView.Jump(handler.MoveForce);
        }

        private void OnPushed(PlayerForceMoveHandler handler)
        {
            _playerView.Push(handler.MoveForce);
        }
        
        private void OnMoved(PlayerForceMoveHandler handler)
        {
            _playerView.Move(handler.MoveForce);
        }
    }
}