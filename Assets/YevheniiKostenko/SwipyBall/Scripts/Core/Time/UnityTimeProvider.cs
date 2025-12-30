using System.Collections.Generic;
using UnityEngine;
using YevheniiKostenko.CoreKit.Utils;

namespace YevheniiKostenko.SwipyBall.Core.Time
{
    public class UnityTimeProvider : SingletonMonoBehaviour<UnityTimeProvider>, ITImeProvider
    {
        private readonly List<ITimeListener> _timeListeners = new();

        public void RegisterTimeListener(ITimeListener listener)
        {
            if (listener == null || _timeListeners.Contains(listener))
                return;

            _timeListeners.Add(listener);
        }

        public void ClearTimeListener(ITimeListener listener)
        {
            if (listener == null)
                return;

            _timeListeners.Remove(listener);
        }

        private void Update()
        {
            float deltaTime = UnityEngine.Time.deltaTime;
            for (int i = 0; i < _timeListeners.Count; i++)
            {
                if(_timeListeners[i] == null)
                    continue;
                
                _timeListeners[i].Update(deltaTime);
            }
        }
    }
}