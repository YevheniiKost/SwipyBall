using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.App;
using YeKostenko.CoreKit.UI;

using YevheniiKostenko.SwipyBall.Data.config;
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
        protected override void OnAppCreate()
        {
            Container container = new Container();
            
            UIRoot.Instance.Initialize(new MonoBehDependencyInjector(container));
            LevelRoot.Instance.Initialize(new MonoBehDependencyInjector(container));
            
            container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingleton();
            
            container.Bind<IGameModel>().To<GameModel>().AsSingleton();
            container.Bind<IInputModel>().To<InputModel>().AsSingleton();

            container.Bind<IUINavigation>().ToInstance(new UINavigation(UIRoot.Instance.UIManager));
            container.Bind<IInputPanelPresenter>().To<InputPanelPresenter>().AsTransient();
            container.Bind<IFinishGameWindowPresenter>().To<FinishGameWindowPresenter>().AsTransient();
            container.Bind<IGameScreenPresenter>().To<GameScreenPresenter>().AsTransient();
            
            container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingleton();
            container.Bind<ICollectableFactory>().To<CollectableFactory>().AsSingleton();
            container.Bind<IDamageSourceFactory>().To<DamageSourceFactory>().AsSingleton();
            
            GameStateContext context = new  GameStateContext(container);
            GameStateMachine gameStateMachine = new GameStateMachine(context);
            gameStateMachine.RegisterState(new BootState(gameStateMachine));
            gameStateMachine.RegisterState(new MainMenuState(gameStateMachine));
            gameStateMachine.RegisterState(new PlayingState(gameStateMachine));
            gameStateMachine.RegisterState(new FinishGameState(gameStateMachine));
            container.Bind<IGameStateMachine>().ToInstance(gameStateMachine);
            
            gameStateMachine.ChangeState<BootState>();
        }

        protected override void OnAppDestroy()
        {
        }
    }
}