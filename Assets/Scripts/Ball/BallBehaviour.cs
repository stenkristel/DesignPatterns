using Border;
using Framework;
using Framework.Interfaces;
using Score;
using UnityEngine;

namespace Ball
{
    public class BallBehaviour : IUpdatable
    {
        private Vector2 _speed;
        private Rigidbody2D _rb;
        private IScoreTracker _scoreTracker;
        
        public BallBehaviour(Vector2 speed, Rigidbody2D rb, IScoreTracker scoreTracker)
        {
            _speed = speed;
            _rb = rb;
            _scoreTracker = scoreTracker;
        }

        public void OnUpdate()
        {
            _rb.linearVelocity = _speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IGoalObject goalObject))
            {
                _scoreTracker.HandleScoreUpdate(goalObject.Player); //todo replace with notifying gameStateManager, and make score updater listen to that
                //destroy self
            }
            
            if (collision.gameObject.TryGetComponent(out IBorderObject borderObject))
            {
                _speed *= borderObject.OnHitDirectionModifier;
            }
        }
    }
}
