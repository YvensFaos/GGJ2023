using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneLoader : MonoBehaviour
    {
        public void GoToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void GoToLevel(GameLevel level)
        {
            GoToScene(level.SceneName);
        }
        
        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
