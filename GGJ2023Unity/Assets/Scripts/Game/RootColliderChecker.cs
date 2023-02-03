using System.Collections;
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

        private void Start()
        {
            StartCoroutine(ColliderCheck());
        }
        
        private IEnumerator ColliderCheck()
        {
            //Wait for one turn
            yield return null;
            
            //Test
            boxCollider.enabled = false;
            var selfCollider = boxCollider.transform;
            var isIntersecting = Physics.CheckBox(
                boxCollider.center + selfCollider.position,
                boxCollider.size * 0.5f,
                selfCollider.rotation,
                checkLayers,
                QueryTriggerInteraction.UseGlobal
            );
            boxCollider.enabled = true;

            Debug.Log(isIntersecting
                ? $"Box collider is intersecting with another collider. {selfCollider.rotation.eulerAngles}"
                : "NO.");
        }

        private void OnDrawGizmos()
        {
            if (boxCollider == null) return;
            var selfTransform = boxCollider.transform;
            var selfTransformPosition = selfTransform.position;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(boxCollider.center + selfTransformPosition, boxCollider.size.x * 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(boxCollider.center + selfTransformPosition, boxCollider.size.y * 0.5f);
        }
    }
}
