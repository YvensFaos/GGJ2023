using System;
using UnityEngine;

namespace Game.UI
{
    public class RootPanel : MonoBehaviour, IComparable
    {
        [SerializeField]
        private GameObject rootPowerGo;

        public void ToggleRootPowerGo(bool toggle) => rootPowerGo.SetActive(toggle);
        
        public int CompareTo(object obj)
        {
            var otherGo = obj as GameObject;
            return otherGo == null ? 0 : transform.position.x.CompareTo(otherGo.transform.position.x);
        }
    }
}
