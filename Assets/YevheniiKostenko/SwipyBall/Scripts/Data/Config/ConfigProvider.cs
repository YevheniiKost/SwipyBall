using System.Collections.Generic;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Data.Config
{
    internal class ConfigProvider : IConfigProvider
    {
        private GlobalGameConfig _globalGameConfig; 

        public PlayerConfig GetPlayerConfig()
        {
            GetConfigIsNeeded();
            
            return new PlayerConfig( 
                _globalGameConfig.MaxAngleDeviation,
                _globalGameConfig.MaxAngle,
                _globalGameConfig.TimeBetweenJumps,
                _globalGameConfig.TimeBetweenHits,
                _globalGameConfig.JumpForce,
                _globalGameConfig.PushForce,
                _globalGameConfig.HitPushForce,
                _globalGameConfig.GroundCheckDistance,
                _globalGameConfig.MaxJumpCount,
                _globalGameConfig.NextJumpDecreaseFactor);
        }

        public GameConfig GetGameConfig()
        {
            GetConfigIsNeeded();

            return new GameConfig(_globalGameConfig.CoinValue,
                _globalGameConfig.SpikeDamage,
                _globalGameConfig.BombDamage,
                _globalGameConfig.BombExplosionRadius,
                _globalGameConfig.BombExplosionDelay);
        }

        public GameLevelsConfig GetLevelsConfig()
        {
            GetConfigIsNeeded();
            
            List<SerializableLevelConfig> serializableConfigs = _globalGameConfig.LevelConfigs;
            List<LevelConfig> levelConfigs = new List<LevelConfig>();
            
            foreach (SerializableLevelConfig serializableConfig in serializableConfigs)
            {
                levelConfigs.Add(new LevelConfig(serializableConfig.LevelId));
            }
            
            return new GameLevelsConfig(levelConfigs);
        }

        public AppConfig GetAppConfig()
        {
            GetConfigIsNeeded();
            
            return new AppConfig( UnityEngine.Application.version,
                _globalGameConfig.LinkedinUrl,
                _globalGameConfig.GithubUrl,
                _globalGameConfig.ItchIoUrl);
        }

        public GameObject GetPlayerPrefab()
        {
            GetConfigIsNeeded();
            
            return _globalGameConfig.PlayerPrefab;
        }
        
        private void GetConfigIsNeeded()
        {
            if (_globalGameConfig != null)
                return;
            
            _globalGameConfig = Resources.Load<GlobalGameConfig>("Configs/GlobalGameConfig");
        }

    }
}