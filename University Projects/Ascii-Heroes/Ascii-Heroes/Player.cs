using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class Player
    {
        private int PlayerX;
        private int PlayerY;
        private int Health;
        private bool Dead;

        public Player(int X, int Y, int Health)
        {
            PlayerX = X;
            PlayerY = Y;
            this.Health = Health;
            Dead = false;
        }

        public bool GetDead()
        {
            return Dead;
        }

        public int GetX()
        {
            return PlayerX;
        }

        public int GetY()
        {
            return PlayerY;
        }

        public void SetY(int Y)
        {
            PlayerY = Y;
        }

        public void SetX(int X)
        {
            PlayerX = X;
        }

        public void Damaged()
        {
            Health--;
            if (Health <= 0)
            {
                Dead = true;
            }
        }

        public int GetHealth()
        {
            return Health;
        }
    }
}
