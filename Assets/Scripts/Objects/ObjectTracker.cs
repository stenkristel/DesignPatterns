using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Objects
{
    public class ObjectTracker : IObjectTracker
    {
        private Dictionary<GameObject, List<object>> _objects = new();
        public Dictionary<GameObject, List<object>> Objects { get => _objects; private set =>  _objects = value; }

        public void Add(GameObject gameObject, params object[] components)
        {
            Objects.TryAdd(gameObject, new List<object>(components));
            if (components.Length != 0) Objects[gameObject].AddRange(components);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            if (!Objects.Remove(gameObject))
            {
                Debug.LogError("Tried to remove " + gameObject.name + ", but it is not located in the collection");
            }
        }

        public void RemoveComponent(GameObject gameObject, object component)
        {
            if (!Objects.ContainsKey(gameObject)) Debug.LogError("Tried to find " + gameObject.name + ", but it is not located in the collection");
            Objects[gameObject].Remove(component);
        }
        
        public void RemoveComponent<T>(GameObject gameObject)
        {
            if (!Objects.ContainsKey(gameObject)) Debug.LogError("Tried to find " + gameObject.name + ", but it is not located in the collection");
            foreach (var item in Objects[gameObject].Where(item => (Type)item == typeof(T)))
            {
                Objects[gameObject].Remove(item);
                return;
            }
        }

        public bool TryGetComponent<T>(GameObject gameObject, out object component)
        {
            if (!Objects.ContainsKey(gameObject)) Debug.LogError("Tried to find " + gameObject.name + ", but it is not located in the collection");
            foreach (var item in Objects[gameObject].Where(item => (Type)item == typeof(T)))
            {
                component = item;
                return true;
            }
            component = null;
            return false;
        }
    }
}
