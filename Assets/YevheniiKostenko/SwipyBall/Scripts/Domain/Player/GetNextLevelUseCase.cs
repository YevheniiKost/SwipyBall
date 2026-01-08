using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Config;
using YevheniiKostenko.SwipyBall.Data.Progress;

namespace YevheniiKostenko.SwipyBall.Domain.Player
{
    public class GetNextLevelUseCase : IGetNextLevelUseCase
    {
        private readonly IProgressStorage _progressStorage;
        private readonly IConfigProvider _configProvider;

        public GetNextLevelUseCase(IProgressStorage progressStorage, IConfigProvider configProvider)
        {
            _progressStorage = progressStorage;
            _configProvider = configProvider;
        }

        public int Execute()
        {
            _progressStorage.Init();
            
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