using YeKostenko.CoreKit.StateMachine;

namespace YevheniiKostenko.SwipyBall.Core.GameStateMachine.States
{
    public abstract class BaseGameState : BaseState<GameStateContext>
    {
        protected BaseGameState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }
    }
}