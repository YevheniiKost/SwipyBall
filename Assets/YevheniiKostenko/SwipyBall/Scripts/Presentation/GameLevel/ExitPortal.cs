using System;
using UnityEngine;

using YeKostenko.AudioEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class ExitPortal : MonoBehaviour
    {
        [SerializeField]
        private SoundComponent2D _enterSound;

        public event Action OnEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerView player))
            {
                OnEnter?.Invoke();
                _enterSound.Play();
            }
        }
    }
}