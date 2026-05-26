using System;
using Enums;
using Framework;
using Score;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreUIUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text scorePlayerLText;
        [SerializeField] private TMP_Text scorePlayerRText;
        private ScoreTracker _scoreTracker;

        private void Start()
        {
            _scoreTracker = ServiceLocator<ScoreTracker>.GetItem;
            AssignEvents();
        }

        private void OnDestroy()
        {
            UnAssignEvents();
        }

        private void AssignEvents()
        {
            _scoreTracker.onScoreUpdate += UpdatePlayerScoreText;
        }

        private void UnAssignEvents()
        {
            _scoreTracker.onScoreUpdate -= UpdatePlayerScoreText;
        }

        private void UpdatePlayerScoreText(Players player, int newScore)
        {
            switch (player)
            {
                case Players.PlayerL:
                    UpdateText(scorePlayerRText, newScore.ToString());
                    break;
                case Players.PlayerR:
                    UpdateText(scorePlayerLText, newScore.ToString());
                    break;
            }
        }
        
        private void UpdateText(TMP_Text uiText, string newText)
        {
            uiText.text = newText;
        }
    }
}
