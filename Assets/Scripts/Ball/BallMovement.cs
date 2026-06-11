using System;
using Border;
using Collision;
using Framework;
using Framework.Interfaces;
using Objects;
using UnityEngine;

namespace Ball
{
    public class BallMovement : IUpdatable
    {
        private Vector2 _speed;
        private Rigidbody2D _rb;
        private IScoreTracker _scoreTracker;
        private IBoxCollider _boxCollider;
        private IObjectTracker _objectTracker;
        
        public BallMovement(Vector2 speed, Rigidbody2D rb, IScoreTracker scoreTracker, IBoxCollider boxCollider, IObjectTracker objectTracker)
        {
            _speed = speed;
            _rb = rb;
            _scoreTracker = scoreTracker;
            _boxCollider = boxCollider;
            _objectTracker = objectTracker;
            
            _boxCollider.OnCollisionEnter += CheckForBorderCollision;
        }

        public void OnUpdate()
        {
            _rb.linearVelocity = _speed;
        }

        private void CheckForBorderCollision(GameObject collision)
        {
            if (_objectTracker.TryGetComponent(collision.gameObject, out IBorderObject borderObject))
            {
                _speed *= borderObject.OnHitDirectionModifier;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IGoalObject goalObject))
            {
                _scoreTracker.HandleScoreUpdate(goalObject.Player); //todo replace with notifying gameStateManager, and make score updater listen to that
                //destroy self
            }
        }
    }
}
