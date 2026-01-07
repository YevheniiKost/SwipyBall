using System;
using YeKostenko.CoreKit.UI;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class FinishGameUIContext : IUIContext
    {
        public readonly GameResult GameResult;
        public readonly Action RestartButtonClick;  
        public readonly Action NextLevelButtonClick;
        public readonly Action ReturnToMenuButtonClick;

        public FinishGameUIContext(GameResult gameResult, Action restartButtonClick, Action nextLevelButtonClick,
            Action returnToMenuButtonClick)
        {
            GameResult = gameResult;
            RestartButtonClick = restartButtonClick;
            NextLevelButtonClick = nextLevelButtonClick;
            ReturnToMenuButtonClick = returnToMenuButtonClick;
        }
    }
}