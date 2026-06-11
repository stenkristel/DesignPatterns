using System;
using System.Collections.Generic;
using Framework.Interfaces;
using UnityEngine;

namespace Objects
{
    public interface IObjectTracker : IUpdatable
    {
        public Dictionary<GameObject, List<object>> Objects { get; }

        public void Add(GameObject gameObject, params object[] components);
        public void RemoveGameObject(GameObject gameObject);
        public void RemoveComponent(GameObject gameObject, object component);
        public void RemoveComponent<T>(GameObject gameObject);
        public bool TryGetComponent<T>(GameObject gameObject, out T component);
    }
}
