using Unity.Cinemachine;
using UnityEngine;
using YevheniiKostenko.CoreKit.Scripts.Utils;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class TargetCamera : SingletonMonoBehaviour<TargetCamera>
    {
        [SerializeField]
        private CinemachineCamera _cinemachineCamera;
        
        public void SetCameraTarget(Transform target)
        {
            if (target == null)
            {
                Debug.LogError("Target cannot be null");
                return;
            }

            _cinemachineCamera.Target = new CameraTarget { TrackingTarget = target };
        }
        
        public void ResetCameraTarget()
        {
            _cinemachineCamera.Target = default;
        }
    }
}