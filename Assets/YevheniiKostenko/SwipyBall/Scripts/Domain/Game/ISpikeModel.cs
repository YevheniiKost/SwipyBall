using System;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface ISpikeModel : IDamageSource, IDisposable
    {
        bool CanHit { get; }
        
        void Initialize();
        void RegisterHit();
    }
}