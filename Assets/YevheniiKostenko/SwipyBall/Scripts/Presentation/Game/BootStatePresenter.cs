using Cysharp.Threading.Tasks;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;
using YevheniiKostenko.SwipyBall.Presentation.Audio;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class BootStatePresenter : GameStatePresenterBase<IBootState>
    {
        protected override async UniTask OnEnterAsync(IBootState state)
        {
            await UniTask.Delay(100);

            Music.Instance.Play();
        }

        protected override UniTask OnExitAsync(IBootState state)
        {
            // Implement boot state exit logic here
            return UniTask.CompletedTask;
        }
    }
}