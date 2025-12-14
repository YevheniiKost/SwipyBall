namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public interface IPlayingState : IGameState
    {
        int CurrentLevelIndex { get; }
    }
}