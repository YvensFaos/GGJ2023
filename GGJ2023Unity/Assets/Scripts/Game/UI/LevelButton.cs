using TMPro;
using UnityEngine;
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

        private bool _unlocked;

        public void Initialize(GameLevel level, LevelRecord levelRecord, SceneLoader loader)
        {
            gameLevel = level;
            levelNameText.text = level.GetCompleteLevelName;
            var growthRecord = levelRecord.maxRootPowerCollected;
            rootPowerText.text = $"{growthRecord}";
            var rootPowerRecord = levelRecord.maxGrowth; 
            growthText.text = $"{rootPowerRecord}";
            sceneLoader = loader;
            _unlocked = levelRecord.unlocked;

            ornament.SetActive(level.IsMaxGrowth(growthRecord) && level.HasMaxRootPower(rootPowerRecord));
        }

        public void LoadLevel()
        {
            if (!_unlocked) return;
            
            sceneLoader.GoToLevel(gameLevel);
        }
    }
}
