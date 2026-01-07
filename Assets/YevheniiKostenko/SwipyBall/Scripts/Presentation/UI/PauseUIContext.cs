using System;
using YeKostenko.CoreKit.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class PauseUIContext : IUIContext
    {
        public Action GoToMenuClicked { get; }
        public Action ResumeClicked { get; }
        public Action RestartClicked { get; }
        
        public PauseUIContext(Action goToMenuClicked, Action resumeClicked, Action restartClicked)
        {
            GoToMenuClicked = goToMenuClicked;
            ResumeClicked = resumeClicked;
            RestartClicked = restartClicked;
        }
    }
}