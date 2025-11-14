using YeKostenko.CoreKit.StateMachine;

namespace YevheniiKostenko.SwipyBall.Core.GameStateMachine
{
    public class GameStateMachine : StateMachine<GameStateContext>
    {
        public GameStateMachine(GameStateContext context) : base(context)
        {
        }
    }
}