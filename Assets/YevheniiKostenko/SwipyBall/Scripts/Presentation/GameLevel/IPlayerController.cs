using System;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IPlayerController : IDisposable
    {
        void Initialize();
        void Tick(float deltaTime);
    }
}