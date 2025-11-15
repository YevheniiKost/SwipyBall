using System.Collections.Generic;
using UnityEngine;

using YeKostenko.CoreKit.DI;

using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private TargetCamera _targetCamera;
        [SerializeField]
        private ExitPortal _exitPortal;
        
        private IPlayerView _playerInstance;
        private IGameModel _gameModel;
        private IPlayerFactory _playerFactory;
        
        private Dictionary<ICollectable, ICollectableView> _collectables = new();
        
        [Inject]
        private void Construct(IGameModel gameModel, IPlayerFactory playerFactory)
        {
            _gameModel = gameModel;
            _playerFactory = playerFactory;
            
            _gameModel.GameStarted += OnGameStarted;
            _gameModel.GameEnded += OnGameEnded;
        }

        private void Start()
        {
            foreach (ICollectableView view in GetComponentsInChildren<ICollectableView>(true))
            {
                view.Collectable.Collected = OnCollected;
                _collectables.Add(view.Collectable, view);
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

        private void OnGameStarted()
        {
            SpawnPlayer();

            _exitPortal.OnExitPortalEntered += OnPlayerEnterPortal;
            ActivateCollectables();
        }

        private void SpawnPlayer()
        {
            _playerInstance = _playerFactory.Create(_spawnPoint.position);
            _playerInstance.OnHit += OnPlayerHit;
            
            if (_targetCamera != null)
            {
                _targetCamera.SetCameraTarget(_playerInstance.Transform);
            }
        }
        
        private void ActivateCollectables()
        {
            foreach (ICollectableView view in _collectables.Values)
            {
                view.Activate();
            }
        }

        private void OnGameEnded(GameResult result)
        {
            if (_playerInstance != null)
            {
                _playerInstance.OnHit -= OnPlayerHit;
                _playerInstance.Destroy();
                _playerInstance = null;
                _targetCamera.ResetCameraTarget();
            }
            
            _exitPortal.OnExitPortalEntered -= OnPlayerEnterPortal;
        }

        private void OnPlayerEnterPortal()
        {
            _gameModel?.PlayerReachGoal();
        }

        private void OnPlayerHit(int damage)
        {
            _gameModel?.HitPlayer(damage);
        }
    }
}