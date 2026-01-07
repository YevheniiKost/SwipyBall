using System;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IPauseView
    {
        event Action<PauseUIContext> Create;
        event Action GoToMenuClick;
        event Action ResumeClick;
        event Action RestartClick;
    }
}