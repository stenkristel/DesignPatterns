using UnityEngine;

namespace Player
{
    public class CharacterStats : MonoBehaviour
    {
        public float Bounce { get; private set; }
        public float Speed { get; private set; }
        public Vector2 Size { get; private set; }

        public CharacterStats(float bounce, float speed, Vector2 size)
        {
            Bounce = bounce;
            Speed = speed;
            Size = size;
        }
    }
}
