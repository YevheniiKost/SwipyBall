using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class FinishGameStatePresenter : GameStatePresenterBase<IFinishGameState>
    {
        private readonly IUINavigation _uiNavigation;

        private IFinishGameState _state;

        public FinishGameStatePresenter(IUINavigation uiNavigation)
        {
            _uiNavigation = uiNavigation;
        }

        protected override UniTask OnEnterAsync(IFinishGameState state)
        {
            _state = state;
            _uiNavigation.OpenFinishGameWindow(state.GameResult, OnRestartButtonClick, OnExitButtonClick);
            
            return UniTask.CompletedTask;
        }

        protected override UniTask OnExitAsync(IFinishGameState state)
        {
            _uiNavigation.CloseTopWindow();
            return UniTask.CompletedTask;
        }
        
        private void OnExitButtonClick()
        {
            _state.ExitToMenu();
        }

        private void OnRestartButtonClick()
        {
            _state.Restart();
        }
    }
}