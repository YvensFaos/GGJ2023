using UnityEngine;

namespace Game
{
    public class RootPoint : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        private RootController root;

        [Header("References")]
        [SerializeField]
        private RootsDatabase rootsDatabase;

        public void GrowRoot()
        {
            if (root != null) return;
            var newRoot = rootsDatabase.GetRoot();
            var selfTransform = transform;
            root = Instantiate(newRoot, selfTransform.position, selfTransform.rotation);
        }
    }
}
