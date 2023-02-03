using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class RootColliderChecker : MonoBehaviour
    {
        [SerializeField] 
        private RootController root;
        [SerializeField] 
        private LayerMask checkLayers;

        private void OnTriggerStay(Collider other)
        {
            if (((1 << other.gameObject.layer) & checkLayers) != 0)
            {
                  root.RootContactWithOther();
            }
        }
    }
}
