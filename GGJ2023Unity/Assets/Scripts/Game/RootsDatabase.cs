using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Game
{
    [CreateAssetMenu(fileName = "Roots Database", menuName = "Roots/Roots Database", order = 0)]
    public class RootsDatabase : ScriptableObject
    {
        [SerializeField]
        private List<RootController> possibleRoots;

        public RootController GetRoot()
        {
            return RandomHelper<RootController>.GetRandomFromList(possibleRoots);
        }
    }
}