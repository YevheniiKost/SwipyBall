using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    internal class PlayerController : IPlayerController
    {
        private readonly IPlayerView _playerView;
        private readonly IInputModel _inputModel;
        private readonly IPlayerModel _playerModel;
        
        public PlayerController(IPlayerView playerView, IInputModel inputModel, IPlayerModel playerModel)
        {
            _playerView = playerView;
            _inputModel = inputModel;
            _playerModel = playerModel;
        }

        public event Action<int> OnHit;

        public void Initialize()
        {
            _inputModel.Swipe += OnSwipe;
            _inputModel.DirectionInputDown += OnDirectionInputDown;
            
            _playerModel.Jumped += OnJumped;
            _playerModel.Pushed += OnPushed;
            _playerModel.Landed += OnLanded;
        }

        public void Tick(float deltaTime)
        {
            bool isGrounded = _playerView.IsGrounded(_playerModel.Config.GroundCheckDistance);
            _playerModel.SetGroundedState(isGrounded);
            _playerModel.Tick(deltaTime);
        }

        public void InteractWithCollectable(ICollectable collectable)
        {
            collectable.Collect();
        }

        public void RegisterHit(int damage, Vector2 hitDirection)
        {
            OnHit?.Invoke(damage);
            _playerModel.RegisterHit(damage, hitDirection);
            _playerView?.ShowDamageEffect();
        }

        public void Dispose()
        {
            _inputModel.Swipe -= OnSwipe;
            
            _playerModel.Jumped -= OnJumped;
            _playerModel.Pushed -= OnPushed;
            _playerModel.Landed -= OnLanded;
        }

        private void OnLanded()
        {
            _playerView.ShowLandedEffect();
        }

        private void OnSwipe(float angle)
        {
            _playerModel.Swipe(angle);
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
    }
}