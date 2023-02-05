using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelNameText;
        [SerializeField] private TextMeshProUGUI rootPowerText;
        [SerializeField] private TextMeshProUGUI growthText;
        [SerializeField] private GameObject ornament;

        [SerializeField] private GameLevel gameLevel;
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private Button selfButton;
        
        private bool _unlocked;
        private int _growthRecord;
        private int _rootPowerRecord;

        public void Initialize(GameLevel level, LevelRecord levelRecord, SceneLoader loader)
        {
            gameLevel = level;
            levelNameText.text = level.GetCompleteLevelName;
            
            _rootPowerRecord = levelRecord.maxRootPowerCollected;
            rootPowerText.text = $"{_rootPowerRecord}";
            
            _growthRecord = levelRecord.maxGrowth;
            growthText.text = $"{_growthRecord}";
            
            sceneLoader = loader;
            _unlocked = levelRecord.unlocked;

            ornament.SetActive(level.IsMaxGrowth(_growthRecord) && level.HasMaxRootPower(_rootPowerRecord));
            selfButton.interactable = levelRecord.unlocked;
        }

        private void OnEnable()
        {
            if (gameLevel != null)
            {
                ornament.SetActive(gameLevel.IsMaxGrowth(_growthRecord) && gameLevel.HasMaxRootPower(_rootPowerRecord));    
            }
        }

        public void LoadLevel()
        {
            if (!_unlocked) return;
            
            sceneLoader.GoToLevel(gameLevel);
        }
    }
}
