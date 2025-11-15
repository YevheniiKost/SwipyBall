using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.App;
using YeKostenko.CoreKit.UI;

using YevheniiKostenko.SwipyBall.Core;
using YevheniiKostenko.SwipyBall.Core.GameStateMachine;
using YevheniiKostenko.SwipyBall.Core.GameStateMachine.States;

using YevheniiKostenko.SwipyBall.Data.config;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Presentation.GameLevel;
using YevheniiKostenko.SwipyBall.Presentation.UI;
using YevheniiKostenko.SwipyBall.Scripts.Domain.Input;

namespace YevheniiKostenko.SwipyBall.Application
{
    public class SwipyBallApplication : BaseApp
    {
        protected override void OnAppCreate()
        {
            Container container = new Container();
            
            UIRoot.Instance.Initialize(new UIDependencyInjector(container));
            
            container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingleton();
            
            container.Bind<IGameModel>().To<GameModel>().AsSingleton();
            container.Bind<IInputModel>().To<InputModel>().AsSingleton();

            container.Bind<IUINavigation>().ToInstance(new UINavigation(UIRoot.Instance.UIManager));
            container.Bind<IInputPanelPresenter>().To<InputPanelPresenter>().AsTransient();
            container.Bind<IFinishGameWindowPresenter>().To<FinishGameWindowPresenter>().AsTransient();
            container.Bind<IGameScreenPresenter>().To<GameScreenPresenter>().AsTransient();
            
            container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingleton();
            container.Bind<ICollectableFactory>().To<CollectableFactory>().AsSingleton();
            
            GameStateContext context = new  GameStateContext(container);
            GameStateMachine gameStateMachine = new GameStateMachine(context);
            gameStateMachine.RegisterState(new BootState(gameStateMachine));
            gameStateMachine.RegisterState(new MainMenuState(gameStateMachine));
            gameStateMachine.RegisterState(new GameState(gameStateMachine));
            gameStateMachine.RegisterState(new FinishGameState(gameStateMachine));
            gameStateMachine.ChangeState<BootState>();
        }

        protected override void OnAppDestroy()
        {
        }
    }
}