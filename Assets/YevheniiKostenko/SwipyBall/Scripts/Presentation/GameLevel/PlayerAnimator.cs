using System;
using DG.Tweening;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Presentation.Animation;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class PlayerAnimator : AnimationComponent
    {
        [Serializable]
        class PlayerAnimationsParams
        {
            public Color DamageColor;
            public float DamageDuration;
        }
        
        [SerializeField]
        private SpriteRenderer[] _playerSprites;

        [SerializeField]
        private PlayerAnimationsParams _params;
        
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
            
            sequence.Play();
        }
    }
}