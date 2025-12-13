using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class PlayingStatePresenter : GameStatePresenterBase<IFinishGameState>
    {
        private readonly IUINavigation _uiNavigation;

        public PlayingStatePresenter(IUINavigation uiNavigation)
        {
            _uiNavigation = uiNavigation;
        }

        protected override UniTask OnEnterAsync(IFinishGameState state)
        {
            _uiNavigation.OpenInputPanel();
            _uiNavigation.OpenGameScreen();
            
            return UniTask.CompletedTask;
        }

        protected override UniTask OnExitAsync(IFinishGameState state)
        {
            _uiNavigation.CloseAllWindows();
            
            return UniTask.CompletedTask;
        }
    }
}