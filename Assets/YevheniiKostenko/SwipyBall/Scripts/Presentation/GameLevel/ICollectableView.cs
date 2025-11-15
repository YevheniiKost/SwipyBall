using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface ICollectableView
    {
        ICollectable Collectable { get; }
        
        void Activate();
        void Deactivate();
    }
}