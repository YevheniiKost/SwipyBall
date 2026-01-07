using Cysharp.Threading.Tasks;
using YellowTape.AudioEngine;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class BootStatePresenter : GameStatePresenterBase<IBootState>
    {
        protected override async UniTask OnEnterAsync(IBootState state)
        {
            await UniTask.Delay(100);
            MyAudio.Engine.PlayClip(SoundType.Background, "clip_1", true);
            // Implement boot state enter logic here
        }

        protected override UniTask OnExitAsync(IBootState state)
        {
            // Implement boot state exit logic here
            return UniTask.CompletedTask;
        }
    }
}