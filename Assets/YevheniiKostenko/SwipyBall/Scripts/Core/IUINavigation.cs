using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Core
{
    public interface IUINavigation
    {
        void CloseTopWindow();
        void OpenInputPanel();
        void OpenFinishGameWindow(GameResult gameResult, Action onRestartButtonClick, Action onExitButtonClick);
    }
}