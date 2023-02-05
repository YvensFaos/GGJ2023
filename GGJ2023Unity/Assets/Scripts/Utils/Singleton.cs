using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour
    {
        private static T _instance;

        public static T Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning($"An instance of this singleton {name} already exists. Destroying this.");
                Destroy(gameObject);
                return;
            }
            _instance = gameObject.GetComponent<T>();
            Debug.Log($"Initiate Singleton for {name}.");
            DontDestroyOnLoad(this);
        }
    }
}