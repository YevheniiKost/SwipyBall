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
            _uiNavigation.OpenFinishGameWindow(state.GameResult, OnRestartButtonClick, OnNextLevelButtonClick);
            
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
    }
}