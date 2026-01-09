using System;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IGameLevelController : IDisposable
    {
        void Initialize();
        void RegisterCollectableInteraction(ICollectable collectable);
        void RegisterPlayerReachedFinish();
    }
}