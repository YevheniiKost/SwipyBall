using YevheniiKostenko.SwipyBall.Core;
using YevheniiKostenko.SwipyBall.Domain;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IFinishGameWindowPresenter
    {
        void AttachView(IFinishGameWindowView view);
        void DetachView();
    }
}