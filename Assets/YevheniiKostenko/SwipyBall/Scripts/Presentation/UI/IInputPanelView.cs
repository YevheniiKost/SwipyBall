using System;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.UI
{
    public interface IInputPanelView
    {
        event Action<float> OnSwipe;
    }
}