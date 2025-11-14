using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IFinishGameWindowView
    {
        event Action<GameResult> Create;
        event Action RestartButtonClick;
    }
}