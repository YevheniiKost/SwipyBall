using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public abstract class CollectableItem : MonoBehaviour
    {
        [SerializeField]
        private Transform _content;
        
        public Transform Content => _content;

        public void SetContentActive(bool isActive)
        {
            _content.gameObject.SetActive(isActive);
        }
    }
}