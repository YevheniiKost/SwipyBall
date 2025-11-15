using UnityEngine;
using YeKostenko.CoreKit.DI;
using YevheniiKostenko.CoreKit.Animation;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class ScoreCoinView : CollectableItem, ICollectableView
    {
        [SerializeField]
        private UniversalAnimator _animator;

        public ICollectable Collectable => _coinModel;

        private ICollectable _coinModel;

        [Inject]
        private void Construct(ICollectableFactory collectableFactory)
        {
            _coinModel = collectableFactory.CreateCoin();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _animator.PlayShow();
            _animator.PlayIdle();
        }

        public void Deactivate()
        {
            _animator.PlayHide();
        }
    }
}