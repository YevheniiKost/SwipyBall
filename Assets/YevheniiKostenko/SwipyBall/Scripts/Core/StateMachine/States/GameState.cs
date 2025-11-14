using System;
using Cysharp.Threading.Tasks;
using Unity.Android.Gradle.Manifest;
using YeKostenko.CoreKit.StateMachine;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;
using YevheniiKostenko.SwipyBall.Scripts.Domain;

namespace YevheniiKostenko.SwipyBall.Scripts.Core.GameStateMachine.States
{
    public class GameState : BaseGameState
    {
        private IUINavigation _uiNavigation;
        private IGameModel _gameModel;
        
        public GameState(StateMachine<GameStateContext> stateMachine) : base(stateMachine)
        {
        }

        public override void Prepare(object payload = null)
        {
            _uiNavigation = Context.Container.Resolve<IUINavigation>();
            _gameModel = Context.Container.Resolve<IGameModel>();
        }

        public override void Enter(object payload = null)
        {
            _uiNavigation.OpenInputPanel();
            
            _gameModel.GameEnded += OnGameEnded;
            
            StartGame().Forget();
        }

        private async UniTask StartGame()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
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
            _uiNavigation.CloseTopWindow();
            StateMachine.ChangeState<FinishGameState>(gameResult);
        }
    }
}