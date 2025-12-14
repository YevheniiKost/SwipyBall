using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface ICollectableFactory
    {
        ICollectable CreateCoin();
    }
}