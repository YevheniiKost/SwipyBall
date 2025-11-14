using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.App;
using YeKostenko.CoreKit.UI;

using YevheniiKostenko.SwipyBall.Scripts.Core;
using YevheniiKostenko.SwipyBall.Scripts.Core.GameStateMachine;
using YevheniiKostenko.SwipyBall.Scripts.Core.GameStateMachine.States;
using YevheniiKostenko.SwipyBall.Scripts.Data.config;
using YevheniiKostenko.SwipyBall.Scripts.Domain;
using YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel;
using YevheniiKostenko.SwipyBall.Scripts.Presentation.UI;

namespace YevheniiKostenko.SwipyBall.Scripts.Application
{
    public class SwipyBallApplication : BaseApp
    {
        protected override void OnAppCreate()
        {
            Container container = new Container();
            
            UIRoot.Instance.Initialize(new UIDependencyInjector(container));
            
            container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingleton();
            
            container.Bind<IGameModel>().To<GameModel>().AsSingleton();

            container.Bind<IUINavigation>().ToInstance(new UINavigation(UIRoot.Instance.UIManager));
            container.Bind<IInputPanelPresenter>().To<InputPanelPresenter>().AsTransient();
            container.Bind<IFinishGameWindowPresenter>().To<FinishGameWindowPresenter>().AsTransient();
            
            container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingleton();
            
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