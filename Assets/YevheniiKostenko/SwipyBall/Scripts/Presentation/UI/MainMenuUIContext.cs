using System;
using YeKostenko.CoreKit.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class MainMenuUIContext : IUIContext
    {
        public readonly Action PlayButtonClick;

        public MainMenuUIContext(Action playButtonClick)
        {
            PlayButtonClick = playButtonClick;
        }
    }
}