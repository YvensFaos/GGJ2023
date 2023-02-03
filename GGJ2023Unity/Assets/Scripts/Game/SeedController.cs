using Game.UI;
using UnityEngine;

namespace Game
{
    public class SeedController : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private RootPoint mainRootPoint;

        [SerializeField] private int numberRootSeedCanvas;
        [SerializeField] private int availableRootPower;

        [Header("References")]
        [SerializeField] private RootSeedCanvas rootSeedCanvas;

        private void Awake()
        {
            numberRootSeedCanvas = rootSeedCanvas.GetRootPanelCount();
            availableRootPower = Mathf.Clamp(availableRootPower, 0, numberRootSeedCanvas);

            rootSeedCanvas.Initialize(availableRootPower);
        }

        public bool TryToUseRootPower()
        {
            if (availableRootPower <= 0) return false;
            availableRootPower--;
            rootSeedCanvas.ResetRootPanelsToMatchRootPower(availableRootPower);
            return true;

        }
    }
}
