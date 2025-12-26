using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.Vfx
{
    [System.Serializable]
    public class VfxEntry
    {
        [SerializeField]
        private VfxType _type;
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private int _preloadedCount = 5;
        
        public VfxType Type => _type;
        public GameObject Prefab => _prefab;
        public int PreloadedCount => _preloadedCount;
    }
}