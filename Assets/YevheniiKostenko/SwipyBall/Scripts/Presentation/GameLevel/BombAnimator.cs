using System;
using DG.Tweening;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Presentation.Animation;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class BombAnimator : AnimationComponent
    {
        [SerializeField]
        private GameObject _explosionObject;

        [SerializeField]
        private float _explosionAnimationDuration = 0.1f;

        public void PlayExplosion(float radius, Action onComplete = null)
        {
            _explosionObject.SetActive(true);
            _explosionObject.transform.localScale = Vector3.zero;

            _explosionObject.transform.DOScale(Vector3.one * radius * 2, _explosionAnimationDuration)
                .SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    _explosionObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }
    }
}