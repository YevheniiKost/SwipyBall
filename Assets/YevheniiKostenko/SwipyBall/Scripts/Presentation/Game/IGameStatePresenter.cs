using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public interface IGameStatePresenter
    {
        UniTask OnEnterAsync(IGameState state);
        UniTask OnExitAsync(IGameState state);
    }
}