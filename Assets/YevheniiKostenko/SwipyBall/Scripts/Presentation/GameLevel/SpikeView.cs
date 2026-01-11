using UnityEngine;
using YeKostenko.CoreKit.DI;
using YellowTape.AudioEngine;
using YevheniiKostenko.SwipyBall.Domain.Game;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class SpikeView : MonoBehaviour, IDamageSourceView
    {
        [SerializeField]
        private SoundComponent _hitSound;
        
        private ISpikeModel _model;
        
        public IDamageSource DamageSource  => _model;

        [Inject]
        public void Construct(IDamageSourceFactory damageSourceFactory)
        {
            _model = damageSourceFactory.CreateSpike();
            _model.Initialize();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<IPlayerView>(out var player) && _model.CanHit)
            {
                Vector2 hitDirection = (transform.position - player.Transform.position).normalized;
                player.Hit(_model.Damage, hitDirection);
                _model.RegisterHit();
                _hitSound.Play();
            }
        }
        
        private void OnDestroy()
        {
            _model.Dispose();
        }
    }
}