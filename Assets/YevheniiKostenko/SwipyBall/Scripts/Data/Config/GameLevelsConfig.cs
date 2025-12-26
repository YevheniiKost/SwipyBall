using System.Collections.Generic;
using YeKostenko.CoreKit.Logging;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Scripts.Data.Config
{
    public class GameLevelsConfig
    {
        private readonly List<LevelConfig> _levelConfigs;

        public GameLevelsConfig(List<LevelConfig> levelConfigs)
        {
            _levelConfigs = levelConfigs;
        }

        public List<LevelConfig> LevelConfigs => _levelConfigs;
        
        public LevelConfig GetNextLevelConfig(int currentLevelIndex)
        {
            var config = _levelConfigs.Find(x => x.LevelId == currentLevelIndex + 1);

            if (config == null)
            {
                Logger.LogWarning($"Level config for level {currentLevelIndex + 1} not found. Returning first level config.");
                return _levelConfigs[0];
            }
            
            return config;
        }
        
        public LevelConfig GetFirstLevelConfig()
        {
            return _levelConfigs[0];
        }
    }
}