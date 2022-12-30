using UnityEngine;

    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        _instance = new GameObject().AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning($"Exist singleton [{_instance.name}], delete [{name}]");
                Destroy(gameObject);
            }
        }
    }