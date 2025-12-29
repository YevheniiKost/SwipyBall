using System;

using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.Input;
using YeKostenko.CoreKit.UI;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class InputPanelView : UIWindow, IInputPanelView
    {
        [SerializeField]
        private SwipeMultiDirectionalComponent _swipeDetector;
        [SerializeField]
        private PointerArea _leftButton;
        [SerializeField]
        private PointerArea _rightButton;  
        
        [Header("Visuals")]
        [SerializeField]
        private Image _leftButtonImage;
        [SerializeField]
        private Image _rightButtonImage;
        [SerializeField]
        private Color _pressedColor;
        [SerializeField]
        private Color _unpressedColor;
        [SerializeField, Min(0.01f)]
        private float _fadeSpeed = 12f;
        
        private IInputPanelPresenter _inputPanelPresenter;
        
        public event Action<float> OnSwipe;
        public event Action LeftButtonPressed;
        public event Action RightButtonPressed;

        [Inject]
        public void Construct(IInputPanelPresenter presenter)
        {
            _swipeDetector.OnSwipe += SwipeHandler;
            
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
            
            return base.OnCloseAsync();
        }
        
        private void SwipeHandler(float angle)
        {
            OnSwipe?.Invoke(angle);
        }

        private void Update()
        {
            bool leftDown = _leftButton.IsPointerDown;
            bool rightDown = _rightButton.IsPointerDown;

            // Smooth fade of button images
            FadeButton(_leftButtonImage, leftDown);
            FadeButton(_rightButtonImage, rightDown);

            // Input events
            if (leftDown)
            {
                OnLeftButtonPressed();
            }
            else if (rightDown)
            {
                OnRightButtonPressed();
            }
        }
        
        private void OnRightButtonPressed()
        {
            RightButtonPressed?.Invoke();
        }

        private void OnLeftButtonPressed()
        {
            LeftButtonPressed?.Invoke();  
        }
        
        private void FadeButton(Image image, bool isPressed)
        {
            if (image == null) return;

            var target = isPressed ? _pressedColor : _unpressedColor;

            var t = 1f - Mathf.Exp(-_fadeSpeed * Time.deltaTime);
            image.color = Color.Lerp(image.color, target, t);
        }
    }
}