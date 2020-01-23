using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            int Option = 1;
            ConsoleKeyInfo Key;
            bool Running = true;
            HighscoresList HSList = new HighscoresList();
            BestiaryList BList = new BestiaryList();
            Equipment Equip = new Equipment();
            //SoundPlayer Music = new SoundPlayer(@"Music\Menu.wav");
            //Music.PlayLooping();
            while (Running)
            {
                LoadMenu(Option);
                Key = Console.ReadKey();
                if (Key.Key == ConsoleKey.UpArrow)
                {
                    if (Option == 1)
                    {
                        Option = 5;
                    }
                    else
                    {
                        Option--;
                    }
                }
                else if (Key.Key == ConsoleKey.DownArrow)
                {
                    if (Option == 5)
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
                    switch (Option)
                    {
                        case 1:
                            {
                                NewGame(ref HSList, ref BList, ref Equip);//, Music);
                                break;
                            }
                        case 2:
                            {
                                HSList.DisplayHighscores();
                                break;
                            }
                        case 3:
                            {
                                LoadBestiary(BList);
                                break;
                            }
                        case 4:
                            {
                                DisplayHelp();
                                break;
                            }
                        default:
                            {
                                Quit(HSList, BList, Equip);
                                Running = false;
                                break;
                            }
                    }
                }
                else if (Key.Key == ConsoleKey.Escape)
                {
                    Quit(HSList, BList, Equip);
                    Running = false;
                }
            }
        }

        static void NewGame(ref HighscoresList HSList, ref BestiaryList BList, ref Equipment Equip)//, SoundPlayer Music)
        {
            int Option = 1;
            bool Picking = true;
            while (Picking)
            {
                LoadNewGame(Option);
                ConsoleKeyInfo Key = Console.ReadKey();
                if (Key.Key == ConsoleKey.UpArrow)
                {
                    if (Option == 1)
                    {
                        Option = 5;
                    }
                    else
                    {
                        Option--;
                    }
                }
                else if (Key.Key == ConsoleKey.DownArrow)
                {
                    if (Option == 5)
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
                    switch (Option)
                    {
                        case 1:
                            {
                                //Music.Stop();
                                Game Game1 = new Game(BList, null, Equip, 1);
                                //Music.PlayLooping();
                                break;
                            }
                        case 2:
                            {
                                UpgradeScreen(Equip);
                                break;
                            }
                        case 3:
                            {
                                //Music.Stop();
                                Game Game1 = new Game(BList, HSList, Equip, 2);
                                //Music.PlayLooping();
                                break;
                            }
                        case 4:
                            {
                                ResetSave(ref BList, ref HSList, ref Equip);
                                break;
                            }
                        default:
                            {
                                Picking = false;
                                break;
                            }
                    }
                }
            }
        }

        static void LoadNewGame(int Option)
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\NewGame.txt");
            int Line = 1;
            while (!SR.EndOfStream)
            {
                if (Option == 1)
                {
                    if (Line == 12)
                    {
                        Console.WriteLine("[                                         -------------                                         ]");
                        Line++;
                    }
                }
                else if (Option == 2)
                {
                    if (Line == 13)
                    {
                        Console.WriteLine("[                                            -------                                            ]");
                        Line++;
                    }
                }
                else if (Option == 3)
                {
                    if (Line == 14)
                    {
                        Console.WriteLine("[                                       -----------------                                       ]");
                        Line++;
                    }
                }
                else if (Option == 4)
                {
                    if (Line == 15)
                    {
                        Console.WriteLine("[                                          -----------                                          ]");
                        Line++;
                    }
                }
                else
                {
                    if (Line == 16)
                    {
                        Console.WriteLine("[                                              ----                                             ]");
                        Line++;
                    }
                }
                Console.WriteLine(SR.ReadLine());
                Line++;
            }
        }

        static void LoadMenu(int Option)
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\Menu.txt");
            int Line = 0;
            while (!SR.EndOfStream)
            {
                if (Option == 1)
                {
                    if (Line == 16)
                    {
                        Console.WriteLine("[                                                    -----------                                                     ]");
                        Line++;
                    }
                }
                else if (Option == 2)
                {
                    if (Line == 17)
                    {
                        Console.WriteLine("[                                                    -----------                                                     ]");
                        Line++;
                    }
                }
                else if (Option == 3)
                {
                    if (Line == 18)
                    {
                        Console.WriteLine("[                                                     --------                                                       ]");
                        Line++;
                    }
                }
                else if (Option == 4)
                {
                    if (Line == 19)
                    {
                        Console.WriteLine("[                                                       ----                                                         ]");
                        Line++;
                    }
                }
                else
                {
                    if (Line == 20)
                    {
                        Console.WriteLine("[                                                       ----                                                         ]");
                        Line++;
                    }
                }

                Console.WriteLine(SR.ReadLine());
                Line++;
            }
            SR.Close();
        }

        static void Quit(HighscoresList HSList, BestiaryList BList, Equipment Equip)
        {
            HSList.Quit();
            BList.Quit();
            Equip.Quit();
        }

        static void DisplayHelp()
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\HelpScreen.txt");
            while (!SR.EndOfStream)
            {
                Console.WriteLine(SR.ReadLine());
            }

            ConsoleKeyInfo Key = Console.ReadKey();
            if ((Key.Key != ConsoleKey.Enter) && (Key.Key != ConsoleKey.Escape))
            {
                DisplayHelp();
            }
        }

        static void ResetSave(ref BestiaryList BList, ref HighscoresList HSList, ref Equipment Equip)
        {
            StreamWriter SW = new StreamWriter(@"Save Files\Bestiary.txt");
            SW.Close();
            SW = new StreamWriter(@"Save Files\Highscores.txt");
            SW.Close();
            SW = new StreamWriter(@"Save Files\Equipment.txt");
            SW.WriteLine("0");
            SW.WriteLine("1");
            SW.WriteLine("1");
            SW.WriteLine("1");
            SW.WriteLine("false");
            SW.WriteLine("false");
            SW.Close();
            Equip = new Equipment();
            BList = new BestiaryList();
            HSList = new HighscoresList();
        }

        static void LoadBestiary(BestiaryList List)
        {
            bool Browsing = true;
            int Entry = 0;
            int Option = 2;
            while (Browsing)
            {
                if (List.Count() > 0)
                {
                    DisplayEntry(Entry, Option, List);
                }
                else
                {
                    Entry = -1;
                    DisplayEntry(Entry, Option, List);
                }
                ConsoleKeyInfo Key = Console.ReadKey();
                if (Key.Key == ConsoleKey.LeftArrow)
                {
                    if (Option == 1)
                    {
                        Option = 3;
                    }
                    else
                    {
                        Option--;
                    }
                }
                else if (Key.Key == ConsoleKey.RightArrow)
                {
                    if (Option == 3)
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
                    if (Option == 2)
                    {
                        Browsing = false;
                    }
                    else if (Option == 1)
                    {
                        if (Entry == 0)
                        {
                            Entry = List.Count() - 1;
                        }
                        else
                        {
                            Entry--;
                        }
                    }
                    else if (Option == 3)
                    {
                        if (Entry == List.Count() - 1)
                        {
                            Entry = 0;
                        }
                        else
                        {
                            Entry++;
                        }
                    }
                }
                else if (Key.Key == ConsoleKey.Escape)
                {
                    Browsing = false;
                }
            }
        }

        static void DisplayEntry(int Entry, int Option, BestiaryList List)
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\BestiaryDesign.txt");
            int Line = 1;
            while (!SR.EndOfStream)
            {
                if (Line == 11)
                {
                    if (Entry == -1)
                    {
                        Console.WriteLine("[                                       Entry 00/00                                      ]");
                    }
                    else
                    {
                        Console.WriteLine("[                                       Entry {0}/{1}                                      ]", STwo(Entry + 1), STwo(List.Count()));
                    }
                }
                else if (Line == 13)
                {
                    if (Entry == -1)
                    {
                        Console.WriteLine("[   You have not defeated any enemies yet. Defeat enemies to add them to the bestiary.   ]");
                    }
                    else
                    {
                        Enemy Current = List.GetEnemy(Entry);
                        Console.WriteLine("[                                            {0}:                                          ]", Current.GetEType());
                        Console.WriteLine("[                                                                                        ]");
                        Console.WriteLine("[                                        Health: {0}                                       ]", Current.GetHealth());
                        foreach (char i in Current.GetEAttributes())
                        {
                            switch (i)
                            {
                                case 'C':
                                    Console.WriteLine("[                                   Deals contact damage                                 ]");
                                    break;
                                case 'I':
                                    Console.WriteLine("[                               Can only move in 1 direction                             ]");
                                    break;
                                case 'S':
                                    Console.WriteLine("[                                 Can shoot every {0} turn/s                               ]", Current.GetReload());
                                    break;
                            }
                        }
                        Console.WriteLine("[                                         Speed: {0}                                       ]", Current.GetSpeed());
                    }
                }
                else if (Line == 16)
                {
                    switch (Option)
                    {
                        case 1:
                            Console.WriteLine("[                                      ---                                               ]");
                            break;
                        case 2:
                            Console.WriteLine("[                                           ----                                         ]");
                            break;
                        case 3:
                            Console.WriteLine("[                                                 ---                                    ]");
                            break;
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

        static void UpgradeScreen(Equipment Equip)
        {
            int Option = 1;
            bool Upgrading = true;
            while (Upgrading)
            {
                DisplayUpgrade(Option, Equip);
                ConsoleKeyInfo Key = Console.ReadKey();
                if (Key.Key == ConsoleKey.DownArrow)
                {
                    if ((Option == 3) || (Option == 6))
                    {
                        Option -= 2;
                    }
                    else
                    {
                        Option++;
                    }

                }
                else if (Key.Key == ConsoleKey.UpArrow)
                {
                    if ((Option == 1) || (Option == 4))
                    {
                        Option += 2;
                    }
                    else
                    {
                        Option--;
                    }
                }
                else if ((Key.Key == ConsoleKey.RightArrow) || (Key.Key == ConsoleKey.LeftArrow))
                {
                    if (Option > 3)
                    {
                        Option -= 3;
                    }
                    else
                    {
                        Option += 3;
                    }
                }
                else if (Key.Key == ConsoleKey.Enter)
                {
                    switch (Option)
                    {
                        case 1:
                            {
                                if (Equip.GetRange() * 25 < 1000)
                                {
                                    if (Equip.GetXP() >= Equip.GetRange() * 25)
                                    {
                                        Equip.Upgrade(Equip.GetRange() * 25);
                                        Equip.UpgradeRange();
                                    }
                                }
                                else
                                {
                                    if (Equip.GetXP() >= 999)
                                    {
                                        Equip.Upgrade(999);
                                        Equip.UpgradeRange();
                                    }
                                }
                                break;
                            }
                        case 2:
                            {
                                if (Equip.GetHealth() * 100 < 1000)
                                {
                                    if (Equip.GetXP() >= Equip.GetHealth() * 100)
                                    {
                                        Equip.Upgrade(Equip.GetRange() * 100);
                                        Equip.UpgradeHealth();
                                    }
                                }
                                else
                                {
                                    if (Equip.GetXP() >= 999)
                                    {
                                        Equip.Upgrade(999);
                                        Equip.UpgradeHealth();
                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                if (Equip.GetDamage() * 50 < 1000)
                                {
                                    if (Equip.GetXP() >= Equip.GetDamage() * 50)
                                    {
                                        Equip.Upgrade(Equip.GetDamage() * 50);
                                        Equip.UpgradeDamage();
                                    }
                                }
                                else
                                {
                                    if (Equip.GetXP() >= 999)
                                    {
                                        Equip.Upgrade(999);
                                        Equip.UpgradeDamage();
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                if (!Equip.GetPierceE())
                                {
                                    if (Equip.GetXP() >= 250)
                                    {
                                        Equip.SetPierceE();
                                        Equip.Upgrade(250);
                                    }
                                }
                                break;
                            }
                        case 5:
                            {
                                if (!Equip.GetPierceP())
                                {
                                    if (Equip.GetXP() >= 250)
                                    {
                                        Equip.SetPierceP();
                                        Equip.Upgrade(250);
                                    }
                                }
                                break;
                            }
                        case 6:
                            {
                                Upgrading = false;
                                break;
                            }
                    }
                }
                else if (Key.Key == ConsoleKey.Escape)
                {
                    Upgrading = false;
                }
            }
        }

        static void DisplayUpgrade(int Option, Equipment Equip)
        {
            Console.Clear();
            StreamReader SR = new StreamReader(@"Menu\UpgradeScreen.txt");
            int Line = 1;
            string Range = STwo(Equip.GetRange());
            string RXP;
            if ((Equip.GetRange() * 25) > 999)
            {
                RXP = "999";
            }
            else
            {
                RXP = SThree(Equip.GetRange() * 25);
            }
            string Damage = STwo(Equip.GetDamage());
            string DXP;
            if ((Equip.GetDamage() * 50) > 999)
            {
                DXP = "999";
            }
            else
            {
                DXP = SThree(Equip.GetDamage() * 50);
            }
            string Health = STwo(Equip.GetHealth());
            string HXP;
            if ((Equip.GetHealth() * 100) > 999)
            {
                HXP = "999";
            }
            else
            {
                HXP = SThree(Equip.GetHealth() * 100);
            }
            string XP = SFour(Equip.GetXP());
            string PEXP;
            if (!Equip.GetPierceE())
            {
                PEXP = "250";
            }
            else
            {
                PEXP = "000";
            }
            string PPXP;
            if (!Equip.GetPierceP())
            {
                PPXP = "250";
            }
            else
            {
                PPXP = "000";
            }
            while (!SR.EndOfStream)
            {
                switch (Line)
                {
                    case 13:
                        {
                            Console.WriteLine("[       Range: {0}  |  Upgrade: {1} XP          Shots Pierce Enemies  |  Unlock: {2} XP     ]", Range, RXP, PEXP);
                            break;
                        }
                    case 14:
                        {
                            if (Option == 1)
                            {
                                Console.WriteLine("[                     ---------------                                                      ]");
                            }
                            else if (Option == 4)
                            {
                                Console.WriteLine("[                                                                       --------------     ]");
                            }
                            else
                            {
                                Console.WriteLine("[                                                                                          ]");
                            }
                            break;
                        }
                    case 15:
                        {
                            Console.WriteLine("[      Health: {0}  |  Upgrade: {1} XP      Shots Pierce Projectiles  |  Unlock: {2} XP     ]", Health, HXP, PPXP);
                            break;
                        }
                    case 16:
                        {
                            if (Option == 2)
                            {
                                Console.WriteLine("[                     ---------------                                                      ]");
                            }
                            else if (Option == 5)
                            {
                                Console.WriteLine("[                                                                       --------------     ]");
                            }
                            else
                            {
                                Console.WriteLine("[                                                                                          ]");
                            }
                            break;
                        }
                    case 17:
                        {
                            Console.WriteLine("[      Damage: {0}  |  Upgrade: {1} XP                                                      ]", Damage, DXP);
                            break;
                        }
                    case 18:
                        {
                            if (Option == 3)
                            {
                                Console.WriteLine("[                     ---------------                                                      ]");
                            }
                            else
                            {
                                Console.WriteLine("[                                                                                          ]");
                            }
                            break;
                        }
                    case 19:
                        {
                            Console.WriteLine("[      Current XP: {0}                                           Back                     ]", XP);
                            break;
                        }
                    case 20:
                        {
                            if (Option == 6)
                            {
                                Console.WriteLine("[                                                                 ----                     ]");
                            }
                            else
                            {
                                Console.WriteLine("[                                                                                          ]");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(SR.ReadLine());
                            break;
                        }
                }
                Line++;
            }
            SR.Close();
        }

        static string STwo(int Value)
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

        static string SThree(int Value)
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

        static string SFour(int Value)
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
    }
}
