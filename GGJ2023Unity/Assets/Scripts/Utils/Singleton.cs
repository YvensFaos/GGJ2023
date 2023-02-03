using UnityEngine;

namespace Utils
{
    public class Singleton : MonoBehaviour
    {
        private Singleton _instance;

        public Singleton Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                Debug.LogError($"An instance of this singleton {name} already exists. Destroying this.");
                Destroy(gameObject);
                return;
            }
            _instance = this;
            Debug.LogWarning($"Initiate Singleton for {name}.");
        }
    }
}