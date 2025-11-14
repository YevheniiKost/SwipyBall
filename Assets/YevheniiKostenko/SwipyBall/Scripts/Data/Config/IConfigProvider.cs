using UnityEngine;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;
using YevheniiKostenko.SwipyBall.Scripts.Data.Config;

namespace YevheniiKostenko.SwipyBall.Scripts.Data.config
{
    public interface IConfigProvider
    {
        PlayerConfig GetPlayerConfig();
        
        GameObject GetPlayerPrefab();
    }
    
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
                _globalGameConfig.JumpForce,
                _globalGameConfig.PushForce,
                _globalGameConfig.GroundCheckDistance,
                _globalGameConfig.MaxJumpCount,
                _globalGameConfig.NextJumpDecreaseFactor);
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