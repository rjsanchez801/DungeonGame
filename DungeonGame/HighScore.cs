using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    class HighScore
    {
        
        public string Hero { get; }
        public int LevelsCleared { get; }
        public int Score { get; }

        public HighScore(string hero, int levels, int score)
        {
            Hero = hero;
            LevelsCleared = levels;
            Score = score;
        }

        public override string ToString()
        {
            return $"{Hero}: {LevelsCleared}: {Score}: ";
        }
    }
}
