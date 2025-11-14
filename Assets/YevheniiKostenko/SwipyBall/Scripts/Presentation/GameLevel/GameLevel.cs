using System;
using System.Collections.Generic;
using UnityEngine;
using YeKostenko.CoreKit.DI;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;
using YevheniiKostenko.SwipyBall.Scripts.Domain;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private TargetCamera _targetCamera;
        [SerializeField]
        private ExitPortal _exitPortal;
        
        private PlayerView _playerInstance;
        private IGameModel _gameModel;
        private IPlayerFactory _playerFactory;
        
        private List<ScoreCoin> _scoreCoins = new();
        
        [Inject]
        private void Construct(IGameModel gameModel, IPlayerFactory playerFactory)
        {
            _gameModel = gameModel;
            _playerFactory = playerFactory;
            
            _gameModel.GameStarted += OnGameStarted;
            _gameModel.GameEnded += OnGameEnded;
        }

        private void Awake()
        {
            foreach (ICollectable collectable in GetComponentsInChildren<ICollectable>(true))
            {
                if (collectable is ScoreCoin coin)
                {
                    coin.Collected = OnScoreCollected;
                     _scoreCoins.Add(coin);
                }
            }
        }

        private void OnScoreCollected(ICollectable collectable)
        {
            if (_gameModel?.IsGameStarted == true)
            {
                _gameModel.AddScore(1);
                collectable.Deactivate();
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
            
            if (_targetCamera != null)
            {
                _targetCamera.SetCameraTarget(_playerInstance.transform);
            }
        }
        
        private void ActivateCollectables()
        {
            foreach (ScoreCoin coin in _scoreCoins)
            {
                coin.Activate();
            }
        }

        private void OnGameEnded(GameResult result)
        {
            if (_playerInstance != null)
            {
                Destroy(_playerInstance.gameObject);
                _playerInstance = null;
                _targetCamera.ResetCameraTarget();
            }
            
            _exitPortal.OnExitPortalEntered -= OnPlayerEnterPortal;
        }
        
        private void OnPlayerEnterPortal()
        {
            if (_gameModel != null)
            {
                _gameModel.PlayerReachGoal();
            }
        }
    }
}