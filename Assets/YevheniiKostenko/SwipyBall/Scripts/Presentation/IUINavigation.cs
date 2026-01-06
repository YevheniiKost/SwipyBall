using YevheniiKostenko.SwipyBall.Presentation.UI;

namespace YevheniiKostenko.SwipyBall.Presentation
{
    public interface IUINavigation
    {
        void CloseTopWindow();
        void CloseAllWindows();
        void OpenInputPanel();
        void OpenGameScreen();
        void OpenFinishGameWindow(FinishGameUIContext context);
        void OpenMainMenu(MainMenuUIContext context);
    }
}