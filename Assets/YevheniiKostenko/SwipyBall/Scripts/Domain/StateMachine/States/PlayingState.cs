using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class PlayingState : BaseGameState, IPlayingState
    {
        private IGameModel _gameModel;
        
        public PlayingState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public override void Prepare(object payload = null)
        {
            _gameModel = Context.Container.Resolve<IGameModel>();
        }

        public override void Enter(object payload = null)
        {
            _gameModel.GameEnded += OnGameEnded;
            
            if (_gameModel.CanStartGame())
            {
                _gameModel.StartGame();
            }
        }

        public override void Exit()
        {
            _gameModel.GameEnded -= OnGameEnded;
        }
        
        private void OnGameEnded(GameResult gameResult)
        {
            StateMachine.ChangeState<FinishGameState>(gameResult);
        }
    }
}