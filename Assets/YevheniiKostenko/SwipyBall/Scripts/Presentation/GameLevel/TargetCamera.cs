using Unity.Cinemachine;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Scripts.Presentation.GameLevel
{
    public class TargetCamera : MonoBehaviour
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