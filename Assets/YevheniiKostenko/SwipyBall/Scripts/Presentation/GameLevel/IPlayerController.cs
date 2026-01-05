using System;
using UnityEngine;

using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IPlayerController : IDisposable
    {
        void Initialize();
        void Tick(float deltaTime);
        void InteractWithCollectable(ICollectable collectable);
        void RegisterHit(int damage, Vector2 direction);
    }
}