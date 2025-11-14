using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel
{
    public interface IPlayerFactory
    {
        PlayerView Create(Vector3 position);
    }
}