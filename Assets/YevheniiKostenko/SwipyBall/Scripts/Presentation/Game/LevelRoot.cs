using UnityEngine;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.UI;
using YevheniiKostenko.CoreKit.Scripts.Utils;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    [DefaultExecutionOrder(-100)]
    public class LevelRoot : SingletonMonoBehaviour<LevelRoot>, ILevelRoot
    {
        [SerializeField]
        private Transform _levelRoot;

        private IDependencyInjector _dependencyInjector;

        private int _levelIndex;
        private GameLevelView _currentLevelInstance;

        public int LoadedLevelIndex => _levelIndex;
        public GameLevelView LoadedLevelView => _currentLevelInstance;

        public void Initialize(IDependencyInjector dependencyInjector)
        {
            _dependencyInjector = dependencyInjector;
        }

        public async UniTask<GameLevelView> LoadLevel(int levelIndex)
        {
            if (levelIndex == _levelIndex && _currentLevelInstance != null)
            {
                return null;
            }

            if (_currentLevelInstance != null)
            {
                Destroy(_currentLevelInstance);
            }
            
            try
            {
                Object resource = await Resources.LoadAsync<GameObject>($"Levels/Level_{levelIndex}");
                GameObject levelPrefab = resource as GameObject;
                if(levelPrefab == null)
                    throw new System.Exception();
                
                _currentLevelInstance = Instantiate(levelPrefab, _levelRoot).GetComponent<GameLevelView>();
                _levelIndex = levelIndex;
                _dependencyInjector.Inject(_currentLevelInstance);
                _dependencyInjector.InjectIntoHierarchy(_currentLevelInstance);

                return _currentLevelInstance;
            }
            catch
            {
                Debug.LogError($"Level_{levelIndex} not found in Resources/Levels/");
            }
            
            return null;
        }

        public void UnloadCurrentLevel()
        {
            if (_currentLevelInstance != null)
            {
                Destroy(_currentLevelInstance.gameObject);
                _currentLevelInstance = null;
            }
        }
    }
}