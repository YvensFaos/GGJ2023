using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Roots Database", menuName = "Roots/Roots Database", order = 0)]
    public class RootsDatabase : ScriptableObject
    {
        [SerializeField]
        private RootController possibleRoots;
        
        [SerializeField]
        private GameObject hoverRoot;

        public GameObject HoverRoot
        {
            get => hoverRoot;
        }

        public RootController GetRoot()
        {
            return possibleRoots;
        }
    }
}