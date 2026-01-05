using System;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IInputPanelView
    {
        event Action<float> OnSwipe;
        
        event Action LeftButtonUp;
        event Action LeftButton;
        event Action RightButtonUp;
        event Action RightButton;
        
        void SetLeftButtonPressedVisual(bool isPressed);
        void SetRightButtonPressedVisual(bool isPressed);
    }
}