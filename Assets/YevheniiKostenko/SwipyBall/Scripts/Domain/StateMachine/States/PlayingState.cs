using YeKostenko.CoreKit.StateMachine;

using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.config;
using YevheniiKostenko.SwipyBall.Data.Progress;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Data.Config;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class PlayingState : BaseGameState, IPlayingState
    {
        private IGameModel _gameModel;
        private IConfigProvider _configProvider;
        private IProgressStorage _progressStorage;
        
        private GameLevelsConfig _levelsConfig;
        private LevelConfig _currentLevelConfig;
        
        public PlayingState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public int CurrentLevelIndex => _currentLevelConfig?.LevelId ?? 0;

        public override void Prepare(object payload = null)
        {
            _gameModel = Context.Container.Resolve<IGameModel>();
            _configProvider = Context.Container.Resolve<IConfigProvider>();
            _progressStorage = Context.Container.Resolve<IProgressStorage>();
            
            _progressStorage.Init();
            
            _levelsConfig = _configProvider.GetLevelsConfig();

            _currentLevelConfig = GetCurrentLevel();
        }

        private LevelConfig GetCurrentLevel()
        {
            PlayerProgress progress = _progressStorage.Progress;
            if(progress.CompletedLevels.Count == 0)
            {
                return _levelsConfig.GetFirstLevelConfig();
            }

            int maxCompletedLevel = -1;
            foreach (int levelId in progress.CompletedLevels)
            {
                if (levelId > maxCompletedLevel)
                {
                    maxCompletedLevel = levelId;
                }
            }
            
            return _levelsConfig.GetNextLevelConfig(maxCompletedLevel);
        }

        public override void Enter(object payload = null)
        {
            _gameModel.GameEnded += OnGameEnded;
            
            if (_gameModel.CanStartGame())
            {
                _gameModel.StartGame();
            }
        }

        public override void Exit()
        {
            _gameModel.GameEnded -= OnGameEnded;
        }
        
        private void OnGameEnded(bool isPlayerWin)
        { 
            GameResult gameResult = new GameResult(isPlayerWin, CurrentLevelIndex);
            StateMachine.ChangeState<FinishGameState>(gameResult);
        }
    }
}