using System;
using YeKostenko.CoreKit.UI;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class FinishGameUIContext : IUIContext
    {
        public readonly GameResult GameResult;
        public readonly Action RestartButtonClick;  
        public readonly Action ExitButtonClick;

        public FinishGameUIContext(GameResult gameResult, Action restartButtonClick, Action exitButtonClick)
        {
            GameResult = gameResult;
            RestartButtonClick = restartButtonClick;
            ExitButtonClick = exitButtonClick;
        }
    }
}