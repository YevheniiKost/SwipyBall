using System.Collections;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.Vfx
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PooledVfx : MonoBehaviour
    {
        [SerializeField] private VfxType type;
        private ParticleSystem _ps;

        private void Awake()
        {
            _ps = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            _ps.Clear();
            _ps.Play();
            StartCoroutine(ReturnAfterLifetime());
        }

        private IEnumerator ReturnAfterLifetime()
        {
            yield return new WaitForSeconds(Mathf.Max(_ps.main.duration, _ps.main.startLifetime.constantMax));
            VfxManager.Instance.ReturnToPool(type, gameObject);
        }
    }
}