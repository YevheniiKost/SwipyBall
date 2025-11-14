using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;
using YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel;

namespace YevheniiKostenko.SwipyBall.Scripts.Domain
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