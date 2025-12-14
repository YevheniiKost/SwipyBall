using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public readonly struct PlayerForceMoveHandler
    {
        public readonly Vector2 MoveForce;

        public PlayerForceMoveHandler(Vector2 moveForce)
        {
            MoveForce = moveForce;
        }
    }
}