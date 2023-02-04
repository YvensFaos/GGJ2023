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
        private GameLevel nextLevel;
        
        public int LevelNumber => levelNumber;
        public string LevelName => levelName;
        public string SceneName => sceneName;
        public bool LastLevel => lastLevel;
        public GameLevel NextLevel => nextLevel;
    }
}