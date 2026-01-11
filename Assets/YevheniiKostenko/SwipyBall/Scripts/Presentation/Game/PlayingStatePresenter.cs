using Cysharp.Threading.Tasks;
using YeKostenko.CoreKit.Logging;
using YevheniiKostenko.CoreKit.Time;
using YevheniiKostenko.SwipyBall.Data.Input;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;
using YevheniiKostenko.SwipyBall.Domain.Input;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;
using YevheniiKostenko.SwipyBall.Presentation.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class PlayingStatePresenter : GameStatePresenterBase<IPlayingState>
    {
        private readonly IUINavigation _uiNavigation;
        private readonly ILevelRoot _levelRoot;

        private readonly IInputProvider _pcInputProvider;
        private readonly IInputModel _inputModel;

        private IPlayingState _state;

        public PlayingStatePresenter(IUINavigation uiNavigation,
            ILevelRoot levelRoot, 
            ITimeProvider timeProvider,
            IInputModel inputModel)
        {
            _uiNavigation = uiNavigation;
            _levelRoot = levelRoot;
            _inputModel = inputModel;

            _pcInputProvider = new PCInputProvider(timeProvider);
        }

        protected override async UniTask OnEnterAsync(IPlayingState state)
        {
            _state = state;
            
            int levelIndex = state.CurrentLevelIndex;
            Logger.Log($"Level started: {levelIndex}");
            
            if (state.CurrentLevelIndex != _levelRoot.LoadedLevelIndex)
            {
                _levelRoot.UnloadCurrentLevel();
                await _levelRoot.LoadLevel(state.CurrentLevelIndex);
            }

            _uiNavigation.OpenInputPanel();
            _uiNavigation.OpenGameScreen(new GameScreenUIContext(OnPauseClick));
            _inputModel.RegisterInputProvider(_pcInputProvider);
        }

        protected override UniTask OnExitAsync(IPlayingState state)
        {
            _uiNavigation.CloseAllWindows();
            _inputModel.ClearInputProvider(_pcInputProvider);

            _state = null;
            return UniTask.CompletedTask;
        }

        private void OnPauseClick()
        {
            _uiNavigation.OpenPauseWindow(new PauseUIContext(OnGoToMainClick, OnResumeClick, OnRestartClick));
        }

        private void OnGoToMainClick()
        {
            _uiNavigation.CloseTopWindow();

            try
            {
                _state.GoToMainMenu();
            }
            catch (System.Exception e)
            {
                Logger.LogWarning($"Failed to go to main menu: {e}");
            }
        }

        private void OnResumeClick()
        {
            _uiNavigation.CloseTopWindow();
        }

        private void OnRestartClick()
        {
            _uiNavigation.CloseTopWindow();

            try
            {
                _state.RestartLevel();
            }
            catch (System.Exception e)
            {
                Logger.LogWarning($"Failed to restart level: {e}");
            }
        }
    }
}