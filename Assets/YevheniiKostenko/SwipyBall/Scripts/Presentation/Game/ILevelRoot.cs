using Cysharp.Threading.Tasks;

using YevheniiKostenko.SwipyBall.Presentation.GameLevel;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public interface ILevelRoot
    {
        int LoadedLevelIndex { get; }
        GameLevelView LoadedLevelView { get; }
        UniTask<GameLevelView> LoadLevel(int levelIndex);
        void UnloadCurrentLevel();
    }
}