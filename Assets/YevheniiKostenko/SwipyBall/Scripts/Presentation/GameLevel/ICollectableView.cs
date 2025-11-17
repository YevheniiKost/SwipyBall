using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface ICollectableView : IActivatableView
    {
        ICollectable Collectable { get; }
    }
}