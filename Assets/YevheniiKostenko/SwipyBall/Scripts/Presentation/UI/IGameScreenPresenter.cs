namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IGameScreenPresenter
    {
        void AttachView(IGameScreen view);
        void DetachView();
    }
}