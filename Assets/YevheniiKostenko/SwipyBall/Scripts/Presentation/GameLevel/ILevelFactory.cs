using Cysharp.Threading.Tasks;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface ILevelFactory
    {
        UniTask<IGameLevelView> Create(int levelIndex, Transform parent);
    }
}