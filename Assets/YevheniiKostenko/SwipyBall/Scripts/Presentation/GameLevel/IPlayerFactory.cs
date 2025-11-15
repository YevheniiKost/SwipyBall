using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface IPlayerFactory
    {
        IPlayerView Create(Vector3 position);
    }
}