using UnityEngine;

using YeKostenko.AudioEngine;
using YeKostenko.CoreKit.DI;
using YevheniiKostenko.SwipyBall.Domain.Game;
using YevheniiKostenko.SwipyBall.Presentation.Vfx;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class BombView : MonoBehaviour, IDamageSourceView, IActivatableView
    {
        [SerializeField]
        private BombAnimator _animator;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Sprite _idleSprite;

        [SerializeField]
        private Sprite _fusedSprite;

        [SerializeField]
        private SoundComponent2D _explosionSound;
        [SerializeField]
        private SoundComponent2D _tickSound;

        private IBombModel _model;
        private PlayHandle _tickHandle;

        public IDamageSource DamageSource => _model;

        [Inject]
        public void Construct(IDamageSourceFactory damageSourceFactory)
        {
            _model = damageSourceFactory.CreateBomb();
            _model.Initialize();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<IPlayerView>(out var player))
            {
                if (_model.RegisterHit())
                {
                    StartCountDown();
                }
            }
        }

        private void OnDestroy()
        {
            _model.Dispose();
            _model = null;
        }

        private void StartCountDown()
        {
            _tickHandle = _tickSound.Play();
            _model.Exploded += OnExploded;
            _spriteRenderer.sprite = _fusedSprite;
        }

        private void OnExploded()
        {
            VfxManager.Instance.Play(VfxType.BombExplosion, transform.position);
            _animator.PlayExplosion(_model.ExplosionRadius, Deactivate);
            _tickHandle.Stop();
            _explosionSound.Play();
            _model.Exploded -= OnExploded;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _model.ExplosionRadius);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<IPlayerView>(out var player))
                {
                    Vector2 hitDirection = (player.Transform.position - transform.position).normalized;
                    player.Hit(_model.Damage, hitDirection);
                }
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _spriteRenderer.sprite = _idleSprite;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}