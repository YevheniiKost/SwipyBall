using System;
using YevheniiKostenko.CoreKit.Time;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public class BombModel : IBombModel, ITimeListener
    {
        private readonly ITimeProvider _timeProvider;
        private readonly int _damage;
        private readonly float _explosionRadius;
        private readonly float _explosionDelay;
        
        private float _timeToExplosion;
        private bool _isTicking;

        public BombModel(int damage, float explosionRadius, float explosionDelay, ITimeProvider timeProvider)
        {
            _damage = damage;
            _explosionRadius = explosionRadius;
            _explosionDelay = explosionDelay;
            
            _timeToExplosion = 0f;
            _isTicking = false;
            _timeProvider = timeProvider;
        }

        public int Damage => _damage;
        
        public float ExplosionRadius => _explosionRadius;
        public float TimeToExplosion => _timeToExplosion;
        public float ExplosionDelay => _explosionDelay;

        public event Action Exploded;

        public void Initialize()
        {
            _timeProvider.RegisterTimeListener(this);
        }

        public bool RegisterHit()
        {
            if (!_isTicking)
            {
                _isTicking = true;
                _timeToExplosion = _explosionDelay;
                return true;
            }
            
            return false;
        }
        
        public void Update(float deltaTime)
        {
            if (_isTicking)
            {
                _timeToExplosion -= deltaTime;
                if (_timeToExplosion <= 0f)
                {
                    Exploded?.Invoke();
                    _isTicking = false;
                    _timeToExplosion = 0f;
                }
            }
        }

        public void Dispose()
        {
            _timeProvider.ClearTimeListener(this);
        }
    }
}