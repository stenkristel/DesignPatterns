using Border;
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
            if (!collision.gameObject.TryGetComponent<IBorderObject>(out IBorderObject borderObject))
            {
                return;
            }

            if (borderObject.IsGoal)
            {
                //goal
                return;
            }

            speed *= borderObject.OnHitDirectionModifier;
        }
    }
}
