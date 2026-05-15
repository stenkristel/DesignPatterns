using System;
using Framework;

namespace Score
{
    public class ScoreTracker : Singleton
    {
        public int ScorePlayerL { get; private set; }
        public int ScorePlayerR { get; private set; }

        public Action<int> onScorePlayerLUpdate;
        public Action<int> onScorePlayerRUpdate;

        public void HandleScorePlayerLUpdate(int scoreAdd)
        {
            ScorePlayerL += scoreAdd;
            onScorePlayerLUpdate?.Invoke(ScorePlayerL);
        }
        
        public void HandleScorePlayerRUpdate(int scoreAdd)
        {
            ScorePlayerR += scoreAdd;
            onScorePlayerRUpdate?.Invoke(ScorePlayerR);
        }
    }
}
