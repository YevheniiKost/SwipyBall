using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.config;
using YevheniiKostenko.SwipyBall.Scripts.Data.Config;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class FinishGameState : BaseGameState, IFinishGameState
    {
        private GameResult _gameResult;
        
        private IConfigProvider _configProvider;
        private GameLevelsConfig _gameLevelsConfig;
        
        public FinishGameState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
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