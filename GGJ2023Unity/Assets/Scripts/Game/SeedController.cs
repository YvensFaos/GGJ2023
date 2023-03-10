using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.UI;
using TMPro;
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
        [SerializeField] private GameObject seedObject;
        [SerializeField] private GameObject treePlacement;
        [SerializeField] private float minimalSize;
        [SerializeField] private float maximalSize;
        [SerializeField] private int steps;

        [Header("Audio")] 
        [SerializeField] private AudioSource seedAudioSource;
        [SerializeField] private AudioClip regulaGrowthSound;
        [SerializeField] private AudioClip fullyGrownSound;

        [Header("UI")] 
        [SerializeField] private TextMeshProUGUI counterRootPower;
        [SerializeField] private TextMeshProUGUI counterGrowth;

        private Vector3 _growthVector;
        private TweenerCore<Vector3, Vector3, VectorOptions> _scaleTween;
        private bool _fullyGrown;
        private int _rootPowerCollected;
        private int _growthMade;
        private Tweener _everShaker;
        
        public float GrowthStep { get; private set; }
        public int RootPowerCollected => _rootPowerCollected;
        public int GrowthMade => _growthMade;
        public bool IsFullyGrown => _fullyGrown;

        private void Awake()
        {
            numberRootSeedCanvas = rootSeedCanvas.GetRootPanelCount();
            availableRootPower = Mathf.Clamp(availableRootPower, 0, numberRootSeedCanvas);

            rootSeedCanvas.Initialize(availableRootPower);

            var min = Mathf.Min(minimalSize, maximalSize);
            var max = Mathf.Max(minimalSize, maximalSize);
            minimalSize = min;
            maximalSize = max;

            GrowthStep = (max - min) / steps;
            _growthVector = new Vector3(GrowthStep, GrowthStep, GrowthStep);
        }

        private void Start()
        {
            if (seedObject == null) return;
            var scale = seedObject.transform.localScale;
            seedObject.transform.localScale = Vector3.zero;
            seedObject.transform.DOScale(scale, 0.3f).OnComplete(() =>
            {
                seedObject.transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), 90.0f, 270.0f);
            });
            
            UpdateGrowthCounter();
            counterRootPower.text = $"{RootPowerCollected}";
        }

        private void UpdateGrowthCounter()
        {
            counterGrowth.text = $"{GrowthMade}/{steps}";
        }

        public void Grow()
        {
            void ScaleTweenCall()
            {
                if (GrowthMade + 1 >= steps)
                {
                    if (IsFullyGrown) return;
                    _growthMade = Mathf.Clamp(_growthMade + 1, 0, steps);
                    FullyGrown();
                }
                else
                {
                    var scale = treePlacement.transform.localScale;
                    _growthMade = Mathf.Clamp(_growthMade + 1, 0, steps);
                    _scaleTween = treePlacement.transform.DOScale(_growthVector * _growthMade, 0.6f).OnComplete(() =>
                    {
                        treePlacement.transform.DOShakeScale(0.3f, 0.1f, 2, 10);
                    });
                    PlaySeedSound(regulaGrowthSound);
                }
            }
            
            ScaleTweenCall();
            UpdateGrowthCounter();
        }

        private void FullyGrown()
        {
            _scaleTween?.Kill();
            _scaleTween = treePlacement.transform.DOScale(new Vector3(maximalSize, maximalSize, maximalSize), 0.2f).OnComplete(() =>
            {
                _everShaker?.Kill();
                _everShaker = treePlacement.transform.DOShakeScale(1.5f, 0.1f, 1, 5).SetLoops(-1);
                
                _fullyGrown = true;
                PlaySeedSound(fullyGrownSound);

                var levelManager = FindObjectOfType<LevelManager>();
                if (levelManager != null)
                {
                    levelManager.NotifySeedGrown(this);
                }
            });
        }

        public bool TryToUseRootPower()
        {
            if (!HasRootPower())
            {
                rootSeedCanvas.transform.DOShakeScale(0.5f, 0.8f).OnComplete(() =>
                {
                    rootSeedCanvas.transform.localScale = new Vector3(1, 1, 1);
                });
                return false;
            }
            availableRootPower--;
            rootSeedCanvas.ResetRootPanelsToMatchRootPower(availableRootPower);
            return true;
        }

        public bool HasRootPower() => availableRootPower > 0;

        public void CollectRootPower(RootPower rootPower)
        {
            availableRootPower = Mathf.Clamp(availableRootPower + rootPower.Power, 0, numberRootSeedCanvas);
            rootSeedCanvas.ResetRootPanelsToMatchRootPower(availableRootPower);
            _rootPowerCollected++;
            counterRootPower.text = $"{RootPowerCollected}";
            rootSeedCanvas.transform.DOShakeScale(0.4f, 0.6f).OnComplete(() =>
            {
                rootSeedCanvas.transform.localScale = new Vector3(1, 1, 1);
            });
        }

        private void PlaySeedSound(AudioClip soundClip)
        {
            if (seedAudioSource != null)
            {
                seedAudioSource.PlayOneShot(soundClip);
            }
        }

        public void LoseGrowth()
        {
            if (_scaleTween != null)
            {
                if (_scaleTween.IsActive())
                {
                    _scaleTween.Kill();
                }
            }
            _everShaker?.Kill();
            
            _growthMade = Mathf.Clamp(GrowthMade - 1, 0, steps);
            _scaleTween = treePlacement.transform.DOScale(_growthVector * _growthMade, 0.2f);
            UpdateGrowthCounter();
        }
    }
}
