using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class FinishGameStateArgs
    {
        public readonly GameResult GameResult;

        public FinishGameStateArgs(GameResult gameResult)
        {
            GameResult = gameResult;
        }
    }
}