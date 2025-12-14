using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class PlayingStatePresenter : GameStatePresenterBase<IPlayingState>
    {
        private readonly IUINavigation _uiNavigation;
        private readonly ILevelRoot _levelRoot;

        public PlayingStatePresenter(IUINavigation uiNavigation, ILevelRoot levelRoot)
        {
            _uiNavigation = uiNavigation;
            _levelRoot = levelRoot;
        }

        protected override async UniTask OnEnterAsync(IPlayingState state)
        {
            GameLevelView levelView = null;
            
            if (state.CurrentLevelIndex != _levelRoot.LoadedLevelIndex)
            {
                _levelRoot.UnloadCurrentLevel();
                levelView = await _levelRoot.LoadLevel(state.CurrentLevelIndex);
            }
            else
            {
                levelView = _levelRoot.LoadedLevelView;
            }

            //if we need to start game 
            if (levelView != null)
            {
                levelView.OnGameStarted();
            }

            _uiNavigation.OpenInputPanel();
            _uiNavigation.OpenGameScreen();
        }

        protected override UniTask OnExitAsync(IPlayingState state)
        {
            _uiNavigation.CloseAllWindows();
            
            return UniTask.CompletedTask;
        }
    }
}