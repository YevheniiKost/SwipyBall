using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.Input;
using YeKostenko.CoreKit.UI;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.UI
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
            _swipeDetector.OnSwipe += direction =>
            {
                OnSwipe?.Invoke(direction);
            };
            
            _inputPanelPresenter = presenter;
            _inputPanelPresenter.AttachView(this);
        }

        public override UniTask OnCreateAsync(IUIContext context)
        {
            return base.OnCreateAsync(context);
        }

        public override UniTask OnOpenAsync()
        {
            return base.OnOpenAsync();
        }

        public override UniTask OnCloseAsync()
        {
            if (_inputPanelPresenter != null)
            {
                _inputPanelPresenter.DetachView();
                _inputPanelPresenter = null;
            }
            
            return base.OnCloseAsync();
        }
    }
}