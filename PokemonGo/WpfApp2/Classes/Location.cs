using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo
{
    class Location
    {
        public double left;
        public double top;
        public Location(double x, double y)
        {
            left = x;
            top = y;
        }
    }
}
