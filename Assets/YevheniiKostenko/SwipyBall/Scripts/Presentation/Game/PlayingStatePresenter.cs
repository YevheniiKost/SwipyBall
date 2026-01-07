using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Core.Time;
using YevheniiKostenko.SwipyBall.Data.Input;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;
using YevheniiKostenko.SwipyBall.Domain.Input;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class PlayingStatePresenter : GameStatePresenterBase<IPlayingState>
    {
        private readonly IUINavigation _uiNavigation;
        private readonly ILevelRoot _levelRoot;
        
        private readonly IInputProvider _pcInputProvider;
        private readonly IInputModel _inputModel;

        public PlayingStatePresenter(IUINavigation uiNavigation, ILevelRoot levelRoot, ITimeProvider timeProvider,
            IInputModel inputModel)
        {
            _uiNavigation = uiNavigation;
            _levelRoot = levelRoot;
            _inputModel = inputModel;

            _pcInputProvider = new PCInputProvider(timeProvider);
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
            _inputModel.RegisterInputProvider(_pcInputProvider);
        }

        protected override UniTask OnExitAsync(IPlayingState state)
        {
            _uiNavigation.CloseAllWindows();
            _inputModel.ClearInputProvider(_pcInputProvider);
            
            return UniTask.CompletedTask;
        }
    }
}