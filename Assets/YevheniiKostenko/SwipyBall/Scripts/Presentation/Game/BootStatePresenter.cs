using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class BootStatePresenter : GameStatePresenterBase<IBootState>
    {
        protected override UniTask OnEnterAsync(IBootState state)
        {
            // Implement boot state enter logic here
            return UniTask.CompletedTask;
        }

        protected override UniTask OnExitAsync(IBootState state)
        {
            // Implement boot state exit logic here
            return UniTask.CompletedTask;
        }
    }
}