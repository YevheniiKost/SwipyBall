using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class FinishGameState : BaseGameState, IFinishGameState
    {
        private GameResult _gameResult;
        
        public FinishGameState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }
        
        public GameResult GameResult => _gameResult;

        public override void Prepare(object payload = null)
        {
        }

        public override void Enter(object payload = null)
        {
            if (payload is not GameResult gameResult)
            {
                throw new System.ArgumentException("Payload must be of type GameResult", nameof(payload));
            }
        }

        public void Restart()
        {
            StateMachine.ChangeState<PlayingState>();
        }

        public void ExitToMenu()
        {
            StateMachine.ChangeState<MainMenuState>();
        }

        public override void Exit()
        {
            
        }

    }
}