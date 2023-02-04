using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class RootPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Data")] 
        [SerializeField] 
        private SeedController seed;
        [SerializeField] 
        private RootController rootParent;
        [SerializeField]
        private RootController spawnRoot;

        [Header("References")]
        [SerializeField]
        private RootsDatabase rootsDatabase;

        private GameObject _hoverRoot;
        private bool _markedForDeath;
        private TweenerCore<Vector3, Vector3, VectorOptions> _scaleTween;

        public SeedController Seed
        {
            get => seed;
            set => seed = value;
        }

        public void GrowRoot()
        {
            //First checks if there is already a root in here
            if (spawnRoot != null) return;
            //Secondly, checks if is there enough root power
            if (!Seed.TryToUseRootPower()) return;
            
            var newRoot = rootsDatabase.GetRoot();
            var selfTransform = transform;
            spawnRoot = Instantiate(newRoot, selfTransform.position, selfTransform.rotation);
            spawnRoot.Initialize(seed);
            if (rootParent != null)
            {
                spawnRoot.RootParent = rootParent;
            }

            spawnRoot.transform.localScale = Vector3.zero;
            spawnRoot.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
            
            Seed.Grow();
            if (_hoverRoot != null)
            {
                Destroy(_hoverRoot.gameObject);
            }
        }

        public void DestroyRoot()
        {
            if (_markedForDeath) return;
            
            _markedForDeath = true;
            if (spawnRoot != null)
            {
                spawnRoot.RootContactWithOther();
                Destroy(spawnRoot.gameObject);
            }
            if (_hoverRoot != null)
            {
                _scaleTween?.Kill();
                Destroy(_hoverRoot);
            }
            Destroy(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_markedForDeath) return;
            if (spawnRoot != null) return;
            if (_hoverRoot == null)
            {
                var selfTransform = transform;
                _hoverRoot = Instantiate(rootsDatabase.HoverRoot, selfTransform.position, selfTransform.rotation);
            }
            _hoverRoot.SetActive(true);
            _hoverRoot.transform.localScale = Vector3.zero;
            _scaleTween?.Kill();
            _scaleTween = _hoverRoot.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hoverRoot == null) return;
            _hoverRoot.SetActive(false);
            _scaleTween?.Kill();
        }
    }
}
