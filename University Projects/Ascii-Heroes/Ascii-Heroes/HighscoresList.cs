using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class HighscoresList
    {
        private ArrayList List;

        public HighscoresList()
        {
            List = new ArrayList();
            ReadFile();
        }

        private void ReadFile()
        {
            StreamReader SR = new StreamReader(@"Save Files\Highscores.txt");
            while (!SR.EndOfStream)
            {
                string Name = SR.ReadLine();
                int Score = int.Parse(SR.ReadLine());
                List.Add(new Highscore(Name, Score));
            }
            SR.Close();
        }

        public void Quit()
        {
            StreamWriter SW = new StreamWriter(@"Save Files\Highscores.txt");
            for (int i = 0; i < List.Count; i++)
            {
                Highscore Current = (Highscore)List[i];
                SW.WriteLine(Current.GetName());
                SW.WriteLine(Current.GetScore());
            }
            SW.Close();
        }

        public int Count()
        {
            return List.Count;
        }

        public Highscore GetHS(int Pos)
        {
            return (Highscore)List[Pos];
        }

        public void AddScore(int Score)
        {
            int Pos = -1;
            for (int i = 0; i < List.Count; i++)
            {
                Highscore Current = (Highscore)List[i];
                if (Current.GetScore() < Score)
                {
                    Pos = i;
                    break;
                }
            }

            if (List.Count < 25)
            {
                Add(Score, Pos);
            }
            else if (Pos != -1)
            {
                Add(Score, Pos);
            }
        }

        private void Add(int Score, int Pos)
        {
            DisplayNewHighscore(Score);
            string Name = Console.ReadLine();
            if (Pos == -1)
            {
                List.Add(new Highscore(Name, Score));
            }
            else
            {
                Insert(Score, Pos, Name);
            }
        }

        private void Insert(int Score, int Pos, string Name)
        {
            Highscore New = new Highscore(Name, Score);
            Highscore Temp = New;
            for (int i = Pos; i < List.Count; i++)
            {
                Temp = (Highscore)List[i];
                List[i] = New;
                New = Temp;
            }
            List.Add(Temp);
        }

        private void DisplayNewHighscore(int Score)
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\NewHighscoreScreen.txt");
            string SScore = GetSScore(Score);
            int Line = 1;
            while (!SR.EndOfStream)
            {
                if (Line == 7)
                {
                    Console.WriteLine("[                       {0}                     ]", SScore);
                }
                else
                {
                    Console.WriteLine(SR.ReadLine());
                }
                Line++;
            }
            Console.Write("                 ");
        }

        public void DisplayHighscores()
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\HighscoreScreen.txt");
            int Line = 1;
            while (!SR.EndOfStream)
            {
                if (Line == 13)
                {
                    foreach (Highscore Current in List)
                    {
                        WriteHighscore(Current);
                    }
                }
                else
                {
                    Console.WriteLine(SR.ReadLine());
                }
                Line++;
            }

            ConsoleKeyInfo Key = Console.ReadKey();
            if ((Key.Key != ConsoleKey.Enter) && (Key.Key != ConsoleKey.Escape))
            {
                DisplayHighscores();
            }
        }

        private string GetSScore(int Score)
        {
            string SScore = Score.ToString();
            switch (SScore.Length)
            {
                case 1:
                    {
                        SScore = "0000" + SScore;
                        break;
                    }
                case 2:
                    {
                        SScore = "000" + SScore;
                        break;
                    }
                case 3:
                    {
                        SScore = "00" + SScore;
                        break;
                    }
                case 4:
                    {
                        SScore = "0" + SScore;
                        break;
                    }
            }
            return SScore;
        }

        private void WriteHighscore(Highscore Current)
        {
            string Score = GetSScore(Current.GetScore());
            string Name = Current.GetName();
            Console.Write("[");
            for (int i = 0; i < (50 - Name.Length); i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("{0} - {1}                                             ]", Name, Score);
        }
    }
}
