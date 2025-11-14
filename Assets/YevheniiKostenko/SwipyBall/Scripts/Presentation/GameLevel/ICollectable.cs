using System;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public interface ICollectable
    {
        Action<ICollectable> Collected { get; set; }
        void Collect();
        void Activate();
        void Deactivate();
    }
}