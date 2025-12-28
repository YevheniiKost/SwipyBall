using System;

using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.Input;
using YeKostenko.CoreKit.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class InputPanelView : UIWindow, IInputPanelView
    {
        [SerializeField]
        private SwipeMultiDirectionalComponent _swipeDetector;
        [SerializeField]
        private Button _leftButton;
        [SerializeField]
        private Button _rightButton;    
        
        private IInputPanelPresenter _inputPanelPresenter;
        
        public event Action<float> OnSwipe;
        public event Action LeftButtonPressed;
        public event Action RightButtonPressed;

        [Inject]
        public void Construct(IInputPanelPresenter presenter)
        {
            _swipeDetector.OnSwipe += SwipeHandler;
            _leftButton.onClick.AddListener(() => LeftButtonPressed?.Invoke());
            _rightButton.onClick.AddListener(() => RightButtonPressed?.Invoke());
            
            _inputPanelPresenter = presenter;
            _inputPanelPresenter.AttachView(this);
        }

        public override UniTask OnCloseAsync()
        {
            if (_inputPanelPresenter != null)
            {
                _inputPanelPresenter.DetachView();
                _inputPanelPresenter = null;
            }
            
            _swipeDetector.OnSwipe -= SwipeHandler;
            _leftButton.onClick.RemoveAllListeners();
            _rightButton.onClick.RemoveAllListeners();
            
            return base.OnCloseAsync();
        }
        
        private void SwipeHandler(float angle)
        {
            OnSwipe?.Invoke(angle);
        }
    }
}