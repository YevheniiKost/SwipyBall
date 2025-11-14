using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Scripts.Domain
{
    public readonly struct PlayerJumpHandler
    {
        public readonly Vector2 JumpForce;

        public PlayerJumpHandler(Vector2 jumpForce)
        {
            JumpForce = jumpForce;
        }
    }
}