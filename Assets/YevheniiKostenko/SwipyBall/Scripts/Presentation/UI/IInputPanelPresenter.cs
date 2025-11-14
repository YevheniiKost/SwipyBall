using YeKostenko.CoreKit.Logging;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IInputPanelPresenter
    {
        void AttachView(IInputPanelView view);
        void DetachView();
    }
}