using System;
using System.Collections.Generic;
using Framework.Interfaces;
using UnityEngine;

namespace Collision
{
    public interface IBoxCollider : IUpdatable, IGameObject
    {
        public Collider[] CurrentColliders { get; }
        public event Action<GameObject> OnCollision;
        public event Action<GameObject> OnCollisionEnter;
        public event Action<GameObject> OnCollisionExit;
        public void CheckForCollision();
    }
}
