using YevheniiKostenko.CoreKit.Time;
using YevheniiKostenko.SwipyBall.Data.Config;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class DamageSourceFactory : IDamageSourceFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly ITimeProvider _timeProvider;
        
        public DamageSourceFactory(IConfigProvider configProvider, ITimeProvider timeProvider)
        {
            _configProvider = configProvider;
            _timeProvider = timeProvider;
        }
        
        public ISpikeModel CreateSpike()
        {
            return new SpikeModel(_configProvider.GetGameConfig().SpikeDamage, _timeProvider);
        }
        
        public IBombModel CreateBomb()
        {
            return new BombModel(_configProvider.GetGameConfig().BombDamage,
                _configProvider.GetGameConfig().BombExplosionRadius,
                _configProvider.GetGameConfig().BombExplosionDelay,
                _timeProvider);
        }
    }
}