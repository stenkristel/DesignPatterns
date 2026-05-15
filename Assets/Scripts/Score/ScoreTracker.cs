using System;
using System.Collections.Generic;
using Framework;
using Enums;

namespace Score
{
    public class ScoreTracker : Singleton<ScoreTracker>
    {
        private Dictionary<Players, int> _playerScores = new();

        public Action<Players, int > onScoreUpdate;
        
        public void HandleScoreUpdate(Players player, int scoreAdd = 1)
        {
            _playerScores.TryAdd(player, 0);
            _playerScores[player] += scoreAdd;
            onScoreUpdate?.Invoke(player, _playerScores[player]);
        }
    }
}
