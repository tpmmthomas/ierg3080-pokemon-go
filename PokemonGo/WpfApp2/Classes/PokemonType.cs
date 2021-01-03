using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo
{
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
}
