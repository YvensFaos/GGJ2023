using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RootController : MonoBehaviour
    {
        [SerializeField] private RootController rootParent;
        [SerializeField] private List<RootPoint> rootPoints;

        public RootController RootParent
        {
            get => rootParent;
            set => rootParent = value;
        }

        public void RootContactWithOther()
        {
            if (RootParent != null)
            {
                RootParent.RootContactWithOther();
            }
            else
            {
                Destroy(gameObject);
            }
            rootPoints.ForEach(rootPoint => rootPoint.DestroyRoot());
        }
    }
}
