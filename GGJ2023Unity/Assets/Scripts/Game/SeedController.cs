using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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
        [SerializeField] private GameObject seedObject;
        [SerializeField] private GameObject treePlacement;
        [SerializeField] private float minimalSize;
        [SerializeField] private float maximalSize;
        [SerializeField] private int steps;

        [Header("Audio")] 
        [SerializeField] private AudioSource seedAudioSource;
        [SerializeField] private AudioClip regulaGrowthSound;
        [SerializeField] private AudioClip fullyGrownSound;

        private float _growthStep;
        private Vector3 _growthVector;
        private TweenerCore<Vector3, Vector3, VectorOptions> _scaleTween;
        private bool _fullyGrown;
        
        public float GrowthStep => _growthStep;

        private void Awake()
        {
            numberRootSeedCanvas = rootSeedCanvas.GetRootPanelCount();
            availableRootPower = Mathf.Clamp(availableRootPower, 0, numberRootSeedCanvas);

            rootSeedCanvas.Initialize(availableRootPower);

            var min = Mathf.Min(minimalSize, maximalSize);
            var max = Mathf.Max(minimalSize, maximalSize);
            minimalSize = min;
            maximalSize = max;

            _growthStep = (max - min) / (float)steps;
            _growthVector = new Vector3(_growthStep, _growthStep, _growthStep);
        }

        private void Start()
        {
            if (seedObject == null) return;
            var scale = seedObject.transform.localScale;
            seedObject.transform.localScale = Vector3.zero;
            seedObject.transform.DOScale(scale, 0.3f).OnComplete(() =>
            {
                seedObject.transform.Rotate(new Vector3(1,0,0), Random.Range(0.0f, 360.0f));
            });
        }
        
        public void Grow()
        {
            void ScaleTweenCall()
            {
                var scale = treePlacement.transform.localScale;
                if (scale.x + _growthStep >= maximalSize)
                {
                    if (_fullyGrown) return;
                    FullyGrown();
                }
                else
                {
                    _scaleTween = treePlacement.transform.DOScale(scale + _growthVector, 0.2f);
                    PlaySeedSound(regulaGrowthSound);
                }
            }
            
            if (_scaleTween != null)
            {
                if (_scaleTween.active)
                {
                    _scaleTween.OnComplete(ScaleTweenCall);
                }
                else
                {
                    ScaleTweenCall();
                }
            }
            else
            {
                ScaleTweenCall();    
            }
        }

        private void FullyGrown()
        {
            _fullyGrown = true;
            PlaySeedSound(fullyGrownSound);

            var levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.NotifySeedGrown();
            }
        }

        public bool TryToUseRootPower()
        {
            if (availableRootPower <= 0) return false;
            availableRootPower--;
            rootSeedCanvas.ResetRootPanelsToMatchRootPower(availableRootPower);
            return true;
        }

        public void CollectRootPower(RootPower rootPower)
        {
            availableRootPower = Mathf.Clamp(availableRootPower + rootPower.Power, 0, numberRootSeedCanvas);
            rootSeedCanvas.ResetRootPanelsToMatchRootPower(availableRootPower);
        }

        private void PlaySeedSound(AudioClip soundClip)
        {
            if (seedAudioSource != null)
            {
                seedAudioSource.PlayOneShot(soundClip);
            }
        }
    }
}
