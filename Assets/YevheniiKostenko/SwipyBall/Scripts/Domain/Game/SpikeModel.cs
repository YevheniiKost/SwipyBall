using YevheniiKostenko.CoreKit.Time;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public class SpikeModel : ISpikeModel, ITimeListener
    {
        private const float RearmDelay = 1f;
        
        private readonly ITimeProvider _timeProvider;
        private readonly int _damage;
        private float _cooldownLeft;
        
        public SpikeModel(int damage, ITimeProvider timeProvider)
        {
            _damage = damage;
            _timeProvider = timeProvider;
        }

        public int Damage => _damage;
        public bool CanHit  => _cooldownLeft <= 0f;


        public void Initialize()
        {
            _timeProvider.RegisterTimeListener(this);
        }

        public void RegisterHit()
        {
            _cooldownLeft = RearmDelay;
        }

        public void Update(float deltaTime)
        {
            if (_cooldownLeft > 0f)
            {
                _cooldownLeft -= deltaTime;
            }
        }
        
        public void Dispose()
        {
            _timeProvider.ClearTimeListener(this);
        }
    }
}