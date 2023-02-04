using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class RootColliderChecker : MonoBehaviour
    {
        [SerializeField] 
        private RootController root;
        [SerializeField] 
        private LayerMask damageLayers;
        [SerializeField] 
        private LayerMask rootPowerLayers;

        private void OnTriggerStay(Collider other)
        {
            var layer = (1 << other.gameObject.layer);
            if ((layer & damageLayers) != 0)
            {
                  root.RootContactWithOther();
            }
            if ((layer & rootPowerLayers) != 0)
            {
                Destroy(other.gameObject);
                root.CollectRootPower();
            }
        }
    }
}
