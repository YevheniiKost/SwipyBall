using System;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface IBombModel : IDamageSource, IDisposable
    {
        float ExplosionRadius { get; }
        float TimeToExplosion { get; }
        float ExplosionDelay { get; }
        
        event Action Exploded;
        
        void Initialize();
        bool RegisterHit();
    }
}