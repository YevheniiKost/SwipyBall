namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class PlayingStateArgs
    {
        public int LevelIndex { get; }
        
        public PlayingStateArgs(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}