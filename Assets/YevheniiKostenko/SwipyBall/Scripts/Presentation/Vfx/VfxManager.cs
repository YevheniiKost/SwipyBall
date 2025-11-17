using System.Collections.Generic;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.Vfx
{
    public class VfxManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _vfxRoot;

        [SerializeField]
        private List<VfxEntry> _vfxEntries;

        private Dictionary<VfxType, Queue<GameObject>> _pools;
        private Dictionary<VfxType, GameObject> _prefabs;
        
        public static VfxManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            InitPools();
        }

        private void InitPools()
        {
            _pools = new Dictionary<VfxType, Queue<GameObject>>();
            _prefabs = new Dictionary<VfxType, GameObject>();

            foreach (var entry in _vfxEntries)
            {
                _prefabs[entry.Type] = entry.Prefab;
                var queue = new Queue<GameObject>();

                for (int i = 0; i < entry.PreloadedCount; i++)
                {
                    var obj = Instantiate(entry.Prefab, _vfxRoot);
                    obj.SetActive(false);
                    queue.Enqueue(obj);
                }

                _pools[entry.Type] = queue;
            }
        }

        public void Play(VfxType type, Vector3 position, Quaternion? rotation = null)
        {
            if (!_pools.TryGetValue(type, out var queue))
            {
                Debug.LogWarning($"No VFX pool for type {type}");
                return;
            }

            GameObject instance;

            if (queue.Count > 0)
            {
                instance = queue.Dequeue();
            }
            else
            {
                instance = Instantiate(_prefabs[type], _vfxRoot);
            }

            instance.transform.position = position;
            instance.transform.rotation = rotation ?? Quaternion.identity;
            instance.SetActive(true);
        }

        public void ReturnToPool(VfxType type, GameObject obj)
        {
            obj.SetActive(false);
            _pools[type].Enqueue(obj);
        }
    }
}