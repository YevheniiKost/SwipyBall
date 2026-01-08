using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Core.Time;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    internal class PlayerModel : IPlayerModel, ITimeListener
    {
        private readonly PlayerConfig _config;
        private readonly ITimeProvider _timeProvider;
        
        private float _lastJumpTime;
        private float _lastHitTime;
        private int _jumpCount = 0;
        private bool _isGrounded;
        
        public PlayerModel(PlayerConfig config, ITimeProvider timeProvider)
        {
            _config = config;
            _timeProvider = timeProvider;
        }

        public Vector2 Position { get; }
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
            _jumpCount = 0;
            Landed?.Invoke();
        }

        private void Jump()
        {
            Vector2 jumpForceDirection = Vector2.up;
            jumpForceDirection *= _config.JumpForce; 
            
            if (_jumpCount > 0)
            {
                // Decrease jump force for subsequent jumps
                jumpForceDirection *= Mathf.Pow(_config.NextJumpDecreaseFactor, _jumpCount);
            }

            Jumped?.Invoke(new PlayerForceMoveHandler(jumpForceDirection));
            
            _jumpCount++;
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
        
        private bool CanJumpMore()
        {
            return _jumpCount <= _config.MaxJumpCount;
        }
    }
}