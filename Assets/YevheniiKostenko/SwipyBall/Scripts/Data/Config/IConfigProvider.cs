using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Data.Config
{
    public interface IConfigProvider
    {
        PlayerConfig GetPlayerConfig();
        GameConfig GetGameConfig();
        GameLevelsConfig GetLevelsConfig();
        AppConfig GetAppConfig();
        
        GameObject GetPlayerPrefab();
    }
}