using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Config;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface IPlayerModel : IDisposable
    {
        bool IsGrounded { get; }
        PlayerConfig Config { get; }

        event Action<PlayerForceMoveHandler> Jumped;
        event Action<PlayerForceMoveHandler> Pushed;
        event Action<PlayerForceMoveHandler> Moved;
        event Action Landed;
        
        bool CanBeHit();

        void Initialize();
        void SetGroundedState(bool isGrounded);
        void RegisterHit(int damage, Vector2 direction);
        void Move(InputDirection direction);
    }
}