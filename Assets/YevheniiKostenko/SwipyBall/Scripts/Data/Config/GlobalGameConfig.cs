using System.Collections.Generic;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Data.Config
{
    [CreateAssetMenu(fileName = "GlobalGameConfig", menuName = "SwipyBall/Config/GlobalGameConfig")]
    public class GlobalGameConfig : ScriptableObject
    {
        [Header("Player Config")]
        [SerializeField]
        private float _maxAngleDeviation;

        [SerializeField]
        private float _maxAngle;

        [SerializeField]
        private float _timeBetweenJumps;

        [SerializeField]
        private float _jumpForce;

        [SerializeField]
        private float _pushForce;
        
        [SerializeField]
        private float _hitPushForce;

        [SerializeField]
        private float _groundCheckDistance;

        [SerializeField]
        private int _maxJumpCount;

        [SerializeField]
        private float _nextJumpDecreaseFactor;

        [SerializeField]
        private GameObject _playerPrefab;

        [Header("Game Config")]
        [SerializeField]
        private int _coinValue;
        
        [SerializeField]
        private int _spikeDamage;
        
        [Header("Bomb Config")]
        [SerializeField]
        private int _bombDamage;
        [SerializeField]
        private float _bombExplosionRadius;
        [SerializeField]
        private float _bombExplosionDelay;
        
        [Header("Level Config")]
        [SerializeField]
        private SerializableLevelConfig[] _levelConfigs;
        
        public float MaxAngleDeviation => _maxAngleDeviation;
        public float MaxAngle => _maxAngle;
        public float TimeBetweenJumps => _timeBetweenJumps;
        public float JumpForce => _jumpForce;
        public float PushForce => _pushForce;
        public float HitPushForce => _hitPushForce;
        public float GroundCheckDistance => _groundCheckDistance;
        public int MaxJumpCount => _maxJumpCount;
        public float NextJumpDecreaseFactor => _nextJumpDecreaseFactor;
        public GameObject PlayerPrefab => _playerPrefab;
        public int CoinValue => _coinValue;
        public int SpikeDamage => _spikeDamage;
        public int BombDamage => _bombDamage;
        public float BombExplosionRadius => _bombExplosionRadius;
        public float BombExplosionDelay => _bombExplosionDelay;
        public List<SerializableLevelConfig> LevelConfigs => new List<SerializableLevelConfig>(_levelConfigs);
    }
}