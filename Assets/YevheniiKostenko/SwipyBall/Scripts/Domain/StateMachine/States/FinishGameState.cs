using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Data.Progress;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class FinishGameState : BaseGameState, IFinishGameState
    {
        private GameResult _gameResult;
        private readonly IProgressStorage _progressStorage;
        
        public FinishGameState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
            _progressStorage = Context.Container.Resolve<IProgressStorage>();
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
            
            _gameResult = gameResult;
            
            if (_gameResult.IsPlayerWon)
            {
                PlayerProgress progress = _progressStorage.Progress;
                progress.CompletedLevels.Add(_gameResult.LevelIndex);
                _progressStorage.SaveProgress(progress);
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