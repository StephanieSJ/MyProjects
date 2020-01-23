using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Ascii_Heroes
{
    class Game
    {
        private BestiaryList BList;
        private HighscoresList HSList;
        private Equipment Equip;
        private int Type;
        private EnemyList EList;
        private ProjectileList PList;
        private Player Player1;
        private int Score;
        private char[,] Layout;
        private int Rows;
        private int Columns;
        private bool Sound;
        private bool Grid;
        //private SoundPlayer SP;

        public Game(BestiaryList BList, HighscoresList HSList, Equipment Equip, int Type)
        {
            this.BList = BList;
            this.HSList = HSList;
            this.Equip = Equip;
            this.Type = Type;
            Sound = true;
            Grid = false;
            bool Playing = true;
            while (Playing)
            {
                Score = 0;
                Score = RunGame(1);
                if (this.Type == 3)
                {
                    QuitGame();
                    Playing = false;
                }
                else if (this.Type == 2)
                {
                    HSList.AddScore(Score);
                    Playing = false;
                }
                else
                {
                    Playing = GameOver();
                    Equip.IncreaseXP(Score);
                }
            }
        }

        private int RunGame(int Level)
        {
            if (Sound)
            {
                //SP = new SoundPlayer(@"Music\Menu.wav");
                //SP.PlayLooping();
            }
            bool Running = true;
            if (Type == 1)
            {
                LoadLevelLayout(Level);
                LoadLevel();
            }
            else
            {
                GenerateLevel(Level);
                LoadLevel();
            }
            while (Running)
            {
                bool Paused = false;
                bool Quit = false;
                DisplayLevel(Layout);
                DisplayHUD();
                ConsoleKeyInfo Key = Console.ReadKey();
                int Move = 0;
                int Attack = 0;

                if (Key.Key == ConsoleKey.W) { Move = 1; }
                else if (Key.Key == ConsoleKey.S) { Move = 2; }
                else if (Key.Key == ConsoleKey.A) { Move = 3; }
                else if (Key.Key == ConsoleKey.D) { Move = 4; }
                if (Move != 0)
                {
                    MovePlayer(Move);
                }

                if (Key.Key == ConsoleKey.UpArrow) { Attack = 1; }
                else if (Key.Key == ConsoleKey.DownArrow) { Attack = 2; }
                else if (Key.Key == ConsoleKey.LeftArrow) { Attack = 3; }
                else if (Key.Key == ConsoleKey.RightArrow) { Attack = 4; }
                if (Attack != 0)
                {
                    AttackPlayer(Attack);
                }

                if (Key.Key == ConsoleKey.Escape)
                {
                    Quit = PauseGame();
                    Paused = true;
                }

                if (!Paused)
                {
                    TestDamage();
                    if (Player1.GetDead())
                    {
                        Running = false;
                    }
                    else if (EList.Count() > 0)
                    {
                        UpdateProjectiles();
                        UpdateEnemies();
                        PList.RemoveDead();
                        DamageStuff();
                        if (Player1.GetDead())
                        {
                            Running = false;
                        }
                        else
                        {
                            UpdateLayout();
                        }
                    }
                    else
                    {
                        Running = false;
                    }
                }
                else
                {
                    if ((Quit) && (Type != 2))
                    {
                        Type = 3;
                        return Score;
                    }
                    else
                    {
                        return Score;
                    }
                }
            }
            if (Sound)
            {
                //SP.Stop();
            }
            if (Player1.GetDead())
            {
                return Score;
            }
            else
            {
                if ((Level == 20) && (Type == 1))
                {
                    Type = 3;
                    return RunBoss();
                }
                else
                {
                    if (Type == 1)
                    {
                        NextLevel(Level);
                        return RunGame(Level + 1);
                    }
                    else
                    {
                        QuitGame();
                        return RunGame(Level + 1);
                    }
                }
            }
        }

        private int RunBoss()
        {
            StreamReader SR = new StreamReader(@"Menu\Credits.txt");
            Console.Clear();
            while (!SR.EndOfStream)
            {
                Console.WriteLine(SR.ReadLine());
            }
            SR.Close();

            ConsoleKeyInfo Key = Console.ReadKey();
            if ((Key.Key != ConsoleKey.Enter) && (Key.Key != ConsoleKey.Escape))
            {
                return RunBoss();
            }
            else
            {
                return Score;
            }
        }

        private void GenerateLevel(int Level)
        {
            Random Rand = new Random();
            Rows = Rand.Next(4, 15);
            Columns = Rand.Next(8, 50);
            int Difficulty = Level * 5;
            if (Difficulty > 100)
            {
                Difficulty = 100;
            }
            Layout = new char[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (j == 0)
                    {
                        Layout[i, j] = '[';
                    }
                    else if (j == Columns - 1)
                    {
                        Layout[i, j] = ']';
                    }
                    else
                    {
                        Layout[i, j] = ' ';
                    }
                }
            }
            int PlayerX = Rand.Next(1, Columns - 1);
            int PlayerY = Rand.Next(0, Rows);
            Layout[PlayerY, PlayerX] = '@';
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    int Place = Rand.Next(1, 101);
                    if ((i == 0) || (i == Rows - 1))
                    {
                        if (Place < Difficulty)
                        {
                            if ((i != PlayerY) || (j != PlayerX))
                            {
                                Layout[i, j] = 'H';
                            }
                        }
                    }
                    else if ((j == 1) || (j == Columns - 2))
                    {
                        if (Place < Difficulty)
                        {
                            if ((i != PlayerY) || (j != PlayerX))
                            {
                                Layout[i, j] = 'I';
                            }
                        }
                    }
                    else if (Place < Difficulty)
                    {
                        if ((i != PlayerY) || (j != PlayerX))
                        {
                            int Enemy = Rand.Next(1, 3);
                            if (Enemy == 1)
                            {
                                int Direction = Rand.Next(1, 5);
                                switch (Direction)
                                {
                                    case 1:
                                        {
                                            Layout[i, j] = '^';
                                            break;
                                        }
                                    case 2:
                                        {
                                            Layout[i, j] = 'V';
                                            break;
                                        }
                                    case 3:
                                        {
                                            Layout[i, j] = '<';
                                            break;
                                        }
                                    case 4:
                                        {
                                            Layout[i, j] = '>';
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                Layout[i, j] = 'X';
                            }
                        }
                    }
                }
            }
        }

        private void LoadLevelLayout(int Level)
        {
            StreamReader SR = new StreamReader(@"Levels\Level" + Level + ".txt");
            Rows = int.Parse(SR.ReadLine());
            Columns = int.Parse(SR.ReadLine());
            Layout = new char[Rows, Columns];
            int RowCount = 0;
            while (!SR.EndOfStream)
            {
                string Line = SR.ReadLine();
                int ColCount = 0;
                foreach (char i in Line)
                {
                    Layout[RowCount, ColCount] = i;
                    ColCount++;
                }
                RowCount++;
            }
        }

        private void LoadLevel()
        {
            EList = new EnemyList();
            PList = new ProjectileList();
            int X = -1, Y = -1;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    if (Layout[i, j] == '@')
                    {
                        X = j;
                        Y = i;
                        break;
                    }
                }
                if ((X != -1) && (Y != -1))
                {
                    break;
                }
            }
            Player1 = new Player(X, Y, Equip.GetHealth());

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    if (Layout[i, j] != '@')
                    {
                        if (Layout[i, j] != ' ')
                        {
                            AddEnemy(Layout[i, j], j, i);
                        }
                    }
                }
            }
        }

        private void DisplayLevel(char[,] Level)
        {
            Console.Clear();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(Level[i, j]);
                }
                Console.WriteLine();
            }
        }

        private void UpdateLayout()
        {
            Layout = new char[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (j == 0)
                    {
                        Layout[i, j] = '[';
                    }
                    else if (j == (Columns - 1))
                    {
                        Layout[i, j] = ']';
                    }
                    else
                    {
                        if (Grid)
                        {
                            Layout[i, j] = '.';
                        }
                        else
                        {
                            Layout[i, j] = ' ';
                        }
                    }
                }
            }

            for (int i = 0; i < EList.Count(); i++)
            {
                Enemy Current = EList.GetEnemy(i);
                Layout[Current.GetY(), Current.GetX()] = Current.GetEType();
            }
            for (int i = 0; i < PList.Count(); i++)
            {
                Projectile Current = PList.GetProjectile(i);
                Layout[Current.GetY(), Current.GetX()] = 'O';
            }

            Layout[Player1.GetY(), Player1.GetX()] = '@';
        }

        private char[,] CopyLevel()
        {
            char[,] Copy = new char[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Copy[i, j] = Layout[i, j];
                }
            }
            return Copy;
        }

        private void MovePlayer(int Move)
        {
            Layout[Player1.GetY(), Player1.GetX()] = ' ';
            switch (Move)
            {
                case 1:
                    {
                        if (Player1.GetY() > 0)
                        {
                            Player1.SetY(Player1.GetY() - 1);
                        }
                        break;
                    }
                case 2:
                    {
                        if (Player1.GetY() < (Rows - 1))
                        {
                            Player1.SetY(Player1.GetY() + 1);
                        }
                        break;
                    }
                case 3:
                    {
                        if (Player1.GetX() > 1)
                        {
                            Player1.SetX(Player1.GetX() - 1);
                        }
                        break;
                    }
                case 4:
                    {
                        if (Player1.GetX() < (Columns - 2))
                        {
                            Player1.SetX(Player1.GetX() + 1);
                        }
                        break;
                    }
            }
            Layout[Player1.GetY(), Player1.GetX()] = '@';
            DisplayLevel(Layout);
            DisplayHUD();
            System.Threading.Thread.Sleep(300);
        }

        private void AttackPlayer(int Attack)
        {
            char[,] Temp = CopyLevel();
            int X = -1, Y = -1;
            switch (Attack)
            {
                case 1:
                    {
                        X = Player1.GetX();
                        Y = Player1.GetY() - Equip.GetRange();
                        for (int i = Player1.GetY() - 1; i >= Y; i--)
                        {
                            if (i < 0)
                            {
                                break;
                            }
                            else if (((EList.FindEnemy(Player1.GetX(), i) != -1) && (!Equip.GetPierceE())) || ((PList.FindProjectile(Player1.GetX(), i) != -1) && (!Equip.GetPierceP())))
                            {
                                X = Player1.GetX();
                                Y = i;
                                Temp[i, Player1.GetX()] = '|';
                                break;
                            }
                            Temp[i, Player1.GetX()] = '|';
                        }
                        break;
                    }
                case 2:
                    {
                        X = Player1.GetX();
                        Y = Player1.GetY() + Equip.GetRange();
                        for (int i = Player1.GetY() + 1; i <= Y; i++)
                        {
                            if (i > Rows - 1)
                            {
                                break;
                            }
                            else if (((EList.FindEnemy(Player1.GetX(), i) != -1) && (!Equip.GetPierceE())) || ((PList.FindProjectile(Player1.GetX(), i) != -1) && (!Equip.GetPierceP())))
                            {
                                X = Player1.GetX();
                                Y = i;
                                Temp[i, Player1.GetX()] = '|';
                                break;
                            }
                            Temp[i, Player1.GetX()] = '|';
                        }
                        break;
                    }
                case 3:
                    {
                        Y = Player1.GetY();
                        X = Player1.GetX() - Equip.GetRange();
                        for (int i = (Player1.GetX() - 1); i >= X; i--)
                        {
                            if (i <= 0)
                            {
                                break;
                            }
                            else if (((EList.FindEnemy(i, Player1.GetY()) != -1) && (!Equip.GetPierceE())) || ((PList.FindProjectile(i, Player1.GetY()) != -1) && (!Equip.GetPierceP())))
                            {
                                X = i;
                                Y = Player1.GetY();
                                Temp[Player1.GetY(), i] = '-';
                                break;
                            }
                            Temp[Player1.GetY(), i] = '-';
                        }
                        break;
                    }
                case 4:
                    {
                        Y = Player1.GetY();
                        X = Player1.GetX() + Equip.GetRange();
                        for (int i = (Player1.GetX() + 1); i <= X; i++)
                        {
                            if (i >= Columns - 1)
                            {
                                break;
                            }
                            else if (((EList.FindEnemy(i, Player1.GetY()) != -1) && (!Equip.GetPierceE())) || ((PList.FindProjectile(i, Player1.GetY()) != -1) && (!Equip.GetPierceP())))
                            {
                                X = i;
                                Y = Player1.GetY();
                                Temp[Player1.GetY(), i] = '-';
                                break;
                            }
                            Temp[Player1.GetY(), i] = '-';
                        }
                        break;
                    }
            }
            DisplayLevel(Temp);
            DisplayHUD();
            System.Threading.Thread.Sleep(600);
            DealDamage(Attack, X, Y);
        }

        private void AddEnemy(char Enemy, int EnemyX, int EnemyY)
        {
            int Distance = Convert.ToInt32(Math.Sqrt(Math.Pow((EnemyX - Player1.GetX()), 2) + Math.Pow((EnemyY - Player1.GetY()), 2)));
            int Direction = 0;
            if (Enemy == 'X')
            {
                EList.AddEnemy(EnemyX, EnemyY, Enemy, "C", Distance, 2, Direction, 0, 1);
            }
            else if ((Enemy == '^') || (Enemy == 'V') || (Enemy == '<') || (Enemy == '>'))
            {
                switch (Enemy)
                {
                    case '^':
                        Direction = 1;
                        break;
                    case 'V':
                        Direction = 2;
                        break;
                    case '<':
                        Direction = 3;
                        break;
                    case '>':
                        Direction = 4;
                        break;
                }
                EList.AddEnemy(EnemyX, EnemyY, Enemy, "", Distance, 1, Direction, 0, 2);
            }
            else if ((Enemy == 'H') || (Enemy == 'I'))
            {
                if (Enemy == 'I')
                {
                    if (EnemyX < 2)
                    {
                        Direction = 4;
                    }
                    else
                    {
                        Direction = 3;
                    }
                }
                else
                {
                    if (EnemyY < 2)
                    {
                        Direction = 2;
                    }
                    else
                    {
                        Direction = 1;
                    }
                }
                EList.AddEnemy(EnemyX, EnemyY, Enemy, "SI", Distance, 1, Direction, 2, 1);
            }
        }

        private void TestDamage()
        {
            for (int i = 0; i < EList.Count(); i++)
            {
                Enemy Current = EList.GetEnemy(i);
                if ((Current.GetX() == Player1.GetX()) && (Current.GetY() == Player1.GetY()))
                {
                    if (Current.GetEAttributes().Contains("C"))
                    {
                        Player1.Damaged();
                    }
                    else
                    {
                        Current.Damaged(1);
                        Score += 5;
                    }
                }
            }
            for (int i = 0; i < PList.Count(); i++)
            {
                Projectile Current = PList.GetProjectile(i);
                if ((Current.GetX() == Player1.GetX()) && (Current.GetY() == Player1.GetY()))
                {
                    Player1.Damaged();
                }
            }
            EList.RemoveDead(BList);
        }

        private void DealDamage(int Direction, int X, int Y)
        {
            for (int i = 0; i < EList.Count(); i++)
            {
                Enemy Current = EList.GetEnemy(i);
                switch (Direction)
                {
                    case 1:
                        {
                            if ((Current.GetX() == X) && (Current.GetY() < Player1.GetY()))
                            {
                                if (!(Current.GetY() < Y))
                                {
                                    Current.Damaged(Equip.GetDamage());
                                    Score += 2;
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            if ((Current.GetX() == X) && (Current.GetY() > Player1.GetY()))
                            {
                                if (!(Current.GetY() > Y))
                                {
                                    Current.Damaged(Equip.GetDamage());
                                    Score += 2;
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            if ((Current.GetY() == Y) && (Current.GetX() < Player1.GetX()))
                            {
                                if (!(Current.GetX() < X))
                                {
                                    Current.Damaged(Equip.GetDamage());
                                    Score += 2;
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            if ((Current.GetY() == Y) && (Current.GetX() > Player1.GetX()))
                            {
                                if (!(Current.GetX() > X))
                                {
                                    Current.Damaged(Equip.GetDamage());
                                    Score += 2;
                                }
                            }
                            break;
                        }
                }
            }

            for (int i = 0; i < PList.Count(); i++)
            {
                Projectile Current = PList.GetProjectile(i);
                switch (Direction)
                {
                    case 1:
                        {
                            if ((Current.GetX() == X) && (Current.GetY() < Player1.GetY()))
                            {
                                if (!(Current.GetY() < Y))
                                {
                                    Current.Destroyed();
                                    Score += 1;
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            if ((Current.GetX() == X) && (Current.GetY() > Player1.GetY()))
                            {
                                if (!(Current.GetY() > Y))
                                {
                                    Current.Destroyed();
                                    Score += 1;
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            if ((Current.GetY() == Y) && (Current.GetX() < Player1.GetX()))
                            {
                                if (!(Current.GetX() < X))
                                {
                                    Current.Destroyed();
                                    Score += 1;
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            if ((Current.GetY() == Y) && (Current.GetX() > Player1.GetX()))
                            {
                                if (!(Current.GetX() > X))
                                {
                                    Current.Destroyed();
                                    Score += 1;
                                }
                            }
                            break;
                        }
                }
            }
            PList.RemoveDead();
            EList.RemoveDead(BList);
        }

        private void UpdateProjectiles()
        {
            for (int i = 0; i < PList.Count(); i++)
            {
                Projectile Current = PList.GetProjectile(i);
                switch (Current.GetDirection())
                {
                    case 1:
                        {
                            Current.SetY(Current.GetY() - 1);

                            if (Current.GetY() < 0)
                            {
                                Current.Destroyed();
                            }
                            break;
                        }
                    case 2:
                        {
                            Current.SetY(Current.GetY() + 1);

                            if (Current.GetY() > Rows - 1)
                            {
                                Current.Destroyed();
                            }
                            break;
                        }
                    case 3:
                        {
                            Current.SetX(Current.GetX() - 1);

                            if (Current.GetX() < 1)
                            {
                                Current.Destroyed();
                            }
                            break;
                        }
                    case 4:
                        {
                            Current.SetX(Current.GetX() + 1);

                            if (Current.GetX() > Columns - 2)
                            {
                                Current.Destroyed();
                            }
                            break;
                        }
                }
            }
        }

        private void DamageStuff()
        {
            for (int i = 0; i < PList.Count(); i++)
            {
                bool Dead = false;
                Projectile Current = PList.GetProjectile(i);
                if ((Current.GetX() < 1) || (Current.GetX() > Columns - 2) || (Current.GetY() < 0) || (Current.GetY() > Rows - 1))
                {
                    Current.Destroyed();
                    Dead = true;
                }
                if ((Current.GetX() == Player1.GetX()) && (Current.GetY() == Player1.GetY()))
                {
                    Player1.Damaged();
                    Current.Destroyed();
                    Dead = true;
                }
                if (!Dead)
                {
                    for (int j = 0; j < EList.Count(); j++)
                    {
                        Enemy Test = EList.GetEnemy(j);
                        if ((Test.GetX() == Current.GetX()) && (Test.GetY() == Current.GetY()))
                        {
                            Test.Damaged(1);
                            Current.Destroyed();
                            break;
                        }
                    }
                }
            }
            PList.RemoveDead();
            EList.ProjectileDead();
        }

        private void UpdateEnemies()
        {
            EList.SortDistance();
            for (int i = 0; i < EList.Count(); i++)
            {
                Enemy Current = EList.GetEnemy(i);
                if (Current.GetSpeed() > 0)
                {
                    FindMovement(i);
                }
                if ((Current.GetEAttributes().Contains("S")) && (Current.Reloading() == 0))
                {
                    Shoot(i);
                    Current.Shot();
                }
                else if (Current.Reloading() > 0)
                {
                    Current.Shot();
                }
            }
        }

        private void Shoot(int Pos)
        {
            Enemy Current = EList.GetEnemy(Pos);
            switch (Current.GetDirection())
            {
                case 1:
                    {
                        PList.Shoot(Current.GetX(), Current.GetY() - 1, 1);
                        break;
                    }
                case 2:
                    {
                        PList.Shoot(Current.GetX(), Current.GetY() + 1, 2);
                        break;
                    }
                case 3:
                    {
                        PList.Shoot(Current.GetX() - 1, Current.GetY(), 3);
                        break;
                    }
                case 4:
                    {
                        PList.Shoot(Current.GetX() + 1, Current.GetY(), 4);
                        break;
                    }
            }
        }

        private void FindMovement(int Pos)
        {
            Enemy Current = EList.GetEnemy(Pos);
            int TestX = Current.GetX();
            int TestY = Current.GetY();
            int TestDistance = Current.GetDistance();
            int MinDistance = Current.GetDistance();
            int Direction = Current.GetDirection();
            int MinX = Current.GetX();
            int MinY = Current.GetY();
            if (Current.GetEAttributes().Contains("I"))
            {
                switch (Direction)
                {
                    case 1:
                    case 2:
                        {
                            if (Player1.GetX() < Current.GetX())
                            {
                                TestX = Current.GetX() - 1;
                                if (CheckAvailability(TestX, TestY, Current))
                                {
                                    MinX = TestX;
                                    MinDistance = GetDistance(TestX, TestY);
                                }
                            }
                            else if (Player1.GetX() > Current.GetX())
                            {
                                TestX = Current.GetX() + 1;
                                if (CheckAvailability(TestX, TestY, Current))
                                {
                                    MinX = TestX;
                                    MinDistance = GetDistance(TestX, TestY);
                                }
                            }
                            break;
                        }
                    case 3:
                    case 4:
                        {
                            if (Player1.GetY() < Current.GetY())
                            {
                                TestY = Current.GetY() - 1;
                                if (CheckAvailability(TestX, TestY, Current))
                                {
                                    MinY = TestY;
                                    MinDistance = GetDistance(TestX, TestY);
                                }
                            }
                            else if (Player1.GetY() > Current.GetY())
                            {
                                TestY = Current.GetY() + 1;
                                if (CheckAvailability(TestX, TestY, Current))
                                {
                                    MinY = TestY;
                                    MinDistance = GetDistance(TestX, TestY);
                                }
                            }
                            break;
                        }
                }
            }
            else
            {
                switch (Direction)
                {
                    case 0:
                        {
                            bool Searching = true;
                            int Attempt = 0;
                            while (Searching)
                            {
                                TestX = Current.GetX();
                                TestY = Current.GetY();
                                GetTestPositions(ref TestX, ref TestY, Current, Attempt);
                                if (CheckAvailability(TestX, TestY, Current))
                                {
                                    TestDistance = GetDistance(TestX, TestY);
                                    if (TestDistance <= MinDistance)
                                    {
                                        MinDistance = TestDistance;
                                        MinX = TestX;
                                        MinY = TestY;
                                    }
                                }
                                Attempt++;
                                if (Attempt == 4)
                                {
                                    Searching = false;
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            if ((CheckAvailability(TestX, TestY - 1, Current)) && (Player1.GetY() < TestY))
                            {
                                MinY = TestY - 1;
                                MinDistance = GetDistance(TestX, TestY - 1);
                                Direction = 1;
                            }
                            else
                            {
                                if (GetDistance(TestX - 1, TestY) < GetDistance(TestX + 1, TestY))
                                {
                                    Direction = 3;
                                }
                                else
                                {
                                    Direction = 4;
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            {

                                if ((CheckAvailability(TestX, TestY + 1, Current)) && (Player1.GetY() > TestY))
                                {
                                    MinY = TestY + 1;
                                    MinDistance = GetDistance(TestX, TestY + 1);
                                    Direction = 2;
                                }
                                else
                                {
                                    if (GetDistance(TestX - 1, TestY) < GetDistance(TestX + 1, TestY))
                                    {
                                        Direction = 3;
                                    }
                                    else
                                    {
                                        Direction = 4;
                                    }
                                }
                                break;
                            }
                        }
                    case 3:
                        {
                            {

                                if ((CheckAvailability(TestX - 1, TestY, Current)) && (Player1.GetX() < TestX))
                                {
                                    MinX = TestX - 1;
                                    MinDistance = GetDistance(TestX - 1, TestY);
                                    Direction = 3;
                                }
                                else
                                {
                                    if (GetDistance(TestX, TestY - 1) < GetDistance(TestX, TestY + 1))
                                    {
                                        Direction = 1;
                                    }
                                    else
                                    {
                                        Direction = 2;
                                    }
                                }
                                break;
                            }
                        }
                    case 4:
                        {
                            {
                                {

                                    if ((CheckAvailability(TestX + 1, TestY, Current)) && (Player1.GetX() > TestX))
                                    {
                                        MinX = TestX + 1;
                                        MinDistance = GetDistance(TestX + 1, TestY);
                                        Direction = 4;
                                    }
                                    else
                                    {
                                        if (GetDistance(TestX, TestY - 1) < GetDistance(TestX, TestY + 1))
                                        {
                                            Direction = 1;
                                        }
                                        else
                                        {
                                            Direction = 2;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                }
            }

            Current.SetX(MinX);
            Current.SetY(MinY);
            Current.SetDistance(MinDistance);
            Current.SetDirection(Direction);

            if ((Current.GetX() == Player1.GetX()) && (Current.GetY() == Player1.GetY()))
            {
                Player1.Damaged();
            }
        }

        private void GetTestPositions(ref int TestX, ref int TestY, Enemy Current, int Attempt)
        {
            if ((Player1.GetX() > Current.GetX()) && (Attempt == 0))
            {
                TestX = Current.GetX() + Current.GetSpeed();

            }
            else if ((Player1.GetX() < Current.GetX()) && (Attempt == 1))
            {
                TestX = Current.GetX() - Current.GetSpeed();
            }
            else
            {
                if ((Player1.GetY() > Current.GetY()) && (Attempt == 2))
                {
                    TestY = Current.GetY() + Current.GetSpeed();
                }
                else if ((Player1.GetY() < Current.GetY()) && (Attempt == 3))
                {
                    TestY = Current.GetY() - Current.GetSpeed();
                }
            }
        }

        private int GetDistance(int TestX, int TestY)
        {
            return Convert.ToInt32(Math.Sqrt(Math.Pow((TestX - Player1.GetX()), 2) + Math.Pow((TestY - Player1.GetY()), 2)));
        }

        private bool CheckAvailability(int TestX, int TestY, Enemy Current)
        {
            if ((TestX > 0) && (TestX < Columns) && (TestY > -1) && (TestY < Rows))
            {
                for (int i = 0; i < EList.Count(); i++)
                {
                    Enemy Test = EList.GetEnemy(i);
                    if (Current != Test)
                    {
                        if ((Test.GetX() == TestX) && (Test.GetY() == TestY))
                        {
                            return false;
                        }
                    }
                }
                for (int i = 0; i < PList.Count(); i++)
                {
                    Projectile Test = PList.GetProjectile(i);
                    if ((Test.GetX() == TestX) && (Test.GetY() == TestY) && (!Test.GetDead()))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DisplayHUD()
        {
            Console.WriteLine("[+++++++++++++++++++++++++++++++++++++++++++++++++++]");
            Console.WriteLine("[                                                   ]");
            Console.WriteLine("[       Health: {0}/{1}    Damage: {2}    Range: {3}    ]", STwo(Player1.GetHealth()), STwo(Equip.GetHealth()), STwo(Equip.GetDamage()), STwo(Equip.GetRange()));
            Console.WriteLine("[     Pierce Enemies? {0}  Pierce Projectiles? {1}  ]", Equip.SPierceE(), Equip.SPierceP());
            Console.WriteLine("[                Current Score: {0}                 ]", SThree(Score));
            Console.WriteLine("[                                                   ]");
            Console.WriteLine("[+++++++++++++++++++++++++++++++++++++++++++++++++++]");
        }

        private string STwo(int Value)
        {
            if (Value < 10)
            {
                return ("0" + Value);
            }
            else
            {
                return ("" + Value);
            }
        }

        private string SThree(int Value)
        {
            if (Value < 10)
            {
                return ("00" + Value);
            }
            else if (Value < 100)
            {
                return ("0" + Value);
            }
            else
            {
                return ("" + Value);
            }
        }

        private string SFour(int Value)
        {
            if (Value < 10)
            {
                return ("000" + Value);
            }
            else if (Value < 100)
            {
                return ("00" + Value);
            }
            else if (Value < 1000)
            {
                return ("0" + Value);
            }
            else
            {
                return ("" + Value);
            }
        }

        private bool GameOver()
        {
            bool Selecting = true;
            int Option = 1;
            while (Selecting)
            {
                DisplayGameOver(Option);
                ConsoleKeyInfo Key = Console.ReadKey();
                if ((Key.Key == ConsoleKey.LeftArrow) || (Key.Key == ConsoleKey.RightArrow))
                {
                    if (Option == 1)
                    {
                        Option = 2;
                    }
                    else
                    {
                        Option = 1;
                    }
                }
                else if (Key.Key == ConsoleKey.Enter)
                {
                    if (Option == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Key.Key == ConsoleKey.Escape)
                {
                    return false;
                }
            }
            return false;
        }

        private void DisplayGameOver(int Option)
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\GameOver.txt");
            int Line = 1;
            while (!SR.EndOfStream)
            {
                if (Line == 7)
                {
                    string SXP = SThree(Score);
                    Console.WriteLine("[       XP earned: {0} XP       ]", SXP);
                }
                else if (Line == 10)
                {
                    if (Option == 1)
                    {
                        Console.WriteLine("[      -----                    ]");
                    }
                    else
                    {
                        Console.WriteLine("[                     ----      ]");
                    }
                }
                else
                {
                    Console.WriteLine(SR.ReadLine());
                }
                Line++;
            }
            SR.Close();
        }

        private void NextLevel(int Level)
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\NextLevel.txt");
            int Line = 1;
            while (!SR.EndOfStream)
            {
                if (Line == 6)
                {
                    string SLevel = STwo(Level + 1);
                    Console.WriteLine("[          Now Proceeding to Level {0}/20            ]", SLevel);
                }
                else
                {
                    Console.WriteLine(SR.ReadLine());
                }
                Line++;
            }
            SR.Close();

            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                NextLevel(Level);
            }
        }

        private void QuitGame()
        {
            Console.Clear();
            Console.WriteLine("[+++++++++++++++++++++++++++++++]");
            Console.WriteLine("[                               ]");
            string SXP = SFour(Score);
            if (Type == 2)
            {
                Console.WriteLine("[       Scored   : {0}         ]", SXP);
            }
            else
            {
                Console.WriteLine("[       XP Earned: {0} XP      ]", SXP);
            }
            Console.WriteLine("[                               ]");
            if (Type == 2)
            {
                Console.WriteLine("[            Continue           ]");
                Console.WriteLine("[            --------           ]");
            }
            else
            {
                Console.WriteLine("[              Back             ]");
                Console.WriteLine("[              ----             ]");
            }
            Console.WriteLine("[+++++++++++++++++++++++++++++++]");

            ConsoleKeyInfo Key = Console.ReadKey();
            if ((Key.Key != ConsoleKey.Enter) && (Key.Key != ConsoleKey.Escape))
            {
                QuitGame();
            }
        }

        private bool PauseGame()
        {
            bool Paused = true;
            int Option = 1;
            while (Paused)
            {
                DisplayPause(Option);
                ConsoleKeyInfo Key = Console.ReadKey();
                if (Key.Key == ConsoleKey.UpArrow)
                {
                    if (Option == 1)
                    {
                        Option = 4;
                    }
                    else
                    {
                        Option--;
                    }
                }
                else if (Key.Key == ConsoleKey.DownArrow)
                {
                    if (Option == 4)
                    {
                        Option = 1;
                    }
                    else
                    {
                        Option++;
                    }
                }
                else if (Key.Key == ConsoleKey.Enter)
                {
                    if (Option == 1)
                    {
                        Sound = !Sound;
                    }
                    else if (Option == 2)
                    {
                        Grid = !Grid;
                    }
                    else if (Option == 3)
                    {
                        return false;
                    }
                    else
                    {
                        Paused = false;
                    }
                }
                else if (Key.Key == ConsoleKey.Escape)
                {
                    Paused = false;
                }
            }
            return true;
        }

        private void DisplayPause(int Option)
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\PauseGame.txt");
            int Line = 1;
            while (!SR.EndOfStream)
            {
                if (Line == 6)
                {
                    if (Sound)
                    {
                        Console.WriteLine("[                  Sound : ON                ]");
                    }
                    else
                    {
                        Console.WriteLine("[                  Sound : OFF               ]");
                    }
                }
                else if (Line == 7)
                {
                    if (Option == 1)
                    {
                        if (Sound)
                        {
                            Console.WriteLine("[                          --                ]");
                        }
                        else
                        {
                            Console.WriteLine("[                          ---               ]");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[                                            ]");
                    }
                }
                else if (Line == 8)
                {
                    if (Grid)
                    {
                        Console.WriteLine("[                   Grid : ON                ]");
                    }
                    else
                    {
                        Console.WriteLine("[                   Grid : OFF               ]");
                    }
                }
                else if (Line == 9)
                {
                    if (Option == 2)
                    {
                        if (Grid)
                        {
                            Console.WriteLine("[                          --                ]");
                        }
                        else
                        {
                            Console.WriteLine("[                          ---               ]");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[                                            ]");
                    }
                }
                else if (Line == 11)
                {
                    if (Option == 3)
                    {
                        Console.WriteLine("[                    ------                  ]");
                    }
                    else
                    {
                        Console.WriteLine("[                                            ]");
                    }
                }
                else if (Line == 13)
                {
                    if (Option == 4)
                    {
                        Console.WriteLine("[                     ----                   ]");
                    }
                    else
                    {
                        Console.WriteLine("[                                            ]");
                    }
                }
                else
                {
                    Console.WriteLine(SR.ReadLine());
                }
                Line++;
            }
            SR.Close();
        }
    }
}
