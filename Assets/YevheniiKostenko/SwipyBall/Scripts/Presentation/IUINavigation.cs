using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Presentation
{
    public interface IUINavigation
    {
        void CloseTopWindow();
        void CloseAllWindows();
        void OpenInputPanel();
        void OpenGameScreen();
        void OpenFinishGameWindow(GameResult gameResult, Action onRestartButtonClick, Action onNextLevelButtonClick);
    }
}