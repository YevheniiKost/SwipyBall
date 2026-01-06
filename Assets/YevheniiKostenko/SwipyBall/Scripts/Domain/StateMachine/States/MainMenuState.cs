using YeKostenko.CoreKit.StateMachine;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class MainMenuState : BaseGameState, IMainMenuState
    {
        public MainMenuState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public override void Prepare(object payload = null)
        {
        }

        public override void Enter(object payload = null)
        {
        }

        public override void Exit()
        {
        }

        public void StartGame()
        {
            StateMachine.ChangeState<PlayingState>(new PlayingStateArgs(1));
        }
    }
}