using System;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IPlayerView
    {
        event Action<int> OnHit;
        
        Transform Transform { get; }
        
        void Jump(Vector2 direction);
        void Push(Vector2 direction);
        void Hit(int damage);
        bool IsGrounded(float groundCheckDistance);
        
        void Destroy();
    }
}