using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Scripts.Data.Config;

namespace YevheniiKostenko.SwipyBall.Data.config
{
    public interface IConfigProvider
    {
        PlayerConfig GetPlayerConfig();
        GameConfig GetGameConfig();
        GameLevelsConfig GetLevelsConfig();
        
        GameObject GetPlayerPrefab();
    }
}