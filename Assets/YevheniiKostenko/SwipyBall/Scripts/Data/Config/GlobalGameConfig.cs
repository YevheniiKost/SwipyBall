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
        
        public float MaxAngleDeviation => _maxAngleDeviation;
        public float MaxAngle => _maxAngle;
        public float TimeBetweenJumps => _timeBetweenJumps;
        public float JumpForce => _jumpForce;
        public float PushForce => _pushForce;
        public float GroundCheckDistance => _groundCheckDistance;
        public int MaxJumpCount => _maxJumpCount;
        public float NextJumpDecreaseFactor => _nextJumpDecreaseFactor;
        public GameObject PlayerPrefab => _playerPrefab;
        public int CoinValue => _coinValue;
        public int SpikeDamage => _spikeDamage;
    }
}