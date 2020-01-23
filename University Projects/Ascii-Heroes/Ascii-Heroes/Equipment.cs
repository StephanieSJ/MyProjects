using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_Heroes
{
    class Equipment
    {
        private int XP;
        private int Range;
        private int Damage;
        private int Health;
        private bool PierceE;
        private bool PierceP;

        public Equipment()
        {
            ReadFile();
        }

        private void ReadFile()
        {
            StreamReader SR = new StreamReader(@"Save Files\Equipment.txt");
            XP = int.Parse(SR.ReadLine());
            Range = int.Parse(SR.ReadLine());
            Damage = int.Parse(SR.ReadLine());
            Health = int.Parse(SR.ReadLine());
            PierceE = bool.Parse(SR.ReadLine());
            PierceP = bool.Parse(SR.ReadLine());
            SR.Close();
        }

        public void Quit()
        {
            StreamWriter SW = new StreamWriter(@"Save Files\Equipment.txt");
            SW.WriteLine(XP);
            SW.WriteLine(Range);
            SW.WriteLine(Damage);
            SW.WriteLine(Health);
            SW.WriteLine(PierceE);
            SW.WriteLine(PierceP);
            SW.Close();
        }

        public void IncreaseXP(int Amount)
        {
            XP += Amount;
        }

        public int GetHealth()
        {
            return Health;
        }

        public int GetRange()
        {
            return Range;
        }

        public bool GetPierceE()
        {
            return PierceE;
        }

        public bool GetPierceP()
        {
            return PierceP;
        }

        public int GetDamage()
        {
            return Damage;
        }

        public int GetXP()
        {
            return XP;
        }

        public void SetPierceE()
        {
            PierceE = true;
        }

        public void SetPierceP()
        {
            PierceP = true;
        }

        public void Upgrade(int Amount)
        {
            XP -= Amount;
        }

        public void UpgradeHealth()
        {
            Health++;
        }

        public void UpgradeDamage()
        {
            Damage++;
        }

        public void UpgradeRange()
        {
            Range++;
        }

        public string SPierceE()
        {
            if (PierceE)
            {
                return "Yes";
            }
            else
            {
                return "No ";
            }
        }
        
        public string SPierceP()
        {
            if (PierceP)
            {
                return "Yes";
            }
            else
            {
                return "No ";
            }
        }
    }
}
