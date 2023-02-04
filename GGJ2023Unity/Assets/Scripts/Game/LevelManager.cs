using Game.UI;
using UnityEngine;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameLevel level;

        [SerializeField] 
        private IntroCanvasController introCanvasController;
        
        public void Start()
        {
            introCanvasController.SetLevelNumberAndName(level);
            introCanvasController.gameObject.SetActive(true);
        }
    }
}
