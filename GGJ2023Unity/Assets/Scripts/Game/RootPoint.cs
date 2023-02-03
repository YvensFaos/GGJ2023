using UnityEngine;

namespace Game
{
    public class RootPoint : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] 
        private SeedController seed;
        [SerializeField] 
        private RootController rootParent;
        [SerializeField]
        private RootController spawnRoot;

        [Header("References")]
        [SerializeField]
        private RootsDatabase rootsDatabase;

        private bool _markedForDeath;

        public SeedController Seed
        {
            get => seed;
            set => seed = value;
        }

        public void GrowRoot()
        {
            if (!Seed.TryToUseRootPower()) return;
            
            if (spawnRoot != null) return;
            var newRoot = rootsDatabase.GetRoot();
            var selfTransform = transform;
            spawnRoot = Instantiate(newRoot, selfTransform.position, selfTransform.rotation);
            spawnRoot.Initialize(seed);
            if (rootParent != null)
            {
                spawnRoot.RootParent = rootParent;
            }
        }

        public void DestroyRoot()
        {
            if (_markedForDeath) return;
            
            _markedForDeath = true;
            if (spawnRoot != null)
            {
                spawnRoot.RootContactWithOther();
                Destroy(spawnRoot.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
