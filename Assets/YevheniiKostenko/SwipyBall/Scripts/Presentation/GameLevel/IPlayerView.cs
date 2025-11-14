using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel
{
    public interface IPlayerView
    {
        void Jump(Vector2 direction);
        void Push(Vector2 direction);
        bool IsGrounded(float groundCheckDistance);
    }
}