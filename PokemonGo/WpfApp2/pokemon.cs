using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo
{
    public class Player
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        private int pokeball_count;
        public int Pokeball_count
        {
            get { return pokeball_count; }
        }

        private List<Pokemon> OwnedPokemons;
        private int currentSerial;
        public int CurrentSerial
        {
            get { currentSerial++; return currentSerial - 1; }
        }
        private int stardust;
        public int Stardust
        {
            get { return stardust; }
        }
        public Player(string pname)
        {
            name = pname;
            currentSerial = 1;
            OwnedPokemons = new List<Pokemon>();
            stardust = 0;
            pokeball_count = 10;
        }

        public List<Pokemon> GetPokemons()
        {
            return OwnedPokemons;
        }

        public int PokemonCount()
        {
            return OwnedPokemons.Count;
        }

        public void AddPokemon(Pokemon a)
        {
            OwnedPokemons.Add(a);
        }

        public void RemovePokemon(Pokemon a)
        {
            int index = OwnedPokemons.IndexOf(a);
            if (index != -1)
            {
                OwnedPokemons.RemoveAt(index);
            }
        }

        public void AddPokeball()
        {
            pokeball_count += 5;
            if (pokeball_count > 500)
                pokeball_count = 500;
        }

        public void RemovePokeball(int x)
        {
            if (pokeball_count - x > 0)
                pokeball_count -= x;
            else
            {
                pokeball_count = 0;
            }
        }
        public void AddStardust(int x)
        {
            stardust += x;
        }
        public void ConsumeStardust(int x)
        {
            stardust -= x;
            if (stardust < 0)
                stardust = 0;
        }
    }

    public class PokemonType
    {
        private int rarity; //1: common, 2: rare, 3: ultra-rare
        public int Rarity
        {
            get { return rarity; }
        }
        private string name;
        public string Name
        {
            get { return name; }
        }
        private AttackMoves[] atkMovesList;
        public AttackMoves[] AtkMovesList
        {
            get { return atkMovesList; }
        }
        private float upperWeight;
        public float UpperWeight
        {
            get { return upperWeight; }
        }
        private float lowerWeight;
        public float LowerWeight
        {
            get { return lowerWeight; }
        }
        private float upperHeight;
        public float UpperHeight
        {
            get { return upperHeight; }
        }
        private float lowerHeight;
        public float LowerHeight
        {
            get { return lowerHeight; }
        }
        private int initialCP;
        public int InitialCP
        {
            get { return initialCP; }
        }
        private int initialHP;
        public int InitialHP
        {
            get { return initialHP; }
        }

        public PokemonType(int rar, string nam, float uw, float lw, float uh, float lh, int icp, int ihp, string mv1, int dmg1, string mv2, int dmg2, string mv3, int dmg3)
        {
            rarity = rar;
            name = nam;
            lowerWeight = lw;
            upperWeight = uw;
            lowerHeight = lh;
            upperHeight = uh;
            initialCP = icp;
            initialHP = ihp;
            atkMovesList = new AttackMoves[3];
            atkMovesList[0] = new AttackMoves(mv1, dmg1);
            atkMovesList[1] = new AttackMoves(mv2, dmg2);
            atkMovesList[2] = new AttackMoves(mv3, dmg3);
        }

    }

    public class AttackMoves
    {
        public string name;
        public int attackPoints;
        public AttackMoves(string n, int ap)
        {
            name = n;
            attackPoints = ap;
        }

    }


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
        private AttackMoves[] moveslist;
        public AttackMoves[] Moveslist
        {
            get { return moveslist; }
        }
        private float weight;
        public float Weight
        {
            get { return weight; }
        }
        private float height;
        public float Height
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
        private int HP;
        public int GetHP
        {
            get { return HP; }
        }
        public String GetImageLocation
        {
            get { return "Images/pokemon/"+name+".gif"; }
        }
        public Pokemon(int pId, PokemonType x)
        {
            id = pId;
            rand = new Random();
            name = x.Name;
            moveslist = x.AtkMovesList;
            weight = rand.Next((int)(x.LowerWeight * 10), (int)(x.UpperWeight * 10 + 1)) / (float)10;
            height = rand.Next((int)(x.LowerHeight * 10), (int)(x.UpperHeight * 10 + 1)) / (float)10;
            CP = x.InitialCP;
            maxHP = x.InitialHP;
            evolvestate = 1;
            HP = maxHP;
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
            name = name + "e";
            weight += rand.Next(1, 10);
            height += rand.Next(1, 10) / 10;
            foreach (AttackMoves x in moveslist)
            {
                x.attackPoints += rand.Next(5, 20);
            }
            CP += rand.Next(60, 200);
            if (CP > 2500) CP = 2500;
            maxHP += rand.Next(40, 100);
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
                x.attackPoints += rand.Next(0, 2);
            }
            return 0;
        }

        public void Rename(string x)
        {
            if (evolvestate >= 2)
                x = x + 'e';
            if (evolvestate == 3)
                x = x + 'e';
            name = x;
        }

    }
}
