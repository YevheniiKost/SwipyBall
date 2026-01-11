using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class FinishGameWindowPresenter : IFinishGameWindowPresenter
    {
        private IFinishGameWindowView _view;

        public void AttachView(IFinishGameWindowView view)
        {
            if (_view != null)
            {
                throw new System.InvalidOperationException("View is already attached.");
            }

            _view = view ?? throw new System.ArgumentNullException(nameof(view), "View cannot be null.");

            _view.Create += OnCreate;
        }
        
        public void DetachView()
        {
            if (_view == null)
            {
                return;
            }
            
            _view.Create -= OnCreate;
            
            _view = null;
        }
        
        private void OnCreate(GameResult result)
        {
            _view.SetGameResult(result.IsPlayerWon);
            _view.SetNextLevelButtonActive(result.IsPlayerWon);
        }
    }
}