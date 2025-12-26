using System;
using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public abstract class GameStatePresenterBase<TState> : IGameStatePresenter where TState : IGameState
    {
        public Type GameStateType => typeof(TState);

        public UniTask OnEnterAsync(IGameState state)
        {
            if(state is not TState typedState)
            {
                throw new System.InvalidCastException($"Cannot cast state of type {state.GetType().Name} to {typeof(TState).Name}");
            }
            
            return OnEnterAsync(typedState);
        }

        public UniTask OnExitAsync(IGameState state)
        {
            if(state is not TState typedState)
            {
                throw new System.InvalidCastException($"Cannot cast state of type {state.GetType().Name} to {typeof(TState).Name}");
            }

            return OnExitAsync(typedState);
        }

        protected abstract UniTask OnEnterAsync(TState state);
        protected abstract UniTask OnExitAsync(TState state);
    }
}