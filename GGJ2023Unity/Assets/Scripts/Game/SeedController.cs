using Cinemachine;
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

        [Header("Cinemachine")] 
        [SerializeField] private CinemachineVirtualCamera vCamera;
        [SerializeField] private CinemachineTargetGroup vTargetGroup;
        [SerializeField] private float weight;
        [SerializeField] private float radius;

        private void Awake()
        {
            numberRootSeedCanvas = rootSeedCanvas.GetRootPanelCount();
            availableRootPower = Mathf.Clamp(availableRootPower, 0, numberRootSeedCanvas);

            rootSeedCanvas.Initialize(availableRootPower);
        }

        public void AddToGroupComposer(GameObject newGameObject)
        {
            vTargetGroup.AddMember(newGameObject.transform, weight, radius);
        }

        public void RemoveFromGroupComposer(GameObject removeGameObject)
        {
            vTargetGroup.RemoveMember(removeGameObject.transform);
        }

        public void ResetGroupComposer()
        {
            foreach (var mTarget in vTargetGroup.m_Targets)
            {
                vTargetGroup.RemoveMember(mTarget.target);
            }

            vTargetGroup.AddMember(transform, weight, radius);
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
