using System;
using UnityEngine;

using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IPlayerController : IDisposable
    {
        event Action<int> OnHit;

        void Initialize();
        void Tick(float deltaTime);
        void InteractWithCollectable(ICollectable collectable);
        void RegisterHit(int damage, Vector2 direction);
    }
}