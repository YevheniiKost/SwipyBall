using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;
using YevheniiKostenko.SwipyBall.Presentation.UI;

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
            _uiNavigation.OpenFinishGameWindow(new FinishGameUIContext(state.GameResult, OnRestartButtonClick,
                OnNextLevelButtonClick, OnReturnToMenuButtonClick));

            return UniTask.CompletedTask;
        }
        

        protected override UniTask OnExitAsync(IFinishGameState state)
        {
            _uiNavigation.CloseTopWindow();
            return UniTask.CompletedTask;
        }

        private void OnNextLevelButtonClick()
        {
            try
            {
                _state.OpenNextLevel();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to open next level: {e}");
            }
        }

        private void OnRestartButtonClick()
        {
            try
            {
                _state.Restart();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to restart level: {e}");
            }
        }
        
        private void OnReturnToMenuButtonClick()
        {
            try
            {
                _state.ExitToMenu();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to return to main menu: {e}");
            }
        }
    }
}