using System;

using UnityEngine;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.UI;

using YevheniiKostenko.SwipyBall.Core.Entities;
using YevheniiKostenko.SwipyBall.Presentation.UI.Common;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class FinishGameWindow : UIWindow, IFinishGameWindowView
    {
        [SerializeField]
        private ButtonView _restartButton;
        [SerializeField]
        private ButtonView _nextLevelButton;
        [SerializeField]
        private ButtonView _returnToMenuButton;
        
        [SerializeField]
        private GameObject _playerWonView;
        [SerializeField]
        private GameObject _playerLostView;
        
        private IFinishGameWindowPresenter _presenter;
        private FinishGameUIContext _context;
        
        public event Action<GameResult> Create;

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
        
        public override UniTask OnCloseAsync()
        {
            if (_presenter != null)
            {
                _presenter.DetachView();
                _presenter = null;
            }
            
            _restartButton.OnButtonClick = null;
            _nextLevelButton.OnButtonClick = null;
            
            return base.OnCloseAsync();
        }
        
        public void SetRestartButtonActive(bool isActive)
        {
            _restartButton.gameObject.SetActive(isActive);
        }

        public void SetNextLevelButtonActive(bool isActive)
        {
            _nextLevelButton.gameObject.SetActive(isActive);
        }

        public void SetGameResult(bool isPlayerWon)
        {
            _playerWonView.SetActive(isPlayerWon);
            _playerLostView.SetActive(!isPlayerWon);
        }

        private void Bind()
        {
            _restartButton.OnButtonClick = OnRestartButtonClick;
            _nextLevelButton.OnButtonClick = OnNextLevelButtonClick;
            _returnToMenuButton.OnButtonClick = OnReturnToMenuButtonClick;
        }
        
        private void OnNextLevelButtonClick()
        {
            _context?.NextLevelButtonClick?.Invoke();
        }

        private void OnRestartButtonClick()
        {
            _context?.RestartButtonClick?.Invoke();
        }
        
        private void OnReturnToMenuButtonClick()
        {
            _context?.ReturnToMenuButtonClick?.Invoke();
        }
    }
}