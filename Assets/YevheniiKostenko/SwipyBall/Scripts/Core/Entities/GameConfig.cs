namespace YevheniiKostenko.SwipyBall.Core.Entities
{
    public class GameConfig
    {
        public int CoinValue { get; }
        public int SpikeDamage { get; }
        public int BombDamage { get; }
        public float BombExplosionRadius { get; }
        public float BombExplosionDelay { get; }

        public GameConfig(int coinValue, int spikeDamage, int bombDamage, float bombExplosionRadius,
            float bombExplosionDelay)
        {
            CoinValue = coinValue;
            SpikeDamage = spikeDamage;
            BombDamage = bombDamage;
            BombExplosionRadius = bombExplosionRadius;
            BombExplosionDelay = bombExplosionDelay;
        }
    }
}