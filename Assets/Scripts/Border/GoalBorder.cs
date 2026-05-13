using UnityEngine;

namespace Border
{
    public class GoalBorder : MonoBehaviour, IBorderObject
    {
        public bool IsGoal => true;
        public Vector2 OnHitDirectionModifier => Vector2.zero;
    }
}
