using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IPlayerFactory
    {
        PlayerView Create(Vector3 position);
    }
}