using System;
using System.Linq;
using UnityEngine;

namespace Collision
{
    public class BoxCollider : IBoxCollider
    {
        private GameObject _gameObject;
        private Collider[] _currentColliders = { };
        
        public GameObject OwnerGameObject => _gameObject;
        public Collider[] CurrentColliders => _currentColliders;
        
        public event Action<GameObject> OnCollision;
        public event Action<GameObject> OnCollisionEnter;
        public event Action<GameObject> OnCollisionExit;

        public BoxCollider(GameObject ownerGameObject)
        {
            _gameObject = ownerGameObject;
        }
        
        public void OnUpdate()
        {
            CheckForCollision();
        }
       
        public void CheckForCollision()
        {
            Collider[] collisions = 
            Physics.OverlapBox(_gameObject.transform.position, _gameObject.transform.lossyScale / 2);

            if (collisions.Length > 0)
            {
                foreach (var currentCollision in collisions)
                {
                    OnCollision?.Invoke(currentCollision.gameObject);

                    if (_currentColliders.Length == 0)
                    {
                        OnCollisionEnter?.Invoke(currentCollision.gameObject);
                        continue;
                    }
                    
                    foreach (var previousCollision in _currentColliders)
                    {
                        if (currentCollision == previousCollision) break;
                        if (previousCollision != _currentColliders.Last()) continue;
                        OnCollisionEnter?.Invoke(currentCollision.gameObject);
                    }
                
                }
            }

            if (_currentColliders.Length > 0)
            {
                foreach (var previousCollision in _currentColliders)
                {
                    
                    if (collisions.Length == 0)
                    {
                        OnCollisionExit?.Invoke(previousCollision.gameObject);
                        continue;
                    }
                    
                    foreach (var currentCollision in collisions)
                    {
                        if (previousCollision == currentCollision) break;
                        if (currentCollision != collisions.Last()) continue;
                        OnCollisionExit?.Invoke(previousCollision.gameObject);
                    }
                }
            }
            
            _currentColliders = collisions;
        }
    }
}
