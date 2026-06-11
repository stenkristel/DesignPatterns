using System;
using Enums;
using UnityEngine;

namespace Border
{
    public class GoalBorder : IGoalObject
    {
        public Players Player { get; }

        public GoalBorder(Players playersGoal)
        {
            Player = playersGoal;
        }
    }
}
