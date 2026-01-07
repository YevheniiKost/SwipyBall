using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YeKostenko.CoreKit.DI;
using YevheniiKostenko.SwipyBall.Core.Time;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;
using YevheniiKostenko.SwipyBall.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    public class GameFlowController : MonoBehaviour
    {
        [SerializeField]
        private LevelRoot _levelRoot;
        
        private IGameStateMachine _gameStateMachine;

        private List<IGameStatePresenter> _statePresenters;
        private UniTask _transitionTask = UniTask.CompletedTask;

        [Inject]
        private void Construct(IGameStateMachine stateMachine, IUINavigation uiNavigation, ITImeProvider timeProvider,
            IInputModel inputModel)
        {
            _gameStateMachine = stateMachine;

            _statePresenters = new List<IGameStatePresenter>
            {
                new BootStatePresenter(),
                new PlayingStatePresenter(uiNavigation, _levelRoot, timeProvider, inputModel),
                new FinishGameStatePresenter(uiNavigation),
                new MainMenuStatePresenter(uiNavigation)
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
            if (previousState != null && TryGetPresenter(previousState, out var previousPresenter))
            {
                await previousPresenter.OnExitAsync(previousState);
            }

            if (newState != null && TryGetPresenter(newState, out var newPresenter))
            {
                await newPresenter.OnEnterAsync(newState);
            }
        }
        
        private bool TryGetPresenter(IGameState state, out IGameStatePresenter presenter)
        {
            presenter = null;
            
            if (state == null)
                return false;

            foreach (var p in _statePresenters)
            {
                if (p.GameStateType.IsInstanceOfType(state))
                {
                    presenter = p;
                    return true;
                }
            }

            return false;
        }
    }
}