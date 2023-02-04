using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class IntroCanvasController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI levelNumberText;
        [SerializeField]
        private TextMeshProUGUI levelNameText;
        
        public void SetLevelNumberAndName(GameLevel level)
        {
            levelNumberText.text = level.LevelNumber.ToString();
            levelNameText.text = level.LevelName;
        }
    }
}