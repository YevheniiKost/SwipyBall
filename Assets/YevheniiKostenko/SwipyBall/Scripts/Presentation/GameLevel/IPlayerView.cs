using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IPlayerView
    {
        void Jump(Vector2 direction);
        void Push(Vector2 direction);
        bool IsGrounded(float groundCheckDistance);
    }
}