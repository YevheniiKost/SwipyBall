using System;
using UnityEngine;
using YevheniiKostenko.CoreKit.Time;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Config;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    internal class PlayerModel : IPlayerModel, ITimeListener
    {
        private readonly PlayerConfig _config;
        private readonly ITimeProvider _timeProvider;
        
        private float _lastJumpTime;
        private float _lastHitTime;
        private bool _isGrounded;
        
        public PlayerModel(PlayerConfig config, ITimeProvider timeProvider)
        {
            _config = config;
            _timeProvider = timeProvider;
        }

        public bool IsGrounded => _isGrounded;
        public PlayerConfig Config => _config;

        public event Action<PlayerForceMoveHandler> Jumped;
        public event Action<PlayerForceMoveHandler> Moved;
        public event Action<PlayerForceMoveHandler> Pushed;
        public event Action Landed;

        public void Initialize()
        {
            _timeProvider.RegisterTimeListener(this);
        }
        
        public void Dispose()
        {
            _timeProvider.ClearTimeListener(this);
        }
        
        public void Update(float deltaTime)
        {
            if (CanJump())
            {
                Jump();
            }
        }

        public bool CanBeHit()
        {
            return Time.time - _lastHitTime >= _config.TimeBetweenHits;
        }
        
        public void SetGroundedState(bool isGrounded)
        {
            if (isGrounded && !_isGrounded)
            {
                OnLand();
            }
            
            _isGrounded = isGrounded;
        }

        public void RegisterHit(int damage, Vector2 direction)
        {
            if(!CanBeHit())
                return;
            
            _lastHitTime = Time.time;
            Vector2 oppositeDirection = -direction.normalized;
            Pushed?.Invoke(new PlayerForceMoveHandler(oppositeDirection * _config.HitPushForce));
        }

        public void Move(InputDirection direction)
        {
            MoveByInputDirection(direction);
        }

        private void OnLand()
        {
            Landed?.Invoke();
        }

        private void Jump()
        {
            Vector2 jumpForceDirection = Vector2.up;
            jumpForceDirection *= _config.JumpForce; 
            
            Jumped?.Invoke(new PlayerForceMoveHandler(jumpForceDirection));
            
            _lastJumpTime = Time.time;
        }
        
        private void MoveByInputDirection(InputDirection direction)
        {
            Vector2 pushForceDirection = direction == InputDirection.Right ? Vector2.right : Vector2.left;
            Move(pushForceDirection);
        }

        private void Move(Vector2 pushForceDirection)
        {
            pushForceDirection *= _config.PushForce;
            Moved?.Invoke(new PlayerForceMoveHandler(pushForceDirection));
        }

        private bool CanJump()
        {
            if (Time.time - _lastJumpTime < _config.TimeBetweenJumps)
                return false;

            return _isGrounded;
        }
    }
}