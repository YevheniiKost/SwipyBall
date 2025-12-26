using System;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine
{
    public interface IGameStateMachine
    {
        event Action<IGameState, IGameState> OnStateChanged;
    }
}