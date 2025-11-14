using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.Logging;
using YeKostenko.CoreKit.StateMachine;
using YeKostenko.CoreKit.UI;
using YevheniiKostenko.SwipyBall.Domain;
using YevheniiKostenko.SwipyBall.Presentation.UI;

namespace YevheniiKostenko.SwipyBall.Core.GameStateMachine.States
{
    public class BootState : BaseGameState
    {
        public BootState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
            
        }

        public override void Prepare(object payload = null)
        {
        }

        public override void Enter(object payload = null)
        {
            Logger.Log("Booting application...");

            Container container = Context.Container;
            container.InjectIntoAllSceneMonos();
          
            StateMachine.ChangeState<GameState>();
        }

        public override void Exit()
        {
        }
    }
}