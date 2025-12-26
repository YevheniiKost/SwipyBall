using YeKostenko.CoreKit.StateMachine;

using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Config;
using YevheniiKostenko.SwipyBall.Data.Progress;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class MainMenuState : BaseGameState, IMainMenuState
    {
        private IProgressStorage _progressStorage;
        private IConfigProvider _configProvider;
        
        public MainMenuState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public override void Prepare(object payload = null)
        {
            _progressStorage = Context.Container.Resolve<IProgressStorage>();
            _configProvider = Context.Container.Resolve<IConfigProvider>();
            _progressStorage.Init();
        }

        public override void Enter(object payload = null)
        {
        }

        public override void Exit()
        {
        }

        public void StartGame()
        {
            StateMachine.ChangeState<PlayingState>(new PlayingStateArgs(GetHighestLevelIndex()));
        }

        private int GetHighestLevelIndex()
        {
            GameLevelsConfig levelsConfig = _configProvider.GetLevelsConfig();
            PlayerProgress progress = _progressStorage.Progress;
            
            int highestCompletedLevel = 0;
            foreach (var levelConfig in levelsConfig.LevelConfigs)
            {
                if (progress.CompletedLevels.Contains(levelConfig.LevelId) &&
                    levelConfig.LevelId > highestCompletedLevel)
                {
                    highestCompletedLevel = levelConfig.LevelId;
                }
            }

            if (highestCompletedLevel == 0)
                return 1;
            
            return levelsConfig.GetNextLevelConfig(highestCompletedLevel).LevelId;
        }
    }
}