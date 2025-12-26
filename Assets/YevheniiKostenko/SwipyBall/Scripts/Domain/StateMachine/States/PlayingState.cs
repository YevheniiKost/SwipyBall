using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States
{
    public class PlayingState : BaseGameState, IPlayingState
    {
        private IGameModel _gameModel;
        
        private int _levelIndex;
        
        public PlayingState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public int CurrentLevelIndex => _levelIndex;

        public override void Prepare(object payload = null)
        {
            if (payload is not PlayingStateArgs args)
            {
                throw new System.ArgumentException($"payload is not of type {typeof(PlayingStateArgs)}", nameof(payload));
            }
            
            _gameModel = Context.Container.Resolve<IGameModel>();
            _levelIndex = args.LevelIndex;
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
        
        public void GoToMainMenu()
        {
            _gameModel.GameEnded -= OnGameEnded;
            _gameModel.EndGame(false);
            StateMachine.ChangeState<MainMenuState>();
        }

        public void RestartLevel()
        {
            _gameModel.GameEnded -= OnGameEnded;
            _gameModel.EndGame(false);
            StateMachine.ChangeState<PlayingState>(new PlayingStateArgs(_levelIndex));
        }
        
        private void OnGameEnded(bool isPlayerWon)
        {
            StateMachine.ChangeState<FinishGameState>(
              new FinishGameStateArgs(new GameResult(isPlayerWon, _levelIndex)));
        }
    }
}