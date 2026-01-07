using System;
using UnityEngine;
using YellowTape.AudioEngine;
using YevheniiKostenko.SwipyBall.Presentation.Vfx;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private LayerMask _groundMask;
        [SerializeField]
        private PlayerAnimator _animator;

        [SerializeField]
        private MultiSoundComponent _multiSoundComponent;
        
        private IPlayerController _playerController;
        private Vector2 _requestedMoveForce;
        
        public Transform Transform => transform;
        
        public void Init(IPlayerController playerController) 
        {
            _playerController = playerController;
            _playerController.Initialize();
        }
        
        public void Jump(Vector2 direction)
        {
            _rigidbody.linearVelocityY = 0; // Reset vertical velocity before applying jump force
            _rigidbody.AddForce(direction, ForceMode2D.Impulse);
            
            DrawJumpVector(direction);
            _multiSoundComponent.PlayByIndex(0);
        }
        
        public void Push(Vector2 direction)
        {
            _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }

        public void Move(Vector2 direction)
        {
            _requestedMoveForce = direction;
        }

        public void Hit(int damage, Vector2 hitDirection)
        {
            _playerController.RegisterHit(damage, hitDirection);
        }

        public bool IsGrounded(float groundCheckDistance)
        {
            Debug.DrawLine(_rigidbody.position, _rigidbody.position + Vector2.down * groundCheckDistance, Color.green, 0.01f);
            return Physics2D.Raycast(_rigidbody.position, Vector2.down, groundCheckDistance, _groundMask);
        }

        public void ShowDamageEffect()
        {
            _animator.PlayDamageAnimation();
        }

        public void ShowLandedEffect()
        {
            VfxManager.Instance.Play(VfxType.Landing, GetLandingPosition());
        }

        private Vector3 GetLandingPosition()
        {
            RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, 10f, _groundMask);
            if (hit.collider != null)
            {
                return hit.point;
            }
            return _rigidbody.position;
        }

        public void Destroy()
        {
            VfxManager.Instance.Play(VfxType.PlayerDeath, this.transform.position);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ICollectableView>(out var view))
            {
                _playerController?.InteractWithCollectable(view.Collectable);
            }
        }
        
        private void Update()
        {
            _playerController?.Tick(Time.deltaTime);
        }
        
        private void FixedUpdate()
        {
            if (_requestedMoveForce != Vector2.zero)
            {
                _rigidbody.AddForce(_requestedMoveForce, ForceMode2D.Force);
                _rigidbody.linearVelocityX = Mathf.Clamp(_rigidbody.linearVelocityX, -10, 10);
                _requestedMoveForce = Vector2.zero;
            }
        }

        private void OnDestroy()
        {
            _playerController?.Dispose();
            _playerController = null;
        }

        private void DrawJumpVector(Vector2 forceDirection)
        {
            Debug.DrawLine(_rigidbody.position, _rigidbody.position + forceDirection, Color.red, 0.5f);
        }
    }
}