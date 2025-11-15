using YevheniiKostenko.SwipyBall.Data.config;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class DamageSourceFactory : IDamageSourceFactory
    {
        private readonly IConfigProvider _configProvider;
        
        public DamageSourceFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        
        public ISpikeModel CreateSpike()
        {
            return new SpikeModel(_configProvider.GetGameConfig().SpikeDamage);
        }
    }
}