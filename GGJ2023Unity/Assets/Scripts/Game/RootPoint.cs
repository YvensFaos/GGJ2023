using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        [SerializeField] 
        private Button rootPointButton;

        private GameObject _hoverRoot;
        private bool _markedForDeath;

        public SeedController Seed
        {
            get => seed;
            set => seed = value;
        }

        private void Start()
        {
            var eventTrigger = rootPointButton.GetComponent<EventTrigger>();
            if (eventTrigger == null) return;
            var pointerEnterEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            pointerEnterEntry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
            eventTrigger.triggers.Add(pointerEnterEntry);

            var pointerExitEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            pointerExitEntry.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
            eventTrigger.triggers.Add(pointerExitEntry);
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
            
            Seed.Grow();
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
            Destroy(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_hoverRoot == null)
            {
                var selfTransform = transform;
                _hoverRoot = Instantiate(rootsDatabase.HoverRoot, selfTransform.position, selfTransform.rotation);
            }
            _hoverRoot.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hoverRoot != null)
            {
                _hoverRoot.SetActive(false);
            }
        }
    }
}
