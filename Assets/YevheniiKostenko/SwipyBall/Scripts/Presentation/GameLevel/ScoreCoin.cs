using System;
using UnityEngine;
using YevheniiKostenko.CoreKit.Animation;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class ScoreCoin : CollectableItem, ICollectable
    {
        [SerializeField]
        private UniversalAnimator _animator;
        public Action<ICollectable> Collected { get; set; }

        public void Collect()
        {
            Collected?.Invoke(this);
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