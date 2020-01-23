using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class Highscore
    {
        private string Name;
        private int Score;

        public Highscore(string Name, int Score)
        {
            this.Name = Name;
            this.Score = Score;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetScore()
        {
            return Score;
        }
    }
}
