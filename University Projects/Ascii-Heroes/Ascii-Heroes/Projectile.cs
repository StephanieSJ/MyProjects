using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class Projectile
    {
        private int X;
        private int Y;
        private int Direction;
        private bool Dead;

        public Projectile(int X, int Y, int Direction)
        {
            this.X = X;
            this.Y = Y;
            this.Direction = Direction;
            Dead = false;
        }

        public int GetX()
        {
            return X;
        }

        public int GetY()
        {
            return Y;
        }

        public int GetDirection()
        {
            return Direction;
        }

        public bool GetDead()
        {
            return Dead;
        }

        public void SetX(int X)
        {
            this.X = X;
        }

        public void SetY(int Y)
        {
            this.Y = Y;
        }

        public void Destroyed()
        {
            Dead = true;
        }
    }
}
