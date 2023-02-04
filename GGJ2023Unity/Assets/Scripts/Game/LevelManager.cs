using Game.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameLevel level;
        [SerializeField]
        private int seeds;
        [SerializeField] 
        private IntroCanvasController introCanvasController;
        [SerializeField] 
        private UnityEvent finishLevelEvents;
        [SerializeField] 
        private GameObject nextLevelCanvas;
        [SerializeField] 
        private SceneLoader sceneLoader;

        private int _completedSeeds;
        
        public void Start()
        {
            introCanvasController.SetLevelNumberAndName(level);
            introCanvasController.gameObject.SetActive(true);
            _completedSeeds = 0;
            if (nextLevelCanvas == null) return;
            nextLevelCanvas.SetActive(false);
        }

        public void NotifySeedGrown()
        {
            _completedSeeds++;
            if (_completedSeeds < seeds) return;
            finishLevelEvents?.Invoke();
            if (nextLevelCanvas == null) return;
            nextLevelCanvas.SetActive(true);
            GameManager.Instance.UnlockLevel(level);
        }

        public void GoToNextLevel()
        {
            if (level.LastLevel)
            {
                sceneLoader.GoToScene("MainMenu");
            }
            else
            {
                sceneLoader.GoToLevel(level.NextLevel);
            }
        }
    }
}
