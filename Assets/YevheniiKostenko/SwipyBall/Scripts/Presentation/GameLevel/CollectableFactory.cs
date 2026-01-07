using YevheniiKostenko.SwipyBall.Data.Config;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class CollectableFactory : ICollectableFactory
    {
        private readonly IConfigProvider _configProvider;
        
        public CollectableFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        
        public ICollectable CreateCoin()
        {
            return new CoinModel(_configProvider.GetGameConfig().CoinValue);
        }
    }
}