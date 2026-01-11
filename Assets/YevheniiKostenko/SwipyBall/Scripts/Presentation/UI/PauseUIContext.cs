using System;
using YeKostenko.CoreKit.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class PauseUIContext : IUIContext
    {
        public readonly Action GoToMenuClicked;
        public readonly Action ResumeClicked;
        public readonly Action RestartClicked;
        
        public PauseUIContext(Action goToMenuClicked, Action resumeClicked, Action restartClicked)
        {
            GoToMenuClicked = goToMenuClicked;
            ResumeClicked = resumeClicked;
            RestartClicked = restartClicked;
        }
    }
}