using System;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel
{
    public interface IPlayerController : IDisposable
    {
        void Initialize();
        void Tick(float deltaTime);
    }
}