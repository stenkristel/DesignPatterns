using UnityEngine;

namespace Border
{
    public interface IBorderObject
    {
        public bool IsGoal { get; }
        public Vector2 OnHitDirectionModifier  { get; }
    }
}
