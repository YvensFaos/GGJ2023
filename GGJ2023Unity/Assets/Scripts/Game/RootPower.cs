using UnityEngine;

namespace Game
{
    public class RootPower : MonoBehaviour
    {
        [SerializeField]
        private int rootPower;

        public int Power => rootPower;
    }
}
