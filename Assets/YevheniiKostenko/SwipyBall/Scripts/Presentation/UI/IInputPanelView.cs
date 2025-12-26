using System;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IInputPanelView
    {
        event Action<float> OnSwipe;
    }
}