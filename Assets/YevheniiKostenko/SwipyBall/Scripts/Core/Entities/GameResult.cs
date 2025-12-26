namespace YevheniiKostenko.SwipyBall.Core.Entities
{
    public class GameResult
    {
        public readonly bool IsPlayerWon;
        public readonly int LevelIndex;
        
        public GameResult(bool isPlayerWon, int levelIndex)
        {
            IsPlayerWon = isPlayerWon;
            LevelIndex = levelIndex;
        }
    }
}