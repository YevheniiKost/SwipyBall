using System;
using YeKostenko.CoreKit.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class GameScreenUIContext : IUIContext
    {
        public readonly Action PauseButtonClicked;
        
        public GameScreenUIContext(Action pauseButtonClicked)
        {
            PauseButtonClicked = pauseButtonClicked;
        }
    }
}