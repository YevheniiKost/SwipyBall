using System;
using UnityEngine;
using YeKostenko.CoreKit.DI;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class SpikeView : MonoBehaviour, IDamageSourceView
    {
        private ISpikeModel _model;
        
        public IDamageSource DamageSource  => _model;

        [Inject]
        public void Construct(IDamageSourceFactory damageSourceFactory)
        {
            _model = damageSourceFactory.CreateSpike();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IPlayerView>(out var player) && _model.CanHit)
            {
                player.Hit(_model.Damage);
                _model.RegisterHit();
            }
        }
    }
}