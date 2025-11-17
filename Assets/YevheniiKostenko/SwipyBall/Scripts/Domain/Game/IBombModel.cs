using System;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface IBombModel : IDamageSource
    {
        float ExplosionRadius { get; }
        float TimeToExplosion { get; }
        float ExplosionDelay { get; }
        
        event Action Exploded;
        
        bool RegisterHit();
        void Tick(float deltaTime);
    }
}