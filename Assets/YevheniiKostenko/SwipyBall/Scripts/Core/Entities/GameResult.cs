namespace YevheniiKostenko.SwipyBall.Core.Entities
{
    public class GameResult
    {
        public readonly int LevelIndex;
        public readonly bool IsPlayerWon;
        
        public GameResult(int levelIndex, bool isPlayerWon)
        {
            IsPlayerWon = isPlayerWon;
            LevelIndex = levelIndex;
        }
    }
}