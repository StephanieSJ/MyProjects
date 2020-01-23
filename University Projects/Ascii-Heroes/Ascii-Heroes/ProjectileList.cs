using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class ProjectileList
    {
        private ArrayList List;

        public ProjectileList()
        {
            List = new ArrayList();
        }

        public void Shoot(int X, int Y, int Direction)
        {
            List.Add(new Projectile(X, Y, Direction));
        }

        public int Count()
        {
            return List.Count;
        }

        public Projectile GetProjectile(int Pos)
        {
            return (Projectile)List[Pos];
        }

        public void RemoveDead()
        {
            int Pos = 0;
            while (Pos < List.Count)
            {
                Projectile Current = (Projectile)List[Pos];
                if (Current.GetDead())
                {
                    List.RemoveAt(Pos);
                }
                else
                {
                    Pos++;
                }
            }
        }

        public int FindProjectile(int X, int Y)
        {
            foreach (Projectile Current in List)
            {
                if ((Current.GetX() == X) && (Current.GetY() == Y))
                {
                    return List.IndexOf(Current);
                }
            }
            return -1;
        }
    }
}
