using System;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.UI
{
    public interface IFinishGameWindowView
    {
        event Action<GameResult> Create;
        event Action RestartButtonClick;
    }
}