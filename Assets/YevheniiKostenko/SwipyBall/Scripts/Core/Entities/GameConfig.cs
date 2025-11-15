namespace YevheniiKostenko.SwipyBall.Core.Entities
{
    public class GameConfig
    {
        public int CoinValue { get; }
        public int SpikeDamage { get; }
        
        public GameConfig(int coinValue, int spikeDamage)
        {
            CoinValue = coinValue;
            SpikeDamage = spikeDamage;
        }
    }
}