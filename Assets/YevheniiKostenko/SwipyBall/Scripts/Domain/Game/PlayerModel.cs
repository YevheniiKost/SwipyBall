using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    internal class PlayerModel : IPlayerModel
    {
        private readonly PlayerConfig _config;
        
        private float _lastJumpTime;
        private int _jumpCount = 0;
        private bool _isGrounded;
        
        public PlayerModel(PlayerConfig config)
        {
            _config = config;
        }

        public Vector2 Position { get; }
        public bool IsGrounded => _isGrounded;
        public PlayerConfig Config => _config;

        public event Action<PlayerJumpHandler> Jumped;
        public event Action<PlayerJumpHandler> Pushed;

        public void Swipe(float angle)
        {
            // up -> 90
            // left -> 180
            // right -> 0

            if (CanJump(angle))
            {
                Jump(angle);
                return;
            }

            if (CanPush(angle))
            {
                Push(angle);
            }
        }

        public void SetGroundedState(bool isGrounded)
        {
            if (isGrounded && !_isGrounded)
            {
                Landed();
            }
            
            _isGrounded = isGrounded;
        }

        public void Landed()
        {
            _jumpCount = 0;
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

            Jumped?.Invoke(new PlayerJumpHandler(jumpForceDirection));
            
            _jumpCount++;
            _lastJumpTime = Time.time;
        }
        
        private void Push(float swipeAngle)
        {
            // we can push only left or right
            bool isRight = Mathf.Abs(swipeAngle) < 90;
            Vector2 pushForceDirection = isRight ? Vector2.right : Vector2.left;
            pushForceDirection *= _config.PushForce;
            
            Pushed?.Invoke(new PlayerJumpHandler(pushForceDirection));
        }

        
        private bool CanJump(float swipeAngle)
        {
            if (swipeAngle > _config.MaxAngle || swipeAngle < 0)
                return false;
            
            if (Time.time - _lastJumpTime < _config.TimeBetweenJumps)
                return false;
            
            if(_isGrounded)
                return true;
            
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