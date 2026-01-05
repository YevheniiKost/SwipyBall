using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public interface IFinishGameState : IGameState
    {
        GameResult GameResult { get;  }

        void Restart();
        void ExitToMenu();
        void OpenNextLevel();
    }
}