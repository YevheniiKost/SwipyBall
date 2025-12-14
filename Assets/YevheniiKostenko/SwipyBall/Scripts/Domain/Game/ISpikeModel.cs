namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface ISpikeModel : IDamageSource
    {
        bool CanHit { get; }
        
        void Tick(float deltaTime);
        void RegisterHit();
    }
}