namespace YevheniiKostenko.SwipyBall.Scripts.Core.Entities
{
    public class GameResult
    {
        public readonly bool IsPlayerWon;
        
        public GameResult(bool isPlayerWon)
        {
            IsPlayerWon = isPlayerWon;
        }
    }
}