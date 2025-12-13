using YeKostenko.CoreKit.StateMachine;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public abstract class BaseGameState : BaseState<GameStateContext>
    {
        protected BaseGameState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }
    }
}