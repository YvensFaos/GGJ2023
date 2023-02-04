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
                if (!other.TryGetComponent<RootPower>(out var rootPower)) return;
                root.CollectRootPower(rootPower);
                Destroy(other.gameObject);
            }
        }
    }
}
