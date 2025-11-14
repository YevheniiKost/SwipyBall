using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Core.GameStateMachine.States
{
    public class FinishGameState : BaseGameState
    {
        private IUINavigation _uiNavigation;
        
        public FinishGameState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public override void Prepare(object payload = null)
        {
            _uiNavigation = Context.Container.Resolve<IUINavigation>();
        }

        public override void Enter(object payload = null)
        {
            if (payload is not GameResult gameResult)
            {
                throw new System.ArgumentException("Payload must be of type GameResult", nameof(payload));
            }

            _uiNavigation.OpenFinishGameWindow(gameResult, OnRestartButtonClick, OnExitButtonClick);
        }

        private void OnExitButtonClick()
        {
            _uiNavigation.CloseTopWindow();
            StateMachine.ChangeState<MainMenuState>();
        }

        private void OnRestartButtonClick()
        {
            _uiNavigation.CloseTopWindow();
            StateMachine.ChangeState<GameState>();
        }

        public override void Exit()
        {
            
        }
    }
}