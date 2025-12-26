using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Progress;
using YevheniiKostenko.SwipyBall.Data.Config;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class FinishGameState : BaseGameState, IFinishGameState
    {
        private GameResult _gameResult;
        private readonly IProgressStorage _progressStorage;
        
        private IConfigProvider _configProvider;
        private GameLevelsConfig _gameLevelsConfig;
        
        public FinishGameState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
            _progressStorage = Context.Container.Resolve<IProgressStorage>();
        }
        
        public GameResult GameResult => _gameResult;

        public override void Prepare(object payload = null)
        {
            if (payload is not FinishGameStateArgs args)
            {
                throw new System.ArgumentException("Payload must be of type GameResult", nameof(payload));
            }
            
            _gameResult = args.GameResult;
            
            _configProvider = Context.Container.Resolve<IConfigProvider>();
            _gameLevelsConfig = _configProvider.GetLevelsConfig();
        }

        public override void Enter(object payload = null)
        {
            if (payload is not FinishGameStateArgs finishGameStateArgs)
            {
                throw new System.ArgumentException("Payload must be of type GameResult", nameof(payload));
            }
            
            _gameResult = finishGameStateArgs.GameResult;
            
            if (_gameResult.IsPlayerWon)
            {
                PlayerProgress progress = _progressStorage.Progress;
                progress.CompletedLevels.Add(_gameResult.LevelIndex);
                _progressStorage.SaveProgress(progress);
            }
        }

        public void Restart()
        {
            StateMachine.ChangeState<PlayingState>(new PlayingStateArgs(_gameResult.LevelIndex));
        }
        
        public void OpenNextLevel()
        {
            int nextLevelIndex = _gameLevelsConfig.GetNextLevelConfig(_gameResult.LevelIndex).LevelId;
            StateMachine.ChangeState<PlayingState>(new PlayingStateArgs(nextLevelIndex));
        }

        public void ExitToMenu()
        {
            StateMachine.ChangeState<MainMenuState>();
        }
        
        public override void Exit()
        {
            
        }
    }
}