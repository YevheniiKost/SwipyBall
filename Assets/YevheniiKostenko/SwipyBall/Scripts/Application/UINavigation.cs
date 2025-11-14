using System;
using Cysharp.Threading.Tasks;
using YeKostenko.CoreKit.UI;
using YevheniiKostenko.SwipyBall.Scripts.Core;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;
using YevheniiKostenko.SwipyBall.Scripts.Presentation.UI;

namespace YevheniiKostenko.SwipyBall.Scripts.Application
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

        public void OpenInputPanel()
        {
            _manager.OpenWindowAsync<InputPanelView>().Forget();
        }
        
        public void OpenFinishGameWindow(GameResult gameResult, Action onRestartButtonClick, Action onExitButtonClick)
        {
            _manager.OpenWindowAsync<FinishGameWindow>(new FinishGameUIContext(gameResult, onRestartButtonClick, onExitButtonClick)).Forget();
        }
    }
}