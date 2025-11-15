using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.Input;
using YeKostenko.CoreKit.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class InputPanelView : UIWindow, IInputPanelView
    {
        [SerializeField]
        private SwipeMultiDirectionalComponent _swipeDetector;
        
        private IInputPanelPresenter _inputPanelPresenter;
        
        public event Action<float> OnSwipe;

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
    }
}