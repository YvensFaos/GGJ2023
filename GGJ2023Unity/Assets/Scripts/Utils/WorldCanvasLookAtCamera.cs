using UnityEngine;

namespace Utils
{
    public class WorldCanvasLookAtCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera worldCanvasCamera;
    
        private void Start()
        {
            if (worldCanvasCamera == null)
            {
                worldCanvasCamera = Camera.main;
            }
        }

        private void LateUpdate()
        {
            var rotation = worldCanvasCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }
}
