using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.UI;
using YevheniiKostenko.SwipyBall.Presentation;
using YevheniiKostenko.SwipyBall.Presentation.UI;

namespace YevheniiKostenko.SwipyBall.Application
{
    public class UINavigation : IUINavigation
    {
        private readonly UIManager _manager;
        
        public UINavigation(UIManager manager)
        {
            _manager = manager;
        }
        
        public void CloseTopWindow()
        {
            _manager.CloseTopWindowAsync().Forget();
        }

        public void CloseAllWindows()
        {
            _manager.CloseAllAsync().Forget();
        }

        public void OpenInputPanel()
        {
            _manager.OpenWindowAsync<InputPanelView>().Forget();
        }

        public void OpenGameScreen()
        {
            _manager.OpenWindowAsync<GameScreenView>().Forget();
        }

        public void OpenFinishGameWindow(FinishGameUIContext context)
        {
            _manager.OpenWindowAsync<FinishGameWindow>(context).Forget();
        }

        public void OpenMainMenu(MainMenuUIContext context)
        {
            _manager.OpenWindowAsync<MainMenuScreen>(context).Forget();
        }
    }
}