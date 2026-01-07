using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    internal class PlayerModel : IPlayerModel
    {
        private readonly PlayerConfig _config;
        
        private float _lastJumpTime;
        private float _lastHitTime;
        private int _jumpCount = 0;
        private bool _isGrounded;
        
        public PlayerModel(PlayerConfig config)
        {
            _config = config;
        }

        public Vector2 Position { get; }
        public bool IsGrounded => _isGrounded;
        public PlayerConfig Config => _config;

        public event Action<PlayerForceMoveHandler> Jumped;
        public event Action<PlayerForceMoveHandler> Moved;
        public event Action<PlayerForceMoveHandler> Pushed;
        public event Action Landed;

        public bool CanBeHit()
        {
            return Time.time - _lastHitTime >= _config.TimeBetweenHits;
        }

        public void Swipe(float angle)
        {
            // up -> 90
            // left -> 180
            // right -> 0

            if (CanPush(angle))
            {
                PushByAngle(angle);
            }
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

        public void Tick(float deltaTime)
        {
            if (CanJump(90))
            {
                Jump(90);
                return;
            }
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

        private void Jump(float swipeAngle)
        {
            swipeAngle = Mathf.Clamp(swipeAngle, 90 - _config.MaxAngleDeviation, 90 + _config.MaxAngleDeviation);
            
            Vector2 jumpForceDirection = new Vector2(Mathf.Cos(swipeAngle * Mathf.Deg2Rad), Mathf.Sin(swipeAngle * Mathf.Deg2Rad));
            jumpForceDirection.Normalize(); 
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
        
        private void PushByAngle(float swipeAngle)
        {
            // we can push only left or right
            bool isRight = Mathf.Abs(swipeAngle) < 90;
            Vector2 pushForceDirection = isRight ? Vector2.right : Vector2.left;
            Move(pushForceDirection);
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


        private bool CanJump(float swipeAngle)
        {
            if (swipeAngle > _config.MaxAngle || swipeAngle < 0)
                return false;
            
            if (Time.time - _lastJumpTime < _config.TimeBetweenJumps)
                return false;

            return _isGrounded;
            
            return CanJumpMore();
        }
        
        private bool CanPush(float swipeAngle)
        {
            return !_isGrounded;
        }
        
        private bool CanJumpMore()
        {
            return _jumpCount < _config.MaxJumpCount;
        }
    }
}