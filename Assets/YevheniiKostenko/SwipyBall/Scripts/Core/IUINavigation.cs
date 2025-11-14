using System;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Scripts.Core
{
    public interface IUINavigation
    {
        void CloseTopWindow();
        void OpenInputPanel();
        void OpenFinishGameWindow(GameResult gameResult, Action onRestartButtonClick, Action onExitButtonClick);
    }
}