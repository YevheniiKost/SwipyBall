using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.config;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Scripts.Data.Config;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class PlayingState : BaseGameState, IPlayingState
    {
        private IGameModel _gameModel;
        private IConfigProvider _configProvider;
        
        private GameLevelsConfig _gameLevelsConfig;

        private LevelConfig _currentLevelConfig;
        
        public PlayingState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public int CurrentLevelIndex => _currentLevelConfig?.LevelId ?? 0;

        public override void Prepare(object payload = null)
        {
            _gameModel = Context.Container.Resolve<IGameModel>();
            _configProvider = Context.Container.Resolve<IConfigProvider>();
            
            _gameLevelsConfig = _configProvider.GetLevelsConfig();

            if (_currentLevelConfig == null)
            {
                _currentLevelConfig = _gameLevelsConfig.GetFirstLevelConfig();
            }
            else
            {
                _currentLevelConfig = _gameLevelsConfig.GetNextLevelConfig(_currentLevelConfig.LevelId);
            }
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
        
        private void OnGameEnded(GameResult gameResult)
        {
            StateMachine.ChangeState<FinishGameState>(gameResult);
        }
    }
}