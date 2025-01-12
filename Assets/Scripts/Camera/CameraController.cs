using UnityEngine;

namespace ArmorVehicle
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraData[] _camerasData;
        
        public void Initialize(Transform target)
        {
            foreach (var cameraData in _camerasData)
            {
                cameraData.Camera.Follow = target;
            }
            
            Switch(CameraType.Idle);
        }

        public void Switch(CameraType type)
        {
            foreach (var cameraData in _camerasData)
            {
                cameraData.Camera.enabled = cameraData.Type == type;
            }
        }
    }
}