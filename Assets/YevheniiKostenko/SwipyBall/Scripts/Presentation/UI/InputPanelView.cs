using System;

using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        
        private Tween _leftButtonTween;
        private Tween _rightButtonTween;
        
        public event Action<float> OnSwipe;
        public event Action LeftButtonUp;
        public event Action LeftButton;
        public event Action RightButtonUp;
        public event Action RightButton;

        [Inject]
        public void Construct(IInputPanelPresenter presenter)
        {
            _swipeDetector.OnSwipe += SwipeHandler;
            
            _leftButton.OnPointerEvent += OnLeftButton;
            _leftButton.OnPointerUpEvent += OnLeftButtonUp;
            _rightButton.OnPointerEvent += OnRightButton;
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
            _leftButton.OnPointerEvent -= OnLeftButton;
            _leftButton.OnPointerUpEvent -= OnLeftButtonUp;
            _rightButton.OnPointerEvent -= OnRightButton;
            _rightButton.OnPointerUpEvent -= OnRightButtonUp;
            
            return base.OnCloseAsync();
        }
        
        public void SetLeftButtonPressedVisual(bool isPressed)
        {
            Color color = isPressed ? _pressedColor : _unpressedColor;
            _leftButtonTween?.Kill();
            _leftButtonTween = _leftButtonImage.DOColor(color, 0.1f);
            _leftButtonTween.onComplete = () => _leftButtonTween = null;
        }

        public void SetRightButtonPressedVisual(bool isPressed)
        {
            Color color = isPressed ? _pressedColor : _unpressedColor;
            _rightButtonTween?.Kill();
            _rightButtonTween = _rightButtonImage.DOColor(color, 0.1f);
            _rightButtonTween.onComplete = () => _rightButtonTween = null;
        }
        
        private void SwipeHandler(float angle)
        {
            OnSwipe?.Invoke(angle);
        }
        
        private void OnRightButton() => RightButton?.Invoke();
        private void OnLeftButton() => LeftButton?.Invoke();
        private void OnRightButtonUp() => RightButtonUp?.Invoke();
        private void OnLeftButtonUp() => LeftButtonUp?.Invoke();

        private void FadeButton(Image image, bool isPressed)
        {
            Color target = isPressed ? _pressedColor : _unpressedColor;

            float t = 1f - Mathf.Exp(-_fadeSpeed * Time.deltaTime);
            image.color = Color.Lerp(image.color, target, t);
        }
    }
}