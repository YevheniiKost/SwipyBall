using System;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public class BombModel : IBombModel
    {
        private readonly int _damage;
        private readonly float _explosionRadius;
        private readonly float _explosionDelay;
        
        private float _timeToExplosion;
        private bool _isTicking;

        public BombModel(int damage, float explosionRadius, float explosionDelay)
        {
            _damage = damage;
            _explosionRadius = explosionRadius;
            _explosionDelay = explosionDelay;
            
            _timeToExplosion = 0f;
            _isTicking = false;
        }

        public int Damage => _damage;
        
        public float ExplosionRadius => _explosionRadius;
        public float TimeToExplosion => _timeToExplosion;
        public float ExplosionDelay => _explosionDelay;

        public event Action Exploded;

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

        public void Tick(float deltaTime)
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
    }
}