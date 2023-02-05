using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public struct LevelRecord : IComparable
    {
        [SerializeField] 
        public int levelNumber;
        [SerializeField]
        public string levelName;
        [SerializeField]
        public int maxGrowth;
        [SerializeField]
        public int maxRootPowerCollected;
        [SerializeField]
        public bool unlocked;

        public int CompareTo(object obj)
        {
            if (obj is LevelRecord record)
            {
                return levelNumber.CompareTo(record.levelNumber);
            }

            return 0;
        }
    }
}