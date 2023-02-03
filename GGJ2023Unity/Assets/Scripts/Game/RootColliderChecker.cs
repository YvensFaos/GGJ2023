using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class RootColliderChecker : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider boxCollider;

        [SerializeField] 
        private LayerMask checkLayers;

        private void Awake()
        {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }
        }

        void Start()
        {
            var selfCollider = boxCollider.transform;
            var isIntersecting = Physics.CheckBox(
                selfCollider.position,
                boxCollider.size * 0.5f,
                selfCollider.rotation,
                checkLayers,
                QueryTriggerInteraction.UseGlobal
            );

            Debug.Log(isIntersecting
                ? "Box collider is intersecting with another collider."
                : "Box collider is not intersecting with another collider.");
        }
    }
}
