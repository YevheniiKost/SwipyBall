using System;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class ExitPortal : MonoBehaviour
    {
        public event Action OnExitPortalEntered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerView player))
            {
                OnExitPortalEntered?.Invoke();
            }
        }
    }
}