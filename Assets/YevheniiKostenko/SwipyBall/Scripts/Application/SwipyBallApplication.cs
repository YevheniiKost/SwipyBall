using System.Collections.Generic;

using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.App;
using YeKostenko.CoreKit.Scripts.Saving;
using YeKostenko.CoreKit.UI;

using YevheniiKostenko.CoreKit.Time;
using YevheniiKostenko.SwipyBall.Data.Config;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;

using YevheniiKostenko.SwipyBall.Presentation;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;
using YevheniiKostenko.SwipyBall.Presentation.UI;
using YevheniiKostenko.SwipyBall.Domain.Input;
using YevheniiKostenko.SwipyBall.Presentation.Game;
using YevheniiKostenko.SwipyBall.Data.Progress;
using YevheniiKostenko.SwipyBall.Domain.Progress;

namespace YevheniiKostenko.SwipyBall.Application
{
    public class SwipyBallApplication : BaseApp
    {
        private Container _container;
        private GameStateMachine _gameStateMachine;
        
        protected override void OnAppCreate()
        {
            _container = new Container();
            
            UIRoot.Instance.Initialize(new MonoBehDependencyInjector(_container));
            LevelRoot.Instance.Initialize(new MonoBehDependencyInjector(_container));
            ITimeProvider timeProvider = UnityTimeProvider.Instance;

            var saveService = new SaveService(new FileJsonStorage(UnityEngine.Application.persistentDataPath),
                new NewtonsoftJsonSerializer(),
                new List<ISaveDataProvider>(), new SaveMigrationService(new List<ISaveMigration>()));
            
            _container.Bind<SaveService>().ToInstance(saveService);
            _container.Bind<IProgressStorage>().To<ProgressStorage>().AsSingleton();
            
            _container.Bind<ITimeProvider>().ToInstance(timeProvider);
            _container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingleton();
            
            _container.Bind<IGameModel>().To<GameModel>().AsSingleton();
            _container.Bind<IInputModel>().To<InputModel>().AsSingleton();
            _container.Bind<IGetNextLevelUseCase>().To<GetNextLevelUseCase>().AsTransient();

            _container.Bind<IUINavigation>().ToInstance(new UINavigation(UIRoot.Instance.UIManager));
            _container.Bind<IInputPanelPresenter>().To<InputPanelPresenter>().AsTransient();
            _container.Bind<IFinishGameWindowPresenter>().To<FinishGameWindowPresenter>().AsTransient();
            _container.Bind<IGameScreenPresenter>().To<GameScreenPresenter>().AsTransient();
            _container.Bind<IMainMenuPresenter>().To<MainMenuPresenter>().AsTransient();
            _container.Bind<IPausePresenter>().To<PausePresenter>().AsTransient();
            
            _container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingleton();
            _container.Bind<ICollectableFactory>().To<CollectableFactory>().AsSingleton();
            _container.Bind<IDamageSourceFactory>().To<DamageSourceFactory>().AsSingleton();
            
            GameStateContext context = new  GameStateContext(_container);
            _gameStateMachine = new GameStateMachine(context);
            _gameStateMachine.RegisterState(new BootState(_gameStateMachine));
            _gameStateMachine.RegisterState(new MainMenuState(_gameStateMachine));
            _gameStateMachine.RegisterState(new PlayingState(_gameStateMachine));
            _gameStateMachine.RegisterState(new FinishGameState(_gameStateMachine));
            _container.Bind<IGameStateMachine>().ToInstance(_gameStateMachine);

            UnityEngine.Application.targetFrameRate = 60;
        }

        protected override void OnAppStart()
        {
            try
            {
                _container.InjectIntoAllSceneMonos();
                _gameStateMachine.ChangeState<BootState>();
                base.OnAppStart();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"Application start failed: {e}");
                throw;
            }
        }

        protected override void OnAppDestroy()
        {
            _container.Dispose();
        }
    }
}