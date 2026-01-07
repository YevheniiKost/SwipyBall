using YevheniiKostenko.SwipyBall.Presentation.UI;

namespace YevheniiKostenko.SwipyBall.Presentation
{
    public interface IUINavigation
    {
        void CloseTopWindow();
        void CloseAllWindows();
        void OpenInputPanel();
        void OpenPauseWindow(PauseUIContext context);
        void OpenGameScreen(GameScreenUIContext context);
        void OpenFinishGameWindow(FinishGameUIContext context);
        void OpenMainMenu(MainMenuUIContext context);
    }
}