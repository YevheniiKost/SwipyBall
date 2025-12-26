using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine
{
    public interface IGameState
    {
        public static IGameState None => new NoneState();
    }
}