using System;
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
            if (collision.gameObject.CompareTag("Vertical"))
            {
                speed.x = -speed.x;
            }
            
            if (collision.gameObject.CompareTag("Horizontal"))
            {
                speed.y = -speed.y;
            }
        }
    }
}
