using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public class CoinModel : CollectableItem
    {
        private readonly int _value;

        public CoinModel(int value)
        {
            _value = value;
        }

        public override CollectableType Type => CollectableType.Coin;
        
        public override int Value => _value;
    }
}