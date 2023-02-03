using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Canvas))]
    public class SetWorldCanvasCamera : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;
        
        private void Awake()
        {
            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
            }

            canvas.worldCamera = Camera.main;
        }
    }
}
