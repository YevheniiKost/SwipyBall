using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IFinishGameWindowView
    {
        event Action<GameResult> Create;
        
        void SetRestartButtonActive(bool isActive);
        void SetNextLevelButtonActive(bool isActive);
        void SetGameResult(bool isPlayerWon);
    }
}