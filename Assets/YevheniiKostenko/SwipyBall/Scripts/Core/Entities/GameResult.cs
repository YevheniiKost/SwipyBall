namespace YevheniiKostenko.SwipyBall.Core.Entities
{
    public class GameResult
    {
        public readonly int LevelIndex;
        public readonly bool IsPlayerWon;
        
        public GameResult(bool isPlayerWon, int levelIndex)
        {
            IsPlayerWon = isPlayerWon;
            LevelIndex = levelIndex;
        }
    }
}