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
        public event Action LeftButtonUp;
        public event Action LeftButtonDown;
        public event Action RightButtonUp;
        public event Action RightButtonDown;

        [Inject]
        public void Construct(IInputPanelPresenter presenter)
        {
            _swipeDetector.OnSwipe += SwipeHandler;
            
            _leftButton.OnPointerDownEvent += OnLeftButtonDown;
            _leftButton.OnPointerUpEvent += OnLeftButtonUp;
            _rightButton.OnPointerDownEvent += OnRightButtonDown;
            _rightButton.OnPointerUpEvent += OnRightButtonUp;
            
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
            _leftButton.OnPointerDownEvent -= OnLeftButtonDown;
            _leftButton.OnPointerUpEvent -= OnLeftButtonUp;
            _rightButton.OnPointerDownEvent -= OnRightButtonDown;
            _rightButton.OnPointerUpEvent -= OnRightButtonUp;
            
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
        }
        
        private void OnRightButtonDown() => RightButtonDown?.Invoke();
        private void OnLeftButtonDown() => LeftButtonDown?.Invoke();
        private void OnRightButtonUp() => RightButtonUp?.Invoke();
        private void OnLeftButtonUp() => LeftButtonUp?.Invoke();

        private void FadeButton(Image image, bool isPressed)
        {
            if (image == null) return;

            var target = isPressed ? _pressedColor : _unpressedColor;

            var t = 1f - Mathf.Exp(-_fadeSpeed * Time.deltaTime);
            image.color = Color.Lerp(image.color, target, t);
        }
    }
}