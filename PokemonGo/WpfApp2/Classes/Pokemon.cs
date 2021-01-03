using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PokemonGo
{
    public class Pokemon
    {
        private Random rand;
        private int id; //sort of a "primary key", cos one player may have more than one pokemon of the same name.
        public int Id
        {
            get { return id; }
        }
        private string name;
        public string Name
        {
            get { return name; }
        }
        private string typeName;
        public string TypeName { get { return typeName; } }

        private AttackMoves[] moveslist;
        public AttackMoves[] Moveslist
        {
            get { return moveslist; }
        }
        private double weight;
        public double Weight
        {
            get { return weight; }
        }
        private double height;
        public double Height
        {
            get { return height; }
        }
        private int CP;
        public int GetCP
        {
            get { return CP; }
        }
        private int maxHP;
        public int MaxHP
        {
            get { return maxHP; }
        }
        private int evolvestate; //1,2,3, 1 lowest, 3 highest
        public int EvolveState
        {
            get { return evolvestate; }
        }
        private int HP;
        public int GetHP
        {
            get { return HP; }
        }
        public String GetFrontImage
        {
            get { return "Images/pokemon/" + typeName + ".gif"; }
        }
        public String GetBackImage
        {
            get { return "Images/pokemon/back/" + typeName + ".gif"; }
        }
        public Pokemon(int pId, PokemonType x)
        {
            id = pId;
            rand = new Random();
            name = x.Name;
            typeName = x.Name;
            moveslist = new AttackMoves[3];
            int i = 0;
            foreach (AttackMoves c in x.AtkMovesList)
            {
                moveslist[i] = new AttackMoves(c.name, c.attackPoints);
                i++;
            }
            weight = rand.Next((int)(x.LowerWeight * 10), (int)(x.UpperWeight * 10 + 1)) / (float)10;
            height = rand.Next((int)(x.LowerHeight * 10), (int)(x.UpperHeight * 10 + 1)) / (float)10;
            CP = x.InitialCP;
            maxHP = x.InitialHP;
            evolvestate = 1;
            HP = maxHP;
        }
        public double GetHPPercentage(int HPBarWidth = 100, int customizeHP = -1)
        {
            if (customizeHP == -1)
            {
                return HPBarWidth * (double)HP / maxHP;
            }
            else
            {
                return HPBarWidth * (double)customizeHP / maxHP;
            }
        }
        public SolidColorBrush GetHPColor()
        {
            if (GetHPPercentage() > 60)
            {
                return new SolidColorBrush(Colors.LightGreen);
            }
            else if (GetHPPercentage() > 20)
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                return new SolidColorBrush(Colors.Red);
            }
        }
        public double GetCPPresentage(int maxRotateDegree = 180)
        {
            double precentage = 180 * ((double)CP / 3000);
            precentage = (precentage > 180) ? 180 : precentage;
            return precentage;
        }
        public bool Hit(int atkhp)
        {
            if (HP - atkhp > 0)
            {
                HP -= atkhp;
                return false;
            }
            else
            {
                HP = 0;
                return true;
            }
        }
        public void Heal()
        {
            HP = maxHP;
        }
        public int Evolve()
        {
            if (evolvestate == 3)
                return 1;
            evolvestate++;
            name = name + "+";
            typeName = typeName + "e";
            weight += rand.Next(1, 20);
            height += rand.Next(5, 50) / (double)10;
            foreach (AttackMoves x in moveslist)
            {
                x.attackPoints += rand.Next(5, 25);
                if (x.attackPoints > 200)
                    x.attackPoints = 200;
            }
            CP += rand.Next(100, 350);
            if (CP > 2500) CP = 2500;
            maxHP += rand.Next(30, 90);
            if (maxHP > 300) maxHP = 300;
            HP = maxHP;
            return 0;
        }
        public int PowerUP()
        {
            CP += rand.Next(30, 100);
            if (CP > 2500) CP = 2500;
            foreach (AttackMoves x in moveslist)
            {
                x.attackPoints += rand.Next(0, 4);
                if (x.attackPoints > 200)
                    x.attackPoints = 200;
            }
            maxHP += rand.Next(1, 10);
            if (maxHP > 300) maxHP = 300;
            Heal();
            return 0;
        }
        public void Rename(string x)
        {
            if (evolvestate >= 2)
                x = x + '+';
            if (evolvestate == 3)
                x = x + '+';
            name = x;
        }
        public void ResetId(int x)
        {
            id = x;
        }
    }
}
