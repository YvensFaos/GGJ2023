using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class RootSeedCanvas : MonoBehaviour
    {
        [SerializeField]
        private List<RootPanel> rootPanels;

        public void Initialize(int initialRootPower)
        {
            ResetRootPanelsToMatchRootPower(initialRootPower);
        }

        private void ResetRootPanelsToMatchRootPower(int rootPower)
        {
            rootPanels.ForEach(rootPanel => rootPanel.ToggleRootPowerGo(false));
            rootPanels.Sort();
            for (var i = 0; i < rootPower; i++)
            {
                rootPanels[i].ToggleRootPowerGo(true);
            }
        }

        public int GetRootPanelCount() => rootPanels.Count;
    }
}
