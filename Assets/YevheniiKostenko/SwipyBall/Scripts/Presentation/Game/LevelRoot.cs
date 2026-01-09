using UnityEngine;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.DI;
using YevheniiKostenko.CoreKit.Utils;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;

namespace YevheniiKostenko.SwipyBall.Presentation.Game
{
    [DefaultExecutionOrder(-100)]
    public class LevelRoot : SingletonMonoBehaviour<LevelRoot>, ILevelRoot
    {
        [SerializeField]
        private Transform _levelRoot;

        private ILevelFactory _levelFactory;
        private IDependencyInjector _dependencyInjector;

        private int _levelIndex = -1;
        private IGameLevelView _currentLevelInstance;

        public int LoadedLevelIndex => _levelIndex;
        
        public void Initialize(IDependencyInjector dependencyInjector)
        {
            _dependencyInjector = dependencyInjector;
        }
        
        [Inject]
        public void Construct(ILevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
        }
        
        public async UniTask<IGameLevelView> LoadLevel(int levelIndex)
        {
            if (levelIndex == _levelIndex && _currentLevelInstance != null)
            {
                return null;
            }

            UnloadCurrentLevel();

            try
            {
                IGameLevelView levelView = await _levelFactory.Create(levelIndex, _levelRoot);
                
                _dependencyInjector.Inject(levelView);
                _dependencyInjector.InjectIntoHierarchy(levelView);
                
                _levelIndex = levelIndex;
                _currentLevelInstance = levelView;
                
                return levelView;
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
                _currentLevelInstance.Destroy();
                _currentLevelInstance = null;
            }
        }
    }
}