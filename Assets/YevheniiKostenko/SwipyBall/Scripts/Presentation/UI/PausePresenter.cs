using System;
using YevheniiKostenko.CoreKit.Time;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class PausePresenter : IPausePresenter
    {
        private readonly ITimeProvider _timeProvider;
        
        private IPauseView _view;
        private PauseUIContext _pauseUIContext;

        public PausePresenter(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void AttachView(IPauseView view)
        {
            if (_view != null)
            {
                throw new InvalidOperationException("View is already attached.");
            }
            
            _view = view ?? throw new ArgumentNullException(nameof(view), "View cannot be null.");
            
            _view.Create += OnCreate;
            _view.GoToMenuClick += OnGoToMenuClick;
            _view.ResumeClick += OnResumeClick;
            _view.RestartClick += OnRestartClick;
        }

        public void DetachView()
        {
            if (_view == null)
            {
                return;
            }
            
            _view.Create -= OnCreate;
            _view.GoToMenuClick -= OnGoToMenuClick;
            _view.ResumeClick -= OnResumeClick;
            _view.RestartClick -= OnRestartClick;
            _view = null;
        }

        private void OnCreate(PauseUIContext pauseUIContext)
        {
            _pauseUIContext = pauseUIContext;
            _timeProvider.SetTimeScale(0f);
        }

        private void OnGoToMenuClick()
        {
            _timeProvider.SetTimeScale(1f);
            _pauseUIContext?.GoToMenuClicked?.Invoke();
        }
        
        private void OnResumeClick()
        {
            _timeProvider.SetTimeScale(1f);
            _pauseUIContext?.ResumeClicked?.Invoke();
        }
        
        private void OnRestartClick()
        {
            _timeProvider.SetTimeScale(1f);
            _pauseUIContext?.RestartClicked?.Invoke();
        }
    }
}