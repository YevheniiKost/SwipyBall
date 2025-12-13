using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YeKostenko.CoreKit.DI;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class GameFlowController : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        private Dictionary<Type, IGameStatePresenter> _statePresenters;
        private UniTask _transitionTask = UniTask.CompletedTask;
        
        [Inject]
        private void Construct(IGameStateMachine stateMachine, IUINavigation uiNavigation)
        {
            _gameStateMachine = stateMachine;

            _statePresenters = new Dictionary<Type, IGameStatePresenter>
            {
                { typeof(IBootState), new BootStatePresenter() },
                { typeof(IPlayingState), new PlayingStatePresenter(uiNavigation) },
                { typeof(IFinishGameState), new FinishGameStatePresenter(uiNavigation) }
            };
            
            _gameStateMachine.OnStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            if (_gameStateMachine != null)
            {
                _gameStateMachine.OnStateChanged -= OnGameStateChanged;
            }
        }

        private void OnGameStateChanged(IGameState previousState, IGameState newState)
        {
            _transitionTask = HandleAsyncStateTransition(previousState, newState);
            _transitionTask.Forget();
        }

        private async UniTask HandleAsyncStateTransition(IGameState previousState, IGameState newState)
        {
            if (previousState != null && _statePresenters.TryGetValue(previousState.GetType(), out var previousPresenter))
            {
                await previousPresenter.OnExitAsync(previousState);
            }

            if (newState != null && _statePresenters.TryGetValue(newState.GetType(), out var newPresenter))
            {
                await newPresenter.OnEnterAsync(newState);
            }
        }
    }
}