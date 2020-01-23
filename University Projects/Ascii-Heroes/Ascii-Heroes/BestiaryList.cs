using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class BestiaryList
    {
        private ArrayList List;

        public BestiaryList()
        {
            List = new ArrayList();
            ReadFile();
        }

        private void ReadFile()
        {
            StreamReader SR = new StreamReader(@"Save Files\Bestiary.txt");
            while (!SR.EndOfStream)
            {
                char Type = SR.ReadLine()[0];
                string Attributes = SR.ReadLine();
                int Speed = int.Parse(SR.ReadLine());
                int Reload = int.Parse(SR.ReadLine());
                int Health = int.Parse(SR.ReadLine());
                AddEntry(Type, Attributes, Speed, Reload, Health);
            }
            SR.Close();
        }

        public void Quit()
        {
            StreamWriter SW = new StreamWriter(@"Save Files\Bestiary.txt");
            for (int i = 0; i < List.Count; i++)
            {
                Enemy Current = (Enemy)List[i];
                SW.WriteLine(Current.GetEType());
                SW.WriteLine(Current.GetEAttributes());
                SW.WriteLine(Current.GetSpeed());
                SW.WriteLine(Current.GetReload());
                SW.WriteLine(Current.GetMaxHealth());
            }
            SW.Close();
        }

        public void AddEntry(char Type, string Attributes, int Speed, int Reload, int Health)
        {
            List.Add(new Enemy(0, 0, Type, Attributes, 0, Speed, 0, Reload, Health));
        }

        public int Count()
        {
            return List.Count;
        }

        public Enemy GetEnemy(int Pos)
        {
            return (Enemy)List[Pos];
        }

        public int Find(char Type)
        {
            for (int i = 0; i < List.Count; i++)
            {
                Enemy Current = (Enemy)List[i];
                if (Current.GetEType() == Type)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
