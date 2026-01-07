using System;

using UnityEngine;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.UI;

using YevheniiKostenko.SwipyBall.Presentation.UI.Common;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class PauseWindow : UIWindow, IPauseView
    {
        [SerializeField]
        private ButtonView _mainMenuButton;
        [SerializeField]
        private ButtonView[] _closeButton;
        [SerializeField]
        private ButtonView _restartButton;
        
        private IPausePresenter _presenter;
        
        public event Action<PauseUIContext> Create;
        public event Action GoToMenuClick;
        public event Action ResumeClick;
        public event Action RestartClick;

        [Inject]
        public void Construct(IPausePresenter presenter)
        {
            _presenter = presenter;
        }

        public override UniTask OnCreateAsync(IUIContext context)
        {
            PauseUIContext pauseUIContext =
                context as PauseUIContext ?? throw new ArgumentException("context");
            
            Bind();
            Create?.Invoke(pauseUIContext);
            
            return base.OnCreateAsync(context);
        }

        public override UniTask OnCloseAsync()
        {
            Unbind();
            return base.OnCloseAsync();
        }

        private void Bind()
        {
            _mainMenuButton.OnButtonClick += () => GoToMenuClick?.Invoke();
            _restartButton.OnButtonClick += () => RestartClick?.Invoke();
            foreach (var button in _closeButton)
            {
                button.OnButtonClick += () => ResumeClick?.Invoke();
            }
            
            _presenter.AttachView(this);
        }
        
        private void Unbind()
        {
            _mainMenuButton.OnButtonClick = null;
            _restartButton.OnButtonClick = null;
            foreach (var button in _closeButton)
            {
                button.OnButtonClick = null;
            }
            
            _presenter.DetachView();
            _presenter = null;
        }
    }
}