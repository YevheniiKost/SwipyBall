using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface IPlayerModel
    {
        Vector2 Position { get; }
        bool IsGrounded { get; }
        PlayerConfig Config { get; }

        event Action<PlayerForceMoveHandler> Jumped;
        event Action<PlayerForceMoveHandler> Pushed;
        
        void Swipe(float angle);
        void SetGroundedState(bool isGrounded);
        void RegisterHit(int damage, Vector2 direction);
    }
}