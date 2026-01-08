using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.App;
using YeKostenko.CoreKit.UI;

using YevheniiKostenko.SwipyBall.Core.Time;
using YevheniiKostenko.SwipyBall.Data.Config;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine;
using YevheniiKostenko.SwipyBall.Domain.GameStateMachine.States;

using YevheniiKostenko.SwipyBall.Presentation;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;
using YevheniiKostenko.SwipyBall.Presentation.UI;
using YevheniiKostenko.SwipyBall.Domain.Input;
using YevheniiKostenko.SwipyBall.Presentation.Game;

namespace YevheniiKostenko.SwipyBall.Application
{
    public class SwipyBallApplication : BaseApp
    {
        private Container _container;
        private GameStateMachine _gameStateMachine;
        
        protected override void OnAppCreate()
        {
            _container = new Container();
            
            UIRoot.Instance.Initialize(new MonoBehDependencyInjector(container));
            LevelRoot.Instance.Initialize(new MonoBehDependencyInjector(container));
            ITimeProvider timeProvider = UnityTimeProvider.Instance;
            
            container.Bind<ITimeProvider>().ToInstance(timeProvider);
            container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingleton();
            
            _container.Bind<IGameModel>().To<GameModel>().AsSingleton();
            _container.Bind<IInputModel>().To<InputModel>().AsSingleton();

            container.Bind<IUINavigation>().ToInstance(new UINavigation(UIRoot.Instance.UIManager));
            container.Bind<IInputPanelPresenter>().To<InputPanelPresenter>().AsTransient();
            container.Bind<IFinishGameWindowPresenter>().To<FinishGameWindowPresenter>().AsTransient();
            container.Bind<IGameScreenPresenter>().To<GameScreenPresenter>().AsTransient();
            container.Bind<IMainMenuPresenter>().To<MainMenuPresenter>().AsTransient();
            container.Bind<IPausePresenter>().To<PausePresenter>().AsTransient();
            
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
            _container.InjectIntoAllSceneMonos();
            _gameStateMachine.ChangeState<BootState>();
            base.OnAppStart();
        }

        protected override void OnAppDestroy()
        {
        }
    }
}