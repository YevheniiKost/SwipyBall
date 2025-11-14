using System;
using System.Collections.Generic;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation
{
    public class SpriteElement : MonoBehaviour
    {
        [SerializeField]
        private int _layer;
        [SerializeField]
        private List<SpriteRenderer> _renderers;

        private void OnValidate()
        {
            if (_renderers == null || _renderers.Count == 0)
            {
                _renderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
            }

            foreach (SpriteRenderer renderer in _renderers)
            {
                renderer.sortingOrder = _layer;
            }
        }
    }
}