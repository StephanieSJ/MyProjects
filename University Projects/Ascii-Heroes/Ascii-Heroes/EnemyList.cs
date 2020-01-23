using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class EnemyList
    {
        private ArrayList List;
        private int Sorted;

        public EnemyList()
        {
            List = new ArrayList();
            Sorted = 0;
        }

        public void AddEnemy(int X, int Y, char Type, string Attributes, int Distance, int Speed, int Direction, int Reload, int Health)
        {
            List.Add(new Enemy(X, Y, Type, Attributes, Distance, Speed, Direction, Reload, Health));
            Sorted = 0;
        }

        public void SortDistance()
        {
            if (Sorted != 1)
            {
                DSort();
            }
        }

        public int FindEnemy(int X, int Y)
        {
            foreach (Enemy Current in List)
            {
                if ((Current.GetX() == X) && (Current.GetY() == Y))
                {
                    return List.IndexOf(Current);
                }
            }
            return -1;
        }

        private void DSort()
        {
            for (int i = 1; i < List.Count; i++)
            {
                Position(i);
            }
            Sorted = 1;
        }

        private void Position(int Pos)
        {
            while ((Pos != 0) && (((Enemy)List[Pos]).GetDistance()) < ((Enemy)List[Pos - 1]).GetDistance())
            {
                Swop(Pos, Pos - 1);
                Pos--;
            }
        }

        private void Swop(int i, int j)
        {
            Enemy Temp = (Enemy)List[i];
            List[i] = (Enemy)List[j];
            List[j] = Temp;
        }

        public int Count()
        {
            return List.Count;
        }

        public Enemy GetEnemy(int Pos)
        {
            return (Enemy)List[Pos];
        }

        public void ProjectileDead()
        {
            int Pos = 0;
            while (Pos < List.Count)
            {
                Enemy Current = (Enemy)List[Pos];
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

        public void RemoveDead(BestiaryList BList)
        {
            int Pos = 0;
            while (Pos < List.Count)
            {
                Enemy Current = (Enemy)List[Pos];
                if (Current.GetDead())
                {
                    string Attributes = Current.GetEAttributes();
                    char EType = Current.GetEType();
                    switch (Current.GetEType())
                    {
                        case '>':
                        case '<':
                        case 'V':
                        case '^':
                            Attributes = Current.GetEAttributes() + "T";
                            EType = '>';
                            break;
                        case 'H':
                        case 'I':
                            {
                                EType = 'H';
                                break;
                            }
                    }
                    if (BList.Find(EType) == -1)
                    {
                        BList.AddEntry(EType, Attributes, Current.GetSpeed(), Current.GetReload(), Current.GetMaxHealth());
                    }
                    List.RemoveAt(Pos);
                }
                else
                {
                    Pos++;
                }
            }
        }
    }
}
