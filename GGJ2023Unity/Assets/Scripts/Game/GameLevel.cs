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

        public int LevelNumber => levelNumber;

        public string LevelName => levelName;
    }
}