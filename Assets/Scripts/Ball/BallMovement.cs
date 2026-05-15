using Border;
using Score;
using UnityEngine;

namespace Ball
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 speed;
        [SerializeField] private Rigidbody2D rb;

        private void Update()
        {
            rb.linearVelocity = speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IGoalObject goalObject))
            {
                ScoreTracker.Instance.HandleScoreUpdate(goalObject.Player); //todo replace with notifying gameStateManager, and make score updater listen to that
                //destroy self
            }
            
            if (collision.gameObject.TryGetComponent(out IBorderObject borderObject))
            {
                speed *= borderObject.OnHitDirectionModifier;
            }
        }
    }
}
