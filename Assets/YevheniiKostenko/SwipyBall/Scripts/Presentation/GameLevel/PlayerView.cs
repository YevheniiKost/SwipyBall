using System;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private LayerMask _groundMask;
        
        private IPlayerController _playerController;
        
        public event Action<int> OnHit
        {
            add => _playerController.OnHit += value; 
            remove => _playerController.OnHit -= value;
        }
        
        public Transform Transform => transform;
        
        public void Init(IPlayerController playerController) 
        {
            _playerController = playerController;
            _playerController.Initialize();
        }
        
        public void Jump(Vector2 direction)
        {
            _rigidbody.linearVelocityY = 0; // Reset vertical velocity before applying jump force
            _rigidbody.linearVelocityX = 0;
            _rigidbody.AddForce(direction, ForceMode2D.Impulse);
            
            DrawJumpVector(direction);
        }
        
        public void Push(Vector2 direction)
        {
            _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }

        public void Hit(int damage)
        {
            _playerController.RegisterHit(damage);
        }

        public bool IsGrounded(float groundCheckDistance)
        {
            Debug.DrawLine(_rigidbody.position, _rigidbody.position + Vector2.down * groundCheckDistance, Color.green, 0.01f);
            return Physics2D.Raycast(_rigidbody.position, Vector2.down, groundCheckDistance, _groundMask);
        }

        public void Destroy()
        {
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