using System.Collections.Generic;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Domain.Game;

using Logger = YeKostenko.CoreKit.Logging.Logger;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class GameLevelView : MonoBehaviour, IGameLevelView
    {
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private ExitPortal _exitPortal;
        
        private IPlayerView _playerInstance;
        private IPlayerFactory _playerFactory;
        private IGameLevelController _controller;
        
        private Dictionary<ICollectable, ICollectableView> _collectables = new();
        private List<IActivatableView> _activatableViews = new();

        public void Init(IGameLevelController controller, IPlayerFactory playerFactory)
        {
            _controller = controller;
            _playerFactory = playerFactory;
            
            _controller.Initialize();
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
            
            _exitPortal.OnEnter += OnEnterPortal;
        }

        private void OnDestroy()
        {
            _exitPortal.OnEnter -= OnEnterPortal;
            
            _controller.Dispose(); 
            _controller = null;
        }
        
        public void SpawnPlayer()
        {
            _playerInstance = _playerFactory.Create(_spawnPoint.position);
            TargetCamera.Instance.SetCameraTarget(_playerInstance.Transform);
        }

        public void DestroyPlayer()
        {
            if (_playerInstance != null)
            {
                _playerInstance.Destroy();
                _playerInstance = null;
                TargetCamera.Instance.ResetCameraTarget();
            }
        }
        
        public void ActivateViews()
        {
            foreach (IActivatableView view in _activatableViews)
            {
                view.Activate();
            }
        }

        public void DeactivateCollectable(ICollectable collectable)
        {
            if (_collectables.TryGetValue(collectable, out ICollectableView view))
            {
                view.Deactivate();
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnCollected(ICollectable collectable)
        {
            _controller.RegisterCollectableInteraction(collectable);
        }

        private void OnEnterPortal()
        {
            _controller.RegisterPlayerReachedFinish();
        }
    }
}