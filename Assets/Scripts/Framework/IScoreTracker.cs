using System;
using System.Collections.Generic;
using Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace Framework
{
    public interface IScoreTracker
    {
        public event Action <Players, int> onScoreUpdate;

        public void HandleScoreUpdate(Players player, int scoreAdd = 1);
    }
}
