using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.Logging;
using YeKostenko.CoreKit.StateMachine;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class BootState : BaseGameState, IBootState
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
          
            StateMachine.ChangeState<MainMenuState>();
        }

        public override void Exit()
        {
        }
    }
}