using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface IPlayerModel
    {
        Vector2 Position { get; }
        bool IsGrounded { get; }
        PlayerConfig Config { get; }

        event Action<PlayerJumpHandler> Jumped;
        event Action<PlayerJumpHandler> Pushed;
        
        void Swipe(float angle);
        void SetGroundedState(bool isGrounded);
        void Landed();
    }
}