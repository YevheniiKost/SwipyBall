using System;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IMainMenuView
    {
        event Action<MainMenuUIContext> Create;
        
        event Action PlayButtonClick;
        event Action LinkedInButtonClick;
        event Action GithubButtonClick;
        event Action ItchIoButtonClick;
        
        void SetAppVersion(string version);
        void SetNextLevelNumber(int number);
    }
}