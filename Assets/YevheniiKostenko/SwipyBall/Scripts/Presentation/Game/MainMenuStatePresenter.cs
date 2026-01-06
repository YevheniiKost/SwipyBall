using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;
using YevheniiKostenko.SwipyBall.Presentation.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class MainMenuStatePresenter : GameStatePresenterBase<IMainMenuState>
    {
        private readonly IUINavigation _uiNavigation;
        private IMainMenuState _state;

        public MainMenuStatePresenter(IUINavigation uiNavigation)
        {
            _uiNavigation = uiNavigation;
        }

        protected override UniTask OnEnterAsync(IMainMenuState state)
        {
            _state = state;
            //close loading
            //open main menu UI
            _uiNavigation.OpenMainMenu(new MainMenuUIContext(OnPlayButtonClick));
            
            return UniTask.CompletedTask;
        }

        protected override UniTask OnExitAsync(IMainMenuState state)
        {
            _uiNavigation.CloseAllWindows();
            return UniTask.CompletedTask;
        }
        
        private void OnPlayButtonClick()
        {
            try
            {
                _state.StartGame();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to start game: {e}");
            }
        }
    }
}