using System.Collections.Generic;
using UnityEngine;

using YeKostenko.CoreKit.DI;

using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Domain.Game;
using Logger = YeKostenko.CoreKit.Logging.Logger;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class GameLevelView : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private ExitPortal _exitPortal;
        
        private IPlayerView _playerInstance;
        private IGameModel _gameModel;
        private IPlayerFactory _playerFactory;
        
        private Dictionary<ICollectable, ICollectableView> _collectables = new();
        
        private List<IActivatableView> _activatableViews = new();
        
        [Inject]
        private void Construct(IGameModel gameModel, IPlayerFactory playerFactory)
        {
            _gameModel = gameModel;
            _playerFactory = playerFactory;
            
            _gameModel.GameEnded += OnGameEnded;
        }
        
        public void OnGameStarted()
        {
            SpawnPlayer();

            _exitPortal.OnExitPortalEntered += OnPlayerEnterPortal;
            ActivateViews();
        }
        
        private void OnGameEnded(bool isPlayerWon)
        {
            if (_playerInstance != null)
            {
                _playerInstance.Destroy();
                _playerInstance = null;
                TargetCamera.Instance.ResetCameraTarget();
            }
            
            _exitPortal.OnExitPortalEntered -= OnPlayerEnterPortal;
        }

        private void Start()
        {
            foreach (ICollectableView view in GetComponentsInChildren<ICollectableView>(true))
            {
                view.Collectable.Collected = OnCollected;
                _collectables.Add(view.Collectable, view);
            }
            
            foreach (IActivatableView activatableView in GetComponentsInChildren<IActivatableView>(true))
            {
                _activatableViews.Add(activatableView);
            }
        }

        private void OnCollected(ICollectable collectable)
        {
            if (_gameModel?.IsGameStarted == true)
            {
                if(collectable.Type == CollectableType.Coin)
                    _gameModel.AddScore(collectable.Value);
                else if(collectable.Type == CollectableType.Hp)
                    _gameModel.AddHealth(collectable.Value);
                
                if (_collectables.TryGetValue(collectable, out ICollectableView view))
                {
                    view.Deactivate();
                }
            }
        }

        private void SpawnPlayer()
        {
            _playerInstance = _playerFactory.Create(_spawnPoint.position);

            TargetCamera.Instance.SetCameraTarget(_playerInstance.Transform);
        }

        private void ActivateViews()
        {
            foreach (IActivatableView view in _activatableViews)
            {
                view.Activate();
            }
        }

        private void OnPlayerEnterPortal()
        {
            try
            {
                _gameModel?.PlayerReachGoal();
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"Error while processing player entering portal: {ex}");
            }
        }
    }
}