namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public class SpikeModel : ISpikeModel
    {
        private const float RearmDelay = 1f;
        
        private readonly int _damage;
        private float _cooldownLeft;
        
        public SpikeModel(int damage)
        {
            _damage = damage;
        }

        public int Damage => _damage;
        public bool CanHit  => _cooldownLeft <= 0f;
        
        public void Tick(float deltaTime)
        {
            if (_cooldownLeft > 0f)
            {
                _cooldownLeft -= deltaTime;
            }
        }

        public void RegisterHit()
        {
            _cooldownLeft = RearmDelay;
        }
    }
}