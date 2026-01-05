using System;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IPlayerView
    {
        Transform Transform { get; }
        
        void Jump(Vector2 direction);
        void Push(Vector2 direction);
        void Move(Vector2 direction);
        void Hit(int damage, Vector2 direction);
        bool IsGrounded(float groundCheckDistance);
        void ShowDamageEffect();
        void ShowLandedEffect();
        
        void Destroy();
    }
}