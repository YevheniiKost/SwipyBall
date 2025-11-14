using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.UI;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.UI
{
    public class FinishGameWindow : UIWindow, IFinishGameWindowView
    {
        [SerializeField]
        private Button _restartButton;
        
        private IFinishGameWindowPresenter _presenter;
        private FinishGameUIContext _context;
        
        public event Action<GameResult> Create;
        public event Action RestartButtonClick;

        [Inject]
        public void Construct(IFinishGameWindowPresenter presenter)
        {
            _presenter = presenter;
            _presenter.AttachView(this);
        }
        
        public override UniTask OnCreateAsync(IUIContext context)
        {
            _context = context as FinishGameUIContext ?? throw new ArgumentException("Invalid context type for FinishGameWindow");

            Bind();
            
            Create?.Invoke(_context.GameResult);
            
            return base.OnCreateAsync(context);
        }

        private void Bind()
        {
            _restartButton.onClick.AddListener(() =>
            {
                if (_context != null)
                {
                    _context.RestartButtonClick?.Invoke();
                }
            });
        }

        public override UniTask OnCloseAsync()
        {
            if (_presenter != null)
            {
                _presenter.DetachView();
                _presenter = null;
            }
            return base.OnCloseAsync();
        }
    }
}