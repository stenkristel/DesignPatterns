using System.Collections.Generic;
using System.Linq;
using Framework.Interfaces;
using UnityEngine;

namespace Objects
{
    public class ObjectTracker : IObjectTracker
    {
        private List<IUpdatable> _updateAbles = new ();
        
        private Dictionary<GameObject, List<object>> _objects = new();
        public Dictionary<GameObject, List<object>> Objects { get => _objects; private set =>  _objects = value; }
        
        public void OnUpdate()
        {
            foreach (var updateAble in _updateAbles)
            {
                updateAble.OnUpdate();
            }
        }

        public void Add(GameObject gameObject, params object[] components)
        {
            if (!Objects.TryAdd(gameObject, new List<object>(components)))
            {
                if (components.Length != 0) Objects[gameObject].AddRange(components);
            }

            foreach (var component in components)
            {
                if (component is IUpdatable updatable)
                {
                    _updateAbles.Add(updatable);
                }
            }
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            if (!Objects.ContainsKey(gameObject))
            {
                Debug.LogError("Tried to remove " + gameObject.name + ", but it is not located in the collection");
            }

            foreach (var component in Objects[gameObject])
            {
                if (component is IUpdatable updatable)
                {
                    _updateAbles.Remove(updatable);
                }
            }
            
            Objects.Remove(gameObject);
        }

        public void RemoveComponent(GameObject gameObject, object component)
        {
            if (!Objects.ContainsKey(gameObject))
            {
                Debug.LogError("Tried to find " + gameObject.name + ", but it is not located in the collection");
                return;
            }
            
            Objects[gameObject].Remove(component);
            if (component is IUpdatable updatable)
            {
                _updateAbles.Remove(updatable);
            }
        }
        
        public void RemoveComponent<T>(GameObject gameObject)
        {
            if (!Objects.ContainsKey(gameObject))
            {
                Debug.LogError("Tried to find " + gameObject.name + ", but it is not located in the collection");
                return;
            }

            foreach (var item in Objects[gameObject].Where(item => item is T))
            {
                Objects[gameObject].Remove(item);
                if (item is IUpdatable updatable)
                {
                    _updateAbles.Remove(updatable);
                }
            }
            
        }
        


        public bool TryGetComponent<T>(GameObject gameObject, out T component)
        {
            if (!Objects.ContainsKey(gameObject))
            {
                Debug.LogError("Tried to find " + gameObject.name + ", but it is not located in the collection");
                component = default;
                return false;
            }

            foreach (var item in Objects[gameObject])
            {
                if (item is not T match) continue;
                component = match;
                return true;
            }
            
            component = default;
            return false;
        }
    }
}
