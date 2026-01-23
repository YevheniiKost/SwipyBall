using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Presentation.Animation;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class PlayerAnimator : AnimationComponent
    {
        [Serializable]
        private class PlayerAnimationsParams
        {
            public Color DamageColor;
            public float DamageDuration;
        }

        [SerializeField]
        private SpriteRenderer[] _playerSprites;

        [SerializeField]
        private PlayerAnimationsParams _params;

        private readonly List<Tween> _activeTweens = new List<Tween>();

        public void PlayDamageAnimation()
        {
            Sequence sequence = DOTween.Sequence();

            foreach (SpriteRenderer sprite in _playerSprites)
            {
                Color initialColor = sprite.color;
                Tween changeToDamageColor = sprite.DOColor(_params.DamageColor, _params.DamageDuration);
                Tween changeToInitialColor = sprite.DOColor(initialColor, _params.DamageDuration);
                Sequence sequencePart = DOTween.Sequence()
                    .Append(changeToDamageColor)
                    .AppendInterval(_params.DamageDuration)
                    .Append(changeToInitialColor)
                    .SetLoops(3);

                sequence.Join(sequencePart);
            }

            _activeTweens.Add(sequence);
            sequence.AppendCallback(() =>
            {
                _activeTweens.Remove(sequence);
            });
            sequence.Play();
        }

        private void OnDestroy()
        {
            foreach (Tween tween in _activeTweens)
            {
                tween.Kill();
            }

            _activeTweens.Clear();
        }
    }
}