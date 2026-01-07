namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IPausePresenter
    {
        void AttachView(IPauseView view);
        void DetachView();
    }
}