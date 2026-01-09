using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public interface ILevelRoot
    {
        int LoadedLevelIndex { get; }
        UniTask<IGameLevelView> LoadLevel(int levelIndex);
        void UnloadCurrentLevel();
    }
}