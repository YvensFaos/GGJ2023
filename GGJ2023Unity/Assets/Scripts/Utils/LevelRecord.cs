using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public struct LevelRecord
    {
        [SerializeField]
        public string levelName;
        [SerializeField]
        public int maxGrowth;
        [SerializeField]
        public int maxRootPowerCollected;
        [SerializeField]
        public bool unlocked;
    }
}