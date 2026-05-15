using System;
using Enums;
using Score;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreUIUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text scorePlayerLText;
        [SerializeField] private TMP_Text scorePlayerRText;

        private void Start()
        {
            ScoreTracker.Instance.onScoreUpdate += UpdatePlayerScoreText;
        }

        private void OnDestroy()
        {
            ScoreTracker.Instance.onScoreUpdate -= UpdatePlayerScoreText;
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
