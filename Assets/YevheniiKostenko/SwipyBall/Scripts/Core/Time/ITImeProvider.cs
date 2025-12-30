namespace YevheniiKostenko.SwipyBall.Core.Time
{
    public interface ITImeProvider
    {
        void RegisterTimeListener(ITimeListener listener);
        void ClearTimeListener(ITimeListener listener);
    }
}