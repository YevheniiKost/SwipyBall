using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IGameLevelView
    {
        void SpawnPlayer();
        void DestroyPlayer();
        void ActivateViews();
        void DeactivateCollectable(ICollectable collectable);
        
        void Destroy();
    }
}