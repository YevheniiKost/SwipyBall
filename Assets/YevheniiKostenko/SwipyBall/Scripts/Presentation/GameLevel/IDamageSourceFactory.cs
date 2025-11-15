using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IDamageSourceFactory
    {
        ISpikeModel CreateSpike();
    }
}