using System;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IInputPanelView
    {
        event Action<float> OnSwipe;
        event Action LeftButtonPressed;
        event Action RightButtonPressed;
    }
}