using System;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IInputPanelView
    {
        event Action<float> OnSwipe;
        
        event Action LeftButtonUp;
        event Action LeftButtonDown;
        event Action RightButtonUp;
        event Action RightButtonDown;
    }
}