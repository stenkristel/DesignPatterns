using System;
using Enums;
using UnityEngine;

namespace Border
{
    public class GoalBorder : MonoBehaviour, IGoalObject
    {
        [SerializeField] private Players playersGoal;

        private void Start()
        {
            Player = playersGoal;
        }

        public Players Player { get; set; }
    }
}
