using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Domain.Progress;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class MainMenuState : BaseGameState, IMainMenuState
    {
        private IGetNextLevelUseCase _getNextLevelUseCase;
        
        public MainMenuState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public override void Prepare(object payload = null)
        {
            _getNextLevelUseCase = Context.Container.Resolve<IGetNextLevelUseCase>();
        }

        public override void Enter(object payload = null)
        {
        }

        public override void Exit()
        {
        }

        public void StartGame()
        {
            StateMachine.ChangeState<PlayingState>(new PlayingStateArgs(_getNextLevelUseCase.Execute()));
        }
    }
}