using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class Enemy
    {
        private int EnemyX;
        private int EnemyY;
        private int Distance;
        private int Direction;
        private string Attributes;
        private char Type;
        private int Speed;
        private bool Dead;
        private int Reload;
        private int CurrentReload;
        private int Health;
        private int MaxHealth;

        public Enemy(int X, int Y, char Type, string Attributes, int Distance, int Speed, int Direction, int Reload, int Health)
        {
            EnemyX = X;
            EnemyY = Y;
            this.Speed = Speed;
            this.Attributes = Attributes;
            this.Distance = Distance;
            this.Type = Type;
            this.Direction = Direction;
            this.Reload = Reload;
            MaxHealth = Health;
            CurrentReload = 0;
            this.Health = Health;
            Dead = false;
        }

        public int GetX()
        {
            return EnemyX;
        }

        public int GetSpeed()
        {
            return Speed;
        }

        public int GetMaxHealth()
        {
            return MaxHealth;
        }

        public int GetDistance()
        {
            return Distance;
        }

        public int GetHealth()
        {
            return Health;
        }

        public int GetY()
        {
            return EnemyY;
        }

        public int GetDirection()
        {
            return Direction;
        }

        public char GetEType()
        {
            return Type;
        }

        public string GetEAttributes()
        {
            return Attributes;
        }

        public bool GetDead()
        {
            return Dead;
        }

        public void SetX(int X)
        {
            EnemyX = X;
        }

        public void SetY(int Y)
        {
            EnemyY = Y;
        }
        
        public void Damaged(int Damage)
        {
            Health -= Damage;
            if (Health <= 0)
            {
                Dead = true;
            }
        }

        public int Reloading()
        {
            return CurrentReload;
        }

        public void SetDistance(int Distance)
        {
            this.Distance = Distance;
        }

        public void Shot()
        {
            if (CurrentReload == 0)
            {
                CurrentReload = Reload;
            }
            else
            {
                CurrentReload--;
            }
        }

        public int GetReload()
        {
            return Reload;
        }

        public void SetDirection(int Direction)
        {
            this.Direction = Direction;
            if (Direction != 0)
            {
                switch (Type)
                {
                    case '^':
                    case '>':
                    case '<':
                    case 'V':
                        {
                            switch (Direction)
                            {
                                case 1:
                                    Type = '^';
                                    break;
                                case 2:
                                    Type = 'V';
                                    break;
                                case 3:
                                    Type = '<';
                                    break;
                                case 4:
                                    Type = '>';
                                    break;
                            }
                            break;
                        }
                }
            }
        }
    }
}
