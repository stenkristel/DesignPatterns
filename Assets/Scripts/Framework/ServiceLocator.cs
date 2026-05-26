using UnityEngine;

namespace Framework
{
    public class ServiceLocator<T>
    {
        private static T _instance;

        public static T GetItem
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("No Instance Found");
                }
                return _instance;
            }
        }

        public static T Provide(T item)
        {
            _instance = item;
            return item;
        }
    }
}
