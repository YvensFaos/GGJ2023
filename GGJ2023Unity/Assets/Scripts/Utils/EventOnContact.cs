using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EventOnContact : MonoBehaviour
{
    [SerializeField]
    private UnityEvent callEvent;
    
    [Header("Call On Contact with Objects with Tag")]
    [SerializeField]
    private bool callOnTag;
    [SerializeField]
    private List<string> tagsToCall;
    
    [Header("Call On Contact with Objects from Specific Layers")]
    [SerializeField]
    private bool callOnLayer;
    [SerializeField]
    private LayerMask layerToCall;

    private void OnTriggerEnter(Collider other)
    {
        SolveContact(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        SolveContact(other.gameObject);
    }

    private void SolveContact(GameObject other)
    {
        if (callOnTag)
        {
            var findIndex = tagsToCall.FindIndex(other.CompareTag);
            if (findIndex != -1)
            {
                callEvent.Invoke();
            }
        }

        if (callOnLayer)
        {
            if (((1 << other.layer) & layerToCall) != 0)
            {
                callEvent.Invoke();
            }
        }
    }
}
