using UnityEngine;

namespace Framework
{
    public class Singleton : MonoBehaviour
    {
        public static Singleton Instance { get; private set; }

        private void Awake()
        {
            AssignInstance();
        }

        private void AssignInstance()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
    }
}