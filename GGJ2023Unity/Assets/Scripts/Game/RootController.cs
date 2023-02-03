using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RootController : MonoBehaviour
    {
        [SerializeField] 
        private SeedController seed;
        [SerializeField] 
        private RootController rootParent;
        [SerializeField] 
        private List<RootPoint> rootPoints;

        public RootController RootParent
        {
            get => rootParent;
            set => rootParent = value;
        }

        public void Initialize(SeedController seedController)
        {
            seed = seedController;
            rootPoints.ForEach(rootPoint => rootPoint.Seed = seed);
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
