using UnityEngine;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Data.config
{
    public interface IConfigProvider
    {
        PlayerConfig GetPlayerConfig();
        GameConfig GetGameConfig();
        
        GameObject GetPlayerPrefab();
    }
}