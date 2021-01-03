using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo
{
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
}
