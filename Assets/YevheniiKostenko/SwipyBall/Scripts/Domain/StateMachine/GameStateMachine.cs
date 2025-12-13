using System;
using YeKostenko.CoreKit.StateMachine;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine
{
    public class GameStateMachine : StateMachine<GameStateContext>, IGameStateMachine
    {
        public GameStateMachine(GameStateContext context) : base(context)
        {
        }

        public event Action<IGameState, IGameState> OnStateChanged;

        protected override void OnChangeState(IState previousState, IState newState)
        {
            if(previousState is IGameState previousGameState && newState is IGameState newGameState)
            {
                OnStateChanged?.Invoke(previousGameState, newGameState);
            }
            else
            {
                throw new InvalidOperationException("States must implement IGameState interface.");
            }
        }
    }
}