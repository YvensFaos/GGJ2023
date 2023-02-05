using UnityEngine;
using Utils;

namespace Game.UI
{
    public class ContinuePanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject continueParent;
        [SerializeField]
        private SceneLoader loader;
        [SerializeField]
        private LevelButton levelButtonPrefab;
        
        private void Start()
        {
            TransformUtils.ClearObjects(continueParent.transform);

            var levelRecords = GameManager.Instance.LevelRecords;
            levelRecords.ForEach(record =>
            {
                var gameLevel = GameManager.Instance.GetLevelFromRecord(record);
                if (gameLevel == null) return;
                var newLevelButton = Instantiate(levelButtonPrefab, continueParent.transform);
                newLevelButton.Initialize(gameLevel, record, loader);
            });
        }
    }
}