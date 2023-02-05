using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    [CreateAssetMenu(fileName = "New Game Level", menuName = "Roots/Game Level", order = 0)]
    public class GameLevel : ScriptableObject
    {
        [SerializeField]
        private int levelNumber;
        [SerializeField]
        private string levelName;
        [SerializeField] 
        private string sceneName;
        [SerializeField] 
        private bool lastLevel;
        [SerializeField] 
        private bool firstLevel;
        [SerializeField] 
        private GameLevel nextLevel;
        [SerializeField] 
        private int maxGrowth;
        [SerializeField] 
        private int maxRootPower;
        
        public int LevelNumber => levelNumber;
        public string LevelName => levelName;
        public string SceneName => sceneName;
        public bool LastLevel => lastLevel;
        public bool FirstLevel => firstLevel;
        public GameLevel NextLevel => nextLevel;

        public string GetCompleteLevelName => $"{LevelNumber} {LevelName}";
        public bool IsMaxGrowth(int growth) => growth >= maxGrowth;
        public bool HasMaxRootPower(int rootPower) => rootPower >= maxRootPower;
    }
}