using Cysharp.Threading.Tasks;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IGameModel _gameModel;
        private readonly IPlayerFactory _playerFactory;

        public LevelFactory(IGameModel gameModel, IPlayerFactory playerFactory)
        {
            _gameModel = gameModel;
            _playerFactory = playerFactory;
        }

        public async UniTask<IGameLevelView> Create(int levelIndex, Transform parent)
        {
            Object resource = await Resources.LoadAsync<GameObject>($"Levels/Level_{levelIndex}");
            GameObject levelPrefab = resource as GameObject;
            if (levelPrefab == null)
                throw new System.Exception();
            
            GameLevelView instance = Object.Instantiate(levelPrefab, parent).GetComponent<GameLevelView>();
            GameLevelController controller = new GameLevelController(_gameModel, instance);
            instance.Init(controller, _playerFactory);
            
            return instance;
        }
    }
}