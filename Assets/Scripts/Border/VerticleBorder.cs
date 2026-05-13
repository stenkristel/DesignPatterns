using UnityEngine;

namespace Border
{
    public class VerticleBorder : MonoBehaviour, IBorderObject
    {
        public bool IsGoal => false;
        public Vector2 OnHitDirectionModifier => new Vector2(-1, 1);
    }
}
